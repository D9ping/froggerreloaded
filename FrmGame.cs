/*
Copyright (C) 2009-2010  Tom Postma, Gertjan Buijs

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
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public partial class FrmGame : Form
    {
        #region Fields (5)

        private FrmMenu frmmenu;
        private GameEngine game;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Creating a new instance of FrmGame.
        /// </summary>
        /// <param name="level">the level number, each numbers draws a other level.</param>
        /// <param name="niveau">the niveau enumaration. freeplay you won't go gameover.
        /// then there is easy, medium, hard and elite.</param>
        public FrmGame(FrmMenu frmmenu, string lvlname, Niveau tier)
        {
            InitializeComponent();
            this.frmmenu = frmmenu;
            this.game = new GameEngine(lvlname, this, tier);
            Program.CheckFullScreen(this);
            this.Text = this.Text + " - level:" + lvlname + " - tier: " + tier.ToString();
        }

        #endregion Constructors

        #region Properties (2)

        /*
        public String TbEnterName
        {
            get
            {
                return this.tbHighscoreName.Text;
            }
        }

        public bool VisibleTbEnterName
        {
            set
            {
                tbHighscoreName.Visible = value;
            }
            get
            {
                return tbHighscoreName.Visible; 
            }
        }
         */

		#endregion Properties 

        #region Methods (9) 

        // Public Methods (1) 

        /// <summary>
        /// Form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.StopEngine(true);
            this.frmmenu.Menustate = MenuState.main;
            this.frmmenu.MenuUpdated = true;
            this.frmmenu.Show();
        }

        /// <summary>
        /// Move a frog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    game.frog.Jump(Direction.East);
                    break;
                case Keys.Left:
                    game.frog.Jump(Direction.East);
                    break;
                case Keys.D:
                    game.frog.Jump(Direction.West);
                    break;
                case Keys.Right:
                    game.frog.Jump(Direction.West);
                    break;
                case Keys.S:
                    game.frog.Jump(Direction.South);
                    break;
                case Keys.Down:
                    game.frog.Jump(Direction.South);
                    break;
                case Keys.W:
                    game.frog.Jump(Direction.North);
                    break;
                case Keys.Up:
                    game.frog.Jump(Direction.North);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// Draw every aspect of the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.RenderScreen(g);
            lbTime.Text = GetFancyGameTime();
        }

        /// <summary>
        /// Begin resizing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeBegin(object sender, EventArgs e)
        {
            game.StopEngine(false);
        }

        /// <summary>
        /// repaint the form, so thing show up correctly a the new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_ResizeEnd(object sender, EventArgs e)
        {
            game.SetupEngine(false); //does resize all resources.
            game.StartEngine();
            this.Refresh();
        }

        /// <summary>
        /// Format the game time string so seconds are always displayed with two numbers,
        /// a lead zero if seconds lower then 10seconds is added.
        /// And there is a : charcter between the minuts and seconds.
        /// </summary>
        private string GetFancyGameTime()
        {
            StringBuilder timestr = new StringBuilder(this.game.min.ToString() + ":");
            if (this.game.sec < 10)
            {
                timestr.Append("0" + this.game.sec.ToString());
                if (this.game.min == 0)
                {
                    //less than 10s make red.
                    lbTime.ForeColor = Color.Red;
                }
            }
            else
            {
                //still more than `10seconds left.
                timestr.Append(this.game.sec.ToString());
                lbTime.ForeColor = Color.LightGray;
            }
            return timestr.ToString();
        }

		#endregion Methods 
    }
}
