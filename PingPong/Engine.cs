using Microsoft.AspNetCore.SignalR.Client;
namespace PingPong;
public class Engine
{
    private readonly Product.Infomation PI;
    private readonly Unit.IInfomation UI;
    public readonly Progress.manager.Task PmT;
    private readonly IHttpClientFactory HttpClientFactory;
    public Engine(IHttpClientFactory HttpClientFactory, Product.Infomation PI, Unit.IInfomation UI, Progress.Manager PM) {
        this.PI = PI;
        this.HttpClientFactory = HttpClientFactory;
        (this.PmT = PM.Register).Install(); 
        (this.UI = UI).Change += async () =>await UI_Change();
        if (this.UI.Network is Unit.infomation.Network.Online)
            _ = this.UI_Change();
    }
    private string[] Domains = new[] { "object.social","memory.claims","bad.claims","good.claims","myos.world", "myos.work", "osmy.world", "osmy.work" };
    private string Domain => this.Domains[new Random().Next(0, Domains.Length - 1)];
    private HubConnection Hub { get; set; } = null!;
    private async Task UI_Change()
    {
        if (this.Hub != null && this.Hub.State is not HubConnectionState.Disconnected) return;
        if (this.UI.Network is Unit.infomation.Network.Online &&(this.Hub == null || this.Hub.State is HubConnectionState.Disconnected)) {
            using var HttpClient = HttpClientFactory.CreateClient();
                this.PmT.Install();
                this.Hub = new HubConnectionBuilder().WithUrl($"https://{await HttpClient.GetStringAsync($"https://{Domain}/pongping/uniformresource/identifier/single")}/PongPing.Services").WithAutomaticReconnect().Build();
                this.Hub.Closed += Hub_Closed;
                await this.Hub.StartAsync();
        }
        if (this.UI.Network is Unit.infomation.Network.Online && this.Hub != null && this.Hub.State is not HubConnectionState.Disconnected) {
            this.PmT.Done();
            return;
        }
        this.PmT.Cancel(); 
    }
    private async Task Hub_Closed(Exception? arg) {
        if (this.UI.Network is Unit.infomation.Network.Offline)
            this.PmT.Install();
        await this.UI_Change();
    }
}