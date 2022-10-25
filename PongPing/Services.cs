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
    private readonly IAuthentication A;
#pragma warning disable CS8604 // Possible null reference argument.
    public async Task UI_V(string Token, string ISO639_1, string ISO3166, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN, int BaseUtcOffsetTotalMinutes) => await UI.Verify(this.Context.ConnectionId,
			  this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Connection.RemoteIpAddress,

		this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value, Token, ISO639_1, ISO3166, SuiT, SpiN, BaseUtcOffsetTotalMinutes);
    public async Task UI_C(string ISO639_1, string ISO3166, StandardInternal.unit.infomation.Type SuiT, StandardInternal.product.infomation.Name SpiN, int BaseUtcOffsetTotalMinutes) =>await UI.Create(this.Context.ConnectionId,
					  this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Connection.RemoteIpAddress,
		this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value, ISO639_1, ISO3166, SuiT, SpiN, BaseUtcOffsetTotalMinutes);
    public async Task UI_I(string Value) => await UI.ISO3166(this.Context.ConnectionId, this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value, Value);
#pragma warning restore CS8604 // Possible null reference argument.
    public async Task A_C() => await A.Create(this.Context.ConnectionId, this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value);
    

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        await UI.Remove(Context.ConnectionId,this.Context.Features.Get<IHttpContextFeature>()?.HttpContext.Request.Host.Value);
#pragma warning restore CS8604 // Possible null reference argument.
        await base.OnDisconnectedAsync(exception);
    }
    public Services(IUnitIdentifications UI, IAuthentication A) { 
        this.UI = UI;
        this.A = A;
    }
}
