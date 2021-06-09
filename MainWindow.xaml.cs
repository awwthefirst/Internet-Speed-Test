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

namespace Internet_Speed_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int HOTKEY_ID = 9000;
        private const byte MOD_NONE = 0x0000; //[NONE]
        private const byte MOD_ALT = 0x0001; //ALT
        private const byte MOD_CONTROL = 0x0002; //CTRL
        private const byte MOD_SHIFT = 0x0004; //SHIFT
        private const byte MOD_WIN = 0x0008; //WINDOWS
                                             //CAPS LOCK:
        private const byte VK_CAPITAL = 0x49;
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private HwndSource source;

        private bool isVisible = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;

            source = HwndSource.FromHwnd(handle);
            source.AddHook(this.HwndHook);

            RegisterHotKey(handle, HOTKEY_ID, MOD_ALT, VK_CAPITAL);
        }

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
                            if (vkey == VK_CAPITAL)
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

        private void ToggleVisibility()
        {
            if (!this.isVisible)
                this.Show();
            else
                this.Hide();
            this.isVisible = !this.isVisible;
        }
    }
}
