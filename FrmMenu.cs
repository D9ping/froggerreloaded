using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public enum MenuState
    {
        main,
        highscore,
        options,
        level
    }

    public partial class FrmMenu : Form
    {
        private MenuState menustate;
        private HoverButton[] btns;

        public FrmMenu()
        {
            InitializeComponent();            

            btns = new HoverButton[4];

            //CreateOptionsMenu();
            //CreateLvlButtons();
            CreateMainMenu();
        }


        /// <summary>
        /// Create main menu buttons
        /// </summary>
        private void CreateMainMenu()
        {           
            btns[0] = new HoverButton("Newgame");
            btns[1] = new HoverButton("Highscore");
            btns[2] = new HoverButton("Options");
            btns[3] = new HoverButton("Exit");
        
            //create events                       
            btns[0].Click += new EventHandler(StartNewGame); //mist event args etc. uitzoeken hoe dit moet vanuit
                                                            //constructor wordt createmainmenu aangeroepen dus
            btns[1].Click += new EventHandler(ShowHighScore);
            btns[2].Click += new EventHandler(ShowOptions);
            btns[3].Click += new EventHandler(Shutdown);
            
            int ypos = 200;
            int xpos = 0;
            for (int i = 0; i < 4; i++)
            {
                xpos = this.Width/2 - (btns[0].Width/2);
                btns[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }         
            
            this.Controls.AddRange(btns);            
        }

        /// <summary>
        /// Creer the option menu
        /// </summary>
        private void CreateOptionsMenu()
        {
            ClearBtns();
            
            //todo: haal setting uit register op, nu standaard aan.

            BigCheckbox chx = new BigCheckbox("sound", true);
            chx.Location = new Point(this.Width / 2 - (chx.Width / 2), this.Height / 2 - (chx.Height / 2));
            this.Controls.Add(chx);
        }

        void FrmMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Create the buttons for level selection.
        /// </summary>
        private void CreateLvlButtons()
        {
            ClearBtns();

            btns[0] = new HoverButton("level1");
            btns[1] = new HoverButton("level2");
            btns[2] = new HoverButton("level3");
            
            int ypos = 250;
            int xpos = 0;
            for (int i = 0; i < 3; i++)
            {
                xpos = this.Width / 2 - (btns[0].Width / 2);
                btns[i].Location = new Point(xpos, ypos);
                ypos += 90;
            }
            this.Controls.AddRange(btns);   
        }

        //private void 

        private void StartNewGame(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            ClearBtns();

            CreateLvlButtons();
            
            FrmGame game = new FrmGame();
            game.Show();

            this.Hide();
            
        }

        private void ShowHighScore(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// The options button is pressed, show options screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOptions(object sender, EventArgs e)
        {
            CreateOptionsMenu();
        }

        /// <summary>
        /// exit button is presed, shutdown application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shutdown(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Form is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_ResizeEnd(object sender, EventArgs e)
        {
            ClearBtns();
            switch (menustate)
            {
                case MenuState.main:
                    CreateMainMenu();
                    break;                
            }                
        }  

        /// <summary>
        /// Clear all buttons from the Form.
        /// </summary>
        /// <returns></returns>
        private bool ClearBtns()
        {
            for (int i = 0; i < 4; i++)
            {
                if (btns[i] != null)
                {
                    btns[i].Dispose();
                }
                else return false;
            }            
            return true;
        }
 

    }
}
