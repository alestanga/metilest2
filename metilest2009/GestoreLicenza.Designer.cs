/*
  Copyright 2009-2015 Alessandro Stanga
  This file is part of metilest2.

   metilest2 is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   metilest2 is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with metilest2.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace metilest2009
{
    partial class GestoreLicenza
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblModo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnInsLicenza = new System.Windows.Forms.Button();
            this.txtLicenza = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblModo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.btnInsLicenza);
            this.splitContainer1.Panel2.Controls.Add(this.txtLicenza);
            this.splitContainer1.Size = new System.Drawing.Size(292, 266);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = true;
            this.lblModo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModo.Location = new System.Drawing.Point(0, 0);
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(32, 13);
            this.lblModo.TabIndex = 0;
            this.lblModo.Text = "Stato";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 130);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Chiudi";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnInsLicenza
            // 
            this.btnInsLicenza.Location = new System.Drawing.Point(174, 130);
            this.btnInsLicenza.Name = "btnInsLicenza";
            this.btnInsLicenza.Size = new System.Drawing.Size(106, 23);
            this.btnInsLicenza.TabIndex = 1;
            this.btnInsLicenza.Text = "Inserisci Licenza";
            this.btnInsLicenza.UseVisualStyleBackColor = true;
            this.btnInsLicenza.Click += new System.EventHandler(this.btnInsLicenza_Click_1);
            // 
            // txtLicenza
            // 
            this.txtLicenza.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLicenza.Location = new System.Drawing.Point(0, 0);
            this.txtLicenza.Name = "txtLicenza";
            this.txtLicenza.Size = new System.Drawing.Size(292, 20);
            this.txtLicenza.TabIndex = 0;
            // 
            // GestoreLicenza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GestoreLicenza";
            this.Text = "Licenza";
            this.Load += new System.EventHandler(this.GestoreLicenza_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GestoreLicenza_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnInsLicenza;
        private System.Windows.Forms.TextBox txtLicenza;

    }
}