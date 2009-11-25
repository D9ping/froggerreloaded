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
            this.pbKnop = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbKnop)).BeginInit();
            this.SuspendLayout();
            // 
            // pbKnop
            // 
            this.pbKnop.Location = new System.Drawing.Point(0, 0);
            this.pbKnop.Name = "pbKnop";
            this.pbKnop.Size = new System.Drawing.Size(100, 50);
            this.pbKnop.TabIndex = 0;
            this.pbKnop.TabStop = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbKnop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbKnop;

    }
}
