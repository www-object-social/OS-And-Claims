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

#pragma warning disable CS8604 // Possible null reference argument.
    public async Task Ui_V(string Token, string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN) => await UI.Verify(this.Context.ConnectionId, this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value, Token, ISO639_1, SuiT, SpiN);

    public async Task UI_C(string ISO639_1, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN) =>await UI.Create(this.Context.ConnectionId, this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value, ISO639_1, SuiT, SpiN);

#pragma warning restore CS8604 // Possible null reference argument.
    public override Task OnConnectedAsync()
    {
        var a = this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value;

        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
     
        return base.OnDisconnectedAsync(exception);
    }
    public Services(IUnitIdentifications UI) { 
        this.UI = UI;
    }
}
