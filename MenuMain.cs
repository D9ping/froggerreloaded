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
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// Onzeker of deze klasse er zo bij moet.
    /// </summary>
    public class MenuMain : MenuScreen
    {
		#region Fields (1) 

        private HoverButton[] hoofdmenuknoppen;
        private FrmMenu frmmenu;

		#endregion Fields 

		#region Constructors (1) 

        public MenuMain(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            hoofdmenuknoppen = new HoverButton[5];

            hoofdmenuknoppen[0] = new HoverButton("Newgame");
            hoofdmenuknoppen[1] = new HoverButton("Highscores");
            hoofdmenuknoppen[2] = new HoverButton("Options");
            hoofdmenuknoppen[3] = new HoverButton("Credits");
            hoofdmenuknoppen[4] = new HoverButton("Exit");
            //hook events  
            hoofdmenuknoppen[0].Click += new EventHandler(CreateLevelMenu);
            hoofdmenuknoppen[1].Click += new EventHandler(CreateHighScore);
            hoofdmenuknoppen[2].Click += new EventHandler(CreateOptions);
            hoofdmenuknoppen[3].Click += new EventHandler(CreateCredits);
            hoofdmenuknoppen[4].Click += new EventHandler(Shutdown);


            int ypos = 200;
            int xpos = 0;
            for (int i = 0; i < 5; i++)
            {
                xpos = frmmenu.Width / 2 - (hoofdmenuknoppen[0].Width / 2);
                hoofdmenuknoppen[i].Location = new Point(xpos, ypos);
                ypos += 75;
            }
            frmmenu.ToonLogo = true;
            frmmenu.ShowTierChoice = false;

            frmmenu.KikkerPic = Frogger.Properties.Resources.kikker_west;
            frmmenu.Controls.AddRange(hoofdmenuknoppen);
			
			this.frmmenu.MenuUpdated = true;
			//frmmenu.Refresh();
        }


		#endregion Constructors 

		#region Methods (1) 

		// Public Methods (1) 

        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in hoofdmenuknoppen)
            {
                curbtn.Dispose();
            }
        }

        /// <summary>
        /// Create main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateMainMenu(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.main;
            this.frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create level selection screen.
        /// </summary>
        private void CreateLevelMenu(object sender, EventArgs e)
        {
            frmmenu.KikkerPic = Frogger.Properties.Resources.kikker_crazy;
            frmmenu.Menustate = MenuState.level;
			this.frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create highscore menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateHighScore(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.highscore;
			this.frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create options menu
        /// </summary>
        private void CreateOptions(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.options;
			this.frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create credits menu
        /// </summary>
        private void CreateCredits(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.credits;
			this.frmmenu.MenuUpdated = true;
            frmmenu.Refresh();
        }

        /// <summary>
        /// exit button is presed, shutdown application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shutdown(object sender, EventArgs e)
        {
            Application.Exit();
        }

		#endregion Methods 
    }
}
