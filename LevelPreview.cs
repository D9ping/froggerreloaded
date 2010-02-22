using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class LevelPreview : UserControl
    {
        private Level level;

        public LevelPreview(string lvlnaam)
        {
            InitializeComponent();
            level = new Level(lvlnaam, this.Width, this.Height);
        }

        private void LevelPreview_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Lime;
        }

        private void LevelPreview_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.ForestGreen;
        }

        private void LevelPreview_Paint(object sender, PaintEventArgs e)
        {
            if (!level.Error)
            {
                level.Draw(e.Graphics);
            }
        }
    }
}

