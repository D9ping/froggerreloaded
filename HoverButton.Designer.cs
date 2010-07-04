namespace Frogger
{
    partial class HoverButton
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
            this.lbButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbButton
            // 
            this.lbButton.BackColor = System.Drawing.Color.Transparent;
            this.lbButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbButton.Font = new System.Drawing.Font("Flubber", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbButton.Location = new System.Drawing.Point(0, 0);
            this.lbButton.Margin = new System.Windows.Forms.Padding(0);
            this.lbButton.Name = "lbButton";
            this.lbButton.Size = new System.Drawing.Size(320, 64);
            this.lbButton.TabIndex = 0;
            this.lbButton.Text = "?";
            this.lbButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbButton.MouseLeave += new System.EventHandler(this.HoverButton_MouseLeave);
            this.lbButton.Click += new System.EventHandler(this.lbButtonText_Click);
            this.lbButton.MouseEnter += new System.EventHandler(this.HoverButton_MouseEnter);
            // 
            // HoverButton
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbButton);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HoverButton";
            this.Size = new System.Drawing.Size(320, 64);
            this.MouseLeave += new System.EventHandler(this.HoverButton_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HoverButton_Paint);
            this.MouseEnter += new System.EventHandler(this.HoverButton_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbButton;
    }
}
