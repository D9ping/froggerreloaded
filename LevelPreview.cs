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
        private int levelnr;
        private Level level;

        public LevelPreview(int levelnr)
        {
            InitializeComponent();
            //this.levelnr = levelnr;
            level = new Level(levelnr, this.Width, this.Height);
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
            Graphics g = e.Graphics;
            level.Draw(g);

            //int space = this.Height / 10;
            //switch (levelnr)
            //{
            //    case 1:
            //        level.DrawRiver(g, space * 1, 1); //this is the level design.
            //        level.DrawRoad(g, space * 3);
            //        level.DrawRoad(g, space * 5);
            //        level.DrawRoad(g, space * 7);
            //        break;
            //    case 2:
            //        level.DrawRiver(g, space * 1, 2);
            //        level.DrawRoad(g, space * 4);
            //        level.DrawRoad(g, space * 6);
            //        break;
            //    case 3:
            //        level.DrawRiver(g, space * 1, 4);
            //        level.DrawRoad(g, space * 6);
            //        level.DrawRoad(g, space * 7);
            //        level.DrawRoad(g, space * 8);
            //        break;
            //}
        }
    }
}

