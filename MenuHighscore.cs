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
        private HoverButton[] highscoremenuknoppen;
        private FrmMenu frmMenu = null;

		#region Constructors (1) 

        public MenuHighscore(FrmMenu frmmenu)
            :base(frmmenu)
        {    
            this.frmMenu = frmmenu;
            highscoremenuknoppen = new HoverButton[2];

            highscoremenuknoppen[0] = new HoverButton("Show highscores");
            highscoremenuknoppen[1] = new HoverButton("Delete highscores");
            // hook events
            highscoremenuknoppen[0].Click +=new EventHandler(ShowHighscores);
            highscoremenuknoppen[1].Click +=new EventHandler(DeleteHighscores);

            int ypos = 220;
            int xpos = 0;
            for (int i = 0; i < 2; i++)
            {
                xpos = frmmenu.Width / 2 - (highscoremenuknoppen[0].Width / 2);
                highscoremenuknoppen[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }
            frmmenu.ToonLogo = false;
            frmmenu.Controls.AddRange(highscoremenuknoppen);
        }

        public void ShowHighscores(object sender, EventArgs e)
        {
            highscoremenuknoppen = new HoverButton[3];

            highscoremenuknoppen[0] = new HoverButton("Level 1");
            highscoremenuknoppen[0].Tag = 0;
            highscoremenuknoppen[1] = new HoverButton("Level 2");
            highscoremenuknoppen[1].Tag = 1;
            highscoremenuknoppen[2] = new HoverButton("Level 3");
            highscoremenuknoppen[2].Tag = 2;
            // hook events
            highscoremenuknoppen[0].Click += new EventHandler(GetHighscores);
            highscoremenuknoppen[1].Click += new EventHandler(GetHighscores);
            highscoremenuknoppen[2].Click += new EventHandler(GetHighscores);

            int ypos = 220;
            int xpos = 0;
            for (int i = 0; i < 3; i++)
            {
                xpos = frmMenu.Width / 2 - (highscoremenuknoppen[0].Width / 2);
                highscoremenuknoppen[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }

            frmMenu.Controls.AddRange(highscoremenuknoppen);
        }

        void DeleteHighscores(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void highscoresLevelTwee_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

		#endregion Constructors 

		#region Methods (7) 

		// Public Methods (6) 

        /// <summary>
        /// Maak highscore scherm leeg.
        /// </summary>
        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in highscoremenuknoppen)
            {
                curbtn.Dispose();
            }
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

        /// <summary>
        /// Verkrijg highscore, (methode moet aan sjabooltje delegate EventHandler voldoen.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetHighscores(object sender, EventArgs e)
        {
            HoverButton btnclicked = (HoverButton)sender;

            string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = " + btnclicked.Tag.ToString() + " ORDER BY SPEELTIJD DESC";
            DataTable dt = DBConnection.ExecuteQuery(query, 4);

            string tijddatum = "";
            string naam = "";
            string speeltijd = "";

            int positie = 0;
            int ypos = 300; // ypos is de Y-coordinaat van de label van de highscore

            foreach (DataRow row in dt.Rows)
            {
                if (positie < 10)
                {
                    Label lbHighscore = new Label();
                    lbHighscore.Font = new Font("Flubber", 20);
                    tijddatum = row[0].ToString();
                    naam = row[1].ToString();
                    speeltijd = row[2].ToString();
                    lbHighscore.Text = positie.ToString() + ".     " + tijddatum + "     " + naam + "     " + speeltijd;
                    lbHighscore.AutoSize = true;
                    lbHighscore.ForeColor = Color.Lime;
                    lbHighscore.Location = new Point(175, ypos);
                    lbHighscore.TextAlign = ContentAlignment.MiddleCenter;
                    ypos = ypos + 40;
                    frmMenu.Controls.Add(lbHighscore);
                    positie++;
                }
            }
        }
		// Private Methods (1)

#endregion Methods 
    }
}
