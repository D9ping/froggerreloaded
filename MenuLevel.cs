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
        private FrmGame game;
        
        private HoverButton[] buttons;

        public MenuLevel(FrmMenu menu, MenuState menustate)
            : base(menu, menustate)
        {
            menustate = MenuState.level;

            /*
            menu[0] = new HoverButton("level1");
            menu[1] = new HoverButton("level2");
            menu[2] = new HoverButton("level3");
            //
            menu[0].Name = "btnLvl1";
            menu[1].Name = "btnLvl2";
            menu[2].Name = "btnLvl3";
            //hook events
            menu[0].Click += new EventHandler(LoadLevel);
            menu[1].Click += new EventHandler(LoadLevel);
            menu[2].Click += new EventHandler(LoadLevel);

            int ypos = 220;
            int xpos = 0;
            int margin = 20;
            for (int i = 0; i < 3; i++)
            {
                xpos = this.Width / 2 - (menu[0].Width / 2);
                menu[i].Location = new Point(xpos, ypos);
                ypos += menu[0].Height + margin;
            }

            //FrmMenu.ActiveForm.Controls.Add(menu);
            //this.Controls.AddRange(menu);

            //todo
            //throw new System.NotImplementedException();
             */
        }

        override public void ClearScreen()
        {
        }

        public void LoadLevel(object sender, EventArgs e)
        {
            int levelnr = whichlevel(sender);
            game = new FrmGame(levelnr);
            game.Show();
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
        
    }
}
