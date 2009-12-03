using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Frogger
{
    /// <summary>
    /// todo: draw the options screen
    /// </summary>
    class MenuOptions : MenuScreen
    {
        private IntPtr HWND_TOP = IntPtr.Zero;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SWP_SHOWWINDOW = 64;

        //This is unmangement code needed for real fullscreen. hidden taskbar etc.
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        //This is also unmangement code, for getting the real screen size with taskbar.
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);

        public MenuOptions(FrmMenu menu, MenuState state)
            : base(menu, state)
        {
            //todo
        }

        override public void ClearScreen()
        {
        }
        

    }
}
