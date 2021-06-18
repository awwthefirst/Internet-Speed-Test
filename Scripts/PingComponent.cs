using System;
using System.Windows;
using System.Net.NetworkInformation;
using System.Timers;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {
        private Timer currentTimer;
        public override void OnSetVisible(MainWindow mainWindow)
        {
            this.currentTimer = new Timer(2000);
            this.currentTimer.Elapsed += (sender, e) => this.UpdatePing(sender, e, mainWindow);
            this.currentTimer.Start();
        }

        private void UpdatePing(object source, ElapsedEventArgs e, MainWindow mainWindow)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                mainWindow.PingText.Content = this.TestPing() + "ms";
            });
        }

        private long TestPing()
        {
            long result;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("1.1.1.1");
                result = reply.RoundtripTime;
                if (!(reply.Status == IPStatus.Success))
                    result = -1;
            }
            catch (PingException)
            {
                result = -1;
            }
            return result;
        }

        public override void OnSetHidden(MainWindow mainWindow)
        {
            if (this.currentTimer != null)
            {
                this.currentTimer.Stop();
            }
        }
    }
}
