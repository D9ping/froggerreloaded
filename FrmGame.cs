using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Frogger
{
    public partial class FrmGame : Form
    {
        public FrmGame()
        {
            InitializeComponent();                        
        }

        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            int hoogtestrook = 30;
            int hoogteweg = 100;            

            Graphics g = e.Graphics;
                        
            //SolidBrush brushStrook = new SolidBrush(Color.Blue);
            SolidBrush brushWeg = new SolidBrush(Color.Black);
            
            //Rectangle rectStrook = new Rectangle(0, this.Height - hoogtestrook, this.Width, hoogtestrook);
            Rectangle rectWeg = new Rectangle(0, this.Height - hoogtestrook - hoogteweg, this.Width, hoogteweg);

            //g.FillRectangle(brushStrook, rectStrook);
            g.FillRectangle(brushWeg, rectWeg);
        }

        private void FrmGame_FormClosed(object sender, FormClosedEventArgs e)
        {            
            Application.Exit();
        }
    }
}
