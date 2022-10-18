using Microsoft.EntityFrameworkCore;
using ServerStorages;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ServerToken;
public class Engine
{
    private readonly IDbContextFactory<OSAndClaimsContext> DbContextFactory;
    private async Task<tokens.Security[]> GetSecurities()
    {
        Func<byte[]> RandomValue = () => System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString().Split("-")[0]);
        using var Db = await DbContextFactory.CreateDbContextAsync();
        if (!Db.TokensSecurities.Any(x => x.Created >= DateTime.UtcNow.AddDays(-1)))
        {
            Db.TokensSecurities.Add(new() { Id = Guid.NewGuid(), Created = DateTime.UtcNow, AutomaticDeletion = DateTime.UtcNow.AddMonths(2), Value0 = RandomValue(), Value1 = RandomValue() });
            Db.SaveChanges();
        }
        return Db.TokensSecurities.OrderByDescending(x => x.Created).Select(x => new tokens.Security { Value0 = System.Text.Encoding.UTF8.GetString(x.Value0), Value1 = System.Text.Encoding.UTF8.GetString(x.Value1) }).ToArray();
    }
    private async Task<string> Encrypt(Guid ID, Guid Code)
    {
        var Secret = (await this.GetSecurities()).First();
        byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes($"{ID.ToString()}@{Code.ToString()}");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
        using DESCryptoServiceProvider des = new();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, des.CreateEncryptor(Encoding.UTF8.GetBytes(Secret.Value0), Encoding.UTF8.GetBytes(Secret.Value1)), CryptoStreamMode.Write);
        cs.Write(inputbyteArray, 0, inputbyteArray.Length);
        cs.FlushFinalBlock();
        return Convert.ToBase64String(ms.ToArray());
    }
    private async Task<(HttpStatusCode StatusCode, Guid ID, Guid Code, string Message)> Decrypt(string Token)
    {
        foreach (var Security in await GetSecurities())
            try
            {
                Guid ID, Code;
                byte[] inputbyteArray = Convert.FromBase64String(Token.Replace(" ", "+"));
#pragma warning disable SYSLIB0021 // Type or member is obsolete
                using DESCryptoServiceProvider des = new();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
                using MemoryStream ms = new();
                using CryptoStream cs = new(ms, des.CreateDecryptor(System.Text.Encoding.UTF8.GetBytes(Security.Value0), System.Text.Encoding.UTF8.GetBytes(Security.Value1)), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                var Content = Encoding.UTF8.GetString(ms.ToArray());
                if (Content.Contains('@') && Guid.TryParse(Content.Split("@")[0], out ID) && Guid.TryParse(Content.Split("@")[1], out Code))
                    return (HttpStatusCode.OK, ID, Code, Content.Split("@")[2]);
            }
            catch (Exception)
            {
            }
        return (HttpStatusCode.Conflict, Guid.Empty, Guid.Empty, null!);
    }
    public async Task<(Guid ID, string Token)> Create()
    {
        using var Db = DbContextFactory.CreateDbContext();
        var Token = new ServerStorages.Token { Id = Guid.NewGuid(), Created = DateTime.UtcNow, AutomaticDeletion = DateTime.UtcNow.AddMonths(2) };
        TokenSecurity TS;
        Token.TokenSecurities.Add(TS = new TokenSecurity { Code = Guid.NewGuid(), Created = DateTime.UtcNow });
        Db.Tokens.Add(Token);
        Db.SaveChanges();
        return (Token.Id, await this.Encrypt(Token.Id, TS.Code));
    }
    public async Task<(bool Valid, Guid ID, string Token)> Verify(string Token)
    {
        var TokenDecrypt = await this.Decrypt(Token);
        using var Db = DbContextFactory.CreateDbContext();
        if (TokenDecrypt.StatusCode is HttpStatusCode.Conflict || !Db.TokenSecurities.Any(x => x.TokenId == TokenDecrypt.ID && x.Code == TokenDecrypt.Code))
            return (false, Guid.Empty, null!);
        var _Token = Db.Tokens.Single(x => x.Id == TokenDecrypt.ID);
        _Token.AutomaticDeletion = DateTime.UtcNow.AddMonths(2);
        TokenSecurity TS;
        if (_Token.TokenSecurities.Any(x => x.Created > DateTime.UtcNow.AddMinutes(-3)))
            TS = _Token.TokenSecurities.First(x => x.Created > DateTime.UtcNow.AddMinutes(-3));
        else
            TS = Db.TokenSecurities.Add(new ServerStorages.TokenSecurity { Code = Guid.NewGuid(), TokenId = TokenDecrypt.ID, Created = DateTime.UtcNow }).Entity;
        try
        {
            if (_Token.TokenSecurities.Count > 2)
                Db.TokenSecurities.RemoveRange(_Token.TokenSecurities.OrderBy(x => x.Created).Skip(2));
        }
        catch (Exception)
        {

        }
        Db.SaveChanges();
        return (true, TokenDecrypt.ID, await this.Encrypt(TokenDecrypt.ID, TS.Code));
    }
    private async Task Constructor() {
        await Task.Run(() => {
            using var Db = this.DbContextFactory.CreateDbContext();
            if (Db.Tokens.Any(x => x.AutomaticDeletion < DateTime.UtcNow))
            {
                Db.Tokens.RemoveRange(Db.Tokens.Where(x => x.AutomaticDeletion < DateTime.UtcNow));
                Db.SaveChanges();

            }
        }).ConfigureAwait(false);
        await Task.Run(() => {
            using var Db = this.DbContextFactory.CreateDbContext();
            if (Db.TokensSecurities.Any(x => x.AutomaticDeletion < DateTime.UtcNow))
            {
                Db.TokensSecurities.RemoveRange(Db.TokensSecurities.Where(x => x.AutomaticDeletion < DateTime.UtcNow));
                Db.SaveChanges();
            }
        }).ConfigureAwait(false);
        await Task.Delay(TimeSpan.FromHours(6));
        await this.Constructor().ConfigureAwait(false);
    }
    public Engine(IDbContextFactory<OSAndClaimsContext> DbContextFactory)
    {
        this.DbContextFactory = DbContextFactory;
        _ = this.Constructor().ConfigureAwait(false);
    }
}