using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSchemaStats
{
    public partial class FormInputBox : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WindowMode"> 1) string 2) text </param>
        public FormInputBox(int WindowMode)
        {
            InitializeComponent();
            if (WindowMode == 1) 
            {
                this.Width = 200;
                this.Height = 120;
                textBoxSqlId.Visible = true;
                richTextBox1.Visible = false;
                textBoxSqlId.Dock = DockStyle.Fill;
                label1.Text = "sql_id";
            }
            if (WindowMode == 2)
            {
                this.Width = 800;
                this.Height = 600;
                textBoxSqlId.Visible = false;
                richTextBox1.Visible = true;
                richTextBox1.Dock = DockStyle.Fill;
                label1.Text = "sql_text";
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormInputBox_Load(object sender, EventArgs e)
        {

        }
    }
}
