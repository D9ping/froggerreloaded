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

namespace Frogger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public partial class FrmMenu : Form
    {
        #region Fields (3)

        private Boolean menuupdated = false;
        private HoverButton backbtn;
        private MenuScreen curmenu;
        private MenuState menustate;
        public const int bottommarginbackbtn = 50;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Creating a new instance of FrmMenu.
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();

            Program.CheckFullScreen(this);

            menustate = MenuState.main;

            this.backbtn = CreateBackBtn();

            cbxTier.SelectedIndex = 2;

            menuupdated = true;
            //this.Refresh();
        }

        #endregion Constructors

        #region Properties (1)

        public MenuState Menustate
        {
            get { return this.menustate; }
            set { menustate = value; }
        }

        public int LogoPosBottom
        {
            get
            {
                return pbLogo.Location.Y + pbLogo.Size.Height;
            }
        }

        public bool ShowTierChoice
        {
            set { cbxTier.Visible = value; }
        }

        public int SelectedTier
        {
            get { return cbxTier.SelectedIndex; }
        }

        public bool ToonLogo
        {
            get
            {
                if (this.pbLogo.Visible)
                    return true;
                else
                    return false;
            }
            set
            {
                this.pbLogo.Visible = value;
                this.pbKikker.Visible = value;
            }
        }

        public Boolean MenuUpdated
        {
            get { return this.menuupdated; }
            set { this.menuupdated = value; }
        }

        public Bitmap KikkerPic
        {
            set { pbKikker.Image = value; }
        }


        #endregion Properties

        #region Methods (4)

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
            curmenu.ClearScreen();
            this.Menustate = MenuState.main;
            menuupdated = true;
            this.Refresh();
        }

        /// <summary>
        /// Hier moet dus afhankelijk van de state het juiste menu getekent worden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_Paint(object sender, PaintEventArgs e)
        {
            if (menuupdated)
            {
                if (curmenu != null)
                {
                    curmenu.ClearScreen();
                }

                switch (menustate)
                {
                    case MenuState.main:
                        curmenu = new MenuMain(this);
                        if (backbtn != null)
                        {
                            backbtn.Visible = false;
                        }
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
                    case MenuState.credits:
                        curmenu = new MenuCredits(this);
                        backbtn.Visible = true;
                        break;
                    default:
                        curmenu = new MenuMain(this);
                        if (backbtn != null)
                        {
                            backbtn.Visible = false;
                        }
                        break;
                }
                if (backbtn != null)
                {
                    backbtn.Location = new Point(this.Width / 2 - backbtn.Width / 2, this.Height - backbtn.Height - bottommarginbackbtn);
                }
                menuupdated = false;
            }

        }

        /// <summary>
        /// Form is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_ResizeEnd(object sender, EventArgs e)
        {
            menuupdated = true;
            this.Refresh();
        }

        /// <summary>
        /// Make sure text is visible if cbxIier is visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTier_VisibleChanged(object sender, EventArgs e)
        {
            if (!Program.CheckFontInstalled() && cbxTier.Visible == true)
            {
                cbxTier.Font = new Font("Arail", 20);
            }
        }

        private void FrmMenu_Resize(object sender, EventArgs e)
        {
            menuupdated = true;
            this.Refresh();
        }

        /// <summary>
        /// A key is being pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                menustate = MenuState.main;
                menuupdated = true;
            }
            this.Refresh();
        }

        /// <summary>
        /// background effect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerbackground_Tick(object sender, EventArgs e)
        {
            int red = this.BackColor.R;
            int green = this.BackColor.G;
            int blue = this.BackColor.B;
            red = StepToColor(red, 0);
            green = StepToColor(green, 128);
            blue = StepToColor(blue, 0);
            this.BackColor = Color.FromArgb(255, red, green, blue);
        }

        private int StepToColor(int curcolorval, int desirecolorval)
        {
            if (curcolorval > desirecolorval)
            {
                if (curcolorval - desirecolorval > 50)
                {
                    curcolorval = curcolorval - 2;
                }
                else
                {
                    curcolorval--;
                }
            }
            else if (curcolorval < desirecolorval)
            {
                if (desirecolorval - curcolorval > 50)
                {
                    curcolorval = curcolorval + 2;
                }
                else
                {
                    curcolorval++;
                }
            }
            return curcolorval;
        }

        #endregion Methods
    }
}
