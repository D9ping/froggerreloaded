using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// todo, draw the level selection.
    /// </summary>
    class MenuLevel : MenuScreen
    {
        #region Fields (4)

        private const int btnlvlmargin = 10;
        private FrmMenu frmmenu;
        private FrmGame game;
        private HoverButton[] levelbtn;
        private LevelPreview[] lvlpreviews;
        private int sellevel = 1;

        #endregion Fields

        #region Constructors (1)

        public MenuLevel(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            CreateLvlPreviews();
            frmmenu.ShowTierChoice = true;
        }

        /// <summary>
        /// Create a level preview from each level file.
        /// </summary>
        private void CreateLvlPreviews()
        {
            lvlpreviews = new LevelPreview[3];
            lvlpreviews[0] = new LevelPreview(1);
            lvlpreviews[0].Tag = 1;
            lvlpreviews[1] = new LevelPreview(2);
            lvlpreviews[1].Tag = 2;
            lvlpreviews[2] = new LevelPreview(3);
            lvlpreviews[2].Tag = 3;

            int locX = 10;
            for (int i = 0; i < lvlpreviews.Length; i++)
            {
                lvlpreviews[i].DoubleClick += new EventHandler(LoadLevel);
                lvlpreviews[i].Location = new Point(locX, 200);
                locX += 260;
            }
            frmmenu.Controls.AddRange(lvlpreviews);
        }

        private void CreateStartLvlBtn()
        {

        }
        /*
        /// <summary>
        /// Create button for level selection.
        /// </summary>
        private void CreateLvlBtns()
        {
            levelbtn = new HoverButton[3];

            levelbtn[0] = new HoverButton("level1");
            levelbtn[1] = new HoverButton("level2");
            levelbtn[2] = new HoverButton("level3");
            levelbtn[0].Name = "btnLvl1";
            levelbtn[1].Name = "btnLvl2";
            levelbtn[2].Name = "btnLvl3";
            levelbtn[0].Tag = 1;
            levelbtn[1].Tag = 2;
            levelbtn[2].Tag = 3;

            int ypos = 220;
            int xpos = 0;

            for (int i = 0; i < 3; i++)
            {
                xpos = frmmenu.Width / 2 - (levelbtn[0].Width / 2);
                levelbtn[i].Location = new Point(xpos, ypos);
                ypos += levelbtn[0].Height + btnlvlmargin;
                levelbtn[i].Click += new EventHandler(LoadLevel);
            }
            frmmenu.Controls.AddRange(levelbtn);
        }
         */

        #endregion Constructors

        #region Methods (3)

        // Public Methods (1) 

        override public void ClearScreen()
        {
            foreach (LevelPreview curlvlpreview in lvlpreviews)
            {
                if (curlvlpreview != null) { curlvlpreview.Dispose(); }
            }
            //foreach (HoverButton curbtn in levelbtn)
            //{
            //    if (curbtn != null) { curbtn.Dispose(); }
            //}
        }
        // Private Methods (2) 

        /// <summary>
        /// Load a new frmGame with the right level from the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadLevel(object sender, EventArgs e)
        {
            int levelnr = whichlevel(sender);
            frmmenu.Hide();

            switch (frmmenu.SelectedTier)
            {
                case 0:
                    game = new FrmGame(frmmenu, levelnr, Niveau.freeplay);
                    break;
                case 1:
                    game = new FrmGame(frmmenu, levelnr, Niveau.easy);
                    break;
                case 2:
                    game = new FrmGame(frmmenu, levelnr, Niveau.medium);
                    break;
                case 3:
                    game = new FrmGame(frmmenu, levelnr, Niveau.hard);
                    break;
                case 4:
                    game = new FrmGame(frmmenu, levelnr, Niveau.elite);
                    break;
            }
            game.Show();
            game.BringToFront();
        }

        /// <summary>
        /// Find out which level number is selected.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private int whichlevel(object lvlprev)
        {
            //HoverButton btn = (HoverButton)button;
            LevelPreview lvlpreview = (LevelPreview)lvlprev;
            int levelnum = Convert.ToInt32(lvlpreview.Tag);
            return levelnum;
        }

        #endregion Methods
    }
}
