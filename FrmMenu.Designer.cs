namespace Frogger
{
    partial class FrmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.pbKikker = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.cbxTier = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbKikker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbKikker
            // 
            this.pbKikker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbKikker.BackColor = System.Drawing.Color.Green;
            this.pbKikker.Image = global::Frogger.Properties.Resources.kikker_west;
            this.pbKikker.Location = new System.Drawing.Point(646, 40);
            this.pbKikker.Name = "pbKikker";
            this.pbKikker.Size = new System.Drawing.Size(146, 166);
            this.pbKikker.TabIndex = 1;
            this.pbKikker.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbLogo.Image = global::Frogger.Properties.Resources.logo_frogger_reloaded;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(792, 206);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // cbxTier
            // 
            this.cbxTier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(205)))), ((int)(((byte)(50)))));
            this.cbxTier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTier.Font = new System.Drawing.Font("Flubber", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTier.FormattingEnabled = true;
            this.cbxTier.Items.AddRange(new object[] {
            "Freeplay",
            "Easy",
            "Medium",
            "Hard",
            "Elite"});
            this.cbxTier.Location = new System.Drawing.Point(588, 515);
            this.cbxTier.Name = "cbxTier";
            this.cbxTier.Size = new System.Drawing.Size(167, 36);
            this.cbxTier.TabIndex = 2;
            this.cbxTier.Visible = false;
            this.cbxTier.VisibleChanged += new System.EventHandler(this.cbxTier_VisibleChanged);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.cbxTier);
            this.Controls.Add(this.pbKikker);
            this.Controls.Add(this.pbLogo);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmMenu";
            this.Text = "Frogger Reloaded";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMenu_Paint);
            this.ResizeEnd += new System.EventHandler(this.FrmMenu_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.pbKikker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pbKikker;
        private System.Windows.Forms.ComboBox cbxTier;
    }
}

