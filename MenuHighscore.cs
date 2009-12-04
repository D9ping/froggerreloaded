using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    /// <summary>
    /// todo
    /// </summary>
    class MenuHighscore : MenuScreen
    {
        public MenuHighscore(FrmMenu frmmenu, MenuState state)
            :base(frmmenu, state)
        {
            state = MenuState.highscore;
            //todo
        }

        public void GetHighscores(int level)
        {
            throw new System.NotImplementedException();
        }

        override public void ClearScreen()
        {
        }
    }
}
