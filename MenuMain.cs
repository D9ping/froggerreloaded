/*
Copyright (C) 2009-2010  Tom Postma

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

namespace Frogger
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Onzeker of deze klasse er zo bij moet.
    /// </summary>
    public class MenuMain : MenuScreen
    {
        #region Fields (4)

        private HoverButton[] hoofdmenuknoppen;
        private FrmMenu frmmenu;
        private FrmLevelEditor lvleditor;
        private const int marginbuttonlogo = 10, marginbetweenhovbtns = 5;
        
        #endregion Fields

        #region Constructors (1)

        public MenuMain(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            this.frmmenu.BackColor = Color.Orange;

            hoofdmenuknoppen = new HoverButton[6];

            hoofdmenuknoppen[0] = new HoverButton("Newgame");
            hoofdmenuknoppen[1] = new HoverButton("Highscores");
            hoofdmenuknoppen[2] = new HoverButton("Options");
            //hoofdmenuknoppen[3] = new HoverButton("Credits");
            hoofdmenuknoppen[3] = new HoverButton("Level Editor");
            hoofdmenuknoppen[3].HoverbuttonSizeText = 34;
            hoofdmenuknoppen[4] = new HoverButton("Exit");
            hoofdmenuknoppen[0].Click += new EventHandler(CreateLevelMenu);
            hoofdmenuknoppen[1].Click += new EventHandler(CreateHighScore);
            hoofdmenuknoppen[2].Click += new EventHandler(CreateOptions);
            //hoofdmenuknoppen[3].Click += new EventHandler(CreateCredits);
            hoofdmenuknoppen[3].Click += new EventHandler(LaunchLevelEditor);
            hoofdmenuknoppen[4].Click += new EventHandler(Shutdown);
            int ypos = frmmenu.LogoPosBottom + marginbuttonlogo;
            int xpos = 0;
            for (int i = 0; i < 5; i++)
            {
                xpos = frmmenu.Width / 2 - (hoofdmenuknoppen[0].Width / 2);
                hoofdmenuknoppen[i].Location = new Point(xpos, ypos);
                ypos += hoofdmenuknoppen[1].Height + marginbetweenhovbtns;
            }
            frmmenu.Controls.AddRange(hoofdmenuknoppen);

            frmmenu.ToonLogo = true;
            frmmenu.ShowTierChoice = false;

            frmmenu.KikkerPic = Frogger.Properties.Resources.kikker_west;
        }

        #endregion Constructors

        #region Methods (8)

        // Public Methods (1) 

        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in hoofdmenuknoppen)
            {
                if (curbtn != null)
                {
                    curbtn.Dispose();
                }
            }
        }

        // Private Methods (6) 

        /// <summary>
        /// Create level selection screen.
        /// </summary>
        private void CreateLevelMenu(object sender, EventArgs e)
        {
            frmmenu.KikkerPic = Frogger.Properties.Resources.kikker_crazy;
            frmmenu.Menustate = MenuState.level;
            this.frmmenu.MenuUpdated = true;
            this.frmmenu.BackColor = Color.DarkGray;
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
            this.frmmenu.BackColor = Color.Yellow;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create options menu
        /// </summary>
        private void CreateOptions(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.options;
            this.frmmenu.MenuUpdated = true;
            this.frmmenu.BackColor = Color.Blue;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create credits menu
        /// </summary>
        private void CreateCredits(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.credits;
            this.frmmenu.MenuUpdated = true;
            this.frmmenu.BackColor = Color.Purple;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Launch the level editor screen.
        /// </summary>
        private void LaunchLevelEditor(object sender, EventArgs e)
        {
            lvleditor = new FrmLevelEditor(frmmenu);
            lvleditor.Show();
            this.frmmenu.Hide();
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
