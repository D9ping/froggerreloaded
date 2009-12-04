using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frogger
{
    /// <summary>
    /// Highscores screen
    /// </summary>
    class MenuHighscore : MenuScreen
    {
		#region Constructors (1) 

        public MenuHighscore(FrmMenu frmmenu)
            :base(frmmenu)
        {            
            //todo
        }

		#endregion Constructors 

		#region Methods (2) 

		// Public Methods (2) 

        override public void ClearScreen()
        {
        }

        public void GetHighscores(int level)
        {
            throw new System.NotImplementedException();
        }

		#endregion Methods 
    }
}
