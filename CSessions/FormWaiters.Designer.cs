using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassWaiters
{
    public partial class FormWaiters : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWaiters));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelSidSerial = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tv1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showSidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryToGetLockedDataRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripLabelSidSerial,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(598, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(50, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripLabelSidSerial
            // 
            this.toolStripLabelSidSerial.Name = "toolStripLabelSidSerial";
            this.toolStripLabelSidSerial.Size = new System.Drawing.Size(22, 22);
            this.toolStripLabelSidSerial.Text = "0,0";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(93, 22);
            this.toolStripButton1.Text = "Show Historical";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 186);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(598, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tv1
            // 
            this.tv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv1.Location = new System.Drawing.Point(0, 25);
            this.tv1.Name = "tv1";
            this.tv1.Size = new System.Drawing.Size(598, 161);
            this.tv1.TabIndex = 1;
            this.tv1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSidToolStripMenuItem,
            this.tryToGetLockedDataRowsToolStripMenuItem,
            this.killSessionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 70);
            // 
            // showSidToolStripMenuItem
            // 
            this.showSidToolStripMenuItem.Name = "showSidToolStripMenuItem";
            this.showSidToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showSidToolStripMenuItem.Text = "Show Session";
            this.showSidToolStripMenuItem.Click += new System.EventHandler(this.showSidToolStripMenuItem_Click);
            // 
            // tryToGetLockedDataRowsToolStripMenuItem
            // 
            this.tryToGetLockedDataRowsToolStripMenuItem.Name = "tryToGetLockedDataRowsToolStripMenuItem";
            this.tryToGetLockedDataRowsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tryToGetLockedDataRowsToolStripMenuItem.Text = "Try to get locked data";
            this.tryToGetLockedDataRowsToolStripMenuItem.Click += new System.EventHandler(this.tryToGetLockedDataRowsToolStripMenuItem_Click);
            // 
            // killSessionToolStripMenuItem
            // 
            this.killSessionToolStripMenuItem.Name = "killSessionToolStripMenuItem";
            this.killSessionToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.killSessionToolStripMenuItem.Text = "Kill Session";
            this.killSessionToolStripMenuItem.Click += new System.EventHandler(this.killSessionToolStripMenuItem_Click);
            // 
            // FormWaiters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 208);
            this.Controls.Add(this.tv1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormWaiters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Waiters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWaiters_FormClosing);
            this.Load += new System.EventHandler(this.FormSessions_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.TreeView tv1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem showSidToolStripMenuItem;
        private ToolStripMenuItem tryToGetLockedDataRowsToolStripMenuItem;
        private ToolStripLabel toolStripLabelSidSerial;
        private ToolStripMenuItem killSessionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton1;
    }
}