using System;
using System.Collections.Generic;
using System.Text;

namespace Frogger
{
    public class Frog : MovingObject
    {
        public const int jumpdistance = 20;

        /// <summary>
        /// create a frog.
        /// </summary>
        public Frog(int velocity, Direction dir)
            :base(velocity, dir)
        {
            this.pic = global::Frogger.Properties.Resources.kikker_west;
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
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    newposY =  this.Location.Y - jumpdistance;
                    if (newposY >= 0)
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.East:
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    newposX = this.Location.X - jumpdistance;
                    if (newposX >= 0)
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.West:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    newposX = this.Location.X + jumpdistance;
                    if (newposX < 2000) //todo
                    {
                        this.Location = new System.Drawing.Point(newposX, newposY);
                    }
                    break;
                case Direction.South:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    newposY =  this.Location.Y + jumpdistance;
                    if (newposY > 2000) //todo
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
