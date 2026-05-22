namespace ClassReports
{
    partial class FormReportMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonReports = new System.Windows.Forms.ToolStripDropDownButton();
            this.lAST24ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOP1000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorReportModuleCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonReports,
            this.toolStripSeparator1,
            this.toolStripButtonStop,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(727, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonReports
            // 
            this.toolStripDropDownButtonReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lAST24ToolStripMenuItem,
            this.tOP1000ToolStripMenuItem,
            this.aLLToolStripMenuItem,
            this.errorReportModuleCountToolStripMenuItem});
            this.toolStripDropDownButtonReports.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonReports.Image")));
            this.toolStripDropDownButtonReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonReports.Name = "toolStripDropDownButtonReports";
            this.toolStripDropDownButtonReports.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButtonReports.Text = "Reports";
            // 
            // lAST24ToolStripMenuItem
            // 
            this.lAST24ToolStripMenuItem.Name = "lAST24ToolStripMenuItem";
            this.lAST24ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.lAST24ToolStripMenuItem.Text = "LAST24";
            // 
            // tOP1000ToolStripMenuItem
            // 
            this.tOP1000ToolStripMenuItem.Name = "tOP1000ToolStripMenuItem";
            this.tOP1000ToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.tOP1000ToolStripMenuItem.Text = "TOP1000";
            // 
            // aLLToolStripMenuItem
            // 
            this.aLLToolStripMenuItem.Name = "aLLToolStripMenuItem";
            this.aLLToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.aLLToolStripMenuItem.Text = "ALL";
            // 
            // errorReportModuleCountToolStripMenuItem
            // 
            this.errorReportModuleCountToolStripMenuItem.Name = "errorReportModuleCountToolStripMenuItem";
            this.errorReportModuleCountToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.errorReportModuleCountToolStripMenuItem.Text = "Error Report Module Count";
            this.errorReportModuleCountToolStripMenuItem.Click += new System.EventHandler(this.errorReportModuleCountToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonStop.Text = "Stop";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FormReportMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 312);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormReportMain";
            this.Text = "FormReportMain";
            this.Load += new System.EventHandler(this.FormReportMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonReports;
        private System.Windows.Forms.ToolStripMenuItem lAST24ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOP1000ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aLLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorReportModuleCountToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}