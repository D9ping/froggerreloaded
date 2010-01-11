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
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

namespace Frogger
{
    public partial class FrmGame : Form
    {
		#region Fields (5) 

        private FrmMenu frmmenu;
        private GameEngine game;
        private int min = 1;
        private int sec = 10;
        private bool timeup = false;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating a new instance of FrmGame.
        /// </summary>
        /// <param name="level">the level number, each numbers draws a other level.</param>
        /// <param name="niveau">the niveau enumaration. freeplay you won't go gameover.
        /// then there is easy, medium, hard and elite.</param>
        public FrmGame(FrmMenu frmmenu, int level, Niveau tier)
        {
            InitializeComponent();
            this.frmmenu = frmmenu;
            this.game = new GameEngine(level, this, tier);
            Program.CheckFullScreen(this);
        }

		#endregion Constructors 

		#region Methods (8) 

		// Public Methods (1) 

        /// <summary>
        /// Hide the frmGame and show the frmMenu in MenuState main again.
        /// </summary>
        public void CloseGame()
        {
            game.StopEngine();
            this.frmmenu.Menustate = MenuState.main;
            this.frmmenu.Show();
            this.Hide();
        }
		// Private Methods (7) 

#if DEBUG
        /// <summary>
        /// quickly test game over.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            timeup = true;
            this.Refresh();
        }
#endif

        /// <summary>
        /// Form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseGame();
        }

        /// <summary>
        /// Makes arrow keys work.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
      protected override bool ProcessDialogKey (Keys keyData)
      {
          switch (keyData)
          {               
                case Keys.Down:
                  game.frog.Jump(Direction.South);
                    
                    break;
                case Keys.Left:
                    game.frog.Jump(Direction.East);
                    
                    break;
                case Keys.Right:
                    game.frog.Jump(Direction.West);
                    
                    break;
                case Keys.Up:
                    game.frog.Jump(Direction.North);
                    
                    break;
                case Keys.Escape:
                    CloseGame();
                    break;
            }
          return true;
      }


        /// <summary>
        /// Draw every aspect of the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.DrawLevel(g);
            if (timeup)
            {
                game.GameOver(g, true, false);
                this.Invalidate();
            }
            else
            {
                lbTime.Text = UpdateGameTime();
            }
        }

        /// <summary>
        /// repaint the form, so thing show up correctly a the new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Update the game time, and check if time
        /// for the current tier is over.
        /// if so, the gameover methode from the GameEngine get excuted..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateGame_Tick(object sender, EventArgs e)
        {
            sec--;
            if (sec < 0)
            {
                min--;
                if (game.CheckGameTime(min))
                {
                    timeup = true;
                }
                else
                {
                    sec = 59;
                }
            }
        }

        /// <summary>
        /// Format the game time string so seconds are always displayed with two numbers,
        /// a lead zero if seconds lower then 10seconds is added.
        /// And there is a : charcter between the minuts and seconds.
        /// </summary>
        private String UpdateGameTime()
        {
            String time = this.min.ToString() + ":";
            if (this.sec < 10) { time += "0" + this.sec.ToString(); 
             if (this.min == 0)
             {
                 lbTime.ForeColor = Color.Red;
            } 
             }
            else { time += this.sec.ToString();
            lbTime.ForeColor = Color.LightGray;
            }
            return time;
        }

		#endregion Methods 
    }
}
