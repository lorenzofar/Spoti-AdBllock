using Spoti_AdBlock.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spoti_AdBlock
{
    public class TrayContext : ApplicationContext
    {
        private static NotifyIcon trayIcon;
        public enum IconState
        {
            Online,
            Offline
        }

        public static void InitializeIcon()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.icon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem(Resources.startupMenuItem, ToggleStartup){Checked = StartupHelper.StartupEnabled()},
                    new MenuItem(Resources.exitMenuItem, Exit)
                }),
                Visible = true,
                Text = Resources.about
            };
        }

        static void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        static void ToggleStartup(object sender, EventArgs e)
        {
            var m_item = sender as MenuItem;
            m_item.Checked = !m_item.Checked;
            StartupHelper.ChangeStartup(m_item.Checked);
        }

        public static void ToggleIconState(IconState state)
        {
            Icon newIcon;
            switch (state)
            {
                case IconState.Online:
                    newIcon = Resources.icon;
                    break;
                case IconState.Offline:
                default:
                    newIcon = Resources.offline;
                    break;                
            }
            trayIcon.Icon = newIcon;
        }
    }
}
