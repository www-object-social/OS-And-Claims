using Microsoft.AspNetCore.SignalR;
using PongPing;

namespace PongPingUnits;
public class Engine:PongPing.IUnits
{
    public Task Verify(string Token) {
        return Task.CompletedTask;
    }
    public Engine(IHubContext<Services> hubContext) { }
}