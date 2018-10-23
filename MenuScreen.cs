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

namespace Frogger
{
    using System;

    public abstract class MenuScreen
    {
        #region Fields (1)

        private FrmMenu frmmenu;

        #endregion Fields

        #region Constructors (1)

        public MenuScreen(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        public abstract void ClearScreen();

        #endregion Methods
    }
}
