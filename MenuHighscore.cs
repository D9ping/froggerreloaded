using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

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

        /// <summary>
        /// Maak highscore scherm leeg.
        /// </summary>
        override public void ClearScreen()
        {
            //todos
        }

        public void GetHighscores(int level)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creer een database connectie.
        /// </summary>
        /// <returns></returns>
        private SqlConnection CreateDBconnection()
        {
            return null;
        }

		#endregion Methods 
    }
}
