namespace Frogger
{
    partial class BigCheckbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbBigcheckbox = new System.Windows.Forms.Label();
            this.pbBigheck = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBigheck)).BeginInit();
            this.SuspendLayout();
            // 
            // lbBigcheckbox
            // 
            this.lbBigcheckbox.AutoSize = true;
            this.lbBigcheckbox.BackColor = System.Drawing.Color.Transparent;
            this.lbBigcheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBigcheckbox.Location = new System.Drawing.Point(65, 3);
            this.lbBigcheckbox.Name = "lbBigcheckbox";
            this.lbBigcheckbox.Size = new System.Drawing.Size(51, 55);
            this.lbBigcheckbox.TabIndex = 1;
            this.lbBigcheckbox.Text = "?";
            this.lbBigcheckbox.Click += new System.EventHandler(this.pbBigheck_Click);
            // 
            // pbBigheck
            // 
            this.pbBigheck.Image = global::Frogger.Properties.Resources.checkbox_on;
            this.pbBigheck.Location = new System.Drawing.Point(3, 3);
            this.pbBigheck.Name = "pbBigheck";
            this.pbBigheck.Size = new System.Drawing.Size(61, 55);
            this.pbBigheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBigheck.TabIndex = 0;
            this.pbBigheck.TabStop = false;
            this.pbBigheck.Click += new System.EventHandler(this.pbBigheck_Click);
            // 
            // BigCheckbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbBigcheckbox);
            this.Controls.Add(this.pbBigheck);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "BigCheckbox";
            this.Size = new System.Drawing.Size(342, 62);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BigCheckbox_Paint);
            this.Click += new System.EventHandler(this.pbBigheck_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbBigheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBigheck;
        private System.Windows.Forms.Label lbBigcheckbox;
    }
}
