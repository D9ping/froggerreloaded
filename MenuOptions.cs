/*
Copyright (C) 2009  Tom Postma, Gertjan Buijs

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// Options screen
    /// </summary>
    class MenuOptions : MenuScreen
    {
        private FrmMenu frmmenu;
        private BigCheckbox[] options;        

        private IntPtr HWND_TOP = IntPtr.Zero;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SWP_SHOWWINDOW = 64;

        public MenuOptions(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;

            int margin = 20;

            options = new BigCheckbox[2];
            
            options[0] = new BigCheckbox("sound", true);
            options[0].Location = new Point(frmmenu.Width / 2 - (options[0].Width / 2), frmmenu.Height / 2 - (options[0].Height / 2));
            
            options[1] = new BigCheckbox("full screen", false);
            options[1].Location = new Point(frmmenu.Width / 2 - (options[1].Width / 2), frmmenu.Height / 2 - (options[1].Height / 2) + options[1].Height + margin);
            
            options[1].Click += new EventHandler(ToggleFullScreen);

            //todo: get settings from windows registery.
            //RegistryKey.RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Frogger\\", true);
            //key.GetValue("fullscreen", null);

            frmmenu.Controls.AddRange(options);
        }

        override public void ClearScreen()
        {
        }

        /// <summary>
        /// Toggle between normal size and fullscreen size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToggleFullScreen(object sender, EventArgs e)
        {
            Program.fullscreen = !Program.fullscreen;

            if (Program.fullscreen)
            {
                frmmenu.WindowState = FormWindowState.Maximized;
                frmmenu.FormBorderStyle = FormBorderStyle.None;
#if Release                 
                this.TopMost = true; //watch out this is actually annoying while debugging, switching back to your IDE with fullscreen will be inpossible.
#endif
                SetWindowPos(frmmenu.Handle, HWND_TOP, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), SWP_SHOWWINDOW);
            }
            else
            {
                frmmenu.WindowState = FormWindowState.Normal;
                frmmenu.FormBorderStyle = FormBorderStyle.Sizable;
                frmmenu.TopMost = false;
            }
        }

        //This is unmangement code needed for real fullscreen. hidden taskbar etc.
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        //This is also unmangement code, for getting the real screen size with taskbar.
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);
    }
}
