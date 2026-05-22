using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassViewWindow
{
    public partial class FormGridView : Form
    {
        DataTable source;
        OracleConnection Connection;

        public FormGridView()
        {
            InitializeComponent();
        }
        public FormGridView(DataTable source, string infoText)
        {
            InitializeComponent();
            this.source = source;
            bindingSource1.DataSource = source;
            dataGridView1.DataSource = bindingSource1;
            this.Text = infoText;
        }
        public FormGridView(OracleConnection Connection, string SqlCommand, string infoText)
        {
            InitializeComponent();
            this.Connection = Connection;
            source = new DataTable();
            this.Text = infoText;

            try
            {
                OracleCommand cmd = new OracleCommand(SqlCommand, Connection);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(source);
                bindingSource1.DataSource = source;
                dataGridView1.DataSource = bindingSource1;
                toolStripStatusLabel1.Text = source.Rows.Count.ToString() + " rows";
            }
            catch (OracleException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FormGriDView_Load(object sender, EventArgs e)
        { }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;

            (new FormTextView(dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), false)).ShowDialog();
 
        }
    }
}
