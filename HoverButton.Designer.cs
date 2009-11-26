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
            this.lbButtonText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbButtonText
            // 
            this.lbButtonText.BackColor = System.Drawing.Color.Transparent;
            this.lbButtonText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbButtonText.Font = new System.Drawing.Font("Flubber", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbButtonText.Location = new System.Drawing.Point(0, 0);
            this.lbButtonText.Margin = new System.Windows.Forms.Padding(0);
            this.lbButtonText.Name = "lbButtonText";
            this.lbButtonText.Size = new System.Drawing.Size(300, 72);
            this.lbButtonText.TabIndex = 0;
            this.lbButtonText.Text = "?";
            this.lbButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbButtonText.MouseLeave += new System.EventHandler(this.HoverButton_MouseLeave);
            this.lbButtonText.MouseEnter += new System.EventHandler(this.HoverButton_MouseEnter);
            // 
            // HoverButton
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbButtonText);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HoverButton";
            this.Size = new System.Drawing.Size(300, 72);
            this.MouseLeave += new System.EventHandler(this.HoverButton_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HoverButton_Paint);
            this.MouseEnter += new System.EventHandler(this.HoverButton_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbButtonText;
    }
}
