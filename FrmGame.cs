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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace Frogger
{
    public partial class FrmGame : Form
    {
		#region Fields (4) 

        private FrmMenu frmmenu;
        private GameEngine game;
        private int min = 0;
        private int sec = 0;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creates a new instance of FrmGame.
        /// The FrmMenu is being stored in the FrmGame, for use in the FrmGame.
        /// FrmGame automatically creates a new GameEngine.
        /// The selected level and tier are called by the GameEngine, so it is able to use the variables.
        /// FrmGame automatically checks whether it should be drawn full screen.
        /// </summary>
        /// <param name="level">The number of the level that the game will start.</param>
        /// <param name="tier">The tier that has to be used for the game.</param>
        public FrmGame(FrmMenu frmmenu, int level, Niveau tier)
        {
            InitializeComponent();
            this.frmmenu = frmmenu;
            this.game = new GameEngine(level, this, tier);
            Program.CheckFullScreen(this);
        }

		#endregion Constructors 

		#region Methods (6) 

		// Private Methods (6) 

        /// <summary>
        /// Closes the FrmGame and shows the main menu.
        /// </summary>
        public void CloseGame()
        {
            this.frmmenu.Menustate = MenuState.main;
            this.frmmenu.Show();
            this.Close();
        }

        /// <summary>
        /// The player will be sent back to the main menu, whenever the Escape button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
            {
                this.frmmenu.Menustate = MenuState.main;
                this.frmmenu.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Draws every aspect of the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.DrawLevel(g);
            UpdateGameTime();
            lbTime.Text = UpdateGameTime();
        }

        /// <summary>
        /// Repaints FrmGame. When fullscreen is toggled, frmGame will show up correctly according to the new size.        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Updates the gametime. Every second, the gametime will decrease with 1 second.
        /// When the gametime reaches zero, the player will be shown the Game Over screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void timerUpdateGame_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec > 59)
            {
                min++;
                game.CheckGameTime(min);
                sec = 0;
            }
        }

        /// <summary>
        /// Closes the current GameEngine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseGame();
        }

        /// <summary>
        /// Formats the game time string, so that seconds are always displayed as two numbers.
        /// A zero is added in front, if the amount of seconds is lower than 10.
        /// A ":" character will be added between the amount of minutes and the amount of seconds.        
        /// </summary>
        /// <param name="g"></param>
        private String UpdateGameTime()
        {
            String time = this.min.ToString() + ":";
            if (this.sec < 10) { time += "0" + this.sec.ToString(); }
            else { time += this.sec.ToString(); }
            return time;
        }

		#endregion Methods 
    }
}
