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
	using System.Text;
	using System.Windows.Forms;
	using System.Drawing;
	
    class MenuCredits : MenuScreen
    {
        private FrmMenu frmmenu;
        private Label[] lblcredits;

        public MenuCredits(FrmMenu frmmenu)
            : base(frmmenu)
        {
            this.frmmenu = frmmenu;

            lblcredits = new Label[6];
            lblcredits[0] = new Label();
            lblcredits[0].Text = "Developed By:";

            lblcredits[1] = new Label();
            lblcredits[1].Text = "Tom";

            lblcredits[2] = new Label();
            lblcredits[2].Text = "Gertjan";

            lblcredits[3] = new Label();
            lblcredits[3].Text = "Tested by:";

            lblcredits[4] = new Label();
            lblcredits[4].Text = "Bert";

            lblcredits[5] = new Label();
            lblcredits[5].Text = "Lucas";

            int locy = 250;
            for (int i = 0; i < lblcredits.Length; i++)
			{
                lblcredits[i].Location = new Point(frmmenu.Size.Width / 2 - lblcredits[i].Size.Width/2, locy);
                if (i < 3)
                {
                    lblcredits[i].Font = new Font("Flubber", 18);
                }
                else
                {
                    lblcredits[i].Font = new Font("Flubber", 14);
                }
                lblcredits[i].AutoSize = true;
                if (i == 2) locy += 36;
                else locy += 28;
			}

            frmmenu.Controls.AddRange(lblcredits);
        }


        override public void ClearScreen()
        {
            foreach (Label lbl in lblcredits)
            {
                lbl.Dispose();
            }
        }

    }
}

