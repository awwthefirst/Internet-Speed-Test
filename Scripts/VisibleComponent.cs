using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Internet_Speed_Test.Scripts
{
    ///<summary>The base class for ping component and download component</summary>
    public abstract class VisibleComponent
    {
        ///<summary>Called by <c>MainWindow</c> when the window is set visible.</summary>
        public abstract void OnSetVisible(MainWindow mainWindow);
        ///<summary>Called by <c>MainWindow</c> when the window is set hidden.</summary>
        public abstract void OnSetHidden(MainWindow mainWindow);
        protected FrameworkElement component;
        protected Label label;
        public FrameworkElement Component { get { return component; } }
        public Label Label { get { return label; } }

        private static event EventHandler<ClickedEventArgs> componentClicked;

        private bool settingsActive = false;
        public static VisibleComponent CurrentComponent = null;

        ///<summary>Asscociated images name. Used for matching the clicked Image.</summary>
        ///<summary>Sets name equal to the name of the entered component.</summary>
        ///<param name="component">The component that this object will be ascociated with.</param>
        public VisibleComponent(FrameworkElement component, Label label)
        {
            this.component = component;
            this.label = label;
            componentClicked += ComponentRightClicked;
        }

        private class ClickedEventArgs : EventArgs
        {
            public MainWindow MainWindow;
        }

        ///<summary>Called when the ascociated <c>Image</c> is clicked.</summary>
        public virtual void OnRightClick(MainWindow mainWindow)
        {
            mainWindow.CloseFontMenu();

            componentClicked?.Invoke(this, new ClickedEventArgs { MainWindow = mainWindow }); //Invokes the componentClicked event

            StackPanel settingsMenu = (StackPanel) mainWindow.FindResource("SettingsMenu"); //Finds settingsMenu

            if (!mainWindow.Grid.Children.Contains(settingsMenu))
            {
                mainWindow.Grid.Children.Add(settingsMenu);
            }
            settingsMenu.Margin = new Thickness(Component.Margin.Left + 102, Component.Margin.Top + 20, Component.Margin.Right, Component.Margin.Bottom); //Positions the settings menu

            CurrentComponent = this;
        }

        private void ComponentRightClicked(object sender, ClickedEventArgs e) //Closes the settings menu if it is open
        {
            if (this.settingsActive)
            {
                StackPanel settingsMenu = (StackPanel)e.MainWindow.FindResource("SettingsMenu");
                this.settingsActive = false;
                e.MainWindow.Grid.Children.Remove(settingsMenu);
            }
        }

        public void OnLeftClick(MainWindow mainWindow) //Closes the settings menu and font menu if they are open
        {
            mainWindow.CloseSettingsMenu();

            mainWindow.CloseFontMenu();

            CurrentComponent = null;
        }
    }
}
