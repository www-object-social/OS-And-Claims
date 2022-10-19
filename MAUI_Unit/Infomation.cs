namespace MAUI_Unit;
public class Infomation:Unit.IInfomation
{
    public string ISO639_1 => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    public Unit.infomation.Network Network => Microsoft.Maui.Networking.Connectivity.NetworkAccess is NetworkAccess.Internet ? Unit.infomation.Network.Online : Unit.infomation.Network.Offline;
    public Infomation() => Microsoft.Maui.Networking.Connectivity.ConnectivityChanged += (s, e) => this.ChangeAction?.Invoke();
    public StandardInternal. unit.infomation.Type Type =>
#if MACCATALYST
    StandardInternal.unit.infomation.Type.Mac;
#elif WINDOWS
	StandardInternal.unit.infomation.Type.Microsft;
#elif ANDROID
	StandardInternal.unit.infomation.Type.Android;
#elif IOS
    StandardInternal.unit.infomation.Type.iOS;
#else
    StandardInternal.unit.infomation.Type.Unknown;
    #endif
    private Action ChangeAction = null!;
    public int BaseUtcOffsetTotalMinutes => (int)TimeZoneInfo.Local.BaseUtcOffset.TotalMinutes;
    public event Action Change
    {
        add => ChangeAction += value;
        remove => ChangeAction -= value;
    }
}