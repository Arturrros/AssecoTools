using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClassTableStats
{
    public partial class FormSchemaStatsInternalTable : Form
    {
        SessionOptions sessionOptions;
        OracleConnection connection;
        private string owner = string.Empty;
        private string tableName = string.Empty;
        private string methodOpt;


        private string columnName;
        private int columnValue;

        BindingSource bs;
        DataTable TCols = new DataTable();


        public FormSchemaStatsInternalTable(OracleConnection Connection, string Owner, string TableName, string MethodOpt, SessionOptions sessionOptions)
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
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                this.BackColor = sessionOptions.SessionColor;
            }
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
            AddColumn();
        }

        private void AddColumn()
        {
            string tmpMethod = textBox1.Text;
            string sectionUpdate = " FOR COLUMNS SIZE " + columnValue.ToString() + " " + columnName;
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

        private void SetMethodOpt(string MetOpt)
        {
            
            string sqlString = "begin\n" +
            "  DBMS_STATS.SET_TABLE_PREFS(ownname => '" + owner + "',\n" +
            "                             Tabname => '" + tableName + "',\n" +
            "                             pname   => '" + "METHOD_OPT" + "',\n" +
            "                             pvalue  => '" + MetOpt + "');\n" +
            "end;";

            try
            {
                OracleCommand tmpcmd = new OracleCommand(sqlString, connection);
                tmpcmd.ExecuteNonQuery();
                //ClassLog.Log.Add(ClassLog.Log.LogLevel.SETTINGSCHANGED, "Zmieniono parametr dla tabeli " + owner + "." + tableName + ": " + methodOpt + " na wartość " + textBox1.Text);
            }
            catch (OracleException exx)
            {
                MessageBox.Show(exx.Message.ToString());
            }
        }

        private string GetaTableMethodOptValue()
        {
            OracleCommand cmdActivePref = new OracleCommand();
            string sqlString = "select preference_value\n" +
                                "  from DBA_TAB_STAT_PREFS\n" +
                                " where owner = :owner\n" +
                                "   and table_name = :table_name\n" +
                                "   and preference_name = 'METHOD_OPT'";

            cmdActivePref = new OracleCommand(sqlString, connection);
            cmdActivePref.Parameters.Clear();
            cmdActivePref.Parameters.Add("owner", owner);
            cmdActivePref.Parameters.Add("table_name", tableName);

            try
            {
                string methodOpt = cmdActivePref.ExecuteScalar().ToString();
                textBox1.Text = methodOpt;
                return methodOpt;
            }
            catch (OracleException ex)
            {
                return string.Empty;
            }
        }

        private void setTo1AndApplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)bs.Current;
            string cName = drv[0].ToString();

            AddColumn();
            SetMethodOpt(textBox1.Text);
            GetaTableCols();
            //MessageBox.Show(cName);
        }
    }
}
