using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Frogger
{
    /// <summary>
    /// Draw the level selection.
    /// </summary>
    class MenuLevel : MenuScreen
    {
        #region Fields (5)

        private const int btnlvlmargin = 10;

        private bool errorshowed = false;
        private FrmMenu frmmenu;
        private FrmGame game;
        private LevelPreview lvlpreviewselected;
        private LevelPreview[] lvlpreviews;

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

        #region Methods (2)

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
        }

        // Private Methods (1) 

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

            for (int i = 0; i < files.Length; i++)
            {
                string filename = files[i].Substring(filepath.Length + 1, files[i].Length - filepath.Length - 5);
                this.lvlpreviews[i] = new LevelPreview(filename);
            }

            int locX = 10;
            for (int i = 0; i < lvlpreviews.Length; i++)
            {
                this.lvlpreviews[i].Location = new Point(locX, 200);
                this.lvlpreviews[i].Click += new EventHandler(MenuLevel_Click);
                this.lvlpreviews[i].DoubleClick += new EventHandler(MenuLevel_DoubleClick);
                locX += 260;
            }
            frmmenu.Controls.AddRange(this.lvlpreviews);
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
