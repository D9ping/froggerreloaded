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
        /// Creating a new instance of FrmGame.
        /// </summary>
        /// <param name="level">the level number, each numbers draws a other level.</param>
        /// <param name="niveau">the niveau enumaration. freeplay you won't go gameover.
        /// then there is easy, medium, hard and elite.</param>
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
        /// Shutdown the whole application. 
        /// Even if frmMenu was not visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// If Escape is pressed get the player back to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
            {
                this.frmmenu.Menustate = MenuState.main;
                this.frmmenu.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// Draw every aspect of the game.
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
        /// repaint the form, so thing show up correctly a the new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Update the game time, and check the game up for the current tier in
        /// if the time for the current tier is over then excute 
        /// the gameover methode from the GameEngine.
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
        /// Format the game time string so seconds are always displayed with two numbers,
        /// a lead zero if lower then 10seconds is added.
        /// And there is a : charcter between the minuts and seconds.
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
