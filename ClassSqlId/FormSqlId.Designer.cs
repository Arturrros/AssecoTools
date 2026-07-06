namespace ClassSqlId
{
    partial class FormSqlId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSqlId));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSqlId = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.richTextBoxSqlPlan = new System.Windows.Forms.RichTextBox();
            this.tabControlProfile = new System.Windows.Forms.TabControl();
            this.tabPageSql = new System.Windows.Forms.TabPage();
            this.richTextBoxSqlText = new System.Windows.Forms.RichTextBox();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.tabPageBind = new System.Windows.Forms.TabPage();
            this.dataGridViewBinds = new System.Windows.Forms.DataGridView();
            this.tabPageProfile = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewSql = new System.Windows.Forms.DataGridView();
            this.tabPageSCursors = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridViewSC = new System.Windows.Forms.DataGridView();
            this.bindingSourceSqlProfile = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tabControlProfile.SuspendLayout();
            this.tabPageSql.SuspendLayout();
            this.tabPagePlan.SuspendLayout();
            this.tabPageBind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBinds)).BeginInit();
            this.tabPageProfile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSql)).BeginInit();
            this.tabPageSCursors.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSqlProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 513);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(999, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBoxSqlId,
            this.toolStripSeparator1,
            this.toolStripButtonStart,
            this.toolStripButtonCancel,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(999, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel1.Text = "Sql_id";
            // 
            // toolStripTextBoxSqlId
            // 
            this.toolStripTextBoxSqlId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxSqlId.Name = "toolStripTextBoxSqlId";
            this.toolStripTextBoxSqlId.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxSqlId.Text = "3pqt9r3b9w9vk";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonStart.Text = "Start";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCancel.Enabled = false;
            this.toolStripButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCancel.Image")));
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonCancel.Text = "Stop";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // richTextBoxSqlPlan
            // 
            this.richTextBoxSqlPlan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSqlPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSqlPlan.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBoxSqlPlan.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxSqlPlan.Name = "richTextBoxSqlPlan";
            this.richTextBoxSqlPlan.Size = new System.Drawing.Size(985, 456);
            this.richTextBoxSqlPlan.TabIndex = 3;
            this.richTextBoxSqlPlan.Text = "";
            // 
            // tabControlProfile
            // 
            this.tabControlProfile.Controls.Add(this.tabPageSql);
            this.tabControlProfile.Controls.Add(this.tabPagePlan);
            this.tabControlProfile.Controls.Add(this.tabPageBind);
            this.tabControlProfile.Controls.Add(this.tabPageProfile);
            this.tabControlProfile.Controls.Add(this.tabPageSCursors);
            this.tabControlProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProfile.Location = new System.Drawing.Point(0, 25);
            this.tabControlProfile.Name = "tabControlProfile";
            this.tabControlProfile.SelectedIndex = 0;
            this.tabControlProfile.Size = new System.Drawing.Size(999, 488);
            this.tabControlProfile.TabIndex = 4;
            // 
            // tabPageSql
            // 
            this.tabPageSql.Controls.Add(this.richTextBoxSqlText);
            this.tabPageSql.Location = new System.Drawing.Point(4, 22);
            this.tabPageSql.Name = "tabPageSql";
            this.tabPageSql.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSql.Size = new System.Drawing.Size(991, 462);
            this.tabPageSql.TabIndex = 1;
            this.tabPageSql.Text = "Sql";
            this.tabPageSql.UseVisualStyleBackColor = true;
            // 
            // richTextBoxSqlText
            // 
            this.richTextBoxSqlText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxSqlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSqlText.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxSqlText.Name = "richTextBoxSqlText";
            this.richTextBoxSqlText.Size = new System.Drawing.Size(985, 456);
            this.richTextBoxSqlText.TabIndex = 0;
            this.richTextBoxSqlText.Text = "";
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.richTextBoxSqlPlan);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(991, 462);
            this.tabPagePlan.TabIndex = 0;
            this.tabPagePlan.Text = "Plan";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // tabPageBind
            // 
            this.tabPageBind.Controls.Add(this.dataGridViewBinds);
            this.tabPageBind.Location = new System.Drawing.Point(4, 22);
            this.tabPageBind.Name = "tabPageBind";
            this.tabPageBind.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBind.Size = new System.Drawing.Size(991, 462);
            this.tabPageBind.TabIndex = 2;
            this.tabPageBind.Text = "Binds";
            this.tabPageBind.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBinds
            // 
            this.dataGridViewBinds.AllowUserToAddRows = false;
            this.dataGridViewBinds.AllowUserToDeleteRows = false;
            this.dataGridViewBinds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBinds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBinds.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewBinds.Name = "dataGridViewBinds";
            this.dataGridViewBinds.ReadOnly = true;
            this.dataGridViewBinds.RowTemplate.Height = 18;
            this.dataGridViewBinds.Size = new System.Drawing.Size(985, 456);
            this.dataGridViewBinds.TabIndex = 1;
            // 
            // tabPageProfile
            // 
            this.tabPageProfile.Controls.Add(this.groupBox1);
            this.tabPageProfile.Location = new System.Drawing.Point(4, 22);
            this.tabPageProfile.Name = "tabPageProfile";
            this.tabPageProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProfile.Size = new System.Drawing.Size(991, 462);
            this.tabPageProfile.TabIndex = 3;
            this.tabPageProfile.Text = "Profile";
            this.tabPageProfile.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewSql);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(985, 150);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dataGridViewSql
            // 
            this.dataGridViewSql.AllowUserToAddRows = false;
            this.dataGridViewSql.AllowUserToDeleteRows = false;
            this.dataGridViewSql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSql.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSql.Name = "dataGridViewSql";
            this.dataGridViewSql.ReadOnly = true;
            this.dataGridViewSql.RowTemplate.Height = 18;
            this.dataGridViewSql.Size = new System.Drawing.Size(979, 131);
            this.dataGridViewSql.TabIndex = 2;
            this.dataGridViewSql.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewSql_DataError);
            // 
            // tabPageSCursors
            // 
            this.tabPageSCursors.Controls.Add(this.panel1);
            this.tabPageSCursors.Controls.Add(this.dataGridViewSC);
            this.tabPageSCursors.Location = new System.Drawing.Point(4, 22);
            this.tabPageSCursors.Name = "tabPageSCursors";
            this.tabPageSCursors.Size = new System.Drawing.Size(991, 462);
            this.tabPageSCursors.TabIndex = 4;
            this.tabPageSCursors.Text = "Shared Cursor";
            this.tabPageSCursors.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 312);
            this.panel1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(251, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(740, 312);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(251, 312);
            this.listBox1.TabIndex = 3;
            // 
            // dataGridViewSC
            // 
            this.dataGridViewSC.AllowUserToAddRows = false;
            this.dataGridViewSC.AllowUserToDeleteRows = false;
            this.dataGridViewSC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewSC.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewSC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSC.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewSC.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSC.Name = "dataGridViewSC";
            this.dataGridViewSC.ReadOnly = true;
            this.dataGridViewSC.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewSC.RowHeadersVisible = false;
            this.dataGridViewSC.RowTemplate.Height = 16;
            this.dataGridViewSC.Size = new System.Drawing.Size(991, 150);
            this.dataGridViewSC.TabIndex = 0;
            // 
            // bindingSourceSqlProfile
            // 
            this.bindingSourceSqlProfile.CurrentChanged += new System.EventHandler(this.bindingSourceSqlProfile_CurrentChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(39, 22);
            this.toolStripButton1.Text = "Flush";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // FormSqlId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 535);
            this.Controls.Add(this.tabControlProfile);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSqlId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sql_Id";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSqlId_FormClosing);
            this.Load += new System.EventHandler(this.FormSqlId_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControlProfile.ResumeLayout(false);
            this.tabPageSql.ResumeLayout(false);
            this.tabPagePlan.ResumeLayout(false);
            this.tabPageBind.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBinds)).EndInit();
            this.tabPageProfile.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSql)).EndInit();
            this.tabPageSCursors.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSqlProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSqlId;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.RichTextBox richTextBoxSqlPlan;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.TabControl tabControlProfile;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.TabPage tabPageSql;
        private System.Windows.Forms.RichTextBox richTextBoxSqlText;
        private System.Windows.Forms.TabPage tabPageBind;
        private System.Windows.Forms.DataGridView dataGridViewBinds;
        private System.Windows.Forms.TabPage tabPageProfile;
        private System.Windows.Forms.DataGridView dataGridViewSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableProfileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TabPage tabPageSCursors;
        private System.Windows.Forms.DataGridView dataGridViewSC;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bindingSourceSqlProfile;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}