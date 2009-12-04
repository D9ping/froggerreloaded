using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Frogger
{
    public abstract class MenuScreen
    {
		#region Fields (1) 

        private FrmMenu frmmenu;

		#endregion Fields 

		#region Constructors (1) 

                public MenuScreen(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;            
        }

		#endregion Constructors 

		#region Methods (3) 

		// Public Methods (2) 

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
		// Private Methods (1) 

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

		#endregion Methods 
    }
}
