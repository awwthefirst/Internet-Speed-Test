using System;
using System.Windows;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {
        
        private static System.Timers.Timer aTimer;
        private MainWindow mainWindow;

        public override void OnSetVisible(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            aTimer = new System.Timers.Timer(1000);   
            aTimer.Elapsed += Refresh;
            aTimer.Start();
        }
        private void Refresh(object source, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
           {
               mainWindow.PingText.Content = TestPing("1.1.1.1") + "ms";
           });
        }
        private long TestPing(string address)
        {
            bool pingable = false;
            Ping pinger = null;
            long result = -1;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(address);
                pingable = reply.Status == IPStatus.Success;
                if (pingable)
                {
                    result = reply.RoundtripTime;
                }
                
            }
            catch (PingException)
            {
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return result;
            
        }
        public override void OnSetHidden(MainWindow mainWindow)
        {

        }
    }
}
