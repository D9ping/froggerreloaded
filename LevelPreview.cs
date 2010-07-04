/*
Copyright (C) 2010 Tom Postma

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
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Data;
	using System.Text;
	using System.Windows.Forms;
	
    public partial class LevelPreview : UserControl
    {
        private Level level;
        private const int avgsizelettercrt = 16;

        public LevelPreview(string lvlnaam, int preflvlprevwidth)
        {
            InitializeComponent();
            level = new Level(lvlnaam, preflvlprevwidth, preflvlprevwidth);
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
                float locx = (this.Width / 2) - ((level.Naam.Length / 2) * avgsizelettercrt);
                float locy = (this.Height / 2) - (fontlvlname.Size / 2);
                e.Graphics.DrawString(level.Naam, fontlvlname, Brushes.BurlyWood, locx, locy);
            }
        }
    }
}

