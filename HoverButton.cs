/*
Copyright (C) Tom Postma

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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace Frogger
{
    public partial class HoverButton : UserControl
    {
        private Boolean highlighted = false, clicked = false;

        private Color highlightcolor = Color.YellowGreen;
        private Color normalcolor = Color.LimeGreen;

        [DllImport("winmm.dll")]
        public static extern int sndPlaySound(string sFile, int sMode);

        /// <summary>
        /// Constructor for creating HoverButton at runtime.
        /// </summary>
        /// <param name="text"></param>
        public HoverButton(String text)
        {
            InitializeComponent();
            this.lbButton.Text = text;
            this.BackColor = normalcolor;
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("The text of the HoverButton")]
        public String HoverbuttonText
        {
            get
            {
                return this.lbButton.Text;
            }
            set
            {
                this.lbButton.Text = value;
            }
        }


        /// <summary>
        /// make it back to normal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverButton_MouseLeave(object sender, EventArgs e)
        {
            this.highlighted = false;
            this.clicked = false;
            this.Refresh();
        }

        private void HoverButton_MouseEnter(object sender, EventArgs e)
        {
            this.highlighted = true;
            this.Refresh();
        }

        private void HoverButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (highlighted)
            {
                int margin = 0;
                if (clicked)
                {
                    Rectangle rect = new Rectangle(new Point(margin, margin), new Size(this.Width - 2 * margin, this.Height - 2 * margin));                  
                    g.FillRectangle(new SolidBrush(Color.DarkRed), rect);
                }
                else
                {
                    Rectangle rect = new Rectangle(new Point(margin, margin), new Size(this.Width - 2 * margin, this.Height - 2 * margin));

                    LinearGradientBrush lgradientbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.Red, Color.Brown);
                    g.FillRectangle(lgradientbrush, rect);
                }
            }

        }

        private void lbButtonText_Click(object sender, EventArgs e)
        { 
            this.clicked = true;
            this.Refresh();
            if (Program.sound)
            {
                string soundbeep = Application.StartupPath + "\\sounds\\beep.wav";
                if (File.Exists(soundbeep))
                {
                    sndPlaySound(Application.StartupPath + "\\sounds\\beep.wav", 1); //1 = Async
                }
                else
                {
                    DialogResult dlgres = MessageBox.Show("Sound file beep.wav not found.\r\nDo you want to disable the sound completly?", "missing sound file", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dlgres == DialogResult.Yes)
                    {
                        Program.sound = false;
                    }
                }
            }
            Thread.Sleep(100);
            this.OnClick(e); //raise click event for obj. because text is in front.
        }

    }
}
