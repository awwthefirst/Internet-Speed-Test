using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Internet_Speed_Test.Scripts
{
    public abstract class VisibleComponent
    {
        ///<summary>Called by <c>MainWindow</c> when the window is set visible.</summary>
        public abstract void OnSetVisible(MainWindow mainWindow);
        ///<summary>Called by <c>MainWindow</c> when the window is set hidden.</summary>
        public abstract void OnSetHidden(MainWindow mainWindow);
        protected FrameworkElement component;
        public FrameworkElement Component { get { return component; } }
        ///<summary>Asscociated images name. Used for matching the clicked Image.</summary>
        ///<summary>Sets name equal to the name of the entered component.</summary>
        ///<param name="component">The component that this object will be ascociated with.</param>
        public VisibleComponent(FrameworkElement component)
        {
            this.component = component;
        }
        ///<summary>Called when the ascociated <c>Image</c> is clicked.</summary>
        public virtual void OnClick(MainWindow mainWindow)
        {
            if (mainWindow.SettingsMode)
            {
                StackPanel settingsMenu = (StackPanel)mainWindow.FindResource("SettingsMenu");
                if (!mainWindow.Grid.Children.Contains(settingsMenu))
                {
                    mainWindow.Grid.Children.Add(settingsMenu);
                }
                settingsMenu.Margin = new Thickness(component.Margin.Left + 102, component.Margin.Top, component.Margin.Right, component.Margin.Bottom);
            }
        }
    }
}
