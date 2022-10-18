using Microsoft.AspNetCore.SignalR.Client;

namespace UnitIdentification;
public class Engine
{
    private readonly PingPong.Engine PPE;
    private readonly IStorage S;
    public readonly Progress.manager.Task PmT;
    private readonly Unit.IInfomation UI;
    private readonly Product.Infomation PI;
    public Engine(Progress.Manager PM,PingPong.Engine PPE, IStorage S,Unit.IInfomation UI,Product.Infomation PI) {
        this.PPE = PPE;
        this.S = S;
        this.PI = PI;
        (this.PmT = PM.Register).Install();
        this.PPE.PmT.Change += async () => await PmT_Change();
        this.UI = UI;
    }
    private async Task PmT_Change()
    {
    
        if (this.PmT.Status is Progress.manager.Status.InProcess) return;
        if (PPE.PmT.Status is Progress.manager.Status.Done)
        {
            this.PmT.InProcess();
            Console.WriteLine(this.PPE.Hub.State.ToString());
            
            if (await this.S.Type() is StandardInternal.unitIdentification.storage.Type.None)
                await this.PPE.Hub.InvokeAsync("UI_C", this.UI.ISO639_1, this.UI.Type, this.PI.Name);
            else 
                await this.PPE.Hub.InvokeAsync("Ui_V", await this.S.Read(), this.UI.ISO639_1, this.UI.Type,this.PI.Name);
        }
        else this.PmT.Install();
    }
}
