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

namespace Frogger
{
    public class Frog : MovingObject
    {
        private int jumpdistance;

        /// <summary>
        /// create a frog.
        /// </summary>
        public Frog(int velocity, Direction dir, int jumpdistance)
            :base(velocity, dir)
        {
            //Make transparant
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            this.pic = global::Frogger.Properties.Resources.kikker_west;

            this.jumpdistance = jumpdistance;
        }

        /// <summary>
        /// Move the frog (with constant jumpdistance pixels)
        /// </summary>
        /// <param name="dir"></param>
        public void Jump(Direction dir)
        {
            int newposY = this.Location.Y;
            int newposX = this.Location.X;

            switch (dir)
            {
                case Direction.North:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    newposY =  this.Location.Y - jumpdistance;
                    if (newposY >= 0)
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.East:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    newposX = this.Location.X - jumpdistance;
                    if (newposX >= 0)
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.West:
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    newposX = this.Location.X + jumpdistance;
                    if (newposX < 2000) //todo
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.South:
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    newposY =  this.Location.Y + jumpdistance;
                    if (newposY < 2000) //todo
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                default:
                    throw new Exception("direction unknow.");
            }
        }
    }
}
