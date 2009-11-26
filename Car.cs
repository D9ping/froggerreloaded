using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    class Car : MovingObject
    {
        /// <summary>
        /// Creating a new instance of a car.
        /// </summary>
        public Car(int carcolor, int speed, Direction direction) :base(speed, direction)
        {
        }        
            
    }
}
