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
    /// <summary>
    /// A tree trunk obj.
    /// </summary>
    class Tree : MovingObject
    {
        /// <summary>
        /// Creating a new instance of a new tree obj.
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="direction"></param>
        public Tree(int velocity, Direction direction, int width, int height)
            :base(velocity, direction)
        {
            this.Size = new Size(width, height);
            base.Pic = ResizesResources.images["treetrunk"];

            //int i1 = base.pic.Width;
            //int i2 = base.pic.Height;
        }
    }
}
