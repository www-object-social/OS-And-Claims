namespace MAUI_Unit;
public class Infomation:Unit.IInfomation
{
    public string ISO639_1 => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    public Unit.infomation.Network Network => Microsoft.Maui.Networking.Connectivity.NetworkAccess is NetworkAccess.Internet ? Unit.infomation.Network.Online : Unit.infomation.Network.Offline;
    public Infomation() => Microsoft.Maui.Networking.Connectivity.ConnectivityChanged += (s, e) => this.ChangeAction?.Invoke();
    public unit.infomation.Type Type =>
#if MACCATALYST
    unit.infomation.Type.Mac;
#elif WINDOWS
	unit.infomation.Type.Microsft;
#elif ANDROID
	unit.infomation.Type.Android;
#elif IOS
    unit.infomation.Type.iOS;
#else
    unit.infomation.Type.Unknown;
    #endif
    private Action ChangeAction = null!;
    public event Action Change
    {
        add => ChangeAction += value;
        remove => ChangeAction -= value;
    }
}