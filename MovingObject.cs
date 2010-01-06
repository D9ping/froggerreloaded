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
using System.Windows.Forms;
using System.Drawing;

namespace Frogger
{
    public class MovingObject : UserControl
    {    
        private int velocity = 0;
        private Direction direction;
        //private PictureBox pbObject;

        /// <summary>
        /// Creating a new instance of a movingobj.
        /// </summary>
        public MovingObject(int velocity, Direction direction)
        {
            this.velocity = velocity;
            this.direction = direction;
            //Add transparancy support
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //Make transparant
            this.BackColor = Color.Transparent;
            //this.BackColor = Color.Red;
            this.DoubleBuffered = true;
            this.BringToFront();
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
        public Bitmap pic { get; set; }


        /// <summary>
        /// cleanup memory
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// Maak control even groot als bitmap
        /// </summary>
        public void SetSize()
        {
            this.Size = pic.Size;
        }
        

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MovingObject
            // 
            this.DoubleBuffered = true;
            this.Name = "MovingObject";
            this.Size = new System.Drawing.Size(112, 66);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MovingObject_Paint);
            this.ResumeLayout(false);

        }

        private void MovingObject_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
            //image scaling is cpu costly, so DrawImageUnscaled is used instead.
            //prescale image when it is first draw, then get the scaled image out of memory.
            g.DrawImageUnscaled((Image)this.pic, rect);
        }

    }
}
