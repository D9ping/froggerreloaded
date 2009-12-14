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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Frogger
{
    public class MovingObject : UserControl
    {    
        private int velocity = 0;
        private Direction direction;
        private PictureBox pbObject;

        /// <summary>
        /// Creating a new instance of a movingobj.
        /// </summary>
        public MovingObject(int velocity, Direction direction)
        {
            this.velocity = velocity;
            this.direction = direction;
            InitializeComponent();
        }

        public Direction Dir
        {
            get
            {
                return this.direction;
            }
        }

        public int Velocity
        {
            get
            {
                return this.velocity;
            }
        }

        /// <summary>
        /// De texture.
        /// </summary>
        public Bitmap pic
        {
            set
            {
                this.pbObject.Image = value;
            }
        }

        /// <summary>
        /// Maak control even groot als picturebox
        /// </summary>
        public void SetSize()
        {
            this.Size = pbObject.Size;
        }



        private void InitializeComponent()
        {
            this.pbObject = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbObject)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pbObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbObject.Location = new System.Drawing.Point(0, 0);
            this.pbObject.Name = "pbObject";
            this.pbObject.Size = new System.Drawing.Size(116, 62);
            this.pbObject.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbObject.TabIndex = 0;
            this.pbObject.TabStop = false;
            this.pbObject.BackColor = Color.Transparent;
            // 
            // MovingObject
            // 
            this.Controls.Add(this.pbObject);
            this.Name = "MovingObject";
            this.Size = new System.Drawing.Size(116, 62);
            ((System.ComponentModel.ISupportInitialize)(this.pbObject)).EndInit();
            this.ResumeLayout(false);

        }
  

    }
}
