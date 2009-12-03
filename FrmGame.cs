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

namespace Frogger
{
    public partial class FrmGame : Form
    {
		#region Fields (1) 

        private int level = 1;
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
            this.level = level;
            this.game = new GameEngine();
        }

		#endregion Constructors 

        public FrmGame()
        {
            throw new System.NotImplementedException();
        }

		#region Methods (3) 

		// Private Methods (3) 

        /// <summary>
        /// Teken een weg.        
        /// </summary>
        /// <param name="g"></param>
        /// <param name="locy">de locatie van Y cooridinaat van het venster.</param>
        private void DrawRoad(Graphics g, int locy)
        {
            int lineDistance = 100, heightRoad = 100;

            SolidBrush brushRoad = new SolidBrush(Color.Black);
            SolidBrush brushRoadLine = new SolidBrush(Color.White);
            Rectangle rectWeg = new Rectangle(0, locy, this.Width, heightRoad);

            g.FillRectangle(brushRoad, rectWeg);
            for (int xpos = 0; xpos < this.Height; xpos += lineDistance)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, locy + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            } 
        }

        private void DrawRivir(Graphics g, int locy)
        {
            int hoogteRiver = 100;

            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            Rectangle rectRiver = new Rectangle(0, locy, this.Width, hoogteRiver);
            g.FillRectangle(brushRiver, rectRiver);
        }

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

            switch (level)
            {
                case 1:
                    DrawRivir(g, 80);
                    DrawRoad(g, 250);                    
                    DrawRoad(g, 405);
                    break;
                case 2:
                    DrawRoad(g, 150);
                    DrawRoad(g, 300);
                    DrawRivir(g, 405);
                    break;
            }
                              
        }

		#endregion Methods 

        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }
     }
}
