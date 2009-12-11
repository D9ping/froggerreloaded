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
        public Frog(int velocity, Direction dir)
            :base(velocity, dir)
        {
        }

        public Boolean Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
