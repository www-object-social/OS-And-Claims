using Foundation;
using UIKit;

namespace MAUI_OBJECTSOCIAL.Platforms.MacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    public override void OnActivated(UIApplication application)
    {
#pragma warning disable CA1416 // Validate platform compatibility
        Window.WindowScene.Titlebar.TitleVisibility = UITitlebarTitleVisibility.Hidden;
#pragma warning restore CA1416 // Validate platform compatibility

        base.OnActivated(application);
    }
}
