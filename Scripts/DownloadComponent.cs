using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace Internet_Speed_Test.Scripts
{
    class DownloadComponent : VisibleComponent
    {
        public override void OnSetVisible(MainWindow mainWindow)
        {
            mainWindow.DownloadText.Content = this.GetDownloadSpeed();
        }

        protected void CheckSpeed()
        {
            Stopwatch watch = new Stopwatch();

            byte[] data;
            using (WebClient client = new System.Net.WebClient())
            {
                watch.Start();
                data = client.DownloadData("http://dl.google.com/googletalk/googletalk-setup.exe?t=" + DateTime.Now.Ticks);
                watch.Stop();
            }

            var speed = data.LongLength / watch.Elapsed.TotalSeconds; // instead of [Seconds] property

            Console.WriteLine("Speed: {0} bps ", speed.ToString("N0"));
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
            //throw new NotImplementedException();
        }
    }
}
