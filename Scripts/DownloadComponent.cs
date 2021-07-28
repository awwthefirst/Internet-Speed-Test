using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows;
using System.Threading;
namespace Internet_Speed_Test.Scripts

{
    class DownloadComponent : VisibleComponent
    {
        //takes the rounded number and places it in the textbox to be displayed
        public override void OnSetVisible(MainWindow mainWindow)
        {
            Thread thread = new Thread(()=> {
                double downloadSpeed = DownloadSpeed();
                double endResult = DownloadMath();
                Application.Current.Dispatcher.Invoke(() => { 
                    mainWindow.DownloadText.Content = endResult + "Mbs";
                });
            });
            thread.Start();
        }

        //rounds the returned number to somthing more readable 
        private double DownloadMath()
        {
            double DownMbs = this.DownloadSpeed();
            double Mbs = DownMbs / 1000000;
            double roundedspeed = Math.Round(Mbs, 2);
            return roundedspeed;                            
        }
       

        private double DownloadSpeed()
        {
            Stopwatch watch = new Stopwatch(); //starts a stopwatch, used to calculate download speed
            byte[] data;
            using (WebClient client = new WebClient())
            {
                watch.Start();
                data = client.DownloadData("http://dl.google.com/googletalk/googletalk-setup.exe?t=" + DateTime.Now.Ticks); //link is file that is being downloaded
                watch.Stop();
            }
            double speed = data.LongLength / watch.Elapsed.TotalSeconds; //takes the size of the file and devides it by the time it took to download it  
            return speed;
        }



        public override void OnSetHidden(MainWindow mainWindow)
        {
        }
    }
}
