using System;
using System.Windows;
using System.Net.NetworkInformation;
using System.Threading;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {

        private Thread currentThread;
        public override void OnSetVisible(MainWindow mainWindow)
        {
            this.currentThread = new Thread(this.UpdatePing);
            this.currentThread.Start(mainWindow);
        }

        private void UpdatePing(object param)
        {
            while (true)
            {
                MainWindow mainWindow = (MainWindow)param;
                Application.Current.Dispatcher.Invoke(delegate
                {
                    mainWindow.PingText.Content = this.TestPing() + "ms";
                });
            }
        }

        private long TestPing()
        {
            long result;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("mc.hypixel.net");
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
            if (this.currentThread != null)
            {
                this.currentThread.Abort();
            }
        }
    }
}
