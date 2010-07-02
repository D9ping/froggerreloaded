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
	using System.IO;
	using System.Windows.Forms;
	
    /// <summary>
    /// Draw the level selection.
    /// </summary>
    class MenuLevel : MenuScreen
    {
		#region Fields (9) 

        private const int btnlvlmargin = 10;
        private bool errorshowed = false;
        private FrmMenu frmmenu;
        private FrmGame game;
        private LevelPreview[] lvlpreviews;
        private LevelPreview lvlpreviewselected;
        private int viewindexlvl = 0, preflvlprevwidth = 250;
        private PictureBox pbNavRight, pbNavLeft;
        private Bitmap leftarrow, leftarrowselected;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating an new instance of MenuLevel classes.
        /// </summary>
        /// <param name="frmmenu"></param>
        public MenuLevel(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;

            this.CreateLvlPreviews();

            frmmenu.ShowTierChoice = true;
        }

		#endregion Constructors 

		#region Methods (8) 

		// Public Methods (1) 

        /// <summary>
        /// Clear the screen.
        /// </summary>
        override public void ClearScreen()
        {
            if (this.lvlpreviews != null)
            {
                foreach (LevelPreview curlvlpreview in this.lvlpreviews)
                {
                    if (curlvlpreview != null)
                    {
                        curlvlpreview.Visible = false;
                        curlvlpreview.Dispose();
                    }
                }
            }
            if (pbNavRight != null)
            {
                pbNavRight.Visible = false;
                pbNavRight.Dispose();
            }
            if (pbNavLeft != null)
            {
                pbNavLeft.Visible = false;
                pbNavLeft.Dispose();
            }
        }
		// Private Methods (7) 

        /// <summary>
        /// Create a level preview from each level file.
        /// </summary>
        private void CreateLvlPreviews()
        {
            string filepath = Directory.GetCurrentDirectory() + "\\levels";

            if (!Directory.Exists(filepath))
            {
                if (!this.errorshowed)
                {
                    MessageBox.Show("Error level folder cannot be found.");
                    this.errorshowed = true;
                }
                return;
            }
            string[] files = Directory.GetFiles(filepath);

            this.lvlpreviews = new LevelPreview[files.Length];

            preflvlprevwidth = ((this.frmmenu.ClientRectangle.Width - 180) / 3);

            for (int i = 0; i < files.Length; i++)
            {
                string filenameNoExt = files[i].Substring(filepath.Length + 1, files[i].Length - filepath.Length - 5);
                this.lvlpreviews[i] = new LevelPreview(filenameNoExt, preflvlprevwidth);
                this.lvlpreviews[i].Visible = true;
                this.lvlpreviews[i].Click += new EventHandler(MenuLevel_Click);
                this.lvlpreviews[i].DoubleClick += new EventHandler(MenuLevel_DoubleClick);
            }

            this.DrawLvlPreviews(preflvlprevwidth);

            pbNavRight = new PictureBox();
            pbNavRight.Image = Frogger.Properties.Resources.level_navigate_right;
            pbNavRight.Location = new Point(frmmenu.ClientRectangle.Width - Frogger.Properties.Resources.level_navigate_right.Width - 10, preflvlprevwidth);
            pbNavRight.SizeMode = PictureBoxSizeMode.AutoSize;
            pbNavRight.Click += new EventHandler(pbNavRight_Click);
            pbNavRight.MouseEnter += new EventHandler(pbNavRight_MouseEnter);
            pbNavRight.MouseLeave += new EventHandler(pbNavRight_MouseLeave);
            frmmenu.Controls.Add(pbNavRight);

            leftarrow = Frogger.Properties.Resources.level_navigate_right;
            leftarrow.RotateFlip(RotateFlipType.Rotate180FlipY);
            leftarrowselected = Frogger.Properties.Resources.level_navigate_selected_right;
            leftarrowselected.RotateFlip(RotateFlipType.Rotate180FlipY);

            pbNavLeft = new PictureBox();
            pbNavLeft.Image = leftarrow;
            pbNavLeft.Location = new Point(0 + 10, preflvlprevwidth);
            pbNavLeft.SizeMode = PictureBoxSizeMode.AutoSize;
            pbNavLeft.Click += new EventHandler(pbNavLeft_Click);
            pbNavLeft.MouseEnter += new EventHandler(pbNavLeft_MouseEnter);
            pbNavLeft.MouseLeave += new EventHandler(pbNavLeft_MouseLeave);
            frmmenu.Controls.Add(pbNavLeft);
        }

        private void pbNavLeft_MouseLeave(object sender, EventArgs e)
        {
            pbNavLeft.Image = leftarrow;
        }

        private void pbNavLeft_MouseEnter(object sender, EventArgs e)
        {
            if (viewindexlvl > 0)
            {
                pbNavLeft.Image = leftarrowselected;
            }
        }

        private void pbNavRight_MouseLeave(object sender, EventArgs e)
        {
            pbNavRight.Image = Frogger.Properties.Resources.level_navigate_right;
        }

        private void pbNavRight_MouseEnter(object sender, EventArgs e)
        {
            if (viewindexlvl < this.lvlpreviews.Length - 3)
            {
                this.pbNavRight.Image = Frogger.Properties.Resources.level_navigate_selected_right;
            }
        }

        /// <summary>
        /// This methode accually draws the 3 visible levelpreviews.
        /// </summary>
        private void DrawLvlPreviews(int preflvlprevwidth)
        {
            for (int c = 0; c < lvlpreviews.Length; c++)
            {
                this.lvlpreviews[c].Visible = false;
                this.frmmenu.Controls.Remove(lvlpreviews[c]);
            }
            int locX = 80;
            
            for (int i = viewindexlvl; i < viewindexlvl + 3; i++)
            {
                this.lvlpreviews[i].Visible = true;
                this.lvlpreviews[i].Size = new Size(preflvlprevwidth, preflvlprevwidth);
                this.lvlpreviews[i].Location = new Point(locX, 250);

                locX += preflvlprevwidth + 10;
            }
            this.frmmenu.Controls.AddRange(lvlpreviews);
        }

        /// <summary>
        /// Clicked a level so it is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLevel_Click(object sender, EventArgs e)
        {
            foreach (LevelPreview curlvlpre in this.lvlpreviews)
            {
                curlvlpre.Deselect();
            }
            LevelPreview lvlpre = (LevelPreview)sender;
            lvlpre.Focus();
            lvlpre.Selected();
            this.lvlpreviewselected = lvlpre;
        }

        /// <summary>
        /// A level preview is dubble clicked and get loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLevel_DoubleClick(object sender, EventArgs e)
        {
            this.game = new FrmGame(this.frmmenu, this.lvlpreviewselected.NameLevel, WhichTier(this.frmmenu.cbxTier.SelectedIndex));
            this.game.Show();
            this.frmmenu.Hide();
        }

        /// <summary>
        /// Navigate to left on level previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbNavLeft_Click(object sender, EventArgs e)
        {            
            if (viewindexlvl > 0)
            {
                viewindexlvl--;
            } 
			else
			{
				this.pbNavLeft.Image = leftarrow;
			}
            this.DrawLvlPreviews(preflvlprevwidth);
			
        }

        /// <summary>
        /// Navigate to right on level previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbNavRight_Click(object sender, EventArgs e)
        {
            
            if (viewindexlvl < this.lvlpreviews.Length-3)
            {
                viewindexlvl++;
            }
			else
			{
				pbNavRight.Image = Frogger.Properties.Resources.level_navigate_right;
			}
            this.DrawLvlPreviews(preflvlprevwidth);
        }

        /// <summary>
        /// Finds out which level is selected.
        /// </summary>
        /// <param name="selectedindex"></param>
        /// <returns></returns>
        private Niveau WhichTier(int selectedindex)
        {
            switch (selectedindex)
            {
                case 0:
                    return Niveau.freeplay;
                case 1:
                    return Niveau.easy;
                case 2:
                    return Niveau.medium;
                case 3:
                    return Niveau.hard;
                case 4:
                    return Niveau.elite;
                default:
                    return Niveau.medium;
            }
        }

		#endregion Methods 
    }
}
