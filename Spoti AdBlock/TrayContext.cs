using Spoti_AdBlock.Properties;
using System;
using System.Windows.Forms;

namespace Spoti_AdBlock
{
    public class TrayContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public TrayContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.icon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(Resources.startupMenuItem, ToggleStartup){Checked = StartupHelper.StartupEnabled()},
                new MenuItem(Resources.exitMenuItem, Exit)
            }),
                Visible = true
            };
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        void ToggleStartup(object sender, EventArgs e)
        {
            var m_item = sender as MenuItem;
            m_item.Checked = !m_item.Checked;
            StartupHelper.ChangeStartup(m_item.Checked);
        }
    }
}
