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
            double downloadSpeed = this.GetDownloadSpeed();
            double speedMBS = downloadSpeed / 1000000;
            double roundedSpeed = Math.Round(speedMBS, 2);
            mainWindow.DownloadText.Content = $"{roundedSpeed} mbs";
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
