using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmMenu : Form
    {
        private HoverButtons hovbuttons;

        public FrmMenu()
        {
            InitializeComponent();
            hovbuttons = new HoverButtons();
            
        }

        private void pbNewGame_Click(object sender, EventArgs e)
        {
            FrmGame game = new FrmGame();
            game.Show();

            this.Hide();
        }

    }
}
