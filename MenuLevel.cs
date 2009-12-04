using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// todo, draw the level selection.
    /// </summary>
    class MenuLevel : MenuScreen
    {
		#region Fields (2) 

        private FrmGame game;
                private HoverButton[] levelbtn;

		#endregion Fields 

		#region Constructors (1) 

        public MenuLevel(FrmMenu frmmenu, MenuState menustate)
            : base(frmmenu, menustate)
        {
            levelbtn = new HoverButton[4];
            menustate = MenuState.level;

            levelbtn[0] = new HoverButton("level1");
            levelbtn[1] = new HoverButton("level2");
            levelbtn[2] = new HoverButton("level3");
            
            levelbtn[0].Name = "btnLvl1";
            levelbtn[1].Name = "btnLvl2";
            levelbtn[2].Name = "btnLvl3";
            //hook events
            levelbtn[0].Click += new EventHandler(LoadLevel);
            levelbtn[1].Click += new EventHandler(LoadLevel);
            levelbtn[2].Click += new EventHandler(LoadLevel);

            int ypos = 220;
            int xpos = 0;
            int margin = 20;
            for (int i = 0; i < 3; i++)
            {
                xpos = frmmenu.Width / 2 - (levelbtn[0].Width / 2);
                levelbtn[i].Location = new Point(xpos, ypos);
                ypos += levelbtn[0].Height + margin;
            }
            frmmenu.Controls.AddRange(levelbtn);
         
        }

		#endregion Constructors 

		#region Methods (3) 

		// Public Methods (2) 

        override public void ClearScreen()
        {
        }

        public void LoadLevel(object sender, EventArgs e)
        {
            int levelnr = whichlevel(sender);
            game = new FrmGame(levelnr);
            game.Show();
        }
		// Private Methods (1) 

        private int whichlevel(object button)
        {
            HoverButton btn = (HoverButton)button;

            switch (btn.Name)
            {
                case "btnLvl1":
                    return 1;                    
                case "btnLvl2":
                    return 2;
                case "btnLvl3":
                    return 3;
                default:
                    throw new Exception("level unknow");                    
            }
        }

        /*
        /// <summary>
        /// Event for level button click
        /// figure out whitch button was clicked.
        /// and create FrmGame with right parameter.
        /// </summary>
        /// <param name="sender">the level x hoverbutton</param>
        /// <param name="e"></param>
        private void LoadLevel(object sender, EventArgs e)
        {
            this.Hide();
            HoverButton btn = (HoverButton)sender;

            switch (btn.Name)
            {
                case "btnLvl1":
                    FrmGame game = new FrmGame(1);
                    game.Show();
                    break;
                case "btnLvl2":
                    game = new FrmGame(2);
                    game.Show();
                    break;
                case "btnLvl3":
                    game = new FrmGame(3);
                    game.Show();
                    break;
                default:
                    MessageBox.Show("Error: level unknow.");
                    break;
            }
        }
        */

		#endregion Methods 
    }
}
