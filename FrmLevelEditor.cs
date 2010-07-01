using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmLevelEditor : Form
    {
        private Level level;
        private FrmMenu frmmenu;
        private Timer redrawtmr;
        private bool savinglevel = false, savedlevel, namealreadyexist = false, toolselected = false, openinglevel = false;
        private int mouseY = 0, mouseX = 0, selecteditemnr = -1;

        /// <summary>
        /// Creating an new instance of FrmLevelEditor.
        /// </summary>
        /// <param name="frmmenu"></param>
        public FrmLevelEditor(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;

            InitializeComponent();

            hovbtnBack.HoverbuttonText = "Back";
            hovbtnBack.SizeText = 24;
            hovbtnSave.HoverbuttonText = "Save";
            hovbtnSave.SizeText = 24;
            hovbtnOpen.HoverbuttonText = "Open";
            hovbtnOpen.SizeText = 24;

            this.level = new Level(this.ClientRectangle.Width, this.ClientRectangle.Height);

            hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
            redrawtmr = new Timer();
            redrawtmr.Enabled = false;
            redrawtmr.Interval = 50;
            redrawtmr.Tick += delegate(object sender, EventArgs e)
            {
                this.Refresh();
                redrawtmr.Enabled = false;
            };
        }

        /// <summary>
        /// Back button pressed. Disable the redraw timer make main menu show up again.
        /// And then close this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            redrawtmr.Enabled = false;
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
        /// Show FrmMenu again, make also sure redrawtmr is disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            redrawtmr.Enabled = false;
            frmmenu.Show();
        }

        /// <summary>
        /// Draw a road on this panel.
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
        private void DeselectAllTools()
        {
            pnlAddRoad.BackgroundImage = null;
            pnlAddRivir.BackgroundImage = null;
            toolselected = false;
            this.selecteditemnr = -1;
        }

        /// <summary>
        /// Item is selected and starting drag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDrag(object sender, MouseEventArgs e)
        {
            DeselectAllTools();

            Panel selectitem = (Panel)sender;
            selectitem.BackgroundImageLayout = ImageLayout.Stretch;
            selectitem.BackgroundImage = Frogger.Properties.Resources.selecteditem;
            this.toolselected = true;
            this.selecteditemnr = Convert.ToInt32(selectitem.Tag);
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

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            level.Draw(g);

            if (toolselected)
            {
                int heightmarkplace = this.ClientRectangle.Height / 10;
                int newlocy = CalcPos(mouseY);

                Rectangle rect1 = new Rectangle(new Point(0, newlocy), new Size(this.ClientRectangle.Width, heightmarkplace));
                g.DrawRectangle(Pens.Green, rect1);
                g.FillRectangle(Brushes.LightGreen, rect1);
            }

            if (savinglevel)
            {
                int margin = 20;
                Rectangle rect = new Rectangle(new Point(panelTools.Width + margin, margin), new Size(this.ClientRectangle.Width - panelTools.Width - (margin * 2), this.ClientRectangle.Height - (margin * 2)));
                g.DrawRectangle(Pens.Black, rect);

                this.DeselectAllTools();
                this.DisableOpenSaveEtc();

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
                        //todo: add yes and no
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LawnGreen, rect);
                        g.DrawString("Saved", new Font("Flubber", 36), Brushes.Black, new PointF(ClientRectangle.Width / 2, ClientRectangle.Height / 2));
                    }
                }
            }
            else if (openinglevel)
            {
                int margin = 20;
                Rectangle rectopenbox = new Rectangle(new Point(panelTools.Width + margin, margin), new Size(this.ClientRectangle.Width - panelTools.Width - (margin * 2), this.ClientRectangle.Height - (margin * 2)));
                g.DrawRectangle(Pens.Black, rectopenbox);
                g.FillRectangle(Brushes.Orange, rectopenbox);
                this.DeselectAllTools();
                this.DisableOpenSaveEtc();

                this.lbxFiles.Visible = true;
                int padding = 50;
                this.lbxFiles.Location = new Point(rectopenbox.X + padding, rectopenbox.Y + padding);
                this.lbxFiles.Size = new Size(rectopenbox.Width - (padding*2), rectopenbox.Height - this.hovbtnOpenFile.Height - (padding * 2));

                hovbtnOpenFile.Visible = true;
                hovbtnOpenFile.HoverbuttonText = "Open";
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
                this.hovbtnOpenFile.Visible = false;
                this.lbxFiles.Visible = false;
            }
        }

        private void DisableOpenSaveEtc()
        {
            this.hovbtnSave.Enabled = false;
            this.hovbtnOpen.Enabled = false;
            this.pnlAddRivir.Enabled = false;
            this.pnlAddRoad.Enabled = false;

        }

        /// <summary>
        /// Calculate the position of the new road or rivir etc.
        /// </summary>
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

        /// <summary>
        /// Cancel to save level
        /// </summary>
        private void hovbtnCancelSave_Click(object sender, EventArgs e)
        {
            savinglevel = false;
            this.Refresh();
        }

        /// <summary>
        /// Button save file clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSaveFile_Click(object sender, EventArgs e)
        {
            string appdir = Path.GetDirectoryName(Application.ExecutablePath);
            string newfile = bigTextboxFilename.Text + ".lvl";
            if (File.Exists(Path.Combine(appdir, newfile)))
            {
                namealreadyexist = true;
            }
            savedlevel = level.SaveDesign(this.bigTextboxFilename.Text);
            this.Refresh();
        }

        /// <summary>
        /// End scaling window, call paint event
        /// </summary>
        private void FrmLevelEditor_ResizeEnd(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// There is clicked in the leveleditor, figure out if a tool is selected
        /// if so what tools and where then place it.
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
                if (mouseY >= 0 && mouseY <= this.ClientRectangle.Height && mouseX >= this.panelTools.Width)
                {
                    int newpos = CalcPos(mouseY);
                    switch (selecteditemnr)
                    {
                        case 1:
                            this.level.AddRoad(newpos);
                            this.DeselectAllTools();
                            break;
                        case 2:
                            this.level.AddRivir(newpos);
                            this.DeselectAllTools();
                            break;
                        default:
                            //add nothing
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// mouse is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseX = e.X;
            this.mouseY = e.Y;
            redrawtmr.Enabled = true;
        }

        private void hovbtnOpen_Click(object sender, EventArgs e)
        {
            string lvldir = Path.GetDirectoryName(Application.ExecutablePath) + @"\levels\";
            if (Directory.Exists(lvldir))
            {
                DirectoryInfo lvldirinfo = new DirectoryInfo(lvldir);
                this.lbxFiles.Items.AddRange(lvldirinfo.GetFiles("*.lvl"));
            }
            else 
            {
                throw new Exception("level folder not found.");
            }
            openinglevel = true;
            this.Refresh();
        }

        private void hovbtnOpenFile_Click(object sender, EventArgs e)
        {
            openinglevel = false;
            //this.level = new Level("basic1", this.ClientRectangle.Width, ClientRectangle.Height - 2);
            this.Refresh();
        }
    }
}
