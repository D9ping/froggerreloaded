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
        private HoverButton[] highscoremenubtn;
        private FrmMenu frmmenu = null;
        private Label[] entries;

		#region Constructors (1) 

        public MenuHighscore(FrmMenu frmmenu)
            :base(frmmenu)
        {    
            this.frmmenu = frmmenu;
            frmmenu.ToonLogo = false;

            highscoremenubtn = new HoverButton[4];
            highscoremenubtn[0] = new HoverButton("Level 1");
            highscoremenubtn[0].Tag = 1;
            highscoremenubtn[0].Click += new EventHandler(GetHighscores);
            
            highscoremenubtn[1] = new HoverButton("Level 2");
            highscoremenubtn[1].Tag = 2;
            highscoremenubtn[1].Click += new EventHandler(GetHighscores);
            
            highscoremenubtn[2] = new HoverButton("Level 3");
            highscoremenubtn[2].Tag = 3;
            highscoremenubtn[2].Click += new EventHandler(GetHighscores);
            
            highscoremenubtn[3] = new HoverButton("Delete all");
            highscoremenubtn[3].Click += new EventHandler(DeleteHighscoreAll);
            int ypos = 80;
            int xpos = 20; //frmmenu.Width / 2 - (highscoremenubtn[0].Width / 2);
            for (int i = 0; i < highscoremenubtn.Length; i++)
            {
                highscoremenubtn[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }

            frmmenu.Controls.AddRange(highscoremenubtn);
        }

		#endregion Constructors 

		#region Methods (7) 

		// Public Methods (6) 

        /// <summary>
        /// Maak highscore scherm leeg.
        /// </summary>
        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in highscoremenubtn)
            {
                if (curbtn != null) { curbtn.Dispose(); }
            }
            if (entries != null)
            {
                foreach (Label curlbl in entries)
                {
                    if (curlbl != null)
                    {
                        curlbl.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Delete all highscore entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteHighscoreAll(Object sender, EventArgs e)
        {
            DialogResult dlgres = MessageBox.Show("Are you sure you want to clear all highscores?", "sure?", MessageBoxButtons.YesNo);
            if (dlgres == DialogResult.Yes)
            {
                DBConnection.SetData("DELETE * FROM HIGHSCORES");
                MessageBox.Show("Highscore table is now empty.");
            }
        }

        /*
         * Latere features nu niet zo belangrijk.
         * 
         
        /// <summary>
        /// Delete highscore from particaler level
        /// </summary>
        public Boolean DeleteHighscoreOneLevel(int level)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete highscore from particaler tier
        /// </summary>
        public Boolean DeleteHighscoreOneNiveau(Tier tier)
        {
            throw new System.NotImplementedException();
        }
        
        */

        /// <summary>
        /// Verkrijg highscore, (methode moet aan sjabooltje delegate EventHandler voldoen.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetHighscores(object sender, EventArgs e)
        {
            if (entries != null)
            {
                foreach (Label curlbl in entries)
                {
                    if (curlbl != null)
                    {
                        curlbl.Text = "";
                        curlbl.Width = 0;
                    }
                }
            }

            HoverButton btnclicked = (HoverButton)sender;
            
            string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = " + btnclicked.Tag.ToString() + " ORDER BY SPEELTIJD ASC";
            DataTable dt = DBConnection.ExecuteQuery(query, 4);

            string tijddatum = "";
            string naam = "";
            string speeltijd = "";

            
            int ypos = 80; // ypos is de Y-coordinaat van de label van de highscore
            int positie = 0;
            entries = new Label[10];
            foreach (DataRow row in dt.Rows)
            {
                if (positie < 10)
                {
                    entries[positie] = new Label();
                    entries[positie].Font = new Font("Flubber", 18);
                    tijddatum = row[0].ToString();
                    naam = row[1].ToString();
                    speeltijd = row[2].ToString();
                    int posnr = positie +1;
                    entries[positie].Text = posnr.ToString() + ". " + naam + "  " + speeltijd + "s  (" + tijddatum+")";
                    entries[positie].AutoSize = true;
                    entries[positie].ForeColor = Color.Yellow;
                    entries[positie].Location = new Point(350, ypos);
                    entries[positie].AutoSize = true;
                    entries[positie].TextAlign = ContentAlignment.MiddleCenter;
                    ypos = ypos + 40;
                    frmmenu.Controls.Add(entries[positie]);
                    positie++;
                }
            }
        }
		// Private Methods (1)

#endregion Methods 
    }
}
