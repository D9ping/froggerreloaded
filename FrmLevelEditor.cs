using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Frogger
{
    public partial class FrmLevelEditor : Form
    {
        private Level level;
        private FrmMenu frmmenu;
        private bool savinglevel = false, savedlevel, namealreadyexist = false, toolselected = false;
        private int mouseY = 0;

        /// <summary>
        /// Creating an new instance of FrmLevelEditor.
        /// </summary>
        /// <param name="frmmenu"></param>
        public FrmLevelEditor(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;

            InitializeComponent();
            this.level = new Level(this.ClientRectangle.Width, this.ClientRectangle.Height);

            hovbtnBack.HoverbuttonText = "Back";
            hovbtnBack.SizeText = 24;
            hovbtnSave.HoverbuttonText = "Save";
            hovbtnSave.SizeText = 24;
            hovbtnOpen.HoverbuttonText = "Open";
            hovbtnOpen.SizeText = 24;
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

        /// <summary>
        /// paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Deselect all tools
        /// </summary>
        private void DelectAllTools()
        {
            pnlAddRoad.BackgroundImage = null;
            pnlAddRivir.BackgroundImage = null;
            toolselected = false;
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
            toolselected = true;
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

        private void FrmLevelEditor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            level.Draw(g);

            if (toolselected)
            {
                int heightmarkplace = this.ClientRectangle.Height / 10;
                int newlocy = CalcPos(mouseY);

                Rectangle rect1 = new Rectangle(new Point(0, newlocy ), new Size(this.ClientRectangle.Width, heightmarkplace));
                g.DrawRectangle(Pens.Green, rect1);
                g.FillRectangle(Brushes.LightYellow, rect1);
            }

            if (savinglevel)
            {
                int margin = 20;
                Rectangle rect = new Rectangle(new Point(panelTools.Width + margin, margin), new Size(this.ClientRectangle.Width - panelTools.Width - (margin * 2), this.ClientRectangle.Height - (margin * 2)));
                g.DrawRectangle(Pens.Black, rect);

                this.DelectAllTools();

                this.hovbtnSave.Enabled = false;
                this.hovbtnOpen.Enabled = false;
                this.pnlAddRivir.Enabled = false;
                this.pnlAddRoad.Enabled = false;

                if (!savedlevel)
                {
                    g.FillRectangle(Brushes.GreenYellow, rect);

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
                    this.lblTextEnterNewFilename.Visible = false;
                    this.bigTextboxFilename.Visible = false;
                    this.lblExtension.Visible = false;
                    this.hovbtnSaveFile.Visible = false;
                    this.hovbtnCancelSave.Visible = false;
                    if (namealreadyexist)
                    {
                        g.FillRectangle(Brushes.Orange, rect);
                        g.DrawString("Overwrite level?", new Font("Flubber", 32), Brushes.Black, new PointF(ClientRectangle.Width / 2, ClientRectangle.Height / 2));
                        //todo
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LawnGreen, rect);
                        g.DrawString("Saved", new Font("Flubber", 36), Brushes.Black, new PointF(ClientRectangle.Width / 2, ClientRectangle.Height / 2));
                    }
                }
            }
            else
            {
                this.hovbtnSave.Enabled = true;
                this.hovbtnOpen.Enabled = true;
                this.pnlAddRivir.Enabled = true;
                this.pnlAddRoad.Enabled = true;
                this.lblTextEnterNewFilename.Visible = false;
                this.bigTextboxFilename.Visible = false;
                this.lblExtension.Visible = false;
                this.hovbtnSaveFile.Visible = false;
                this.hovbtnCancelSave.Visible = false;
            }
        }

        private int CalcPos(int loc)
        {
            int heightmarkplace = this.ClientRectangle.Height / 10;
            int num = 0;
            for (int i = loc; i >= heightmarkplace; i -= heightmarkplace)
            {
                num++;
            }
            if (num == 0)
            {
                num = 1;
            }
            else if (num >= 9)
            {
                num = 8;
            }
            int pos = heightmarkplace * num;
            return pos;
        }

        private void hovbtnCancelSave_Click(object sender, EventArgs e)
        {
            savinglevel = false;
            this.Refresh();
        }

        /// <summary>
        /// Button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSaveFile_Click(object sender, EventArgs e)
        {
            string newfile = bigTextboxFilename.Text + ".lvl";
            if (File.Exists(newfile))
            {
                namealreadyexist = true;
            }

            //todo

            savedlevel = true;
            this.Refresh();
        }

        private void FrmLevelEditor_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void FrmLevelEditor_Click(object sender, EventArgs e)
        {
            if (savedlevel)
            {
                savedlevel = false;
                savinglevel = false;
                this.Refresh();
            }
            if (toolselected)
            {
                if (mouseY >= 0 && mouseY <= this.ClientRectangle.Height)
                {
                    int newpos = CalcPos(mouseY);
                    this.level.AddRivir(newpos);

                    MessageBox.Show(mouseY.ToString());
                }
            }
        }

        private void FrmLevelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            mouseY = e.Y;
            this.Refresh();
        }

    }
}
