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
            componentClicked?.Invoke(this, new ClickedEventArgs { MainWindow = mainWindow });
            StackPanel settingsMenu = (StackPanel) mainWindow.FindResource("SettingsMenu");
            if (!mainWindow.Grid.Children.Contains(settingsMenu))
            {
                mainWindow.Grid.Children.Add(settingsMenu);
            }
            settingsMenu.Margin = new Thickness(Component.Margin.Left + 102, Component.Margin.Top, Component.Margin.Right, Component.Margin.Bottom);

            CurrentComponent = this;
        }

        private void ComponentRightClicked(object sender, ClickedEventArgs e)
        {
            if (this.settingsActive)
            {
                StackPanel settingsMenu = (StackPanel)e.MainWindow.FindResource("SettingsMenu");
                this.settingsActive = false;
                e.MainWindow.Grid.Children.Remove(settingsMenu);
            }
        }

        public void OnLeftClick(MainWindow mainWindow)
        {
            StackPanel settingMenu = (StackPanel)mainWindow.FindResource("SettingsMenu");
            if (mainWindow.Grid.Children.Contains(settingMenu))
            {
                mainWindow.Grid.Children.Remove(settingMenu);
            }

            CurrentComponent = null;
        }
    }
}
