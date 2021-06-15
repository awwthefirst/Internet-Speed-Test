using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Speed_Test.Scripts
{
    public abstract class VisibleComponent
    {
        public abstract void OnSetVisible(MainWindow mainWindow);
        public abstract void OnSetHidden(MainWindow mainWindow);
    }
}
