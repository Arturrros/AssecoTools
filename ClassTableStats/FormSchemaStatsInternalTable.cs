using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClassTableStats
{
    public partial class FormSchemaStatsInternalTable : Form
    {
        OracleConnection connection;
        private string owner = string.Empty;
        private string tableName = string.Empty;
        private string methodOpt;


        private string columnName;
        private int columnValue;

        BindingSource bs;
        DataTable TCols = new DataTable();


        public FormSchemaStatsInternalTable(OracleConnection Connection, string Owner, string TableName, string MethodOpt)
        {
            InitializeComponent();
            connection = Connection;
            owner = Owner;
            tableName = TableName;
            methodOpt = MethodOpt;
            textBox1.Text = methodOpt;

            bs = new BindingSource(TCols, null);
            bs.CurrentChanged += Bs_CurrentChanged;
            dataGridView1.DataSource = bs;

        }

        private void Bs_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView drt = (DataRowView)bs.Current;
            columnName = drt["column_name"].ToString();
            columnValue = Convert.ToInt32(drt["num_buckets"]);

//            if (drt["opts"].ToString() == "METHOD_OPT")
        }

        private void FormSchemaStatsInternalTable_Load(object sender, EventArgs e)
        {
            GetaTableCols();
            dataGridView1.DataSource = bs;
        }

        private void GetaTableCols()
        {

            string sqlString = "select ts.column_name, ts.num_buckets\n" +
                               "  from dba_tab_col_statistics ts\n" +
                               " where owner = :owner\n" +
                               "   and table_name = :table_name";

            OracleCommand cmdCols = new OracleCommand(sqlString, connection);
            cmdCols.Parameters.Add("owner", owner);
            cmdCols.Parameters.Add("table_name", tableName);
            
            try
            {
                OracleDataAdapter adaCols = new OracleDataAdapter(cmdCols);
                adaCols.Fill(TCols);
            }
            catch (OracleException ex)
            {
            }
        }

        private void FormSchemaStatsInternalTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string tmpMethod = textBox1.Text;
            string sectionUpdate = " for columns size " + columnValue.ToString() + " " + columnName ;
            
            textBox1.Text = tmpMethod + sectionUpdate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = methodOpt.ToString();   
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
