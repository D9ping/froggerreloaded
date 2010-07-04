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
#define windows //platform

using System;
namespace Frogger
{
	using System.Collections.Generic;
	using System.Windows.Forms;
	using System.Runtime.InteropServices;
	using System.Drawing;
	using System.IO;
	
    public static class ResizesResources
    {
        //een soort lijst van geresized images, scheelt cpu power!
        static public Dictionary<String, Bitmap> images;
    }

    static class Program
    {
        //global settings
        public static bool fullscreen = false;
        public static bool sound = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fullscreen = Frogger.Properties.Settings.Default.fullscreen;
            sound = Frogger.Properties.Settings.Default.sound;
            if (!CheckFontInstalled())
            {
                MessageBox.Show("The font for this game does not exist. Try to reinstall the game.", "font missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Run(new FrmMenu());
        }

        /// <summary>
        /// Toggle between normal size and fullscreen size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void CheckFullScreen(Form curfrm)
        {
            if (Program.fullscreen)
            {
                curfrm.WindowState = FormWindowState.Maximized;
                curfrm.FormBorderStyle = FormBorderStyle.None;
#if Release
                this.TopMost = true; //watch out this is actually annoying while debugging, switching back to your IDE with fullscreen will be impossible.
#endif
                SetWindowPos(curfrm.Handle, IntPtr.Zero, 0, 0, GetSystemMetrics(0), GetSystemMetrics(1), 64);
            }
            else
            {
                curfrm.WindowState = FormWindowState.Normal;
                curfrm.FormBorderStyle = FormBorderStyle.Sizable;
                curfrm.TopMost = false;
            }
        }

        /// <summary>
        /// Check if the Flubber font exist in the windows font directory.
        /// </summary>
        /// <returns>true if it exist or font directory could not be found. false if font does not exist in the font direcotory.</returns>
        static public bool CheckFontInstalled()
        {
#if windows
            string fontdir = Path.Combine(System.Environment.GetEnvironmentVariable("windir"), "fonts");
#elif linux
            string fontdir = "/usr/share/fonts????";
#endif
            if (Directory.Exists(fontdir))
            {
                if (File.Exists(Path.Combine(fontdir,"Flubber.ttf")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //cannot find fonts directory ingore error for now.  Maybe we are not running windows.
                return true;
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
