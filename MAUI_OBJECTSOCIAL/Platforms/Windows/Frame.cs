using Microsoft.UI.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics;
namespace MAUI_OBJECTSOCIAL.Platforms;
public static class Frame
{
    public static void Settings(Microsoft.Maui.Handlers.IWindowHandler handler)
    {
        var nativeWindow = handler.PlatformView;
        nativeWindow.Activate();
        nativeWindow.ExtendsContentIntoTitleBar = false;
        AppWindow appWindow = AppWindow.GetFromWindowId(Microsoft.UI.Win32Interop.GetWindowIdFromWindow(WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow)));
        appWindow.TitleBar.ButtonInactiveBackgroundColor = appWindow.TitleBar.InactiveBackgroundColor = appWindow.TitleBar.ButtonBackgroundColor = appWindow.TitleBar.BackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
        appWindow.TitleBar.ForegroundColor = appWindow.TitleBar.ButtonHoverForegroundColor = appWindow.TitleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
        appWindow.Title = "";
        appWindow.Changed += (s, a) =>
        {
            if (s.ClientSize.Height < 600 || s.ClientSize.Width < 600)
            {
                try
                {
                    s.Resize(new SizeInt32(s.ClientSize.Height < 600 ? 600 : s.ClientSize.Height, s.ClientSize.Width < 600 ? 600 : s.ClientSize.Width));
                }
                catch (Exception)
                {

                }
                return;
            }
        };
        appWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
    }

}