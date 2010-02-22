using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Frogger
{
    /// <summary>
    /// todo, draw the level selection.
    /// </summary>
    class MenuLevel : MenuScreen
    {
		#region Fields (5) 

        private const int btnlvlmargin = 10;
        private FrmMenu frmmenu;
        private FrmGame game;
        private HoverButton[] levelbtn;
        private LevelPreview[] lvlpreviews;

		#endregion Fields 

		#region Constructors (1) 

        //private int sellevel = 1;
        public MenuLevel(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;
            
            CreateLvlPreviews();
            
            frmmenu.ShowTierChoice = true;
        }

		#endregion Constructors 

		#region Methods (2) 

		// Public Methods (1) 

        override public void ClearScreen()
        {
            if (lvlpreviews != null)
            {
                foreach (LevelPreview curlvlpreview in lvlpreviews)
                {
                    if (curlvlpreview != null) { curlvlpreview.Dispose(); }
                }
            }
        }
		// Private Methods (1) 

        /// <summary>
        /// Create a level preview from each level file.
        /// </summary>
        private void CreateLvlPreviews()
        {
            String filepath = Directory.GetCurrentDirectory() + "\\levels";

            if (!Directory.Exists(filepath))
            {
                MessageBox.Show("Error");
            }
            String[] files = Directory.GetFiles(filepath);

            LevelPreview[] lvlpreviews = new LevelPreview[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                String filename = files[i].Substring(filepath.Length+1, files[i].Length - filepath.Length -5); //remove extension
                lvlpreviews[i] = new LevelPreview(filename);
            }

            int locX = 10;
            for (int i = 0; i < lvlpreviews.Length; i++)
            {
                //lvlpreviews[i].DoubleClick += new EventHandler(LoadLevel);
                lvlpreviews[i].Location = new Point(locX, 200);
                locX += 260;
            }
            frmmenu.Controls.AddRange(lvlpreviews);
        }

		#endregion Methods 
    }
}
