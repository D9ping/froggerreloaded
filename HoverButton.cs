using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Frogger
{
    public partial class HoverButton : UserControl
    {
        private Boolean highlighted = false;

        private Color highlightcolor = Color.YellowGreen;
        private Color normalcolor = Color.LimeGreen;


        public HoverButton(String text)
        {
            InitializeComponent();
            this.lbButtonText.Text = text;
            this.BackColor = normalcolor;
        }


        /// <summary>
        /// make it back to normal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverButton_MouseLeave(object sender, EventArgs e)
        {
            this.highlighted = false;
            this.Refresh();
        }

        private void HoverButton_MouseEnter(object sender, EventArgs e)
        {
            this.highlighted = true;
            this.Refresh();
        }

        private void HoverButton_Paint(object sender, PaintEventArgs e)
        {
            if (highlighted)
            {                
                Graphics g = e.Graphics;

                int margin = 0;
                Rectangle rect = new Rectangle(new Point(margin, margin), new Size(this.Width - 2*margin, this.Height - 2*margin));

                LinearGradientBrush lgradientbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.Red, Color.Brown);
                g.FillRectangle(lgradientbrush, rect);

            }
        }


    }
}
