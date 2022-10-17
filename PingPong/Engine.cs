using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace PingPong;
public class Engine
{
    private readonly Product.Infomation PI;
    private readonly Unit.IInfomation UI;
    public readonly Progress.manager.Task PmT;
    private readonly HttpClient HttpClient;
    public Engine(HttpClient HttpClient,Product.Infomation PI, Unit.IInfomation UI, Progress.Manager PM) {
        this.PI = PI;
        this.HttpClient = HttpClient;
        (this.PmT = PM.Register).Install(); 
        (this.UI = UI).Change += UI_Change;
    
    }
    private string[] Domains = new[] { "object.social","memory.claims","bad.claims","good.claims","myos.world", "myos.work", "osmy.world", "osmy.work" };
    private string Domain => this.Domains[new Random().Next(0, Domains.Length - 1)];
    private HubConnection Hub { get; set; } = null!;
    private async void UI_Change()
    {
        if (this.UI.Network is Unit.infomation.Network.Online)
        {
            if (this.Hub.State is HubConnectionState.Disconnected) {
                this.Hub = new HubConnectionBuilder().WithUrl($"https://{await this.HttpClient.GetFromJsonAsync<string>($"https://{Domain}/pongping/uniformresource/identifier/single")}/PongPing.Services").WithAutomaticReconnect().Build();
                this.Hub.Closed += Hub_Closed;
                await this.Hub.StartAsync();
            }
            if (this.Hub.State is not HubConnectionState.Disconnected)
                this.PmT.Cancel();
            else 
                this.PmT.Done();
        }
        else this.PmT.Cancel(); 
    }
    private Task Hub_Closed(Exception? arg)
    {
        this.UI_Change();
        return Task.CompletedTask;
    }
}