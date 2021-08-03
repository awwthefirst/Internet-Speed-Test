using System;
using System.Windows;
using System.Net.NetworkInformation;
using System.Timers;
using System.Collections.Generic;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {
        private Timer currentTimer; //A timer to call UpdatePing() once a second
        private List<long> previousResults; //Used for getting a average of results to smooth them out

        public PingComponent(FrameworkElement component) : base(component)
        {

        }

        public override void OnSetVisible(MainWindow mainWindow)
        {
            this.previousResults = new List<long>();

            this.currentTimer = new Timer(1000); //Setups the timer
            this.currentTimer.Elapsed += (sender, e) => this.UpdatePing(sender, e, mainWindow);
            this.currentTimer.Start();
        }

        private void UpdatePing(object source, ElapsedEventArgs e, MainWindow mainWindow) //Called once a second by the currentTimer
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                if (this.previousResults.Count >= 3)
                {
                    this.previousResults.Remove(0);
                }
                long result = this.TestPing();

                if (result == -1) //If you have no internet
                {
                    mainWindow.PingText.Content = "Disconected";
                } else
                {
                    this.previousResults.Add(result);
                    long combinedResults = 0;
                    this.previousResults.ForEach(l => { combinedResults += l; });
                    long averageResult = combinedResults / this.previousResults.Count; //Average of the past 3 calls

                    mainWindow.PingText.Content = averageResult + "ms"; //Sets the label's text
                }
            });
        }

        ///<summary>Returns the ping roundtrip time to the 1.1.1.1(cloudflare).</summary>
        ///<returns>The roundtrip time or -1 if it was not successful.</returns>
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
                this.currentTimer.Stop(); //Stops the timer
            }
        }
    }
}
