namespace ClassViewWindow
{
    partial class FormTextView
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSimpleNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSimpleNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSimpleNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.saveSimpleNoteToolStripMenuItem,
            this.showSimpleNoteToolStripMenuItem,
            this.clearSimpleNotesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(939, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveSimpleNoteToolStripMenuItem
            // 
            this.saveSimpleNoteToolStripMenuItem.Name = "saveSimpleNoteToolStripMenuItem";
            this.saveSimpleNoteToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveSimpleNoteToolStripMenuItem.Text = "Save";
            this.saveSimpleNoteToolStripMenuItem.ToolTipText = "Save Simple Note";
            this.saveSimpleNoteToolStripMenuItem.Click += new System.EventHandler(this.saveSimpleNoteToolStripMenuItem_Click);
            // 
            // showSimpleNoteToolStripMenuItem
            // 
            this.showSimpleNoteToolStripMenuItem.Name = "showSimpleNoteToolStripMenuItem";
            this.showSimpleNoteToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.showSimpleNoteToolStripMenuItem.Text = "Show";
            this.showSimpleNoteToolStripMenuItem.ToolTipText = "Show All Notes";
            this.showSimpleNoteToolStripMenuItem.Click += new System.EventHandler(this.showSimpleNoteToolStripMenuItem_Click);
            // 
            // clearSimpleNotesToolStripMenuItem
            // 
            this.clearSimpleNotesToolStripMenuItem.Name = "clearSimpleNotesToolStripMenuItem";
            this.clearSimpleNotesToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.clearSimpleNotesToolStripMenuItem.Text = "Clear";
            this.clearSimpleNotesToolStripMenuItem.ToolTipText = "Clear All Notes";
            this.clearSimpleNotesToolStripMenuItem.Click += new System.EventHandler(this.clearSimpleNotesToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(939, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(939, 272);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // FormTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 318);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormTextView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View";
            this.Load += new System.EventHandler(this.FormTextView_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSimpleNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSimpleNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSimpleNotesToolStripMenuItem;
    }
}