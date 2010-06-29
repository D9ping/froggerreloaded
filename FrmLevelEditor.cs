using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmLevelEditor : Form
    {
        //private GameEngine game;
        private bool savinglevel = false;
        private Level level;
        private FrmMenu frmmenu;

        /// <summary>
        /// Creating an new instance of FrmLevelEditor.
        /// </summary>
        /// <param name="frmmenu"></param>
        public FrmLevelEditor(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;

            InitializeComponent();
            //game = new GameEngine(this);
            this.level = new Level(this.ClientRectangle.Width - this.panelTools.ClientRectangle.Width, this.ClientRectangle.Height);

            hovbtnBack.HoverbuttonText = "Back";
            hovbtnBack.SizeText = 24;
            hovbtnSave.HoverbuttonText = "Save";
            hovbtnSave.SizeText = 24;
            hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
        }

        /// <summary>
        /// Back button pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            frmmenu.Show();
            this.Close();
        }

        /// <summary>
        /// Check keys pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                hovbtnBack_Click(sender, null);
            }
        }

        /// <summary>
        /// show FrmMenu again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmmenu.Show();
        }

        private void pnlAddRoad_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brushRoadLine = new SolidBrush(Color.White); // the color of the lines on the road
            int lineloc = (pnlAddRoad.ClientRectangle.Height / 2);
            for (int xpos = 0; xpos < pnlAddRoad.ClientRectangle.Width; xpos += 25)
            {
                Rectangle rectRoadLine = new Rectangle(xpos, lineloc, 10, 4);
                e.Graphics.FillRectangle(brushRoadLine, rectRoadLine);
            }
        }

        private void DelectAllTools()
        {
            pnlAddRoad.BackgroundImage = null;
            pnlAddRivir.BackgroundImage = null;
        }

        /// <summary>
        /// Item is selected and starting drag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDrag(object sender, MouseEventArgs e)
        {
            DelectAllTools();

            Panel selectitem = (Panel)sender;
            selectitem.BackgroundImageLayout = ImageLayout.Stretch;
            selectitem.BackgroundImage = Frogger.Properties.Resources.selecteditem;

            selectitem.DoDragDrop(1, DragDropEffects.Copy);
        }

        private void FrmLevelEditor_DragDrop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("loc. X:" + e.X + " Y:" + e.Y);
        }

        /// <summary>
        /// Save the made level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSave_Click(object sender, EventArgs e)
        {
            savinglevel = true;
            this.Refresh();
        }

        private void FrmLevelEditor_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void FrmLevelEditor_Paint(object sender, PaintEventArgs e)
        {
            //todo
            Graphics g = e.Graphics;

            if (savinglevel)
            {
                int margin = 20;
                Rectangle rect = new Rectangle(new Point(panelTools.Width + margin, margin), new Size(this.ClientRectangle.Width - panelTools.Width - (margin * 2), this.ClientRectangle.Height - (margin * 2)));
                g.DrawRectangle(Pens.Black, rect);
                g.FillRectangle(Brushes.GreenYellow, rect);

                this.DelectAllTools();

                this.hovbtnSave.Enabled = false;
                this.pnlAddRivir.Enabled = false;
                this.pnlAddRoad.Enabled = false;

                this.lblTextEnterNewFilename.Location = new Point(rect.X + (rect.Width / 2) - (lblTextEnterNewFilename.Width / 2), (rect.Height / 2) + rect.Y - 100);
                this.lblTextEnterNewFilename.Visible = true;

                this.bigTextboxFilename.Location = new Point(rect.X + (rect.Width / 2) - (bigTextboxFilename.Width / 2), (rect.Height / 2) + rect.Y - (bigTextboxFilename.Height / 2));
                this.bigTextboxFilename.Visible = true;

                this.lblExtension.Location = new Point(rect.X + (rect.Width / 2) + (bigTextboxFilename.Width / 2) + 5, (rect.Height / 2) + rect.Y - (bigTextboxFilename.Height / 2) + 15);
                this.lblExtension.Visible = true;

                this.hovbtnSaveFile.Location = new Point(rect.X + (rect.Width / 2) + (margin / 2), rect.Height - hovbtnSaveFile.Height - 20);
                this.hovbtnSaveFile.HoverbuttonText = "save!";
                this.hovbtnSaveFile.Visible = true;

                this.hovbtnCancelSave.Location = new Point(rect.X + (rect.Width / 2) - hovbtnCancelSave.Width - (margin / 2), rect.Height - hovbtnSaveFile.Height - 20);
                this.hovbtnCancelSave.HoverbuttonText = "cancel";
                this.hovbtnCancelSave.Visible = true;
            }
            else
            {
                this.hovbtnSave.Enabled = true;
                this.pnlAddRivir.Enabled = true;
                this.pnlAddRoad.Enabled = true;
                this.lblTextEnterNewFilename.Visible = false;
                this.bigTextboxFilename.Visible = false;
                this.lblExtension.Visible = false;
                this.hovbtnSaveFile.Visible = false;
                this.hovbtnCancelSave.Visible = false;
            }
        }

        private void hovbtnCancelSave_Click(object sender, EventArgs e)
        {
            savinglevel = false;
            this.Refresh();
        }

    }
}
