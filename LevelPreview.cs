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

        public string NameLevel
        {
            get
            {
                return this.level.Naam;
            }
        }

        public void Selected()
        {
            this.BackColor = Color.Lime;
        }

        public void Deselect()
        {
            this.BackColor = Color.ForestGreen;
        }

        private void LevelPreview_Paint(object sender, PaintEventArgs e)
        {
            if (!level.HasError)
            {
                level.Draw(e.Graphics);
                Font fontlvlname = new Font("Flubber", 24);
                float locx = (this.Width / 2) - ((level.Naam.Length / 2) * fontlvlname.Size);
                float locy = (this.Height / 2) - (fontlvlname.Size / 2);
                e.Graphics.DrawString(level.Naam, fontlvlname, Brushes.BurlyWood, locx, locy);
            }
        }
    }
}

