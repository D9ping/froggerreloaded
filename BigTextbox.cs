using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class BigTextbox : UserControl
    {
        public BigTextbox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The content
        /// </summary>
        public string Text
        {
            get
            {
                return this.tbName.Text;
            }

            set
            {
                this.tbName.Text = value;
            }
        }
    }
}
