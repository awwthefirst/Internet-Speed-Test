using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Internet_Speed_Test.Scripts
{
    ///<summary>This class shows your current download speed.</summary>
    public class DownloadComponent : VisibleComponent
    {

        public DownloadComponent(FrameworkElement component, Label label) : base(component, label) //Needed for the default constructor
        {
        }

        public override void OnSetVisible(MainWindow mainWindow)
        {
            mainWindow.DownloadText.Content = "Loading";
            Thread thread = new Thread(this.DownloadSpeedTest); //Runs the test in a new thread
            thread.Start(mainWindow);
        }

        private void DownloadSpeedTest(object param) //Runs the download test and updates the text
        {
            MainWindow mainWindow = (MainWindow)param;

            string content;
            int fontSize;
            try
            {
                double downloadSpeed = this.GetDownloadSpeed();
                double speedMBPS = downloadSpeed / 1000000; //Converts to mbps
                double roundedSpeed = Math.Round(speedMBPS, 2);
                content = $"{roundedSpeed} mbps"; //Formats the result
                fontSize = 17;
            }
            catch (WebException) //Shows disconnected if the test fails
            {
                content = "Disconnected"; 
                fontSize = 12;
            }
            Application.Current.Dispatcher.Invoke(delegate
            {
                mainWindow.DownloadText.Content = content; //Changes the text
                mainWindow.DownloadText.FontSize = fontSize;
            });
        }

        ///<summary>Returns the download speed in bytes.</summary>
        ///<remarks>Can take several seconds to run.</remarks>
        protected double GetDownloadSpeed()
        {
            Stopwatch watch = new Stopwatch(); //Stopwatch to time how long the download takes

            byte[] data;
            using (WebClient client = new WebClient())
            {
                watch.Start();
                data = client.DownloadData("http://dl.google.com/googletalk/googletalk-setup.exe?t=" + DateTime.Now.Ticks); //Downloads the file
                watch.Stop();
            }
            double speed = data.LongLength / watch.Elapsed.TotalSeconds; //Divides the size of the data downloaded by the time taken to get the speed
            return speed;
        }

        public override void OnSetHidden(MainWindow mainWindow) //Neded for inheritence
        {

        }
    }
}
