using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;

namespace PongPing;
public class Services:Hub
{
    private readonly IUnitIdentifications UI;

    public async Task UI_V(string Token, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN) => await UI.Verify(this.Context.ConnectionId, Token, ISO639_1, SuiT, SpiN);
    public async Task UI_C(string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN) {
        this.Clients.Caller.SendAsync("UI_D", this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value);
        await UI.Create(this.Context.ConnectionId, ISO639_1, SuiT, SpiN);
    }
    public Services(IUnitIdentifications UI) { 
        this.UI = UI;
    }
}
