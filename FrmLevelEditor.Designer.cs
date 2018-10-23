namespace Frogger
{
    partial class FrmLevelEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLevelEditor));
            this.panelTools = new System.Windows.Forms.Panel();
            this.hovbtnOpen = new Frogger.HoverButton();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.hovbtnSave = new Frogger.HoverButton();
            this.hovbtnBack = new Frogger.HoverButton();
            this.pnlAddRivir = new System.Windows.Forms.Panel();
            this.pnlAddRoad = new System.Windows.Forms.Panel();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblTextEnterNewFilename = new System.Windows.Forms.Label();
            this.lbxFiles = new System.Windows.Forms.ListBox();
            this.hovbtnDelete = new Frogger.HoverButton();
            this.hovbtnOpenFile = new Frogger.HoverButton();
            this.hovbtnCancelSave = new Frogger.HoverButton();
            this.hovbtnSaveFile = new Frogger.HoverButton();
            this.bigTextboxFilename = new Frogger.BigTextbox();
            this.panelTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.Color.ForestGreen;
            this.panelTools.Controls.Add(this.hovbtnOpen);
            this.panelTools.Controls.Add(this.lblInstructions);
            this.panelTools.Controls.Add(this.hovbtnSave);
            this.panelTools.Controls.Add(this.hovbtnBack);
            this.panelTools.Controls.Add(this.pnlAddRivir);
            this.panelTools.Controls.Add(this.pnlAddRoad);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTools.Location = new System.Drawing.Point(0, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(161, 562);
            this.panelTools.TabIndex = 0;
            // 
            // hovbtnOpen
            // 
            this.hovbtnOpen.AllowDrop = true;
            this.hovbtnOpen.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnOpen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnOpen.ForeColor = System.Drawing.Color.Black;
            this.hovbtnOpen.HoverbuttonSizeText = 32F;
            this.hovbtnOpen.HoverbuttonText = "open";
            this.hovbtnOpen.Location = new System.Drawing.Point(7, 295);
            this.hovbtnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnOpen.Name = "hovbtnOpen";
            this.hovbtnOpen.Size = new System.Drawing.Size(141, 72);
            this.hovbtnOpen.TabIndex = 9;
            this.hovbtnOpen.Click += new System.EventHandler(this.hovbtnOpen_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Flubber", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(3, 1);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(153, 66);
            this.lblInstructions.TabIndex = 8;
            this.lblInstructions.Text = "Select what you want to place";
            // 
            // hovbtnSave
            // 
            this.hovbtnSave.AllowDrop = true;
            this.hovbtnSave.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnSave.Enabled = false;
            this.hovbtnSave.ForeColor = System.Drawing.Color.Black;
            this.hovbtnSave.HoverbuttonSizeText = 32F;
            this.hovbtnSave.HoverbuttonText = "save";
            this.hovbtnSave.Location = new System.Drawing.Point(7, 370);
            this.hovbtnSave.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnSave.Name = "hovbtnSave";
            this.hovbtnSave.Size = new System.Drawing.Size(141, 72);
            this.hovbtnSave.TabIndex = 7;
            this.hovbtnSave.Click += new System.EventHandler(this.hovbtnSave_Click);
            // 
            // hovbtnBack
            // 
            this.hovbtnBack.AllowDrop = true;
            this.hovbtnBack.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnBack.ForeColor = System.Drawing.Color.Black;
            this.hovbtnBack.HoverbuttonSizeText = 32F;
            this.hovbtnBack.HoverbuttonText = "back";
            this.hovbtnBack.Location = new System.Drawing.Point(9, 456);
            this.hovbtnBack.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnBack.Name = "hovbtnBack";
            this.hovbtnBack.Size = new System.Drawing.Size(141, 75);
            this.hovbtnBack.TabIndex = 6;
            // 
            // pnlAddRivir
            // 
            this.pnlAddRivir.AccessibleDescription = "rivir";
            this.pnlAddRivir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAddRivir.BackColor = System.Drawing.Color.Blue;
            this.pnlAddRivir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddRivir.Font = new System.Drawing.Font("Flubber", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddRivir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlAddRivir.Location = new System.Drawing.Point(7, 165);
            this.pnlAddRivir.Name = "pnlAddRivir";
            this.pnlAddRivir.Size = new System.Drawing.Size(137, 86);
            this.pnlAddRivir.TabIndex = 5;
            this.pnlAddRivir.Tag = "2";
            this.pnlAddRivir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartDrag);
            // 
            // pnlAddRoad
            // 
            this.pnlAddRoad.AccessibleDescription = "road";
            this.pnlAddRoad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAddRoad.BackColor = System.Drawing.Color.Black;
            this.pnlAddRoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddRoad.Font = new System.Drawing.Font("Flubber", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddRoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlAddRoad.Location = new System.Drawing.Point(7, 70);
            this.pnlAddRoad.Name = "pnlAddRoad";
            this.pnlAddRoad.Size = new System.Drawing.Size(137, 89);
            this.pnlAddRoad.TabIndex = 4;
            this.pnlAddRoad.Tag = "1";
            this.pnlAddRoad.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAddRoad_Paint);
            this.pnlAddRoad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartDrag);
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.BackColor = System.Drawing.Color.Transparent;
            this.lblExtension.Font = new System.Drawing.Font("Flubber", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtension.Location = new System.Drawing.Point(611, 238);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(45, 28);
            this.lblExtension.TabIndex = 11;
            this.lblExtension.Text = ".lvl";
            this.lblExtension.Visible = false;
            // 
            // lblTextEnterNewFilename
            // 
            this.lblTextEnterNewFilename.AutoSize = true;
            this.lblTextEnterNewFilename.BackColor = System.Drawing.Color.Transparent;
            this.lblTextEnterNewFilename.Font = new System.Drawing.Font("Flubber", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextEnterNewFilename.Location = new System.Drawing.Point(230, 146);
            this.lblTextEnterNewFilename.Name = "lblTextEnterNewFilename";
            this.lblTextEnterNewFilename.Size = new System.Drawing.Size(408, 28);
            this.lblTextEnterNewFilename.TabIndex = 12;
            this.lblTextEnterNewFilename.Text = "Enter a new name for this level:";
            this.lblTextEnterNewFilename.Visible = false;
            // 
            // lbxFiles
            // 
            this.lbxFiles.BackColor = System.Drawing.Color.LimeGreen;
            this.lbxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxFiles.FormattingEnabled = true;
            this.lbxFiles.ItemHeight = 29;
            this.lbxFiles.Location = new System.Drawing.Point(180, 23);
            this.lbxFiles.Name = "lbxFiles";
            this.lbxFiles.Size = new System.Drawing.Size(458, 120);
            this.lbxFiles.TabIndex = 14;
            this.lbxFiles.Visible = false;
            this.lbxFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxFiles_MouseDoubleClick);
            this.lbxFiles.SelectedIndexChanged += new System.EventHandler(this.lbxFiles_SelectedIndexChanged);
            // 
            // hovbtnDelete
            // 
            this.hovbtnDelete.AllowDrop = true;
            this.hovbtnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hovbtnDelete.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnDelete.Enabled = false;
            this.hovbtnDelete.ForeColor = System.Drawing.Color.Black;
            this.hovbtnDelete.HoverbuttonSizeText = 20F;
            this.hovbtnDelete.HoverbuttonText = "delete";
            this.hovbtnDelete.Location = new System.Drawing.Point(417, 456);
            this.hovbtnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnDelete.Name = "hovbtnDelete";
            this.hovbtnDelete.Size = new System.Drawing.Size(93, 75);
            this.hovbtnDelete.TabIndex = 15;
            this.hovbtnDelete.Visible = false;
            this.hovbtnDelete.Click += new System.EventHandler(this.hovbtnDelete_Click);
            // 
            // hovbtnOpenFile
            // 
            this.hovbtnOpenFile.AllowDrop = true;
            this.hovbtnOpenFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hovbtnOpenFile.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnOpenFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnOpenFile.ForeColor = System.Drawing.Color.Black;
            this.hovbtnOpenFile.HoverbuttonSizeText = 42F;
            this.hovbtnOpenFile.HoverbuttonText = "Open";
            this.hovbtnOpenFile.Location = new System.Drawing.Point(546, 456);
            this.hovbtnOpenFile.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnOpenFile.Name = "hovbtnOpenFile";
            this.hovbtnOpenFile.Size = new System.Drawing.Size(207, 75);
            this.hovbtnOpenFile.TabIndex = 13;
            this.hovbtnOpenFile.Visible = false;
            this.hovbtnOpenFile.Click += new System.EventHandler(this.hovbtnOpenFile_Click);
            // 
            // hovbtnCancelSave
            // 
            this.hovbtnCancelSave.AllowDrop = true;
            this.hovbtnCancelSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hovbtnCancelSave.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnCancelSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnCancelSave.ForeColor = System.Drawing.Color.Black;
            this.hovbtnCancelSave.HoverbuttonSizeText = 36F;
            this.hovbtnCancelSave.HoverbuttonText = "cancel";
            this.hovbtnCancelSave.Location = new System.Drawing.Point(204, 456);
            this.hovbtnCancelSave.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnCancelSave.Name = "hovbtnCancelSave";
            this.hovbtnCancelSave.Size = new System.Drawing.Size(196, 75);
            this.hovbtnCancelSave.TabIndex = 10;
            this.hovbtnCancelSave.Visible = false;
            this.hovbtnCancelSave.Click += new System.EventHandler(this.hovbtnCancelSave_Click);
            // 
            // hovbtnSaveFile
            // 
            this.hovbtnSaveFile.AllowDrop = true;
            this.hovbtnSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hovbtnSaveFile.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnSaveFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnSaveFile.ForeColor = System.Drawing.Color.Black;
            this.hovbtnSaveFile.HoverbuttonSizeText = 42F;
            this.hovbtnSaveFile.Location = new System.Drawing.Point(487, 456);
            this.hovbtnSaveFile.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnSaveFile.Name = "hovbtnSaveFile";
            this.hovbtnSaveFile.Size = new System.Drawing.Size(220, 75);
            this.hovbtnSaveFile.TabIndex = 9;
            this.hovbtnSaveFile.Visible = false;
            this.hovbtnSaveFile.Click += new System.EventHandler(this.hovbtnSaveFile_Click);
            // 
            // bigTextboxFilename
            // 
            this.bigTextboxFilename.BackColor = System.Drawing.Color.Transparent;
            this.bigTextboxFilename.Location = new System.Drawing.Point(310, 219);
            this.bigTextboxFilename.Name = "bigTextboxFilename";
            this.bigTextboxFilename.Size = new System.Drawing.Size(295, 58);
            this.bigTextboxFilename.TabIndex = 1;
            this.bigTextboxFilename.Visible = false;
            // 
            // FrmLevelEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Frogger.Properties.Resources.texure_grass;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.lbxFiles);
            this.Controls.Add(this.hovbtnOpenFile);
            this.Controls.Add(this.lblTextEnterNewFilename);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.hovbtnCancelSave);
            this.Controls.Add(this.hovbtnSaveFile);
            this.Controls.Add(this.bigTextboxFilename);
            this.Controls.Add(this.panelTools);
            this.Controls.Add(this.hovbtnDelete);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmLevelEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FroggerReloaded Level Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLevelEditor_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmLevelEditor_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLevelEditor_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLevelEditor_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLevelEditor_KeyDown);
            this.ResizeEnd += new System.EventHandler(this.FrmLevelEditor_ResizeEnd);
            this.panelTools.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Panel pnlAddRoad;
        private System.Windows.Forms.Panel pnlAddRivir;
        private HoverButton hovbtnBack;
        private HoverButton hovbtnSave;
        private System.Windows.Forms.Label lblInstructions;
        private BigTextbox bigTextboxFilename;
        private HoverButton hovbtnSaveFile;
        private HoverButton hovbtnCancelSave;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblTextEnterNewFilename;
        private HoverButton hovbtnOpen;
        private HoverButton hovbtnOpenFile;
        private System.Windows.Forms.ListBox lbxFiles;
        private HoverButton hovbtnDelete;
    }
}