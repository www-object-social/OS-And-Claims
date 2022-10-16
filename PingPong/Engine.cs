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
        if (this.UI.Network is Unit.infomation.Network.Online)
        {
            this.Hub = new HubConnectionBuilder().WithUrl("https://win-9ndprc00ff9.object.social/PongPing.Services").WithAutomaticReconnect().Build();
            await this.Hub.StartAsync();
            Console.WriteLine(this.Hub.State);
        }
        else this.PmT.Cancel(); 
    }
}