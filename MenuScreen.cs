using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Frogger
{
    public abstract class MenuScreen
    {
        private FrmMenu frmmenu;
        private MenuState menustate;

        public MenuScreen(FrmMenu frmmenu, MenuState menustate)
        {
            this.frmmenu = frmmenu;
            this.menustate = menustate;
        }

        public abstract void ClearScreen();

        /// <summary>
        /// Draw a back button. 
        /// </summary>
        public void CreateBackBtn()
        {
            int margin = 50;

            HoverButton backbtn = new HoverButton("back");
            backbtn.Click += new EventHandler(backMainMenu);            
            backbtn.Location = new Point(frmmenu.Width / 2 - frmmenu.Width / 2, frmmenu.Height - frmmenu.Height - margin);
            frmmenu.Controls.Add(backbtn);
        }

        /// <summary>
        /// This methode is fired if back button is pressed.
        /// it clears all buttons etc. and recreates the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backMainMenu(object sender, EventArgs e)
        {
            frmmenu.CreateMainMenu(sender, e);
        }
    }
}
