using Microsoft.AspNetCore.SignalR;

namespace ServerUnitIdentifications;

public class Engine:PongPing.IUnitIdentifications
{
    private readonly IHubContext<PongPing.Services> HubContext;
    public Engine(IHubContext<PongPing.Services> HubContext)
    {
        this.HubContext = HubContext;
    }
    public Task Verify(string ConnectionID,string Token,string ISO639_1,StandardInternal.unit.infomation.Type SuiT,StandardInternal.product.infomation.Name SpiN) {

        return Task.CompletedTask;
    }
    public Task Create(string ConnectionID, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN)
    {

        return Task.CompletedTask;
    }
}