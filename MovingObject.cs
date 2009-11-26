using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{

    public class MovingObject
    {
        public enum Direction
        {
            East,
            West
        }

        private int speed;
        private Direction direction;

        /// <summary>
        /// Creating a new instance of a movingobj.
        /// </summary>
        public MovingObject(int speed, Direction direction)
        {
            this.speed = speed;
            this.direction = direction;
        }        

    }
}
