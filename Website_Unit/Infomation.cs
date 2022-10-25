using Microsoft.JSInterop;
using System.Globalization;
using Unit.infomation;
namespace Website_Unit;
public class Infomation : Unit.IInfomation
{
	public string ISO639_1 => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
	public Network Network { get; private set; } = Network.Online;

	public StandardInternal.unit.infomation.Type Type { get; private set; } = StandardInternal.unit.infomation.Type.Unknown;
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

	public int BaseUtcOffsetTotalMinutes => (int)TimeZoneInfo.Local.BaseUtcOffset.TotalMinutes;

	public string ISO3166 { get; private set; } = "US";

	private DotNetObjectReference<Infomation> DotNetObjectRef = null!;
	private async Task Construter(IJSRuntime jSRuntime) {
		JSObject = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/Website_Unit/Infomation.js");
		var TestCountry = (await JSObject.InvokeAsync<string[]>("AttemptGetCountry")).Where(x => x.IndexOf("-") != -1);
		if (TestCountry.Any())
			this.ISO3166 = new RegionInfo(TestCountry.First()).TwoLetterISORegionName.ToUpper();
		var UA = await JSObject.InvokeAsync<string>("UserAgent");
		if (UA.ToLower().Contains(" firefox/"))
			Type = StandardInternal.unit.infomation.Type.Firefox;
		else if (UA.ToLower().Contains(" opr/"))
			Type = StandardInternal.unit.infomation.Type.Oprea;
		else if (UA.ToLower().Contains(" edg/"))
			Type = StandardInternal.unit.infomation.Type.Edge;
		else if (UA.ToLower().Contains(" chrome/"))
			Type = StandardInternal.unit.infomation.Type.Chrome;
		else if (UA.ToLower().Contains(" safari/"))
			Type = StandardInternal.unit.infomation.Type.Safari;
		else
			Type = StandardInternal.unit.infomation.Type.Unknown;
		if (await JSObject.InvokeAsync<bool>("Network", this.DotNetObjectRef = DotNetObjectReference.Create(this)))
			this.Online();
		else
			this.Offline();

	}
	private Action ChangeAction = null!;
	public event Action Change
	{
		add => ChangeAction += value;
#pragma warning disable CS8601 // Possible null reference assignment.
		remove => ChangeAction -= value;
#pragma warning restore CS8601 // Possible null reference assignment.
	}
}
