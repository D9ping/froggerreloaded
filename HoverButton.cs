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
    public partial class HoverButton : Control
    {
        private string menuitem;

        public HoverButton(string menuitem)
        {
            InitializeComponent();

            switch (menuitem)
	        {
                case "newgame":
                    pbKnop.Image = global::Frogger.Properties.Resources.btn_new_game;
                    break;
                case "highscore":
                    pbKnop.Image = global::Frogger.Properties.Resources.btn_highscore;
                    break;
                case "options":
                    pbKnop.Image = global::Frogger.Properties.Resources.btn_options;
                    break;
		        case "exit":
                    pbKnop.Image = global::Frogger.Properties.Resources.btn_exit;
                    break;
	        }
            this.menuitem = menuitem;
            pbKnop.MouseHover += new EventHandler(pbKnop_MouseHover);
        }

        /// <summary>
        /// Knop wordt met muis over bewogen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pbKnop_MouseHover(object sender, EventArgs e)
        {
            //todo
            //pbKnop.    
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


        
    }
}
