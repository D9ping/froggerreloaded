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
            this.panelTools = new System.Windows.Forms.Panel();
            this.pnlAddRivir = new System.Windows.Forms.Panel();
            this.pnlAddRoad = new System.Windows.Forms.Panel();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.hovbtnSave = new Frogger.HoverButton();
            this.hovbtnBack = new Frogger.HoverButton();
            this.panelTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.Color.ForestGreen;
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
            // pnlAddRivir
            // 
            this.pnlAddRivir.AccessibleDescription = "rivir";
            this.pnlAddRivir.BackColor = System.Drawing.Color.Blue;
            this.pnlAddRivir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddRivir.Font = new System.Drawing.Font("Flubber", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddRivir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlAddRivir.Location = new System.Drawing.Point(9, 165);
            this.pnlAddRivir.Name = "pnlAddRivir";
            this.pnlAddRivir.Size = new System.Drawing.Size(137, 86);
            this.pnlAddRivir.TabIndex = 5;
            // 
            // pnlAddRoad
            // 
            this.pnlAddRoad.AccessibleDescription = "road";
            this.pnlAddRoad.BackColor = System.Drawing.Color.Black;
            this.pnlAddRoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddRoad.Font = new System.Drawing.Font("Flubber", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddRoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlAddRoad.Location = new System.Drawing.Point(9, 70);
            this.pnlAddRoad.Name = "pnlAddRoad";
            this.pnlAddRoad.Size = new System.Drawing.Size(137, 89);
            this.pnlAddRoad.TabIndex = 4;
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Flubber", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(5, 9);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(153, 66);
            this.lblInstructions.TabIndex = 8;
            this.lblInstructions.Text = "Drag and drop where you want a rivir or road";
            // 
            // hovbtnSave
            // 
            this.hovbtnSave.AllowDrop = true;
            this.hovbtnSave.BackColor = System.Drawing.Color.LimeGreen;
            this.hovbtnSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hovbtnSave.ForeColor = System.Drawing.Color.Black;
            this.hovbtnSave.HoverbuttonText = "?";
            this.hovbtnSave.Location = new System.Drawing.Point(9, 370);
            this.hovbtnSave.Margin = new System.Windows.Forms.Padding(0);
            this.hovbtnSave.Name = "hovbtnSave";
            this.hovbtnSave.Size = new System.Drawing.Size(141, 72);
            this.hovbtnSave.TabIndex = 7;
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
            // FrmLevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Frogger.Properties.Resources.texure_grass;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panelTools);
            this.Name = "FrmLevelEditor";
            this.Text = "FrmLevelEditor";
            this.panelTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Panel pnlAddRoad;
        private System.Windows.Forms.Panel pnlAddRivir;
        private HoverButton hovbtnBack;
        private HoverButton hovbtnSave;
        private System.Windows.Forms.Label lblInstructions;
    }
}