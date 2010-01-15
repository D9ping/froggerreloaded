using System;
using System.Collections.Generic;
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

        private FrmMenu frmmenu;
        private HoverButton[] levelbtn;
        private FrmGame game;

		#endregion Fields 

		#region Constructors (1) 

        public MenuLevel(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;

            levelbtn = new HoverButton[4];

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

            frmmenu.ShowTierChoice = true;
         
        }

		#endregion Constructors 

		#region Methods (3) 

		// Public Methods (1) 

        override public void ClearScreen()
        {
            foreach (HoverButton curbtn in levelbtn)
            {
                if (curbtn != null) { curbtn.Dispose(); }
            }
        }
		// Private Methods (2) 

        private void LoadLevel(object sender, EventArgs e)
        {
            int levelnr = whichlevel(sender);
            frmmenu.Hide();

            switch (frmmenu.SelectedTier)
            {
                case 0:
                    game = new FrmGame(frmmenu, levelnr, Niveau.freeplay);
                    break;
                case 1:
                    game = new FrmGame(frmmenu, levelnr, Niveau.easy);
                    break;
                case 2:
                    game = new FrmGame(frmmenu, levelnr, Niveau.medium);
                    break;
                case 3:
                    game = new FrmGame(frmmenu, levelnr, Niveau.hard);
                    break;
                case 4:
                    game = new FrmGame(frmmenu, levelnr, Niveau.elite);
                    break;
            }
            game.Show();
            game.BringToFront();
        }

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

		#endregion Methods 
    }
}
