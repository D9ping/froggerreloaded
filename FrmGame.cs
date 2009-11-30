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
        public FrmGame()
        {
            InitializeComponent();                        
        }

        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            int hoogteRiver = 100;
            int heightRoad = 100;
            int breedtegras = 100;
            int LineDistanse = 100;

            Graphics g = e.Graphics;

            SolidBrush brushRoad = new SolidBrush(Color.Black);
            SolidBrush brushRiver = new SolidBrush(Color.Blue);
            SolidBrush brushRoadLine = new SolidBrush(Color.White);
            
            //Rectangle rectStrook = new Rectangle(0, this.Height - hoogtestrook, this.Width, hoogtestrook);
            Rectangle rectWeg = new Rectangle(0, this.Height - heightRoad - breedtegras, this.Width, heightRoad);
            Rectangle rectRiver = new Rectangle(0, 80, this.Width, hoogteRiver);

            g.FillRectangle(brushRoad, rectWeg);
            g.FillRectangle(brushRiver, rectRiver);
            for (int xpos = 0; xpos < this.Height; xpos += LineDistanse)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, this.Height - heightRoad - breedtegras + (heightRoad / 2), 20, 5);
                g.FillRectangle(brushRoadLine, rectRoadLine);
            }

            
            


        }

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {            
            Application.Exit();
        }
    }
}
