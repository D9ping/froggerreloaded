using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    public abstract class MenuScreen
    {
        private FrmMenu menu;
        private MenuState menustate;

        public MenuScreen(FrmMenu menu, MenuState menustate)
        {
            this.menu = menu;
            this.menustate = menustate;
        }

        public abstract void ClearScreen();
    }
}
