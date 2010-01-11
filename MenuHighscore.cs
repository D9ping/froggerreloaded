using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
            HoverButton highscoresLevelEen = new HoverButton("Level 1");
            highscoresLevelEen.Click +=new EventHandler(hovbtn_Click);
            highscoresLevelEen.Location = new Point(300, 200);
            frmmenu.Controls.Add(highscoresLevelEen);
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

        void hovbtn_Click(object sender, EventArgs e)
        {

            string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = 1 ORDER BY SPEELTIJD DESC";
            // String query = "SELECT * FROM scoren ORDER BY Score DESC";
            OleDbDataReader reader = DBConnection.GetData(query);

            int positie = 1;
            int ypos = 50;
            while (reader.Read())
            {
                if (positie < 11)
                {
                    Label lbscore = new Label();
                    String tijddatum = Convert.ToString(reader["Tijddatum"]);
                    String naam = Convert.ToString(reader["Naam"]);
                    String speeltijd = Convert.ToString(reader["Speeltijd"]);
                    String level = Convert.ToString(reader["Level"]);
                    lbscore.Text = positie.ToString() + ".\t" + tijddatum + "\t" + naam + "\t" + speeltijd + "\t";
                    lbscore.AutoSize = true;
                    lbscore.ForeColor = Color.Lime;
                    lbscore.Location = new Point(300, ypos);
                    lbscore.TextAlign = ContentAlignment.MiddleCenter;
                    ypos = ypos + 40;
                    //this.gbxHighscoren.Controls.Add(lbscore);
                    
                }
                positie++;
            }


        }



		#endregion Methods 
    }
}
