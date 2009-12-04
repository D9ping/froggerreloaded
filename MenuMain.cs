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
        private FrmMenu frmmain;

		#endregion Fields 

		#region Constructors (1) 

        public MenuMain(FrmMenu frmmenu, MenuState state)
            : base(frmmenu, state)
        {
            this.
            hoofdmenuknoppen = new HoverButton[4];
            hoofdmenuknoppen[0] = new HoverButton("Newgame");
            hoofdmenuknoppen[1] = new HoverButton("Highscore");
            hoofdmenuknoppen[2] = new HoverButton("Options");
            hoofdmenuknoppen[3] = new HoverButton("Exit");
            //hook events  
            //hoofdmenuknoppen[0].Click += new EventHandler(frmmain.CreateLevelMenu);
            //hoofdmenuknoppen[1].Click += new EventHandler(frmmain.CreateHighScore);
            //hoofdmenuknoppen[2].Click += new EventHandler(frmmain.CreateOptions);
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
