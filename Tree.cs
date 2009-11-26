using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    /// <summary>
    /// 
    /// </summary>
    class Tree : MovingObject
    {
        /// <summary>
        /// Creating a new instance of a new tree obj.
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public Tree(int speed, Direction direction)
            :base(speed, direction)
        {
        }
    }
}
