using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_Speed_Test.Scripts
{
    public abstract class VisibleComponent
    {
        ///<summary>Called by <c>MainWindow</c> when the window is set visible.</summary>
        public abstract void OnSetVisible(MainWindow mainWindow);
        ///<summary>Called by <c>MainWindow</c> when the window is set hidden.</summary>
        public abstract void OnSetHidden(MainWindow mainWindow);
    }
}
