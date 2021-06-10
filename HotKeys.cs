using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Internet_Speed_Test
{
    class HotKeys
    {
        public const int HOTKEY_ID = 9000;
        ///<summary>Entered when you dont want a mod.</summary>
        public const byte MOD_NONE = 0x0000;
        public const byte MOD_ALT = 0x0001; //ALT
        public const byte MOD_CONTROL = 0x0002; //CTRL
        public const byte MOD_SHIFT = 0x0004; //SHIFT
        public const byte MOD_WIN = 0x0008; //WINDOWS

        public const byte VK_I = 0x49;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private HwndSource source;

        private void testc()
        {
            HwndHook(1, 1, 1, 1, true, e => HwndHook())
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled, Action test)
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
    }
}
