﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServerUnitIdentifications;

public class Engine:PongPing.IUnitIdentifications
{
    private readonly IHubContext<PongPing.Services> HubContext;
    private readonly IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory;
    private readonly ServerToken.Engine STE;
    private Guid _ID = Guid.Empty;
    private readonly Product.Infomation PI;
    private string _Token = null!;

    public string GetISO3166 {
        get {
            using var Db = this.DbContextFactory.CreateDbContext();
            return Db.UnitIdentifications.Single(x => x.Id == _ID).Iso3166;
        }
    }

    public string GetISO639_1
	{
		get
		{
			using var Db = this.DbContextFactory.CreateDbContext();
			return Db.UnitIdentifications.Single(x => x.Id == _ID).Iso6391;
		}
	}

	public Engine(IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory,IHubContext<PongPing.Services> HubContext,ServerToken.Engine STE,Product.Infomation PI)
    {
        this.HubContext = HubContext;
        this.DbContextFactory = DbContextFactory;
        this.STE = STE;
        this.PI = PI;
        if (!BackgroundService) {
            BackgroundService = true;
            _ = BackgroundAction(DbContextFactory).ConfigureAwait(false);

		}
    }
    public async Task UpdateUI() {
        using var Db = this.DbContextFactory.CreateDbContext();
        var D_UI = Db.UnitIdentifications.Single(x => x.Id == _ID);
        DateTime E = DateTime.UtcNow.AddDays(14);
        if (D_UI.UnitUsers.Any(x => x.IsVerified && x.IsActive)) {
            var UU = D_UI.UnitUsers.Single(x => x.IsVerified && x.IsActive);
            var U = UU.User;
            E = DateTime.UtcNow.AddDays(U.ExpiresNumberOfDays).AddMonths(U.ExpiresNumberOfMonths).AddYears(U.ExpiresNumberOfYears);
            if (UU.Expires < E)
				UU.Expires = E;
			if (U.Expires < E) 
                U.Expires = E;
        }
        if (D_UI.AutomaticDeletion < E)
			D_UI.AutomaticDeletion = E;
		Db.SaveChanges();
        await this.HubContext.Clients.Clients(D_UI.UnitConnections.Select(x => x.Value).ToArray()).SendAsync("UI_S", _Token, D_UI.UnitUsers.Any(x => x.IsActive), PI.ISO639_1s.Any(x => x == D_UI.Iso6391) ? D_UI.Iso6391 : "EN",D_UI.Iso3166);
    }
    private void AddConnection(string ConnectionID, string Host) {
        using var Db = this.DbContextFactory.CreateDbContext();
        var D_UI = Db.UnitIdentifications.Single(x => x.Id == _ID);
        if (!D_UI.UnitConnections.Any(x => x.Host == Host && x.Value == ConnectionID)) {
            D_UI.UnitConnections.Add(new ServerStorages.UnitConnection { Id = Guid.NewGuid(), Host = Host, Value = ConnectionID });
            Db.SaveChanges();
        }
    }
    public async Task Verify(string ConnectionID, System.Net.IPAddress RemoteIpAddress, string Host,string Token,string ISO639_1,string ISO3166, StandardInternal.unit.infomation.Type SuiT,StandardInternal.product.infomation.Name SpiN,int BaseUtcOffsetTotalMinutes) {
        var STE_V = await STE.Verify(Token);
        _Token = STE_V.Token;
        using var Db = this.DbContextFactory.CreateDbContext();
        if (STE_V.Valid&&Db.UnitIdentifications.Any(x=>x.TokenId==STE_V.ID && x.SpiN==(int)SpiN&&x.SuiT==(int)SuiT)) {
            var D_UI = Db.UnitIdentifications.Single(x => x.TokenId == STE_V.ID);
            _ID = D_UI.Id;
            if (D_UI.BaseUtcOffsetTotalMinutes != BaseUtcOffsetTotalMinutes) { 
                D_UI.BaseUtcOffsetTotalMinutes = BaseUtcOffsetTotalMinutes;
                Db.SaveChanges();
            }
            this.AddConnection(ConnectionID, Host);
            await this.UpdateUI();
            return;
        }
        await Create(ConnectionID,RemoteIpAddress, Host, ISO639_1,ISO3166, SuiT, SpiN, BaseUtcOffsetTotalMinutes);
    }
    public async Task Create(string ConnectionID, System.Net.IPAddress RemoteIpAddress, string Host, string ISO639_1, string ISO3166, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN, int BaseUtcOffsetTotalMinutes)
    {
        if (RemoteIpAddress.ToString() != "::1") {
			var a = Whois.NET.WhoisClient.Query(RemoteIpAddress.ToString());
			var b = a.Raw.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Where(x => x.ToLower().IndexOf("country:") != -1).Select(x => x.Replace(" ", "").Split(":")[1]);
			List<(string Name, int Count)> c = new List<(string Name, int Count)>();
			if (b.Any()) foreach (var e in b)
					if (c.Any(x => x.Name == e))
					{
						var f = c.Single(x => x.Name == e);
						f.Count++;
					}
					else
						c.Add(new(e, 1));
			if (c.Any())
				ISO3166 = c.OrderByDescending(x => x.Count).First().Name;
		}

		var STE_C = await STE.Create();
        _Token = STE_C.Token;
        using var Db = this.DbContextFactory.CreateDbContext();
        Db.UnitIdentifications.Add(new ServerStorages.UnitIdentification { Id = _ID = Guid.NewGuid(), AutomaticDeletion = DateTime.UtcNow.AddDays(14), Created = DateTime.UtcNow, BaseUtcOffsetTotalMinutes = BaseUtcOffsetTotalMinutes, Iso6391 = ISO639_1.ToUpper(), SpiN = (int)SpiN, SuiT = (int)SuiT, TokenId = STE_C.ID, Iso3166 = ISO3166 });
        Db.SaveChanges();
        this.AddConnection(ConnectionID, Host);
        await this.UpdateUI();
    }
    public Task Remove(string ConnectionID, string Host)
    {
        using var Db = this.DbContextFactory.CreateDbContext();
        if (Db.UnitConnections.Any(x => x.Value == ConnectionID && x.Host == Host)) {
            Db.RemoveRange(Db.UnitConnections.Where(x => x.Host == Host && x.Value == ConnectionID));
            Db.SaveChanges();
        }
        return Task.CompletedTask;
    }
    public Task<Guid> GetID(string ConnectionID, string Host) {
		using var Db = this.DbContextFactory.CreateDbContext();
        return Task.FromResult(_ID = Db.UnitConnections.Single(x => x.Host == Host && x.Value == ConnectionID).UnitIdentificationId);
	}
    public async Task ISO3166(string ConnectionID, string Host, string Value) {
        await GetID(ConnectionID, Host);
        using var Db = this.DbContextFactory.CreateDbContext();
        var UI = Db.UnitIdentifications.Single(x => x.Id == _ID);
        if (UI.UnitUsers.Any(x => x.IsVerified && x.IsActive)) {
            var U = UI.UnitUsers.Single(x => x.IsVerified && x.IsActive).User;
            U.Iso3166 = Value;
            //send update to all devices
		}
		UI.Iso3166 = Value;
        Db.SaveChanges();
        await this.UpdateUI();
    }
    private static bool BackgroundService { get; set; } = false;
    private static async Task BackgroundAction(IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory) {
        using (var Db = DbContextFactory.CreateDbContext())
        {
			if (Db.UnitIdentifications.Any(x => x.AutomaticDeletion < DateTime.UtcNow))
			{
				Db.UnitIdentifications.RemoveRange(Db.UnitIdentifications.Where(x => x.AutomaticDeletion < DateTime.UtcNow));
				Db.SaveChanges();
			}
		}
		using (var Db = DbContextFactory.CreateDbContext())
		{
			if (Db.UnitUsers.Any(x => x.Expires < DateTime.UtcNow))
			{
				Db.UnitUsers.RemoveRange(Db.UnitUsers.Where(x => x.Expires < DateTime.UtcNow));
				Db.SaveChanges();
			}
		}
		using (var Db = DbContextFactory.CreateDbContext())
		{
			if (Db.Users.Any(x => x.Expires < DateTime.UtcNow))
			{
				Db.Users.RemoveRange(Db.Users.Where(x => x.Expires < DateTime.UtcNow));
				Db.SaveChanges();
			}
		}
		await Task.Delay(TimeSpan.FromHours(1));
        await BackgroundAction(DbContextFactory).ConfigureAwait(false); 
    }
}