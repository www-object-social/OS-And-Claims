using Microsoft.AspNetCore.SignalR.Client;

namespace PingPong;
public class Engine
{
    private readonly Product.Infomation PI;
    private readonly Unit.IInfomation UI;
    public readonly Progress.manager.Task PmT;
    public Engine(Product.Infomation PI, Unit.IInfomation UI, Progress.Manager PM) {
        this.PI = PI;
        (this.PmT = PM.Register).Install(); 
        (this.UI = UI).Change += UI_Change;
        this.UI_Change();
    }
    private HubConnection Hub { get; set; } = null!;
    private async void UI_Change()
    {
        if (Hub != null ||this.PmT.Status  is Progress.manager.Status.Install or Progress.manager.Status.Cancel || Hub.State is not HubConnectionState.Disconnected) return;
        if (this.UI.Network is Unit.infomation.Network.Online)
        {
            this.Hub = new HubConnectionBuilder().WithUrl("https://win-9ndprc00ff9.object.social/PongPing.Services").WithAutomaticReconnect().Build();
            await this.Hub.StartAsync();
        }
        else this.PmT.Cancel(); 
    }
}