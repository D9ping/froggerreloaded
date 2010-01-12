namespace Frogger
{
    partial class FrmGame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGame));
            this.timerUpdateGame = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.btnTestGameOver = new System.Windows.Forms.Button();
            this.tbHighscoreName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timerUpdateGame
            // 
            this.timerUpdateGame.Enabled = true;
            this.timerUpdateGame.Interval = 1000;
            this.timerUpdateGame.Tick += new System.EventHandler(this.timerUpdateGame_Tick);
            // 
            // lbTime
            // 
            this.lbTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTime.AutoSize = true;
            this.lbTime.BackColor = System.Drawing.Color.Transparent;
            this.lbTime.Font = new System.Drawing.Font("Flubber", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.LightGray;
            this.lbTime.Location = new System.Drawing.Point(703, 9);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(78, 33);
            this.lbTime.TabIndex = 0;
            this.lbTime.Text = "0:00";
            // 
            // btnTestGameOver
            // 
            this.btnTestGameOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestGameOver.Location = new System.Drawing.Point(94, 551);
            this.btnTestGameOver.Name = "btnTestGameOver";
            this.btnTestGameOver.Size = new System.Drawing.Size(50, 49);
            this.btnTestGameOver.TabIndex = 1;
            this.btnTestGameOver.Text = "TEST Game Over";
            this.btnTestGameOver.UseVisualStyleBackColor = true;
            this.btnTestGameOver.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbHighscoreName
            // 
            this.tbHighscoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHighscoreName.Location = new System.Drawing.Point(225, 327);
            this.tbHighscoreName.Name = "tbHighscoreName";
            this.tbHighscoreName.Size = new System.Drawing.Size(284, 44);
            this.tbHighscoreName.TabIndex = 2;
            this.tbHighscoreName.Text = "anoniem";
            this.tbHighscoreName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHighscoreName.Visible = false;
            // 
            // FrmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Green;
            this.BackgroundImage = global::Frogger.Properties.Resources.texure_grass;
            this.ClientSize = new System.Drawing.Size(794, 612);
            this.Controls.Add(this.tbHighscoreName);
            this.Controls.Add(this.btnTestGameOver);
            this.Controls.Add(this.lbTime);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 640);
            this.Name = "FrmGame";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGame";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmGame_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGame_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.FrmGame_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Timer timerUpdateGame;
#if DEBUG
        private System.Windows.Forms.Button btnTestGameOver;
        private System.Windows.Forms.TextBox tbHighscoreName;
#endif

    }
}