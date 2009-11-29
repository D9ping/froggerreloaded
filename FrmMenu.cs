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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Frogger
{
    public enum MenuState
    {
        main,
        highscore,
        options,
        level
    }

    public partial class FrmMenu : Form
    {
        private MenuState menustate;

        private HoverButton[] menu;
        private BigCheckbox[] options;

        private bool fullscreen = false;

        public FrmMenu()
        {
            InitializeComponent();
            menustate = MenuState.main;

            CreateMainMenu(this, EventArgs.Empty);
            //CreateOptionsMenu();
            //CreateLvlButtons();                       

            SetScreenSize();
        }

        /// <summary>
        /// Create main menu buttons
        /// </summary>
        private void CreateMainMenu(object sender, EventArgs e)
        {
            menustate = MenuState.main;

            menu = new HoverButton[4];
            menu[0] = new HoverButton("Newgame");
            menu[1] = new HoverButton("Highscore");
            menu[2] = new HoverButton("Options");
            menu[3] = new HoverButton("Exit");

            //hook events  
            menu[0].Click += new EventHandler(CreateLevelMenu);
            menu[1].Click += new EventHandler(CreateHighScore);
            menu[2].Click += new EventHandler(CreateOptions);
            menu[3].Click += new EventHandler(Shutdown);

            int ypos = 220;
            int xpos = 0;
            for (int i = 0; i < 4; i++)
            {
                xpos = this.Width / 2 - (menu[0].Width / 2);
                menu[i].Location = new Point(xpos, ypos);
                ypos += 80;
            }

            this.Controls.AddRange(menu);
        }


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64;

        /// <summary>
        /// Create level selection screen.
        /// </summary>
        private void CreateLevelMenu(object sender, EventArgs e)
        {
            ClearScreen();
            menustate = MenuState.level;
            menu[0] = new HoverButton("level1");
            menu[1] = new HoverButton("level2");
            menu[2] = new HoverButton("level3");

            int ypos = 220;
            int xpos = 0;
            int margin = 20;
            for (int i = 0; i < 3; i++)
            {
                xpos = this.Width / 2 - (menu[0].Width / 2);
                menu[i].Location = new Point(xpos, ypos);
                ypos += menu[0].Height + margin;
            }
            this.Controls.AddRange(menu);

            CreateBackBtn();
        }

        /// <summary>
        /// Creer highscore menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateHighScore(object sender, EventArgs e)
        {
            ClearScreen();
            menustate = MenuState.highscore;
            CreateBackBtn();
        }

        /// <summary>
        /// Creer option menu
        /// </summary>
        private void CreateOptions(object sender, EventArgs e)
        {
            ClearScreen();
            menustate = MenuState.options;
            //todo: haal setting uit register op, nu standaard aan.

            int margin = 20;
            options = new BigCheckbox[2];

            options[0] = new BigCheckbox("sound", true);
            options[0].Location = new Point(this.Width / 2 - (options[0].Width / 2), this.Height / 2 - (options[0].Height / 2));

            options[1] = new BigCheckbox("full screen", false);
            options[1].Location = new Point(this.Width / 2 - (options[1].Width / 2), this.Height / 2 - (options[1].Height / 2) + options[1].Height + margin);
            options[1].Click += new EventHandler(ToggleFullScreen);

            this.Controls.AddRange(options);

            CreateBackBtn();
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

        private void CreateBackBtn()
        {
            menu[3] = new HoverButton("back");
            menu[3].Click += new EventHandler(backMainMenu);
            int margin = 50;
            menu[3].Location = new Point(this.Width / 2 - menu[0].Width / 2, this.Height - menu[0].Height - margin);
            this.Controls.Add(menu[3]);
        }

        void backMainMenu(object sender, EventArgs e)
        {
            ClearScreen();
            CreateMainMenu(sender, e);
        }

        private void ToggleFullScreen(object sender, EventArgs e)
        {
            fullscreen = !fullscreen;

            SetScreenSize();
        }

        /// <summary>
        /// Set the size of this window.
        /// If full screen is true then go to onttop fullscreen window with hidden taskbar.
        /// </summary>
        private void SetScreenSize()
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                this.TopMost = true;
                SetWindowPos(this.Handle, HWND_TOP, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN), SWP_SHOWWINDOW);                
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
            }
            OnResizeEnd(EventArgs.Empty);

        }

        /// <summary>
        /// Form is resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMenu_ResizeEnd(object sender, EventArgs e)
        {
            ClearScreen();

            switch (menustate)
            {
                case MenuState.main:
                    CreateMainMenu(sender, e);
                    break;
                case MenuState.options:
                    CreateOptions(sender, e);
                    break;
                case MenuState.level:
                    CreateLevelMenu(sender, e);
                    break;
                case MenuState.highscore:
                    CreateHighScore(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Clear all buttons from the Form.
        /// </summary>
        /// <returns></returns>
        private void ClearScreen()
        {
            if (menu != null)
            {
                foreach (HoverButton curbtn in menu)
                {
                    curbtn.Dispose();
                }
            }
            if (options != null)
            {
                foreach (BigCheckbox curcbx in options)
                {
                    curcbx.Dispose();
                }
            }
        }


    }
}
