using Microsoft.Win32;
using System.Windows.Forms;

namespace Spoti_AdBlock
{
    public partial class StartupHelper
    {
        private static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static bool StartupEnabled()
        {
            return rkApp.GetValue("SpotifyAdBlock") != null;
        }

        public static void ChangeStartup(bool state)
        {
            if (state) rkApp.SetValue("SpotifyAdBlock", Application.ExecutablePath);
            else rkApp.DeleteValue("SpotifyAdBlock", false);
        }
    }
}
