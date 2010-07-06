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
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

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
        private Panel pnl;

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

            pnl = new Panel();
            pnl.Location = new Point(10, 50);
            //hovbtn default width is 320, so 340 for panel and 20px for optional scrollbar.
            pnl.Size = new Size(340, frmmenu.ClientRectangle.Height - 150); 
            pnl.Visible = true;
            pnl.AutoScroll = true;
            pnl.AutoSize = false;
            try
            {
                frmmenu.Controls.Add(pnl);
            }
            catch
            {
                frmmenu.Menustate = MenuState.main;
                frmmenu.MenuUpdated = true;
            }
            //System.Windows.Forms.ScrollBar.DefaultForeColor = Color.Blue;

            highscoremenubtn = new List<HoverButton>();
            this.CreateLvlBtns();

            deletebtn = new HoverButton("Delete all");
            deletebtn.Click += new EventHandler(DeleteHighscoreAll);
            deletebtn.Location = new Point(10, frmmenu.ClientRectangle.Height - 70);
            deletebtn.Size = new Size(150, 40);
            deletebtn.HoverbuttonSizeText = 22;
            frmmenu.Controls.Add(deletebtn);

            int ypos = 0;
            int xpos = 0;
            for (int i = 0; i < highscoremenubtn.Count; i++)
            {
                highscoremenubtn[i].Location = new Point(xpos, ypos);
                ypos += highscoremenubtn[1].Height + marginbetweenhovbtns;
            }

            lbshowcurlvlscore = new Label();
            lbshowcurlvlscore.Location = new Point(frmmenu.Width / 2, 5);
            lbshowcurlvlscore.Font = new Font("Flubber", 40);
            lbshowcurlvlscore.ForeColor = Color.Brown;
            lbshowcurlvlscore.AutoSize = true;
            lbshowcurlvlscore.Text = "";
            frmmenu.Controls.Add(lbshowcurlvlscore);

            foreach (HoverButton levelbtn in this.highscoremenubtn)
            {
                levelbtn.HoverbuttonSizeText = 32;
                pnl.Controls.Add(levelbtn);
            }
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

            pnl.Visible = false;
            pnl.Dispose();
            deletebtn.Visible = false;
            deletebtn.Dispose();

            ClearAllEntries();
        }

        /// <summary>
        /// Create a button for each level to get highscore for.
        /// </summary>
        private void CreateLvlBtns()
        {
            string lvldir = Program.GetLevelFolder(); //Path.Combine(Directory.GetCurrentDirectory(), "levels");

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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Level directory cannot be read.");
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
                try
                {
                    DBConnection.SetData("DELETE * FROM HIGHSCORES");
                    ClearAllEntries();
                    MessageBox.Show("Highscore table is now empty.", "succeeded");
                }
                catch
                {
                    MessageBox.Show("Cannot delete all highscores.", "failed");
                }
                
            }

        }


        /// <summary>
        /// Delete highscore from particaler level
        /// </summary>
        public void DeleteHighscoreOneLevel(int lvlname)
        {
            DialogResult dlgres = MessageBox.Show("Are you sure you want to remove the highscores from "+lvlname, "sure?", MessageBoxButtons.YesNo);
            if (dlgres == DialogResult.Yes)
            {
                try
                {
                    DBConnection.SetData("DELETE * FROM HIGHSCORES WHERE level = '"+lvlname+"'");
                    ClearAllEntries();
                    MessageBox.Show(lvlname+" has been delete from the highscores.", "succeeded");
                }
                catch
                {
                    MessageBox.Show("Cannot delete the highscores.", "failed");
                }
            }
        }

        /// <summary>
        /// Verkrijg highscore, (methode moet aan sjabooltje delegate EventHandler voldoen.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetHighscores(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int n = rnd.Next(0, 2);
            if (n == 1)
            {
                this.frmmenu.BackColor = Color.Black;
            }
            else
            {
                this.frmmenu.BackColor = Color.DarkOliveGreen;
            }

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
