namespace Frogger
{
    partial class LevelPreview
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
            this.SuspendLayout();
            // 
            // LevelPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.Name = "LevelPreview";
            this.Size = new System.Drawing.Size(280, 280);
            this.DoubleClick += new System.EventHandler(this.LevelPreview_DoubleClick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LevelPreview_Paint);
            this.Leave += new System.EventHandler(this.LevelPreview_Leave);
            this.Enter += new System.EventHandler(this.LevelPreview_Enter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
