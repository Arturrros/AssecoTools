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
    public partial class FormBlockedObjects : Form
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showSidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryToGetLockedDataRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(598, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 186);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(598, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
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
            // 
            // killSessionToolStripMenuItem
            // 
            this.killSessionToolStripMenuItem.Name = "killSessionToolStripMenuItem";
            this.killSessionToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.killSessionToolStripMenuItem.Text = "Kill Session";
            this.killSessionToolStripMenuItem.Click += new System.EventHandler(this.killSessionToolStripMenuItem_Click);
            // 
            // FormBlockedObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 208);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormBlockedObjects";
            this.Text = "Waiters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBlockedObjects_FormClosing);
            this.Load += new System.EventHandler(this.FormSessions_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem showSidToolStripMenuItem;
        private ToolStripMenuItem tryToGetLockedDataRowsToolStripMenuItem;
        private ToolStripMenuItem killSessionToolStripMenuItem;
    }
}