using AssecoToolsOptions;
using ClassSession;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassMViews
{
    public partial class FormMViews : Form
    {
        readonly OracleConnection Connection;
        SessionOptions sessionOptions;

        public FormMViews(OracleConnection Connection, SessionOptions SessionOptions)
        {
            InitializeComponent();
            this.Connection = Connection;
            sessionOptions = SessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }

        private void FormMViews_Load(object sender, EventArgs e)
        {
            ShowMViews(SQLStrings.MVIEWVSMAIN);
        }

        private void ShowMViews(string command)
        {
            DataTable table = new DataTable() { TableName = "MViewsTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = command };

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);
            bindingSource1.DataSource = table;
            dataGridView1.DataSource = bindingSource1;
            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }

    }
}
