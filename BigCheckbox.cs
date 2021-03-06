﻿/*
Copyright (C) 2009-2010  Tom Postma

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
    using System.ComponentModel;
    using System.Windows.Forms;
	
    public partial class BigCheckbox : UserControl
    {
        private bool ischecked =true;

        /// <summary>
        /// Constructor for creating at designtime.
        /// </summary>
        public BigCheckbox()
        {
            InitializeComponent();
            this.lbBigcheckbox.Text = "test";
        }

        /// <summary>
        /// Constructor for creating at runtime.
        /// </summary>
        /// <param name="textoption"></param>
        /// <param name="ischecked"></param>
        public BigCheckbox(string textoption, bool ischecked)
        {
            InitializeComponent();
            this.lbBigcheckbox.Text = textoption;
            this.ischecked = ischecked;
            this.Refresh();
        }

        /// <summary>
        /// Set or get the text by this checkbox.
        /// </summary>
        [Description("The text of the checkbox")]
        public string CheckboxText
        {
            get
            {
                return this.lbBigcheckbox.Text;
            }
            set
            {
                this.lbBigcheckbox.Text = value;
            }

        }

        private void BigCheckbox_Paint(object sender, PaintEventArgs e)
        {
            if (ischecked)
            {
                this.pbBigheck.Image = global::Frogger.Properties.Resources.checkbox_on;
            }
            else
            {
                this.pbBigheck.Image = global::Frogger.Properties.Resources.checkbox_off;
            }

        }

        private void pbBigheck_Click(object sender, EventArgs e)
        {
            this.ischecked = !this.ischecked;
            this.Refresh();

            this.OnClick(e);
        }

    }
}
