namespace MAUI_MemoryClaims;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
#if WINDOWS

/* Unmerged change from project 'MAUI_MemoryClaims (net6.0-windows10.0.19041.0)'
Before:
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>MAUI_MemoryClaims.Platforms.Frame.Settings(handler));
After:
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) => Platforms.Platforms.Frame.Settings(handler));
*/
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>Platforms.Windows.Frame.Settings(handler));
#endif
    }
}