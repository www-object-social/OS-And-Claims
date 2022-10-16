using Foundation;
using UIKit;

namespace MAUI_OBJECTSOCIAL.Platforms.MacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    public override void OnActivated(UIApplication application)
    {
        Window.WindowScene.Titlebar.TitleVisibility = UITitlebarTitleVisibility.Hidden;

        base.OnActivated(application);
    }
}
