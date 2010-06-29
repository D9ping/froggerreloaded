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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.pnlAddRivir = new System.Windows.Forms.Panel();
            this.pnlAddRoad = new System.Windows.Forms.Panel();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblTextEnterNewFilename = new System.Windows.Forms.Label();
            this.hovbtnCancelSave = new Frogger.HoverButton();
            this.hovbtnSaveFile = new Frogger.HoverButton();
            this.bigTextboxFilename = new Frogger.BigTextbox();
            this.hovbtnSave = new Frogger.HoverButton();
            this.hovbtnBack = new Frogger.HoverButton();
            this.hovbtnOpen = new Frogger.HoverButton();
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
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Flubber", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(3, 1);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(153, 66);
            this.lblInstructions.TabIndex = 8;
            this.lblInstructions.Text = "Drag and drop where you want a rivir or road";
            // 
            // pnlAddRivir
            // 
            this.pnlAddRivir.AccessibleDescription = "rivir";
            this.pnlAddRivir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAddRivir.BackColor = System.Drawing.Color.Blue;
            this.pnlAddRivir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddRivir.Font = new System.Drawing.Font("Flubber", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddRivir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlAddRivir.Location = new System.Drawing.Point(7, 180);
            this.pnlAddRivir.Name = "pnlAddRivir";
            this.pnlAddRivir.Size = new System.Drawing.Size(137, 86);
            this.pnlAddRivir.TabIndex = 5;
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
            this.pnlAddRoad.Location = new System.Drawing.Point(7, 85);
            this.pnlAddRoad.Name = "pnlAddRoad";
            this.pnlAddRoad.Size = new System.Drawing.Size(137, 89);
            this.pnlAddRoad.TabIndex = 4;
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
            // hovbtnCancelSave
            // 
            this.hovbtnCancelSave.AllowDrop = true;
            this.hovbtnCancelSave.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnCancelSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnCancelSave.ForeColor = System.Drawing.Color.Black;
            this.hovbtnCancelSave.HoverbuttonText = "?";
            this.hovbtnCancelSave.Location = new System.Drawing.Point(235, 456);
            this.hovbtnCancelSave.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnCancelSave.Name = "hovbtnCancelSave";
            this.hovbtnCancelSave.Size = new System.Drawing.Size(220, 53);
            this.hovbtnCancelSave.TabIndex = 10;
            this.hovbtnCancelSave.Visible = false;
            this.hovbtnCancelSave.Click += new System.EventHandler(this.hovbtnCancelSave_Click);
            // 
            // hovbtnSaveFile
            // 
            this.hovbtnSaveFile.AllowDrop = true;
            this.hovbtnSaveFile.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnSaveFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnSaveFile.ForeColor = System.Drawing.Color.Black;
            this.hovbtnSaveFile.HoverbuttonText = "?";
            this.hovbtnSaveFile.Location = new System.Drawing.Point(487, 456);
            this.hovbtnSaveFile.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnSaveFile.Name = "hovbtnSaveFile";
            this.hovbtnSaveFile.Size = new System.Drawing.Size(220, 53);
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
            // hovbtnSave
            // 
            this.hovbtnSave.AllowDrop = true;
            this.hovbtnSave.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnSave.ForeColor = System.Drawing.Color.Black;
            this.hovbtnSave.HoverbuttonText = "?";
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
            this.hovbtnBack.HoverbuttonText = "?";
            this.hovbtnBack.Location = new System.Drawing.Point(9, 456);
            this.hovbtnBack.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnBack.Name = "hovbtnBack";
            this.hovbtnBack.Size = new System.Drawing.Size(141, 75);
            this.hovbtnBack.TabIndex = 6;
            // 
            // hovbtnOpen
            // 
            this.hovbtnOpen.AllowDrop = true;
            this.hovbtnOpen.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnOpen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnOpen.ForeColor = System.Drawing.Color.Black;
            this.hovbtnOpen.HoverbuttonText = "?";
            this.hovbtnOpen.Location = new System.Drawing.Point(7, 295);
            this.hovbtnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnOpen.Name = "hovbtnOpen";
            this.hovbtnOpen.Size = new System.Drawing.Size(141, 72);
            this.hovbtnOpen.TabIndex = 9;
            // 
            // FrmLevelEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Frogger.Properties.Resources.texure_grass;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.lblTextEnterNewFilename);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.hovbtnCancelSave);
            this.Controls.Add(this.hovbtnSaveFile);
            this.Controls.Add(this.bigTextboxFilename);
            this.Controls.Add(this.panelTools);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmLevelEditor";
            this.Text = "FroggerReloaded Level Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmLevelEditor_Paint);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmLevelEditor_DragDrop);
            this.Click += new System.EventHandler(this.FrmLevelEditor_Click);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmLevelEditor_DragEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLevelEditor_FormClosing);
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
    }
}