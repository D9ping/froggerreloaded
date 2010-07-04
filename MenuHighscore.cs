/*
Copyright (C) 2009-2010 Tom Postma, Gertjan Buijs

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
	using System.Collections.Generic;
	using System.Text;
	using System.Data;
	using System.Data.OleDb;
	using System.Drawing;
	using System.Windows.Forms;
	using System.IO;
	
    /// <summary>
    /// Highscores screen
    /// </summary>
    class MenuHighscore : MenuScreen
    {
        private List<HoverButton> highscoremenubtn;
        private HoverButton deletebtn;
        private FrmMenu frmmenu = null;
        private Label[] entries;
        private Label lbshowcurlvlscore;
        private const int marginbetweenhovbtns = 2;

        #region Constructors (1)

        /// <summary>
        /// Creating a new instance of MenuHighscore class.
        /// </summary>
        /// <param name="frmmenu"></param>
        public MenuHighscore(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            frmmenu.ToonLogo = false;

            highscoremenubtn = new List<HoverButton>();
            this.CreateLvlBtns();

            deletebtn = new HoverButton("Delete all");
            deletebtn.Click += new EventHandler(DeleteHighscoreAll);

            int ypos = 80;
            int xpos = 20; //frmmenu.Width / 2 - (highscoremenubtn[0].Width / 2);
            for (int i = 0; i < highscoremenubtn.Count; i++)
            {
                highscoremenubtn[i].Location = new Point(xpos, ypos);
                ypos += highscoremenubtn[1].Height + marginbetweenhovbtns;
            }

            lbshowcurlvlscore = new Label();
            lbshowcurlvlscore.Location = new Point(frmmenu.Width / 2, 5);
            lbshowcurlvlscore.Font = new Font("Flubber", 28);
            lbshowcurlvlscore.ForeColor = Color.Brown;
            lbshowcurlvlscore.AutoSize = true;
            lbshowcurlvlscore.Text = "";

            frmmenu.Controls.Add(lbshowcurlvlscore);
            foreach (HoverButton levelbtn in this.highscoremenubtn)
            {
                frmmenu.Controls.Add(levelbtn);
            }
            //frmmenu.Controls.AddRange(highscoremenubtn);
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
            lbshowcurlvlscore.Visible = false;
            lbshowcurlvlscore.Dispose();
            ClearAllEntries();
        }

        private void CreateLvlBtns()
        {
            string lvldir = Path.Combine(Directory.GetCurrentDirectory(), "levels");

            if (!Directory.Exists(lvldir))
            {
                MessageBox.Show("Error level folder cannot be found.");
                return;
            }

            string[] files;
            try
            {
                files = Directory.GetFiles(lvldir);
                for (int i = 0; i < files.Length; i++)
                {
                    string filelvlname = files[i].Substring(lvldir.Length + 1, files[i].Length - lvldir.Length - 5);
                    HoverButton lvlbtn = new HoverButton(filelvlname);
                    lvlbtn.Tag = filelvlname;
                    lvlbtn.Click += new EventHandler(GetHighscores);
                    highscoremenubtn.Add(lvlbtn);
                }
            }
            catch (DirectoryNotFoundException dirnotfoundexc)
            {
                MessageBox.Show(dirnotfoundexc.Message);
            }
            

           
        }

        /// <summary>
        /// Remove and make invisible all entries.
        /// </summary>
        private void ClearAllEntries()
        {
            if (entries != null)
            {
                foreach (Label curlbl in entries)
                {
                    if (curlbl != null)
                    {
                        curlbl.Visible = false;
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
                ClearAllEntries();
                MessageBox.Show("Highscore table is now empty.");
            }

        }

        /*
         * Later features
         * 
         
        /// <summary>
        /// Delete highscore from particaler level
        /// </summary>
        public bool DeleteHighscoreOneLevel(int level)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete highscore from particaler tier
        /// </summary>
        public bool DeleteHighscoreOneNiveau(Tier tier)
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
            lbshowcurlvlscore.Text = btnclicked.Tag.ToString();
            lbshowcurlvlscore.Visible = true;

            string query = "SELECT * FROM HIGHSCORES WHERE LEVEL = '" + btnclicked.Tag.ToString() + "' ORDER BY SPEELTIJD ASC";
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
                    int posnr = positie + 1;
                    entries[positie].Text = posnr.ToString() + ". " + naam + "  " + speeltijd + "s  (" + tijddatum + ")";
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

        #endregion Methods
    }
}
