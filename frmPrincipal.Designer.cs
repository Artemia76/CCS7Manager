/****************************************************************************
**
** Copyright (C) 2019 Gianni Peschiutta (F4IKZ).
** Contact: neophile76@gmail.com
**
** CCS7Manager is free software; you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation; either version 3 of the License, or
** (at your option) any later version.
**
** CCS7Manager is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
** GNU General Public License for more details.
**
** The license is as published by the Free Software
** Foundation and appearing in the file LICENSE.GPL3
** included in the packaging of this software. Please review the following
** information to ensure the GNU General Public License requirements will
** be met: https://www.gnu.org/licenses/gpl-3.0.html.
****************************************************************************/

namespace CCS7Manager
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblContactSelected = new System.Windows.Forms.Label();
            this.chkAllCountries = new System.Windows.Forms.CheckBox();
            this.chkBoxCountries = new System.Windows.Forms.CheckedListBox();
            this.btnImportWeb = new System.Windows.Forms.Button();
            this.stBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsState = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAllRadios = new System.Windows.Forms.CheckBox();
            this.chkListRadios = new System.Windows.Forms.CheckedListBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.tb_OutputFolder = new System.Windows.Forms.TextBox();
            this.lblVersionDate = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbDatabaseList = new System.Windows.Forms.ComboBox();
            this.btnOpenJSON = new System.Windows.Forms.Button();
            this.chkEmpty = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.stBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEmpty);
            this.groupBox2.Controls.Add(this.lblContactSelected);
            this.groupBox2.Controls.Add(this.chkAllCountries);
            this.groupBox2.Controls.Add(this.chkBoxCountries);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 238);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Country";
            // 
            // lblContactSelected
            // 
            this.lblContactSelected.AutoSize = true;
            this.lblContactSelected.Location = new System.Drawing.Point(6, 214);
            this.lblContactSelected.Name = "lblContactSelected";
            this.lblContactSelected.Size = new System.Drawing.Size(109, 13);
            this.lblContactSelected.TabIndex = 2;
            this.lblContactSelected.Text = "Contacts Selected : 0";
            // 
            // chkAllCountries
            // 
            this.chkAllCountries.AutoSize = true;
            this.chkAllCountries.Enabled = false;
            this.chkAllCountries.Location = new System.Drawing.Point(9, 194);
            this.chkAllCountries.Name = "chkAllCountries";
            this.chkAllCountries.Size = new System.Drawing.Size(45, 17);
            this.chkAllCountries.TabIndex = 1;
            this.chkAllCountries.Text = "ALL";
            this.chkAllCountries.UseVisualStyleBackColor = true;
            this.chkAllCountries.CheckedChanged += new System.EventHandler(this.chkAllCountries_CheckedChanged);
            // 
            // chkBoxCountries
            // 
            this.chkBoxCountries.CheckOnClick = true;
            this.chkBoxCountries.Enabled = false;
            this.chkBoxCountries.FormattingEnabled = true;
            this.chkBoxCountries.Location = new System.Drawing.Point(6, 19);
            this.chkBoxCountries.Name = "chkBoxCountries";
            this.chkBoxCountries.Size = new System.Drawing.Size(217, 169);
            this.chkBoxCountries.TabIndex = 0;
            this.chkBoxCountries.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkBoxCountries_ItemCheck);
            // 
            // btnImportWeb
            // 
            this.btnImportWeb.Location = new System.Drawing.Point(125, 57);
            this.btnImportWeb.Name = "btnImportWeb";
            this.btnImportWeb.Size = new System.Drawing.Size(98, 23);
            this.btnImportWeb.TabIndex = 2;
            this.btnImportWeb.Text = "Download";
            this.btnImportWeb.UseVisualStyleBackColor = true;
            this.btnImportWeb.Click += new System.EventHandler(this.BtnImportWeb_Click);
            // 
            // stBar
            // 
            this.stBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsState});
            this.stBar.Location = new System.Drawing.Point(0, 363);
            this.stBar.Name = "stBar";
            this.stBar.Size = new System.Drawing.Size(650, 30);
            this.stBar.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(88, 25);
            this.toolStripStatusLabel1.Text = "STATUS :";
            // 
            // tsState
            // 
            this.tsState.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.tsState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tsState.Name = "tsState";
            this.tsState.Size = new System.Drawing.Size(547, 25);
            this.tsState.Spring = true;
            this.tsState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(388, 265);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(87, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAllRadios);
            this.groupBox1.Controls.Add(this.chkListRadios);
            this.groupBox1.Location = new System.Drawing.Point(388, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 244);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Radios";
            // 
            // chkAllRadios
            // 
            this.chkAllRadios.AutoSize = true;
            this.chkAllRadios.Enabled = false;
            this.chkAllRadios.Location = new System.Drawing.Point(7, 221);
            this.chkAllRadios.Name = "chkAllRadios";
            this.chkAllRadios.Size = new System.Drawing.Size(45, 17);
            this.chkAllRadios.TabIndex = 1;
            this.chkAllRadios.Text = "ALL";
            this.chkAllRadios.UseVisualStyleBackColor = true;
            this.chkAllRadios.CheckedChanged += new System.EventHandler(this.ChkAllRadios_CheckedChanged);
            // 
            // chkListRadios
            // 
            this.chkListRadios.CheckOnClick = true;
            this.chkListRadios.Enabled = false;
            this.chkListRadios.FormattingEnabled = true;
            this.chkListRadios.Location = new System.Drawing.Point(7, 20);
            this.chkListRadios.Name = "chkListRadios";
            this.chkListRadios.Size = new System.Drawing.Size(237, 184);
            this.chkListRadios.TabIndex = 0;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(388, 294);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(87, 23);
            this.btnOutputFolder.TabIndex = 10;
            this.btnOutputFolder.Text = "Output Folder";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // tb_OutputFolder
            // 
            this.tb_OutputFolder.Location = new System.Drawing.Point(390, 325);
            this.tb_OutputFolder.Name = "tb_OutputFolder";
            this.tb_OutputFolder.ReadOnly = true;
            this.tb_OutputFolder.Size = new System.Drawing.Size(241, 20);
            this.tb_OutputFolder.TabIndex = 11;
            // 
            // lblVersionDate
            // 
            this.lblVersionDate.AutoSize = true;
            this.lblVersionDate.Location = new System.Drawing.Point(6, 88);
            this.lblVersionDate.Name = "lblVersionDate";
            this.lblVersionDate.Size = new System.Drawing.Size(85, 13);
            this.lblVersionDate.TabIndex = 12;
            this.lblVersionDate.Text = "No File in Cache";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbDatabaseList);
            this.groupBox3.Controls.Add(this.btnOpenJSON);
            this.groupBox3.Controls.Add(this.btnImportWeb);
            this.groupBox3.Controls.Add(this.lblVersionDate);
            this.groupBox3.Location = new System.Drawing.Point(12, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 104);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database ";
            // 
            // cbDatabaseList
            // 
            this.cbDatabaseList.FormattingEnabled = true;
            this.cbDatabaseList.Location = new System.Drawing.Point(9, 20);
            this.cbDatabaseList.Name = "cbDatabaseList";
            this.cbDatabaseList.Size = new System.Drawing.Size(214, 21);
            this.cbDatabaseList.TabIndex = 14;
            // 
            // btnOpenJSON
            // 
            this.btnOpenJSON.Location = new System.Drawing.Point(9, 57);
            this.btnOpenJSON.Name = "btnOpenJSON";
            this.btnOpenJSON.Size = new System.Drawing.Size(98, 23);
            this.btnOpenJSON.TabIndex = 13;
            this.btnOpenJSON.Text = "Open JSON";
            this.btnOpenJSON.UseVisualStyleBackColor = true;
            this.btnOpenJSON.Click += new System.EventHandler(this.btnOpenJSON_Click);
            // 
            // chkEmpty
            // 
            this.chkEmpty.AutoSize = true;
            this.chkEmpty.Enabled = false;
            this.chkEmpty.Location = new System.Drawing.Point(70, 194);
            this.chkEmpty.Name = "chkEmpty";
            this.chkEmpty.Size = new System.Drawing.Size(79, 17);
            this.chkEmpty.TabIndex = 3;
            this.chkEmpty.Text = "No Country";
            this.chkEmpty.UseVisualStyleBackColor = true;
            this.chkEmpty.CheckedChanged += new System.EventHandler(this.chkEmpty_CheckedChanged);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 393);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tb_OutputFolder);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.stBar);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CCS7ID Manager by F4IKZ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.Move += new System.EventHandler(this.OnMove);
            this.Resize += new System.EventHandler(this.OnResize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.stBar.ResumeLayout(false);
            this.stBar.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnImportWeb;
        private System.Windows.Forms.StatusStrip stBar;
        private System.Windows.Forms.CheckedListBox chkBoxCountries;
        private System.Windows.Forms.CheckBox chkAllCountries;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAllRadios;
        private System.Windows.Forms.CheckedListBox chkListRadios;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsState;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.TextBox tb_OutputFolder;
        private System.Windows.Forms.Label lblVersionDate;
        private System.Windows.Forms.Label lblContactSelected;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbDatabaseList;
        private System.Windows.Forms.Button btnOpenJSON;
        private System.Windows.Forms.CheckBox chkEmpty;
    }
}