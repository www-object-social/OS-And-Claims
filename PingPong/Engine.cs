using Microsoft.AspNetCore.SignalR.Client;
namespace PingPong;
public class Engine
{
    private readonly Product.Infomation PI;
    private readonly Unit.IInfomation UI;
    public readonly Progress.manager.Task PmT;
    private readonly IHttpClientFactory HttpClientFactory;
    private readonly HttpClient HttpClient;
    public Engine(IHttpClientFactory HttpClientFactory, Product.Infomation PI, Unit.IInfomation UI, Progress.Manager PM,HttpClient HttpClient) {
        this.PI = PI;
        this.HttpClient = HttpClient;
        this.HttpClientFactory = HttpClientFactory;
        (this.PmT = PM.Register).Install(); 
        (this.UI = UI).Change += async () =>await UI_Change();
        if (this.UI.Network is Unit.infomation.Network.Online&&this.Hub==null)
            _ = this.UI_Change();
    }
    private string[] Domains = new[] { "object.social","memory.claims","bad.claims","good.claims","myos.world", "myos.work", "osmy.world", "osmy.work" };
    private string Domain => this.Domains[new Random().Next(0, Domains.Length - 1)];
    public HubConnection Hub { get; set; } = null!;
    private async Task UI_Change()
    {
       
        if ((this.Hub != null && this.Hub.State is not HubConnectionState.Disconnected)||this.PmT.Status is Progress.manager.Status.InProcess) return;
        this.PmT.InProcess();
        if (this.UI.Network is Unit.infomation.Network.Online &&(this.Hub == null || this.Hub.State is HubConnectionState.Disconnected)) {
            this.PmT.Install();
            if (PI.ISDeveloper)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                this.Hub = new HubConnectionBuilder().WithUrl($"https://{HttpClient.BaseAddress.Host}:{HttpClient.BaseAddress.Port}/PongPing.Services").WithAutomaticReconnect().Build();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            else {
                using var HttpClient = HttpClientFactory.CreateClient();
                this.Hub = new HubConnectionBuilder().WithUrl($"https://{await HttpClient.GetStringAsync($"https://{Domain}/pongping/uniformresource/identifier/single")}/PongPing.Services").WithAutomaticReconnect().Build();
            }

            this.Hub.On<string>("Console", Console.WriteLine);
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