using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassReports
{
    /// <summary>
    /// Description:    Wyświetlanie historycznych poleceń
    /// Date:           2025-08
    /// Autor:          artur.balon@asseco.pl
    /// Changelog:  
    /// </summary>
    public partial class FormASH : Form
    {
        string sqlString_last1h = "select sql_id, time_waited, sql_text, sample_time \n" +
        "  from (select ash.sql_id, sa.sql_text, ash.sample_time, sum(ash.wait_time + ash.time_waited) time_waited\n" +
        "          from v$active_session_history ash,\n" +
        "               v$sqlarea sa,\n" +
        "               dba_users du\n" +
        "         where ash.sample_time between (sysdate-1/24) and sysdate\n" +
        "           and ash.sql_id = sa.sql_id\n" +
        "           and ash.user_id = du.user_id\n" +
        "         group by ash.sql_id,\n" +
        "                  sa.sql_text,\n" +
        "                  du.username,ash.sample_time\n" +
        "         order by 3 desc)\n" +
        " where rownum < 10";

        string sqlString_between = "select sql_id, time_waited, sql_text, sample_time\n" +
        "  from (select ash.sql_id, sa.sql_text, ash.sample_time, sum(ash.wait_time + ash.time_waited) time_waited\n" +
        "          from v$active_session_history ash,\n" +
        "               v$sqlarea sa,\n" +
        "               dba_users du\n" +
        "         where ash.sample_time between :sample1 and :sample2 \n" +
        "           and ash.sql_id = sa.sql_id\n" +
        "           and ash.user_id = du.user_id\n" +
        "         group by ash.sql_id,\n" +
        "                  sa.sql_text,\n" +
        "                  du.username,ash.sample_time\n" +
        "         order by 3 desc)\n" +
        " where rownum < 10";


        //DateTimePicker dpi = new DateTimePicker();
        readonly OracleConnection Connection;
        BindingSource bs;
        DataRowView bsdrv;
        string bssql = string.Empty;

        public FormASH(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            this.bs = new BindingSource();
            bs.CurrentChanged += Bs_CurrentChanged;
        }

        private void Bs_CurrentChanged(object sender, EventArgs e)
        {
            bsdrv = (DataRowView)bs.Current;
            if (bsdrv != null)
            {
                bssql = bsdrv[2].ToString();
            }
        }

        private void FormASH_Load(object sender, EventArgs e)
        {
            Get1hData();
        }
        private void Get1hData()
        {
            DataTable ASHtable = new DataTable() { TableName = "ASHTable" };
            OracleCommand ASHcmd = new OracleCommand() { Connection = Connection, CommandText = sqlString_last1h };

            OracleDataAdapter adapter = new OracleDataAdapter(ASHcmd);
            adapter.Fill(ASHtable);

            bs.DataSource = ASHtable;

            dataGridView1.DataSource = bs;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            toolStripStatusLabel1.Text = ASHtable.Rows.Count.ToString() + " Rows";

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetData()
        {

            DataTable ASHtable = new DataTable() { TableName = "ASHTable" };
            OracleCommand ASHcmd = new OracleCommand() { Connection = Connection, CommandText = sqlString_between };
            ASHcmd.Parameters.Add(new OracleParameter("sample1", dateTimePicker1.Value));
            ASHcmd.Parameters.Add(new OracleParameter("sample2", dateTimePicker2.Value));

            OracleDataAdapter adapter = new OracleDataAdapter(ASHcmd);
            adapter.Fill(ASHtable);

            bs.DataSource = ASHtable;

            dataGridView1.DataSource = bs;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            toolStripStatusLabel1.Text = ASHtable.Rows.Count.ToString() + " Rows";

        }

    }
    
}
