/*
Copyright (C) 2009-2010  Tom Postma, Gertjan Buijs

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
    class Car : MovingObject
    {
        private int carcolor;
        private bool istruck = false;

        /// <summary>
        /// Creating a new instance of a car.
        /// </summary>
        public Car(int carcolor, int velocity, int pos, Direction direction, int width, int height) 
            :base(velocity, direction, pos)
        {
            this.carcolor = carcolor;

            this.Size = new Size(width, height);
            switch (direction)
            {
                case Direction.East:
                    base.Pic = CreateCarEast(carcolor);
                    break;
                case Direction.West:
                    base.Pic = CreateCarWest(carcolor);
                    break;
            }
        }

        /// <summary>
        /// Color number of the car
        /// 1 grey car
        /// 2 yellow car
        /// </summary>
        public int Color
        {
            get
            {
                return this.carcolor;
            }
        }

        public bool IsTruck { 
            get 
            {
                return this.istruck;
            }
        }

        private Bitmap CreateCarEast(int carcolor)
        {
            switch (carcolor)
            {
                case 1:
                    return ResizesResources.images["car_grey_east"]; //Frogger.Properties.Resources.car_grey_east;
                case 2:
                    return ResizesResources.images["car_yellow_east"]; //Frogger.Properties.Resources.car_yellow_east;
                case 3:
                    return ResizesResources.images["car_green_east"]; //Frogger.Properties.Resources.car_green_east;
                case 4:
                    istruck = true;
                    return ResizesResources.images["truck_east"]; //Frogger.Properties.Resources.truck_east;
                default:
                    ThrowCarColorNotFoundExc();
                    return null;
            }
        }

        private Bitmap CreateCarWest(int carcolor)
        {
            switch (carcolor)
            {
                case 1:
                    return ResizesResources.images["car_grey_west"]; //Frogger.Properties.Resources.car_grey_west;
                case 2:
                    return ResizesResources.images["car_yellow_west"]; //Frogger.Properties.Resources.car_yellow_west;
                case 3:
                    return ResizesResources.images["car_green_west"]; //Frogger.Properties.Resources.car_green_west;
                case 4:
                    istruck = true;
                    return ResizesResources.images["truck_west"];//Frogger.Properties.Resources.truck_west;
                default:
                    ThrowCarColorNotFoundExc();
                    return null;
            }
        }


        private void ThrowCarColorNotFoundExc()
        {
            throw new Exception("car color not found");
        }
    }
}
