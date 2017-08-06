using Spoti_AdBlock.Properties;
using SpotifyAPI.Local;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Spoti_AdBlock.NotificationHelper;

namespace Spoti_AdBlock
{
    static class Program
    {
        private static SpotifyLocalAPI _spotify;
        private static Random r = new Random();

        private const int WAIT_TIME = 500;

        [STAThread]
        static void Main()
        {
            Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayContext());
        }

        private static async void Initialize()
        {
            _spotify = new SpotifyLocalAPI();
            try
            {
                if (!_spotify.Connect()) throw new Exception();
            }
            catch
            {
                bool retry = true;
                while (retry)
                {
                    try
                    {
                        if (_spotify.Connect()) retry = false;
                        else throw new Exception();
                    }
                    catch
                    {
                        int wait = r.Next(1000, 10000);
                        System.Diagnostics.Debug.WriteLine($"Retrying in {wait / 1000} seconds");
                        await Task.Delay(wait);
                        retry = true;
                    }
                }
            }
            ShowBalloon(new NotificationData
            {
                title = Resources.connectionTitle,
                body = Resources.connectionBody
            });
            _spotify.OnTrackChange += _spotify_OnTrackChange;
            _spotify.ListenForEvents = true;
        }

        private static void _spotify_OnTrackChange(object sender, TrackChangeEventArgs e)
        {
            if (e.NewTrack.TrackType == "ad")
            {
                Thread.Sleep(WAIT_TIME);
                _spotify.Mute();
            }
            else if (e.OldTrack.TrackType == "ad")
            {
                Thread.Sleep(WAIT_TIME);
                _spotify.UnMute();
            }
        }
    }
}