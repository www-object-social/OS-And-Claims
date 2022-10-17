namespace MAUI_MemoryClaims;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
#if WINDOWS
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>Platforms.Frame.Settings(handler));
#endif
    }
}