namespace MAUI_Unit;

// All the code in this file is included in all platforms.
public class Infomation:Unit.IInfomation
{
    public string ISO639_1 => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

    public Unit.infomation.Network Network => Microsoft.Maui.Networking.Connectivity.NetworkAccess is NetworkAccess.Internet ? Unit.infomation.Network.Online : Unit.infomation.Network.Offline;
    public Infomation() => Microsoft.Maui.Networking.Connectivity.ConnectivityChanged += (s, e) => this.ChangeAction?.Invoke();

    public Unit.infomation.Type Type =>
#if MACCATALYST
			 Unit.infomation.Type.Mac;
#elif WINDOWS
	 Unit.infomation.Type.Microsft;
#elif ANDROID
	 Unit.infomation.Type.Android;
#elif IOS
 Unit.infomation.Type.iOS;
#else
         Unit.infomation.Type.Unknown;
#endif

    private Action ChangeAction = null!;
    public event Action Change
    {
        add => ChangeAction += value;
        remove => ChangeAction -= value;
    }
}