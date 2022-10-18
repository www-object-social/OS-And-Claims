using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ServerUnitIdentifications;

public class Engine:PongPing.IUnitIdentifications
{
    private readonly IHubContext<PongPing.Services> HubContext;
    private readonly IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory;
    public Engine(IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory,IHubContext<PongPing.Services> HubContext)
    {
        this.HubContext = HubContext;
        this.DbContextFactory = DbContextFactory;
    }
    public Task Verify(string ConnectionID,string Host,string Token,string ISO639_1,StandardInternal.unit.infomation.Type SuiT,StandardInternal.product.infomation.Name SpiN) {


        return Task.CompletedTask;
    }
    public Task Create(string ConnectionID,string Host, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN)
    {

        return Task.CompletedTask;
    }
}