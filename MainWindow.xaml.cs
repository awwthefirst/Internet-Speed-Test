using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Internet_Speed_Test.Scripts;
using System.IO;
using System.Threading;

namespace Internet_Speed_Test
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int HOTKEY_ID = 9000; //Not sure why this is needed but it is
        private const byte MOD_ALT = 0x0001; //Hex code for alt

        private const byte VK_I = 0x49; //Hex code for I

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pos);

        private HwndSource source;

        private bool isVisible = false; //Stores whether the overlay is currently visible
        private VisibleComponent[] visibleComponents;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        public MainWindow()
        {
            InitializeComponent();
            visibleComponents = new VisibleComponent[] { new PingComponent(this.PingComponent, this.PingText), new DownloadComponent(this.DownloadComponent, this.DownloadText) };
            this.OnSetVisible();
            Grid fontMenu = (Grid)this.FindResource("FontMenu");
            ComboBox fontFamilies = fontMenu.Children.OfType<ComboBox>().First();
            foreach (FontFamily i in Fonts.SystemFontFamilies)
            {
                fontFamilies.Items.Add(i);
            }
        }

        protected override void OnSourceInitialized(EventArgs e) //Registers a global hotkey for alt + i
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;

            source = HwndSource.FromHwnd(handle);
            source.AddHook(this.HwndHook);

            RegisterHotKey(handle, HOTKEY_ID, MOD_ALT, VK_I);
        }

        //Checks if the HotKey is alt + i and then calls ToggleVisibility if it is
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const short WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_I)
                            {
                                this.ToggleVisibility();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        ///<summary>Toggles whether the overlay is visible.</summary>
        private void ToggleVisibility()
        {
            if (!this.isVisible)
            {
                this.Hide();
                this.OnSetVisible();
            }
            else
            {
                this.Show();
                this.OnSetHidden();
            }
            this.isVisible = !this.isVisible;
        }

        private void OnSetVisible()
        {
            foreach (VisibleComponent i in this.visibleComponents)
            {
                i.OnSetVisible(this);
            }
        }

        private void OnSetHidden()
        {
            foreach (VisibleComponent i in this.visibleComponents)
            {
                i.OnSetHidden(this);
            }
        }

        private void VisibleComponentLeftClick(object sender, MouseButtonEventArgs e)
        {
            string name = ((Image)e.Source).Name;
            foreach (VisibleComponent v in this.visibleComponents)
            {
                if (v.Component.Name == name)
                {
                    v.OnLeftClick(this);
                }
            }
        }

        private void VisibleComponentRightClick(object sender, MouseButtonEventArgs e)
        {
            string name = ((Image)e.Source).Name;
            foreach (VisibleComponent v in this.visibleComponents)
            {
                if (v.Component.Name == name)
                {
                    v.OnRightClick(this);
                }
            }
        }

        public void CloseSettingsMenu()
        {
            StackPanel settingMenu = (StackPanel)this.FindResource("SettingsMenu");
            if (this.Grid.Children.Contains(settingMenu))
            {
                this.Grid.Children.Remove(settingMenu);
            }
        }

        public void CloseFontMenu()
        {
            Grid fontMenu = (Grid)this.FindResource("FontMenu");
            if (this.Grid.Children.Contains(fontMenu))
            {
                this.Grid.Children.Remove(fontMenu);
            }
        }

        private void OpenFontMenu(object sender, RoutedEventArgs e)
        {
            this.CloseSettingsMenu();
            Grid fontMenu = (Grid) this.FindResource("FontMenu");
            this.Grid.Children.Add(fontMenu);
            FrameworkElement component = VisibleComponent.CurrentComponent.Component;
            fontMenu.Margin = new Thickness(component.Margin.Left + 102, component.Margin.Top, component.Margin.Right, component.Margin.Bottom);
            TextBox[] inputs = fontMenu.Children.OfType<TextBox>().ToArray();
            Label label = VisibleComponent.CurrentComponent.Label;
            Color color = ((SolidColorBrush)label.Foreground).Color;
            inputs[0].Text = color.R.ToString();
            inputs[1].Text = color.G.ToString();
            inputs[2].Text = color.B.ToString();
        }

        private void ChangeFontColor(object sender, RoutedEventArgs e)
        {
            Grid fontMenu = (Grid)this.FindResource("FontMenu");
            TextBox[] inputs = fontMenu.Children.OfType<TextBox>().ToArray(); //Probably not a good way of doing it but it does work
            Label errorLabel = fontMenu.Children.OfType<Label>().ToList().Find(i => i.Name == "ErrorLabel");
            ComboBox fontFamilies = fontMenu.Children.OfType<ComboBox>().First();
            try
            {
                byte r = byte.Parse(inputs[0].Text), g = byte.Parse(inputs[1].Text), b = byte.Parse(inputs[2].Text);
                Label label = VisibleComponent.CurrentComponent.Label;
                if (label != null)
                {
                    label.Foreground = new SolidColorBrush(Color.FromRgb(r, g, b));
                    if (fontFamilies.SelectedItem != null)
                    {
                        label.FontFamily = new FontFamily(fontFamilies.SelectedItem.ToString());
                    }
                    errorLabel.Content = "";
                }
            } catch (Exception error) when (error is FormatException || error is OverflowException) {
                errorLabel.Content = "Input must be a number between 0 and 255";
            }
        }

        private void MoveComponent(object sender, RoutedEventArgs e)
        {
            this.CloseSettingsMenu();

            FrameworkElement component = VisibleComponent.CurrentComponent.Component;
            Label label = VisibleComponent.CurrentComponent.Label;
            Thread thread = new Thread(() => {
                while (true)
                {
                    MouseButtonState mouseButtonState = MouseButtonState.Released;
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        Point point = GetCursorPosition();
                        component.Margin = new Thickness(point.X - 50, point.Y - 50, 0, 0);
                        label.Margin = new Thickness(point.X - 45, point.Y - 20, 0, 0);
                        mouseButtonState = Mouse.LeftButton;
                    });
                    if (mouseButtonState == MouseButtonState.Pressed)
                    {
                        break;
                    }
                }
            });
            thread.Start();
        }

        private void MoreInformation(object sender, RoutedEventArgs e)
        {
            Grid information = (Grid)this.FindResource("Information");
            if (this.Grid.Children.Contains(information))
            {
                this.Grid.Children.Remove(information);
            } else
            {
                this.Grid.Children.Add(information);
            }
        }
    }
}
