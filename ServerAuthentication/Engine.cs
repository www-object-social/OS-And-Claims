using Microsoft.EntityFrameworkCore;

namespace ServerAuthentication;
public class Engine : PongPing.IAuthentication
{
	private Guid UnitID;
	private readonly PongPing.IUnitIdentifications UI;
	private readonly IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory;
	public Engine(PongPing.IUnitIdentifications UI,IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory) { 
		this.UI = UI;
		this.DbContextFactory = DbContextFactory;
	}
	public async Task Create(string ConnectionID, string Host)
	{
		UnitID = await this.UI.GetID(ConnectionID, Host);
		using var Db = this.DbContextFactory.CreateDbContext();
		Db.Users.Add(new ServerStorages.User
		{
			Created = DateTime.UtcNow,
			Expires = DateTime.UtcNow.AddDays(60),
			ExpiresNumberOfDays = 60,
			IsDeleted = false,
			Id = Guid.NewGuid(),
			ExpiresNumberOfMonths = 0,
			ExpiresNumberOfYears = 0,
			Iso3166 = this.UI.GetISO3166,
			Iso6391 = this.UI.GetISO639_1
		}).Entity.UnitUsers.Add(new ServerStorages.UnitUser
		{
			IsActive = true,
			Created = DateTime.UtcNow,
			Expires = DateTime.UtcNow.AddDays(60),
			Id = Guid.NewGuid(),
			IsBlock = false,
			IsVerified = true,
			UnitIdentificationId = UnitID,
			VerdificationCode = System.Text.Encoding.UTF8.GetBytes("******")
		});
		Db.SaveChanges();
		await this.UI.UpdateUI();
	}
}