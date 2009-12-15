using System;
using System.Collections.Generic;
using System.Text;

namespace Frogger
{
    public class Frog : MovingObject
    {
        /// <summary>
        /// create a frog.
        /// </summary>
        public Frog(int velocity, Direction dir)
            :base(velocity, dir)
        {
            switch (dir)
            {
                case Direction.North:
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    break;
                case Direction.East:
                    this.pic = global::Frogger.Properties.Resources.kikker_east;
                    break;
                case Direction.West:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    break;
                case Direction.South:
                    this.pic = global::Frogger.Properties.Resources.kikker_west;
                    break;
                default:
                    break;
            }
        }

        public Boolean Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
