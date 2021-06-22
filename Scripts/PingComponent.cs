using System;
using System.Windows;
using System.Net.NetworkInformation;
using System.Timers;
using System.Collections.Generic;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {
        private Timer currentTimer;
        private List<long> previousResults;
        public override void OnSetVisible(MainWindow mainWindow)
        {
            this.previousResults = new List<long>();

            this.currentTimer = new Timer(1000);
            this.currentTimer.Elapsed += (sender, e) => this.UpdatePing(sender, e, mainWindow);
            this.currentTimer.Start();
        }

        private void UpdatePing(object source, ElapsedEventArgs e, MainWindow mainWindow)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                if (this.previousResults.Count >= 3)
                {
                    this.previousResults.Remove(0);
                }
                long result = this.TestPing();
                Console.WriteLine(result);

                if (result == -1)
                {
                    mainWindow.PingText.Content = "Disconected";
                } else
                {
                    this.previousResults.Add(result);
                    long combinedResults = 0;
                    this.previousResults.ForEach(l => { combinedResults += l; });
                    long averageResult = combinedResults / this.previousResults.Count;

                    mainWindow.PingText.Content = averageResult + "ms";
                }
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
                if (reply.Status != IPStatus.Success)
                {
                    result = -1;
                }
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
