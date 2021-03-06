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

#define windows  //platvorm

namespace Frogger
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Drawing.Drawing2D;
	using System.Threading;
	using System.Runtime.InteropServices;
	using System.IO;
	
    public partial class HoverButton : UserControl //System.ComponentModel.Component
    {
        private bool highlighted = false, clicked = false;

        private Color highlightcolor = Color.YellowGreen;
        private Color normalcolor = Color.LimeGreen;
#if windows
        [DllImport("winmm.dll")]
        public static extern int sndPlaySound(string sFile, int sMode);
#endif
        /// <summary>
        /// Constructor for creating HoverButton at designtime.
        /// </summary>
        public HoverButton()
        {
            InitializeComponent();
            this.BackColor = normalcolor;
        }

        /// <summary>
        /// Constructor for creating HoverButton at runtime.
        /// </summary>
        /// <param name="text">the text to display on the button.</param>
        public HoverButton(string text)
        {
            InitializeComponent();
            this.lbButton.Text = text;
        }

        /// <summary>
        /// designtime properties.
        /// </summary>
        [Description("The text of the HoverButton"), DefaultValue("?"), Category("design")]
        public string HoverbuttonText
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

        [Description("The textsize of the HoverButton"), DefaultValue(36), Category("design")]
        public float HoverbuttonSizeText
        {
            get
            {
                return this.lbButton.Font.Size;
            }
            set
            {
                this.lbButton.Font = new Font("Flubber", value);
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
                string soundbeep = Path.Combine(Program.GetSoundDir(), "beep.wav");
                if (File.Exists(soundbeep))
                {
#if windows
                    sndPlaySound(soundbeep, 1); //1 = Async
#elif linux
                    System.Media.SoundPlayer playsnd = new System.Media.SoundPlayer(soundbeep);
                    playsnd.Play(); //issue cannot mix sound.
#endif
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
