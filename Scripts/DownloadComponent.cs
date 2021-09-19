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
    public class DownloadComponent : VisibleComponent
    {

        public DownloadComponent(FrameworkElement component, Label label) : base(component, label)
        {
        }

        public override void OnSetVisible(MainWindow mainWindow)
        {
            mainWindow.DownloadText.Content = "Loading";
            Thread thread = new Thread(this.DownloadSpeedTest);
            thread.Start(mainWindow);
        }

        private void DownloadSpeedTest(object param)
        {
            MainWindow mainWindow = (MainWindow)param;

            string content;
            int fontSize;
            try
            {
                double downloadSpeed = this.GetDownloadSpeed();
                double speedMBS = downloadSpeed / 1000000;
                double roundedSpeed = Math.Round(speedMBS, 2);
                content = $"{roundedSpeed} mbs";
                fontSize = 20;
            } catch (WebException)
            {
                content = "Disconnected";
                fontSize = 12;
            }
            Application.Current.Dispatcher.Invoke(delegate
            {
                mainWindow.DownloadText.Content = content;
                mainWindow.DownloadText.FontSize = fontSize;
            });
        }

        protected double GetDownloadSpeed()
        {
            Stopwatch watch = new Stopwatch();

            byte[] data;
            using (WebClient client = new WebClient())
            {
                watch.Start();
                data = client.DownloadData("http://dl.google.com/googletalk/googletalk-setup.exe?t=" + DateTime.Now.Ticks);
                watch.Stop();
            }
            double speed = data.LongLength / watch.Elapsed.TotalSeconds;
            return speed;
        }

        public override void OnSetHidden(MainWindow mainWindow)
        {

        }
    }
}
