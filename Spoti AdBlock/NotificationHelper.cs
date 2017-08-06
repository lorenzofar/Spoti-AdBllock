using Spoti_AdBlock.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Spoti_AdBlock
{
    public static class NotificationHelper
    {
        private static NotifyIcon _notifyIcon;
        private static int EXPIRYTIME = 2500;

        public class NotificationData
        {
            public string title { get; set; }
            public string body { get; set; }
        }

        public static void ShowBalloon(NotificationData data)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync(data);
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Show(e);
            Thread.Sleep(EXPIRYTIME);
            DisposeOff();
        }

        private static void Show(DoWorkEventArgs e)
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = Resources.icon;
            var data = (NotificationData)e.Argument;
            if (!string.IsNullOrEmpty(data.title))
                _notifyIcon.BalloonTipTitle = data.title;
            if (!string.IsNullOrEmpty(data.body))
                _notifyIcon.BalloonTipText = data.body;
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.Visible = true;
            _notifyIcon.ShowBalloonTip(EXPIRYTIME);
        }

        private static void DisposeOff()
        {
            if (_notifyIcon == null) return;
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }
    }
}