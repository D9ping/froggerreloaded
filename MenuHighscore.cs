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
        private FrmMenu frmMenu = null;

		#region Constructors (1) 

        public MenuHighscore(FrmMenu frmmenu)
            :base(frmmenu)
        {            
            this.frmMenu = frmmenu;
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
            DataTable dt = DBConnection.ExecuteQuery(query, 4);

            string tijddatum = "";
            string naam = "";
            string speeltijd = "";

            int positie = 0; 
            int ypos = 50; // ypos is de Y-coordinaat van de label van de highscore

            foreach (DataRow row in dt.Rows)
            {
                if (positie < 10)
                {
                    Label lbHighscore = new Label();
                    tijddatum = row[0].ToString();
                    naam = row[1].ToString();
                    speeltijd = row[2].ToString();
                    lbHighscore.Text = positie.ToString() + ".\t" + tijddatum + "\t" + naam + "\t" + speeltijd;
                    lbHighscore.AutoSize = true;
                    lbHighscore.ForeColor = Color.Lime;
                    lbHighscore.Location = new Point(300, ypos);
                    lbHighscore.TextAlign = ContentAlignment.MiddleCenter;
                    ypos = ypos + 40;
                    frmMenu.Controls.Add(lbHighscore);  //  <<<<<<<<<<<<<<<--------------------------
                    
                }
                positie++;
            }

            /*
            lbOverzicht.Items.Clear();
          DataTable dt = new DataTable();
          dt = DatabaseConnection.ExecuteQuery("SELECT A.INTAKEID , B.DATEINTAKE, C.MACHINEID  FROM APP_WAREHOUSE A, APP_INTAKE B, APP_MACHINE C WHERE A.INTAKEID = B.INTAKEID AND B.INTAKEID = C.INTAKEID AND A.KLAARZETID ='0'", 3);
          string ID = "0";
          string DateIntake = "0";
          string MachineId = "0";

          foreach (DataRow row in dt.Rows)
          {
              ID = row[0].ToString();
              DateIntake = row[1].ToString();
              MachineId = row[2].ToString();
              string AddString = ID + "," + MachineId;
              lbOverzicht.Items.Add(AddString);
          }
          */


        }



		#endregion Methods 
    }
}
