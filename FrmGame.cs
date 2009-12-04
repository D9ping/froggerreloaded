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
		#region Fields (1) 

        private GameEngine game;        
		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating a new instance of FrmGame.
        /// </summary>
        /// <param name="level"></param>
        public FrmGame(int level)
        {
            InitializeComponent();
            this.game = new GameEngine(level);
        }

		#endregion Constructors 
        
		#region Methods (3) 

		// Private Methods (3) 

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {            
            Application.Exit();
        }

        /// <summary>
        /// Draw everything of the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.DrawScreen(g);                              
        }		 

        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }
        
        private void timerUpdateGame_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion Methods
    }
}
