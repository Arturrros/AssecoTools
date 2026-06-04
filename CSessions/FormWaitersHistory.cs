using ClassSqlId;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssecoToolsOptions;

namespace CSessions
{
    public partial class FormWaitersHistory : Form
    {
        SessionOptions sessionOptions;
        readonly OracleConnection Connection;
        readonly Int32 sid;
        readonly Int32 serial;
        DataRowView drv;
        DataTable dt_waiters;

        static string SQL_WAITERS = "select id, dt, info, blo_sid, blo_osuser, blo_program, blo_machine, blo_last_call_et, blo_status, blo_sql_id, wai_sid, wai_osuser, wai_program, wai_machine, wai_wait_time, wai_sql_id, blo_client_info, wai_client_info, wai_status from holding_locks order by dt desc, wai_wait_time asc";

        public FormWaitersHistory(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            InitializeEaiters();
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                
                this.menuStrip1.BackColor = sessionOptions.SessionColor;
            }
        }

        public FormWaitersHistory(OracleConnection Connection, int Sid)
        {
            InitializeComponent();
            this.Connection = Connection;
            this.sid = Sid;
            InitializeEaiters();
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                this.menuStrip1.BackColor = sessionOptions.SessionColor;
            }
        }
        private void InitializeEaiters()
        {
            OracleCommand cmd = new OracleCommand(SQL_WAITERS, Connection);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            dt_waiters = new DataTable();
            da.Fill(dt_waiters);

            bindingSource1.DataSource = dt_waiters;
            dataGridView1.DataSource = bindingSource1;
        }
        private void FormWaitersHistory_Load(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            drv = (DataRowView)bindingSource1.Current;
        }

        private void sqlidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql_id = drv["wai_sql_id"].ToString();
            FormSqlId fsi = new FormSqlId(Connection, sql_id, sessionOptions);
            fsi.Show(this);
        }

        private void FormWaitersHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }
    }
}
