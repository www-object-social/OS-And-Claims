using Microsoft.AspNetCore.SignalR.Client;
using System.Globalization;

namespace UnitIdentification;
public class Engine
{
    private readonly PingPong.Engine PPE;
    private readonly IStorage S;
    public readonly Progress.manager.Task PmT;
    private readonly Unit.IInfomation UI;
    private readonly Product.Infomation PI;
    public Engine(Progress.Manager PM,PingPong.Engine PPE, IStorage S,Unit.IInfomation UI,Product.Infomation PI) {
        _ISO639_1 = PI.ISO639_1s.Any(x => x == UI.ISO639_1.ToUpper()) ? UI.ISO639_1.ToUpper() : "EN";
        this.PPE = PPE;
        this.S = S;
        this.PI = PI;
        (this.PmT = PM.Register).Install();
        this.PPE.PmT.Change += async () => await PmT_Change();
        this.UI = UI;
    }
    private string _ISO3166 = null!;
    public string ISO3166 {
        get => _ISO3166;
        set {
            if (_ISO3166 == value) return;
            _ISO3166 = value;
            this.ChangeAction?.Invoke();
        }
    }

    private Action ChangeAction = null!;
    public event Action Change {
        add => ChangeAction += value;
#pragma warning disable CS8601 // Possible null reference assignment.
        remove => ChangeAction -= value;
#pragma warning restore CS8601 // Possible null reference assignment.
    }
    private string _ISO639_1 =null!;
    public string ISO639_1 {
        get => _ISO639_1;
        private set {
            if(_ISO639_1==value) return;
            _ISO639_1 = value;
            this.ChangeAction?.Invoke();
        }
    }
    private bool _AnyUser = false;
    public bool AnyUser {
        get => _AnyUser;
        private set { 
            if(_AnyUser==value) return;
            _AnyUser = value;
            this.ChangeAction?.Invoke();
        }
    }
    public async Task Update() => await this.PPE.Hub.InvokeAsync("UI_V", await this.S.Read(), this.UI.ISO639_1, this.ISO3166, this.UI.Type, this.PI.Name, this.UI.BaseUtcOffsetTotalMinutes);
    private bool IsUpdateRuning = false;
    private async Task TimeUpdate() {
        if (this.PmT.Status is Progress.manager.Status.Done)
        {
            await Task.Delay(TimeSpan.FromMinutes(7));
            await Update();
        }
        else IsUpdateRuning = false;
    }
    private async Task PmT_Change()
    {
        if (this.PmT.Status is Progress.manager.Status.InProcess) return;
        if (PPE.PmT.Status is Progress.manager.Status.Done)
        {
            this.PmT.InProcess();
			this.ISO3166 = this.UI.ISO3166;
			this.PPE.Hub.On<string, bool, string,string>("UI_S", async (token, anyuser, iSO639_1, iso3166) => {
                await S.Save(token, anyuser ? StandardInternal.unitIdentification.storage.Type.Local : StandardInternal.unitIdentification.storage.Type.Temporarily);
                this.ISO639_1 = iSO639_1;
                this.AnyUser = anyuser;
                this.ISO3166 = iso3166;
                if(this.PmT.Status is not Progress.manager.Status.Done)
                this.PmT.Done();
                if (this.IsUpdateRuning) {
                    this.IsUpdateRuning = true;
                    await TimeUpdate();
                }
            });

            if (await this.S.Type() is StandardInternal.unitIdentification.storage.Type.None)
                await this.PPE.Hub.InvokeAsync("UI_C", this.UI.ISO639_1, this.ISO3166, this.UI.Type, this.PI.Name, this.UI.BaseUtcOffsetTotalMinutes);
            else
                await Update();
        }
        else this.PmT.Install();
    }
}
