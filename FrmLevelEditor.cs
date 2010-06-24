﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmLevelEditor : Form
    {
        private GameEngine game;
        private FrmMenu frmmenu;

        public FrmLevelEditor(FrmMenu frmmenu)
        {
            this.frmmenu = frmmenu;

            InitializeComponent();
            game = new GameEngine("new", this, Niveau.freeplay);

            hovbtnBack.HoverbuttonText = "Back";
            hovbtnBack.SizeText = 24;
            hovbtnSave.HoverbuttonText = "Save";
            hovbtnSave.SizeText = 24;
            hovbtnBack.Click += new EventHandler(hovbtnBack_Click);
        }

        private void hovbtnBack_Click(object sender, EventArgs e)
        {
            frmmenu.Show();
            this.Close();
        }
    }
}