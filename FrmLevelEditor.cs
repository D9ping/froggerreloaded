/*
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
#define windows //platform

namespace Frogger
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class FrmLevelEditor : Form
    {
        private Level level;
        private FrmMenu frmmenu;
        private Timer redrawtmr;
        private bool savinglevel = false, savedlevel, namealreadyexist = false, toolselected = false, openinglevel = false, lvlchanged = false;
        private int mouseY = 0, mouseX = 0, selecteditemnr = -1;
        private const int margin = 20, padding = 50;

        /// <summary>
        /// Creating an new instance of FrmLevelEditor.
        /// </summary>
        /// <param name="frmmenu"></param>
        public FrmLevelEditor(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;

            InitializeComponent();

            this.hovbtnBack.HoverbuttonText = "Back";
            this.hovbtnBack.HoverbuttonSizeText = 24;
            this.hovbtnSave.HoverbuttonText = "Save";
            this.hovbtnSave.HoverbuttonSizeText = 24;
            this.hovbtnOpen.HoverbuttonText = "Open";
            this.hovbtnOpen.HoverbuttonSizeText = 24;
            this.hovbtnOpenFile.HoverbuttonText = "Open";
            this.hovbtnOpenFile.HoverbuttonSizeText = 24;
            this.hovbtnCancelSave.HoverbuttonText = "cancel";
            this.hovbtnCancelSave.HoverbuttonSizeText = 24;

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
            lblInstructions.Text = "Select what you want to place";
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
            lblInstructions.Text = "Click where you want it.";
        }

        /// <summary>
        /// Save the made level.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSave_Click(object sender, EventArgs e)
        {
            savinglevel = true;
            lblTextEnterNewFilename.Text = "Enter a new name for this level:";
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
                        g.DrawString("Overwrite level?", new Font("Flubber", 32), Brushes.Black, new PointF((this.ClientRectangle.Width / 2) - 100, this.ClientRectangle.Height / 2));

                        this.hovbtnSaveFile.Visible = true;
                        this.hovbtnCancelSave.Visible = true;
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LawnGreen, rect);
                        g.DrawString("Saved", new Font("Flubber", 36), Brushes.Black, new PointF(ClientRectangle.Width / 2, ClientRectangle.Height / 2));
                        lvlchanged = false;
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
                this.hovbtnCancelSave.Visible = true;
                this.hovbtnCancelSave.Location = new Point(rectopenbox.X + (rectopenbox.Width / 2) - (hovbtnCancelSave.Width + hovbtnDelete.Width) + margin, rectopenbox.Height - hovbtnSaveFile.Height);

                this.hovbtnDelete.Location = new Point(rectopenbox.X + (rectopenbox.Width / 2) - hovbtnDelete.Width + (margin * 2), rectopenbox.Height - hovbtnSaveFile.Height);
                
                this.hovbtnOpenFile.Visible = true;
                this.hovbtnOpenFile.Location = new Point(rectopenbox.X + (rectopenbox.Width / 2) + (margin * 3), rectopenbox.Height - hovbtnSaveFile.Height);
                
            }
            else
            {
                if (lvlchanged)
                {
                    this.hovbtnSave.Enabled = true;
                }

                this.hovbtnOpen.Enabled = true;
                this.pnlAddRivir.Enabled = true;
                this.pnlAddRoad.Enabled = true;

                this.hovbtnDelete.Visible = false;
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
            int heightmarkplace = CalcPieceHeight();
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

        private int CalcPieceHeight()
        {
            int heightmarkplace = this.ClientRectangle.Height / 10;
            return heightmarkplace;
        }

        /// <summary>
        /// Cancel to save level
        /// </summary>
        private void hovbtnCancelSave_Click(object sender, EventArgs e)
        {
            openinglevel = false;
            savedlevel = false;
            savinglevel = false;
            namealreadyexist = false;
            this.Refresh();
        }

        /// <summary>
        /// Button save file clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnSaveFile_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(bigTextboxFilename.Text))
            {
                string lvldir = Program.GetLevelFolder();
                string newfile = bigTextboxFilename.Text + ".lvl";
                if ((File.Exists(Path.Combine(lvldir, newfile)) == true) && (namealreadyexist == false))
                {
                    namealreadyexist = true;
                    savedlevel = true;
                }
                else
                {
                    savedlevel = level.SaveDesign(this.bigTextboxFilename.Text);
                    namealreadyexist = false;
                }
                this.Refresh();
            }
            else
            {
                lblTextEnterNewFilename.Text = "Enter a filename!";
            }
        }

        /// <summary>
        /// End scaling window, call paint event.
        /// </summary>
        private void FrmLevelEditor_ResizeEnd(object sender, EventArgs e)
        {
            level.SetLevelSize(this.ClientRectangle.Width, this.ClientRectangle.Height, false);
            this.Refresh();
        }

        /// <summary>
        /// There is clicked in the leveleditor, figure out if a tool is selected
        /// if so what tools and where then place it.
        private void FrmLevelEditor_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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
                                break;
                            case 2:
                                this.level.AddRivir(newpos);
                                break;
                            default:
                                return;
                        }
                        this.DeselectAllTools();
                        this.lvlchanged = true;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                int pieceheight = CalcPieceHeight();
                if ((mouseY >= pieceheight) && (mouseY <= (this.ClientRectangle.Height - pieceheight)) && (mouseX >= this.panelTools.Width))
                {
                    int atpos = CalcPos(mouseY);
                    this.level.RemoveObj(atpos);
                    ////this.DeselectAllTools();
                    this.lvlchanged = true;
                }
            }
        }

        /// <summary>
        /// Mouse is moved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouseX = e.X;
            this.mouseY = e.Y;
            redrawtmr.Enabled = true;
        }

        /// <summary>
        /// Their is requested to open a level.
        /// -Read all .lvl files in the level directory and added them to lbxFiles.
        ///  except for readonly levels.
        /// -Put lbxFiles on the right place with the right size.
        /// </summary>
        /// <param name="sender">
        /// A <see cref="System.Object"/>
        /// </param>
        /// <param name="e">
        /// A <see cref="EventArgs"/>
        /// </param>
        private void hovbtnOpen_Click(object sender, EventArgs e)
        {
            string lvldir = Program.GetLevelFolder(); //Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "levels");

            DirectoryInfo lvldirinfo = new DirectoryInfo(lvldir);
            List<FileInfo> files = new List<FileInfo>();
            List<string> levelnamen = new List<string>();
            files.AddRange(lvldirinfo.GetFiles("*.lvl"));
            for (int i = 0; i < files.Count; i++)
            {
#if windows
                if (!files[i].IsReadOnly)
                {
                    levelnamen.Add(files[i].Name.Substring(0, files[i].Name.Length - 4));
                }
#elif linux
                levelnamen.Add(files[i].Name.Substring(0,files[i].Name.Length-4));
#endif
            }
            this.lbxFiles.Items.Clear();

            foreach (string levelnaam in levelnamen)
            {
                this.lbxFiles.Items.Add(levelnaam);
            }
            this.hovbtnDelete.Visible = true;
            this.lbxFiles.Location = new Point(panelTools.Width + margin + padding, margin + padding);
            int widthfileslist = this.ClientRectangle.Width - panelTools.Width - (margin * 2) - (padding * 2);
            int heightfileslist = this.ClientRectangle.Height - (margin * 2) - this.hovbtnOpenFile.Height - (padding * 2);
            this.lbxFiles.Size = new Size(widthfileslist, heightfileslist);
            openinglevel = true;
            this.Refresh();
        }

        /// <summary>
        /// Requesten to open the selected file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnOpenFile_Click(object sender, EventArgs e)
        {
            if (lbxFiles.SelectedIndex >= 0)
            {
                openinglevel = false;
                this.level = new Level(lbxFiles.SelectedItem.ToString(), this.ClientRectangle.Width, ClientRectangle.Height - 2);
                this.lvlchanged = false;
                this.Refresh();
            }
            else
            {
                lblInstructions.Text = "Please select a file to open.";
            }
        }

        private void lbxFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            hovbtnOpenFile_Click(sender, null);
        }

        /// <summary>
        /// Delete the selected level file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hovbtnDelete_Click(object sender, EventArgs e)
        {
            if (lbxFiles.SelectedIndex >= 0)
            {
                DialogResult dlgresdelete = MessageBox.Show("Are you sure you want to\r\ndelete " + lbxFiles.SelectedItem.ToString() + " ?", "delete level", MessageBoxButtons.YesNo);
                if (dlgresdelete == DialogResult.Yes)
                {
                    string levelfile = Path.Combine(Program.GetLevelFolder(), lbxFiles.SelectedItem.ToString() + ".lvl");
                    if (File.Exists(levelfile))
                    {
                        FileInfo fi = new FileInfo(levelfile);
                        if (fi.Attributes == FileAttributes.System)
                        {
                            MessageBox.Show("Deletion aborted, level file appears to be a system file!", "fatal error");
                            return;
                        }
                        try
                        {
                            File.Delete(levelfile);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "error");
                        }
                        hovbtnOpen_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Level file is missing.", "error");
                    }
                }
            }
        }

        private void lbxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hovbtnDelete.Enabled = false;
            if (this.lbxFiles.SelectedIndex >= 0)
            {
                this.hovbtnDelete.Enabled = true;
            }
        }
    }
}
