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
		#region Fields (3) 

        private HoverButton backbtn;
        private MenuScreen curmenu;
        private MenuState menustate;
        public const int bottommarginbackbtn = 50;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating a new instance of FrmMenu.
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();

            Program.CheckFullScreen(this);

            menustate = MenuState.main;

            this.Refresh();

            this.backbtn = CreateBackBtn();

            cbxTier.SelectedIndex = 2;
        }

		#endregion Constructors 

		#region Properties (1) 

        public MenuState Menustate
        {
            get
            {
                return this.menustate;
            }
            set
            {
                menustate = value;
            }
        }

        public Boolean ShowTierChoice
        {
            set
            {
                cbxTier.Visible = value;
            }
        }

        public int SelectedTier
        {
            get
            {
                return cbxTier.SelectedIndex;
            }
        }

        public bool ToonLogo
        {
            get
            {
                if (this.pbLogo.Visible) return true;
                else return false;
            }
            set
            {
                this.pbLogo.Visible = value;
                this.pbKikker.Visible = value;
            }
        }

        public Bitmap KikkerPic
        {
            set
            {
                pbKikker.Image = value;
            }
        }

		#endregion Properties 

		#region Methods (4) 

		// Public Methods (1) 

        /// <summary>
        /// Draw a back button. 
        /// </summary>
        public HoverButton CreateBackBtn()
        {
            int margin = 50;
            HoverButton backbtn = new HoverButton("back");
            backbtn.Click += new EventHandler(backMainMenu);
            backbtn.Location = new Point(this.Width / 2 - backbtn.Width / 2, this.Height - backbtn.Height - margin);
            backbtn.Visible = true;
            this.Controls.Add(backbtn);
            return backbtn;
        }

		// Private Methods (3) 

        /// <summary>
        /// This methode is fired if back button is pressed.
        /// it clears all buttons etc. and recreates the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backMainMenu(object sender, EventArgs e)
        {
            this.Menustate = MenuState.main;
            Refresh();
        }

        /// <summary>
        /// Hier moet dus afhankelijk van de state het juiste menu getekent worden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_Paint(object sender, PaintEventArgs e)
        {
            if (curmenu != null) { curmenu.ClearScreen(); }

            switch (menustate)
            {
                case MenuState.main:
                    curmenu = new MenuMain(this);
                    if (backbtn != null) { backbtn.Visible = false; }
                    break;
                case MenuState.highscore:
                    curmenu = new MenuHighscore(this);
                    backbtn.Visible = true;
                    break;
                case MenuState.options:
                    curmenu = new MenuOptions(this);
                    backbtn.Visible = true;
                    break;
                case MenuState.level:
                    curmenu = new MenuLevel(this);
                    backbtn.Visible = true;
                    break;
            }
            if (backbtn != null)
            {
                backbtn.Location = new Point(this.Width / 2 - backbtn.Width / 2, this.Height - backbtn.Height - bottommarginbackbtn);
            }
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

		#endregion Methods 
	 }
}
