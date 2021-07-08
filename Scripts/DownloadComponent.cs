using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows;
namespace Internet_Speed_Test.Scripts
    
{
    class DownloadComponent : VisibleComponent
    {
        public override void OnSetVisible(MainWindow mainWindow)
        {
            double downloadSpeed = DownloadSpeed();
            String endResult = "";
            mainWindow.DownloadText.Content = endResult;
        }

        private double DownloadSpeed()
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
