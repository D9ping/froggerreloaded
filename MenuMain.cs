using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    public class MenuMain : MenuScreen
    {
		#region Constructors (1) 

        public MenuMain(FrmMenu menu, MenuState state)
            : base(menu, state)
        {

        }

        override public void ClearScreen()
        {
        }

		#endregion Constructors 
    }
}
