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
using System.IO;

namespace Frogger
{
    public class Frog : MovingObject
    {
		#region Fields (3) 

        private int jumpdistance, maxscreenwidth, maxscreenheight;
        private Boolean canmove = true;
        private const int offscreenmargin = 20;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// create a frog.
        /// </summary>
        public Frog(int velocity, Direction dir, int jumpdistance, int width, int height, Form frmgame)
            : base(velocity, dir)
        {
            //Make transparant
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            this.Size = new Size(width, height);
            this.pic = global::Frogger.Properties.Resources.kikker_west;
            this.pic.MakeTransparent();
            if (!Program.fullscreen)
            {
                this.maxscreenwidth = frmgame.ClientSize.Width - offscreenmargin;
                this.maxscreenheight = frmgame.ClientSize.Height - offscreenmargin;
            }
            else if (Program.fullscreen)
            {
                this.maxscreenwidth = Screen.PrimaryScreen.WorkingArea.Width - offscreenmargin;
                this.maxscreenheight = Screen.PrimaryScreen.WorkingArea.Height - offscreenmargin;
            }

            this.jumpdistance = jumpdistance;
        }

		#endregion Constructors 

		#region Properties (4) 

        public Boolean CanMove
        {
            get
            {
                return this.canmove;
            }
            set
            {
                this.canmove = value;
            }
        }

        public Boolean OnTree { set; get; }

        public Direction TreeDir { set; get; }

        public int TreeVelocity { set; get; }

		#endregion Properties 

		#region Methods (1) 

		// Public Methods (1) 

        /// <summary>
        /// Move the frog (with constant jumpdistance pixels)
        /// </summary>
        /// <param name="dir"></param>
        public void Jump(Direction dir)
        {
            if (this.canmove)
            {
                int newposY = this.Location.Y;
                int newposX = this.Location.X;
                switch (dir)
                {
                    case Direction.North:
                        this.pic = ResizesResources.images["kikker_west"]; //global::Frogger.Properties.Resources.kikker_west;
                        newposY = this.Location.Y - jumpdistance;
                        if (newposY + this.Height >= 0)
                        {
                            this.Location = new System.Drawing.Point(newposX, newposY);
                            if (this.Location.Y <= this.Height)
                            {
                                string soundmadeit = Application.StartupPath + "\\sounds\\frog_made_it.wav";
                                if (File.Exists(soundmadeit))
                                {
                                    GameEngine.sndPlaySound(soundmadeit, 1);
                                }
                                
                            }
                        }
                        break;
                    case Direction.East:
                        this.pic = ResizesResources.images["kikker_west"];
                        newposX = this.Location.X - jumpdistance;
                        if (newposX >= 0)
                        {
                            this.Location = new System.Drawing.Point(newposX, newposY);
                        }
                        break;
                    case Direction.West:
                        this.pic = ResizesResources.images["kikker_east"]; //global::Frogger.Properties.Resources.kikker_east;
                        newposX = this.Location.X + jumpdistance;
                        if (newposX < maxscreenwidth)
                        {
                            this.Location = new System.Drawing.Point(newposX, newposY);
                        }
                        break;
                    case Direction.South:
                        this.pic = ResizesResources.images["kikker_east"]; //global::Frogger.Properties.Resources.kikker_east;
                        newposY = this.Location.Y + jumpdistance;
                        if (newposY < maxscreenheight) //todo
                        {
                            this.Location = new System.Drawing.Point(newposX, newposY);
                        }
                        break;
                    default:
                        throw new Exception("direction unknow.");
                }
                canmove = false;
            }

        }

		#endregion Methods 
    }
}
