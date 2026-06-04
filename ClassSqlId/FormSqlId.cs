using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace ClassSqlId
{
    /// <summary>
    /// Autor: Artur Bałon 
    /// Changelog: 
    /// Created 10-2023
    /// Monitorowanie Sql_id
    /// 23-07-2024 zmiana referencji w projekcie i dodanie funkcjonalnosci wybierania SqlId
    /// </summary>
    public partial class FormSqlId : Form
    {
        SessionOptions sessionOptions;

        private OracleConnection Connection;

        private readonly BackgroundWorker worker;
        private readonly OracleCommand cmdWorker;
        private string sqlid;
        private Int32? sqlchildnumber;

        string sqlPlan = string.Empty;
        string sqlText = string.Empty;
        private readonly DataGridView dgvsql = new DataGridView();
        private readonly DataGridView dgvbind = new DataGridView();

        private BindingSource bsSC;
        private readonly DataGridView dgvSCursor = new DataGridView();

        private readonly string sqlProfile = string.Empty;
        private readonly string sqlBaseLine = string.Empty;
        DataRowView drvSC;
        string reason;
        DataRowView drvProfile;
        /// <summary>
        /// Sql_id Info
        /// </summary>
        /// <param name="connectionString">connectionSrting - otwiera nowe połącznie do bazy</param>
        public FormSqlId(OracleConnection Connection, SessionOptions sessionOptions)
        {
            InitializeComponent();
            
            this.Connection = Connection;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            cmdWorker = new OracleCommand();
            cmdWorker.Connection = Connection;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }
        public FormSqlId(OracleConnection Connection, string sqlId, SessionOptions sessionOptions)
        {
            InitializeComponent();

            this.Connection = Connection;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            cmdWorker = new OracleCommand();
            cmdWorker.Connection = Connection;
            toolStripTextBoxSqlId.Text = sqlId;
            
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }

            sqlid = sqlId;
            Go();
        }
        public FormSqlId(OracleConnection Connection, string sqlId, Int32 sqlChildNumber, SessionOptions sessionOptions)
        {
            InitializeComponent();

            this.Connection = Connection;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            cmdWorker = new OracleCommand();
            cmdWorker.Connection = Connection;
            toolStripTextBoxSqlId.Text = sqlId;

            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }

            sqlid = sqlId;
            sqlchildnumber = sqlChildNumber;
            Go();
        }

        private void FormSqlId_Load(object sender, EventArgs e)
        {
            

        }
        private void dataGridViewSql_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void FormSqlId_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                CancelWorker();
            }
            Connection.Close();
        }

        /// <summary>
        /// Anulowanie workera
        /// </summary>
        private void CancelWorker()
        {
            //if (worker.IsBusy)
            {
                worker.CancelAsync();
                cmdWorker.Cancel();
            }
        }

        /// <summary>
        /// Generowanie informacji o SqlId
        /// </summary>
        private void GetSqlIdInfo()
        {
            //główne wykonanie z workera
            sqlid = toolStripTextBoxSqlId.Text;
            sqlPlan = GetXPlain(sqlid);
            sqlText = GetSqlText(Connection, sqlid);
            dgvsql.DataSource = GetSqlInfo(Connection, sqlid);
            dgvbind.DataSource = GetSqlBind(Connection, sqlid);
            bsSC = new BindingSource();
            bsSC.DataSource = ClassSharedCursors.GetSCursor(Connection, sqlid);
            bsSC.CurrentChanged += BsSC_CurrentChanged;
            dgvSCursor.DataSource = bsSC;

            bindingSourceSqlProfile.DataSource = dgvsql.DataSource;
        }

        private void BsSC_CurrentChanged(object sender, EventArgs e)
        {
            drvSC = (DataRowView)bsSC.Current;
            reason = drvSC["REASON"].ToString();
            try
            {
                reason = "<root>" + reason + "</root>";
                var xxx = XDocument.Parse(reason);
                richTextBox1.Text = xxx.ToString();

            }
            catch (Exception ex)
            { }
        }

        #region worker
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripButtonStart.Enabled = true;
            toolStripButtonCancel.Enabled = false;
            richTextBoxSqlPlan.Text = sqlPlan;
            richTextBoxSqlText.Text = sqlText;
            dataGridViewSql.DataSource = dgvsql.DataSource;
            dataGridViewBinds.DataSource = dgvbind.DataSource;
            dataGridViewSC.DataSource = dgvSCursor.DataSource;
            Check();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;


            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                GetSqlIdInfo();
            }

        }
        #endregion

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            sqlid = toolStripTextBoxSqlId.Text;
            sqlchildnumber = null;
            Go();
        }

        private void Go()
        {
            toolStripButtonStart.Enabled = false;
            toolStripButtonCancel.Enabled = true;
            if (worker.IsBusy != true)
            {
                worker.RunWorkerAsync();
            }
        }

        private void toolStripButtonCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
            toolStripButtonStart.Enabled = true;
            toolStripButtonCancel.Enabled = false;

        }

        #region function

        public string GetXPlain(string sqlid)
        {
            string sqlText = string.Empty;
            string sqlString = "SELECT * FROM table(DBMS_XPLAN.DISPLAY_CURSOR(:sql_id, NULL, 'ALL ALLSTATS'))";
            cmdWorker.CommandText = sqlString;
            cmdWorker.Parameters.Clear();
            cmdWorker.Parameters.Add("sql_id", sqlid);
            try
            {
                OracleDataReader reader = cmdWorker.ExecuteReader();
                while (reader.Read())
                {
                    sqlText += reader.GetValue(0).ToString() + "\n";
                }
                reader.Close();
            }
            catch { }

            return sqlText;
        }

        /// <summary>
        /// Get Sql FULL Text
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns>return (string)FULL Sql</returns>
        public string GetSqlText(OracleConnection connection, string sqlid)
        {
            string sqlText = string.Empty;
            string sqlString = "SELECT SQL_TEXT FROM v$sqltext_with_newlines where sql_id = :sql_id order by piece asc";
            //OracleCommand cmd = new OracleCommand(sqlString, connection);
            //cmd.Parameters.Add("sql_id", sqlid);
            //string sqlString = "select distinct dbms_lob.substr(s.sql_fulltext,32767) from v$sql s where s.sql_id = :sql_id";
            cmdWorker.CommandText = sqlString; 
            cmdWorker.Parameters.Clear();
            cmdWorker.Parameters.Add("sql_id", sqlid);
            try
            {
                OracleDataReader reader = cmdWorker.ExecuteReader();
                while (reader.Read())
                {
                    sqlText += reader.GetValue(0).ToString();
                }
                reader.Close();
            }catch (OracleException exc) 
            {
                MessageBox.Show(exc.Message.ToString());
            }

            return sqlText;
        }

        /// <summary>
        /// Get Sql FULL Text
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns>return (string)FULL Sql</returns>
        public DataTable GetSqlInfo(OracleConnection connection, string sqlid)
        {
            DataTable dt = new DataTable();

            string sqlString = "select q.sql_profile, q.sql_plan_baseline, q.sorts, q.executions, q.first_load_time, q.users_executing, q.px_servers_executions, q.concurrency_wait_time,'---', q.*  from v$sql q where sql_id = :sql_id";
            cmdWorker.CommandText = sqlString;
            cmdWorker.Parameters.Clear();
            cmdWorker.Parameters.Add("sql_id", sqlid);
            try
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmdWorker);
                dataAdapter.Fill(dt);
            }
            catch { }

            foreach (DataRow dr in dt.Rows)
            {
                string sql_profile = dr["SQL_PROFILE"].ToString();
                string sql_baseline = dr["SQL_PLAN_BASELINE"].ToString();

                if (!string.IsNullOrEmpty(sql_profile))
                {
                    DataTable dtProfile = new DataTable();
                    cmdWorker.CommandText = "select * from dba_sql_profiles where name = :name";
                    cmdWorker.Parameters.Clear();
                    cmdWorker.Parameters.Add("name", sql_profile);
                    try
                    {
                        OracleDataAdapter dataAdapterProfile = new OracleDataAdapter(cmdWorker);
                        DataTable dtProfileProfile = new TabSqlProfile().TableProfile;
                        dataAdapterProfile.Fill (dtProfileProfile);
                    }
                    catch { }
                }

                if (!string.IsNullOrEmpty(sql_baseline))
                {
                    DataTable dtBaseLine = new DataTable();
                    cmdWorker.CommandText = "select * from dba_sql_plan_baselines where name = :name";
                    cmdWorker.Parameters.Clear();
                    cmdWorker.Parameters.Add("name", sql_profile);
                    try
                    {
                        OracleDataAdapter dataAdapterProfile = new OracleDataAdapter(cmdWorker);
                        dataAdapterProfile.Fill(dtBaseLine);
                        dataAdapterProfile.Fill(new TabSqlBaseLine().TableBaseLine);
                    }
                    catch { }
                }
            }
            return dt;
        }

        /// <summary>
        /// Get Sql Binds
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns>return (string)Sql Bind values</returns>
        public DataTable GetSqlBind(OracleConnection connection, string sqlid)
        {
            DataTable dt = new DataTable();

            string sqlString = "SELECT s.SQL_PROFILE,\n" +
            "       s.SQL_PLAN_BASELINE,\n" +
            "       s.EXECUTIONS,\n" +
            "       s.BUFFER_GETS,\n" +
            "       s.ROWS_PROCESSED,\n" +
            "       s.HASH_VALUE,\n" +
            "       s.LOADED_VERSIONS,\n" +
            "       b.NAME,\n" +
            "       b.POSITION,\n" +
            "       b.VALUE_STRING,\n" +
            "       b.DATATYPE,\n" +
            "       b.DATATYPE_STRING,\n" +
            "       b.CHARACTER_SID,\n" +
            "       b.PRECISION,\n" +
            "       b.SCALE,\n" +
            "       b.WAS_CAPTURED,\n" +
            "       b.LAST_CAPTURED\n" +
            "  FROM v$sql s\n" +
            "  JOIN v$sql_bind_capture b\n" +
            " using (sql_id)\n" +
            " WHERE sql_id = :sql_id\n" +
            "   AND b.value_string is not null";

            cmdWorker.CommandText = sqlString;
            cmdWorker.Parameters.Clear();
            cmdWorker.Parameters.Add("sql_id", sqlid);
            try
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmdWorker);
                dataAdapter.Fill(dt);
            }catch { }
            return dt;
        }

        #endregion

        private void Check()
        {
            List<string> mismatchList = new List<string>();
            Int32 bindMissmatch = 0;
            foreach (DataGridViewRow row in dataGridViewSC.Rows)
            {
                string child1 = row.Cells["CHILD_NUMBER"].Value.ToString();
                foreach (DataGridViewColumn col in dataGridViewSC.Columns)
                {
                    if (dataGridViewSC.Rows[row.Index].Cells[col.Index].Value.ToString() == "Y")
                    {
                        bindMissmatch++;
                        dataGridViewSC.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.Red;
                        mismatchList.Add("(child: " + child1.ToString() + ") " + col.Name);
                    }
                    else
                    {
                        dataGridViewSC.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.AliceBlue;
                    }
                }
            }
            listBox1.Items.Clear();
            listBox1.Items.AddRange(mismatchList.ToArray());
        }

        private void bindingSourceSqlProfile_CurrentChanged(object sender, EventArgs e)
        {
            drvProfile = (DataRowView)bindingSourceSqlProfile.Current;
        }
    }

}
