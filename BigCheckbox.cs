﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class BigCheckbox : UserControl
    {
        private bool ischecked = false;

        public BigCheckbox(String textoption, bool ischecked)
        {
            InitializeComponent();
            this.lbTextBigcheckbox.Text = textoption;
            this.ischecked = ischecked;
            this.Refresh();
        }

        private void BigCheckbox_MouseDown(object sender, MouseEventArgs e)
        {
            this.ischecked = !this.ischecked;
            this.Refresh();            
        }

        private void BigCheckbox_Paint(object sender, PaintEventArgs e)
        {
            if (ischecked)
            {
                this.pbBigheck.Image = global::Frogger.Properties.Resources.checkbox_on;
            }
            else
            {
                this.pbBigheck.Image = global::Frogger.Properties.Resources.checkbox_on;
            }

        }

    }
}