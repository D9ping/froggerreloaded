﻿/*
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
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace Frogger
{
    public partial class FrmGame : Form
    {
		#region Fields (5) 

        private int min = 0;
        private int sec = 0;

        private GameEngine game;
        private IntPtr HWND_TOP = IntPtr.Zero;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SWP_SHOWWINDOW = 64;

		#endregion Fields 

		#region Constructors (1) 

                /// <summary>
        /// Creating a new instance of FrmGame.
        /// </summary>
        /// <param name="level"></param>
        public FrmGame(int level, Niveau niv)
        {
            InitializeComponent();
            this.game = new GameEngine(level, this, niv);
            Program.CheckFullScreen(this);
        }

		#endregion Constructors 

		#region Methods (7) 

		// Private Methods (7) 

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            //todo
            if (e.KeyChar == (Char)Keys.Escape)
            {
                MessageBox.Show("Test");
            }
        }

        /// <summary>
        /// Draw everything of the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.DrawLevel(g);
            UpdateGameTime();
            lbTime.Text = UpdateGameTime();
        }

        /// <summary>
        /// Teken speel tijd string
        /// </summary>
        /// <param name="g"></param>
        private String UpdateGameTime()
        {
            String time = this.min.ToString() + ":";
            if (this.sec < 10) { time += "0" + this.sec.ToString(); }
            else { time += this.sec.ToString(); }
            return time;
        }

        /// <summary>
        /// Hier gebeurt het..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateGame_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec > 59)
            {
                min++;
                sec = 0;
            }
            lbTime.Refresh();
        }

        /// <summary>
        /// repaint form resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //This is also unmangement code, for getting the real screen size with taskbar.
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);

        //This is unmangement code needed for real fullscreen. hidden taskbar etc.
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);

		#endregion Methods 
    }
}
