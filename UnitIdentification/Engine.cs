using Microsoft.AspNetCore.SignalR.Client;

namespace UnitIdentification;
public class Engine
{
    private readonly PingPong.Engine PPE;
    private readonly IStorage S;
    public readonly Progress.manager.Task PmT;
    private readonly Unit.IInfomation UI;
    public Engine(Progress.Manager PM,PingPong.Engine PPE, IStorage S,Unit.IInfomation UI) {
        this.PPE = PPE;
        this.S = S;
        (this.PmT = PM.Register).Install();
        this.PPE.PmT.Change += async () => await PmT_Change();
        (this.UI = UI).Change += () =>
        {
            if (UI.Network is Unit.infomation.Network.Offline)
                this.PmT.Install();
        };
    }

    private async Task PmT_Change()
    {
        if (this.PmT.Status is Progress.manager.Status.InProcess) return;
        if (PPE.PmT.Status is Progress.manager.Status.Done)
        {
            this.PmT.InProcess();
            if (await this.S.Type() is StandardInternal.unitIdentification.storage.Type.None) {

                return;
            }
            await this.PPE.Hub.SendAsync("UnitIdentification_Verify", await this.S.Read(), this.UI.ISO639_1, this.UI.Type);
        }
        else this.PmT.Install();
    }
}
