using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Internet_Speed_Test.Scripts
{
    public abstract class VisibleComponent
    {
        ///<summary>Called by <c>MainWindow</c> when the window is set visible.</summary>
        public abstract void OnSetVisible(MainWindow mainWindow);
        ///<summary>Called by <c>MainWindow</c> when the window is set hidden.</summary>
        public abstract void OnSetHidden(MainWindow mainWindow);
        ///<summary>Called when the ascociated <c>Image</c> is clicked.</summary>
        public abstract void OnClick();
        protected string name;
        ///<summary>Asscociated images name. Used for matching the clicked Image.</summary>
        public string Name { get { return name; } }

        ///<summary>Sets name equal to the name of the entered component.</summary>
        ///<param name="component">The component that this object will be ascociated with.</param>
        public VisibleComponent(FrameworkElement component)
        {
            this.name = component.Name;
        }
    }
}
