using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ClassViewWindow
{
    public partial class FormTextView : Form
    {
        private string database;
        private DataRowView drSessionInfo;
        public FormTextView()
        {
            InitializeComponent();
        }

        public FormTextView(string Txt)
        {
            InitializeComponent();
            richTextBox1.Text = Txt;
        }

        public FormTextView(string Txt, bool ShowHidden)
        {
            InitializeComponent();
            richTextBox1.Text = Txt;
            saveSimpleNoteToolStripMenuItem.Visible = false;
            showSimpleNoteToolStripMenuItem.Visible = false;
            clearSimpleNotesToolStripMenuItem.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Txt">Info</param>
        /// <param name="adds">Data Row sesio info</param>
        public FormTextView(string Txt, DataRowView DrSessionInfo, string Database)
        {
            InitializeComponent();
            database = Database;
            drSessionInfo = DrSessionInfo;
            richTextBox1.AppendText("*** \n");
            richTextBox1.AppendText("*** Database     :" + database + "\n");
            richTextBox1.AppendText("*** date Now     :" + DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") + "\n");
            richTextBox1.AppendText("*** sid:         :" + drSessionInfo["sid"] + "\n");
            richTextBox1.AppendText("*** osuser:      :" + drSessionInfo["osuser"] + "\n");
            richTextBox1.AppendText("*** program:     :" + drSessionInfo["program"] + "\n");
            richTextBox1.AppendText("*** event:       :" + drSessionInfo["event"] + "\n");
            richTextBox1.AppendText("*** last_call_et :" + drSessionInfo["last_call_et"] + "\n");
            richTextBox1.AppendText("*** sql_child    :" + drSessionInfo["sql_child"] + "\n");
            richTextBox1.AppendText("*** \n");


            richTextBox1.AppendText("\n");
            richTextBox1.AppendText(Txt);
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();

            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               sf.FileName.Length > 0)
            {
                richTextBox1.SaveFile(sf.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveSimpleNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatsSimpleNote ssn = new StatsSimpleNote(database,drSessionInfo);    
            ssn.SaveNote(richTextBox1.Text);
            saveSimpleNoteToolStripMenuItem.Text = "Saved";
        }

        private void showSimpleNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormGridView fgv = new FormGridView(new StatsSimpleNote(database).GetSimpleNotes(), "Simple Notes");
            fgv.ShowDialog();
            
        }

        private void clearSimpleNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StatsSimpleNote(database).ClearSimpleNotes();
            
        }

        private void FormTextView_Load(object sender, EventArgs e)
        {

        }
    }
}
