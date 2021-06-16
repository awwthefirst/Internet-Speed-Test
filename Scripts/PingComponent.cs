using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.NetworkInformation;
using System.Threading;

namespace Internet_Speed_Test.Scripts
{
    public class PingComponent : VisibleComponent
    {

        public override void OnSetVisible(MainWindow mainWindow)
        {
            mainWindow.PingText.Content = this.TestPing().ToString() + "ms";
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

        }
    }
}
