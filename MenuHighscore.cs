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
            HoverButton hovbtn = new HoverButton("test highscore bvow");
            hovbtn.Click +=new EventHandler(hovbtn_Click);
            frmmenu.Controls.Add(hovbtn);
        }

        void hovbtn_Click(object sender, EventArgs e)
        {
            DateTime testdatum = new DateTime();
            testdatum.AddDays(11);
            testdatum.AddHours(14);
            testdatum.AddMinutes(20);
            testdatum.AddMonths(1);
            testdatum.AddYears(2010);


            DBConnection.VoegHighscoreToe(testdatum, "Gertjan", 20, 1);
        }

		#endregion Constructors 

		#region Methods (7) 

		// Public Methods (6) 

        /// <summary>
        /// Maak highscore scherm leeg.
        /// </summary>
        override public void ClearScreen()
        {
            //todos
        }

        public Boolean DeleteHighscore(int level, Niveau niveau)
        {
            throw new System.NotImplementedException();
        }

        public Boolean DeleteHighscoreAll()
        {
            throw new System.NotImplementedException();
        }

        public Boolean DeleteHighscoreOneLevel(int level)
        {
            throw new System.NotImplementedException();
        }

        public Boolean DeleteHighscoreOneNiveau(Niveau niveau)
        {
            throw new System.NotImplementedException();
        }

        public void GetHighscores(int level)
        {
            throw new System.NotImplementedException();
        }
		// Private Methods (1) 

		#endregion Methods 
    }
}
