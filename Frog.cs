using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    public class Frog : MovingObject
    {
        /// <summary>
        /// create a frog.
        /// </summary>
        public Frog(int speed, Direction dir)
            :base(speed, dir)
        {
        }

        public Boolean Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
