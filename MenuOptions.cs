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
using System.Drawing;

namespace Frogger
{
    /// <summary>
    /// Options screen
    /// </summary>
    class MenuOptions : MenuScreen
    {
        private FrmMenu frmmenu;
        private BigCheckbox[] options;

        public MenuOptions(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;

            int margin = 20;

            //todo: get settings from windows registery.
            //RegistryKey.RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Frogger\\", true);
            //key.GetValue("fullscreen", null);
            bool sound = Frogger.Properties.Settings.Default.sound;
            bool fullscreen = Frogger.Properties.Settings.Default.fullscreen;

            options = new BigCheckbox[2];
            options[0] = new BigCheckbox("sound", sound);
            options[0].Location = new Point(frmmenu.Width / 2 - (options[0].Width / 2), frmmenu.Height / 2 - (options[0].Height / 2));
            options[0].Click += new EventHandler(ToggleSound);

            options[1] = new BigCheckbox("full screen", fullscreen);
            options[1].Location = new Point(frmmenu.Width / 2 - (options[1].Width / 2), frmmenu.Height / 2 - (options[1].Height / 2) + options[1].Height + margin);
            
            options[1].Click += new EventHandler(ToggleFullscreen);

            frmmenu.Controls.AddRange(options);
        }

        override public void ClearScreen()
        {
            foreach (BigCheckbox curchx in options)
            {
                curchx.Dispose();
            }
        }

        /// <summary>
        /// Toggle between fulscreen and not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFullscreen(object sender, EventArgs e)
        {
            Program.fullscreen = !Program.fullscreen;
            Frogger.Properties.Settings.Default.fullscreen = Program.fullscreen;
            Frogger.Properties.Settings.Default.Save();
            Program.CheckFullScreen(frmmenu);
            frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Toggle between sound on and off.
        /// </summary>
        public void ToggleSound(object sender, EventArgs e)
        {
            Program.sound = !Program.sound; //in memory, fast to access
            Frogger.Properties.Settings.Default.sound = Program.sound; //on disk, slow to access
            Frogger.Properties.Settings.Default.Save();
        }
    }
}
