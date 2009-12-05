using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// Onzeker of deze klasse er zo bij moet.
    /// </summary>
    public class MenuMain : MenuScreen
    {
		#region Fields (1) 

        private HoverButton[] hoofdmenuknoppen;
        private FrmMenu frmmenu;

		#endregion Fields 

		#region Constructors (1) 

        public MenuMain(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            hoofdmenuknoppen = new HoverButton[4];

            hoofdmenuknoppen[0] = new HoverButton("Newgame");
            hoofdmenuknoppen[1] = new HoverButton("Highscores");
            hoofdmenuknoppen[2] = new HoverButton("Options");
            hoofdmenuknoppen[3] = new HoverButton("Exit");
            //hook events  
            hoofdmenuknoppen[0].Click += new EventHandler(CreateLevelMenu);
            hoofdmenuknoppen[1].Click += new EventHandler(CreateHighScore);
            hoofdmenuknoppen[2].Click += new EventHandler(CreateOptions);
            hoofdmenuknoppen[3].Click += new EventHandler(Shutdown);

            int ypos = 220;
            int xpos = 0;
            for (int i = 0; i < 4; i++)
            {
                xpos = frmmenu.Width / 2 - (hoofdmenuknoppen[0].Width / 2);
                hoofdmenuknoppen[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }

            frmmenu.Controls.AddRange(hoofdmenuknoppen);
        }


		#endregion Constructors 

		#region Methods (1) 

		// Public Methods (1) 

        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in hoofdmenuknoppen)
            {
                curbtn.Dispose();
            }
        }

        /// <summary>
        /// Create main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateMainMenu(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.main;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create level selection screen.
        /// </summary>
        private void CreateLevelMenu(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.level;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create highscore menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateHighScore(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.highscore;
            frmmenu.Refresh();
        }

        /// <summary>
        /// Create options menu
        /// </summary>
        private void CreateOptions(object sender, EventArgs e)
        {
            frmmenu.Menustate = MenuState.options;
            frmmenu.Refresh();
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


		#endregion Methods 
    }
}
