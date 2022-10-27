namespace CCS7Manager
{
	partial class dlgConServeur
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Sources :");
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nouveauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAppliquer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.ListSources = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbProfil = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // nouveauToolStripMenuItem
            // 
            this.nouveauToolStripMenuItem.Name = "nouveauToolStripMenuItem";
            this.nouveauToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.nouveauToolStripMenuItem.Text = "Nouveau";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 262);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnAppliquer);
            this.panel1.Controls.Add(this.btnAnnuler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 232);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 30);
            this.panel1.TabIndex = 0;
            // 
            // btnAppliquer
            // 
            this.btnAppliquer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAppliquer.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAppliquer.Enabled = false;
            this.btnAppliquer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAppliquer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppliquer.ForeColor = System.Drawing.Color.White;
            this.btnAppliquer.Location = new System.Drawing.Point(381, 0);
            this.btnAppliquer.Name = "btnAppliquer";
            this.btnAppliquer.Size = new System.Drawing.Size(75, 30);
            this.btnAppliquer.TabIndex = 1;
            this.btnAppliquer.Text = "Apply";
            this.btnAppliquer.UseVisualStyleBackColor = false;
            this.btnAppliquer.Click += new System.EventHandler(this.btnAppliquer_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAnnuler.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAnnuler.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.btnAnnuler.Location = new System.Drawing.Point(456, 0);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(75, 30);
            this.btnAnnuler.TabIndex = 0;
            this.btnAnnuler.Text = "Close";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 232);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please select entry";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnSupprimer, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnNewUser, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ListSources, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(259, 213);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSupprimer.Enabled = false;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupprimer.Location = new System.Drawing.Point(132, 186);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(75, 23);
            this.btnSupprimer.TabIndex = 1;
            this.btnSupprimer.Text = "Delete";
            this.btnSupprimer.UseVisualStyleBackColor = false;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewUser.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNewUser.FlatAppearance.BorderSize = 0;
            this.btnNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewUser.ForeColor = System.Drawing.Color.White;
            this.btnNewUser.Location = new System.Drawing.Point(51, 186);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(75, 23);
            this.btnNewUser.TabIndex = 0;
            this.btnNewUser.Text = "New";
            this.btnNewUser.UseVisualStyleBackColor = false;
            this.btnNewUser.Click += new System.EventHandler(this.OnNewSource_Click);
            // 
            // ListSources
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.ListSources, 2);
            this.ListSources.Location = new System.Drawing.Point(3, 3);
            this.ListSources.Name = "ListSources";
            treeNode2.Name = "root";
            treeNode2.Text = "Sources :";
            this.ListSources.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.ListSources.Size = new System.Drawing.Size(253, 177);
            this.ListSources.TabIndex = 2;
            this.ListSources.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.ListSources_BeforeSelect);
            this.ListSources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ListSources_AfterSelect);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbProfil);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbURL);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(265, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 232);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Content";
            // 
            // tbProfil
            // 
            this.tbProfil.BackColor = System.Drawing.SystemColors.Control;
            this.tbProfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProfil.Enabled = false;
            this.tbProfil.Location = new System.Drawing.Point(56, 30);
            this.tbProfil.Name = "tbProfil";
            this.tbProfil.Size = new System.Drawing.Size(198, 20);
            this.tbProfil.TabIndex = 7;
            this.tbProfil.TextChanged += new System.EventHandler(this.tbProfil_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name :";
            // 
            // tbURL
            // 
            this.tbURL.BackColor = System.Drawing.SystemColors.Control;
            this.tbURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbURL.Enabled = false;
            this.tbURL.Location = new System.Drawing.Point(10, 91);
            this.tbURL.Multiline = true;
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(244, 105);
            this.tbURL.TabIndex = 5;
            this.tbURL.TextChanged += new System.EventHandler(this.tbURL_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "URL:";
            // 
            // dlgConServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(531, 262);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "dlgConServeur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CCS7 ID Sources";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem nouveauToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnAppliquer;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbURL;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbProfil;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.TreeView ListSources;
    }
}