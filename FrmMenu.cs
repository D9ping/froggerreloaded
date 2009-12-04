/*
Copyright (C) 2009  Tom Postma, Gertjan Buijs

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Frogger
{
    public partial class FrmMenu : Form
    {
		#region Fields (8) 

        private bool fullscreen = false;

        private HoverButton[] menu;
        private BigCheckbox[] options;

        private MenuState menustate;
        private MenuScreen curmenu;

        

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating a new instance of FrmMenu.
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();

            menustate = MenuState.main;

            this.Refresh();            
            
            //SetScreenSize(); //check full screen
        }        

		#endregion Constructors 
        

		#region Methods (14) 

        // Private Methods (14) 

        /// <summary>
        /// Create main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CreateMainMenu(object sender, EventArgs e)
        {
            menustate = MenuState.main;
            this.Refresh();            
        }

        /// <summary>
        /// Create level selection screen.
        /// </summary>
        public void CreateLevelMenu(object sender, EventArgs e)
        {
            menustate = MenuState.level;
            this.Refresh();
        }

        /// <summary>
        /// Create highscore menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CreateHighScore(object sender, EventArgs e)
        {
            menustate = MenuState.highscore;
            this.Refresh();            
        }

        /// <summary>
        /// Create options menu
        /// </summary>
        public void CreateOptions(object sender, EventArgs e)
        {
            menustate = MenuState.options;
            this.Refresh();            
        }

        /// <summary>
        /// Form is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Hier moet dus afhankelijk van de state het juiste menu getekent worden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_Paint(object sender, PaintEventArgs e)
        {
            switch (menustate)
            {
                case MenuState.main:
                    curmenu = new MenuMain(this);
                    break;
                case MenuState.highscore:
                    curmenu = new MenuHighscore(this);
                    break;
                case MenuState.options:
                    curmenu = new MenuOptions(this);
                    break;
                case MenuState.level:
                    curmenu = new MenuLevel(this);
                    break;                
            }
        }  

		#endregion Methods
    }
}
