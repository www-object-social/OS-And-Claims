using Microsoft.JSInterop;
using Unit.infomation;
namespace Website_Unit;
public class Infomation : Unit.IInfomation
{
	public string ISO639_1 => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
	public Network Network { get; private set; } = Network.Online;

	public Unit.infomation.Type Type { get; private set; } = Unit.infomation.Type.Unknown;
	[JSInvokable]
	public void Online() {
		this.Network = Network.Online;
		this.ChangeAction?.Invoke();
	}
	[JSInvokable]
	public void Offline() {
		this.Network = Network.Offline;
		this.ChangeAction?.Invoke();
	}
	public Infomation(IJSRuntime jSRuntime) => _ = this.Construter(jSRuntime);
	private IJSObjectReference JSObject { get; set; } = null!;
	private DotNetObjectReference<Infomation> DotNetObjectRef = null!;
	private async Task Construter(IJSRuntime jSRuntime) {
		JSObject = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/Website_Unit/Infomation.js");
		var UA = await JSObject.InvokeAsync<string>("UserAgent");
		if (UA.ToLower().Contains(" firefox/"))
			Type = Unit.infomation.Type.Firefox;
		else if (UA.ToLower().Contains(" opr/"))
			Type = Unit.infomation.Type.Oprea;
		else if (UA.ToLower().Contains(" edg/"))
			Type = Unit.infomation.Type.Edge;
		else if (UA.ToLower().Contains(" chrome/"))
			Type = Unit.infomation.Type.Chrome;
		else if (UA.ToLower().Contains(" safari/"))
			Type = Unit.infomation.Type.Safari;
		else
			Type = Unit.infomation.Type.Unknown;
		if (await JSObject.InvokeAsync<bool>("Network", this.DotNetObjectRef = DotNetObjectReference.Create(this)))
			this.Online();
		else
			this.Offline();
	}
	private Action ChangeAction = null!;
	public event Action Change
	{
		add => ChangeAction += value;
		remove => ChangeAction -= value;
	}
}
