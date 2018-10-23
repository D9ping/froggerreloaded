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
        public static Dictionary<String, Bitmap> images;
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
            string fontdir = "/usr/share/fonts/truetype/freefont/";
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

        /// <summary>
        /// Get the folder with the sound files.
        /// </summary>
        /// <returns></returns>
        static public string GetSoundDir()
        {
            const string SOUNDFOLDER = "sounds";
            string sounddir = Path.Combine(Application.StartupPath, SOUNDFOLDER);
            if (Directory.Exists(sounddir))
            {
                return sounddir;
            }
            else
            {
                throw new Exception("Cannot find Sound directory.");
            }

        }

        /// <summary>
        /// Get the folder with the levels
        /// </summary>
        /// <returns></returns>
        public static string GetLevelFolder()
        {
            const string LEVELFOLDERNAME = "levels";
            string lvldir = Path.Combine(GetAppDataFolder(), LEVELFOLDERNAME);

            if (!Directory.Exists(lvldir))
            {
                Directory.CreateDirectory(lvldir);

                ExtractFile(lvldir, "basic1.lvl", Frogger.Properties.Resources.lvl_basic1);
                ExtractFile(lvldir, "basic2.lvl", Frogger.Properties.Resources.lvl_basic2);
                ExtractFile(lvldir, "basic3.lvl", Frogger.Properties.Resources.lvl_basic3);
                ExtractFile(lvldir, "coolroad.lvl", Frogger.Properties.Resources.lvl_coolroad);
                ExtractFile(lvldir, "highway.lvl", Frogger.Properties.Resources.lvl_highway);
                ExtractFile(lvldir, "sea.lvl", Frogger.Properties.Resources.lvl_sea);
            }

            return lvldir;
        }

        /// <summary>
        /// Extract level file
        /// </summary>
        /// <param name="lvldir"></param>
        /// <param name="filename"></param>
        /// <param name="lvlresources"></param>
        private static void ExtractFile(string folder, string filename, byte[] resource)
        {
            string filepath =  Path.Combine(folder, filename);
            if (!File.Exists(filepath))
            {
                System.IO.FileStream writer = null;
                try
                {
                    writer = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    writer.Write(resource, 0, resource.Length);                    
                    writer.Flush();
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the application data folder of froggerreloaded.
        /// </summary>
        /// <returns></returns>
        static public string GetAppDataFolder()
        {
            const string APPDATAFOLDERNAME = "froggerreloaded";
#if windows
            // todo
            string appdatadir = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), APPDATAFOLDERNAME);
            if (!String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("ProgramData")))
            {
                appdatadir = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), APPDATAFOLDERNAME);
            }            
#elif linux
            string lvldir = Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), "."+APPDATAFOLDERNAME);;
#endif
            if (!Directory.Exists(appdatadir))
            {
                try
                {
                    Directory.CreateDirectory(appdatadir);

                    ExtractFile(appdatadir, "highscores.mdb", Frogger.Properties.Resources.highscores);
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
            
            return appdatadir;
        }

        //This is unmangement code needed for real fullscreen. hidden taskbar etc.
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        //This is also unmangement code, for getting the real screen size with taskbar.
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);
    }
}
