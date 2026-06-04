using AssecoToolsOptions;
using ClassViewWindow;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassMonitor
{
    public partial class FormMonitorObj : Form
    {
        SessionOptions sessionOptions;
        public delegate void RefreshInfo_d(string buff);
        string SpecialQuery;
        string Title;
        int sid;
        OracleConnection Connection;

        OracleCommand cmd;

        DataTable TMain = new DataTable();
        int interval = 1;

        DataRowView drv;
        BackgroundWorker worker;

        public FormMonitorObj(string title, string specialQuery,int sessionid, OracleConnection Connection, SessionOptions sessionOptions)
        {
            InitializeComponent();
            sid = sessionid;
            SpecialQuery = specialQuery;
            Title = title;
            this.Connection = Connection;
            cmd = new OracleCommand(specialQuery, Connection);
            cmd.Parameters.Add("sid", sid);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(TMain);

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            Text = title;
            toolStripComboBox1.SelectedIndex = 4;
            //toolStripComboBox2.SelectedIndex = 1;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }

        public FormMonitorObj(string title, string specialQuery, int sessionid, OracleConnection Connection, int width, int height, SessionOptions sessionOptions)
        {
            InitializeComponent();
            sid = sessionid;
            SpecialQuery = specialQuery;
            Title = title;
            this.Connection = Connection;
            cmd = new OracleCommand(specialQuery, Connection);
            cmd.Parameters.Add("sid", sid);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(TMain);

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            Text = title;
            toolStripComboBox1.SelectedIndex = 4;
            //toolStripComboBox2.SelectedIndex = 1;
            this.Width = width;
            this.Height = height;

            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }
        private void FormMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                SetClientInfo("ASSECO_TOOLS:MONITORING:" + Title);
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        void SetClientInfo(string info)
        {
            OracleCommand cmd = new OracleCommand("DBMS_APPLICATION_INFO.SET_CLIENT_INFO",Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("client_info",info));
            cmd.ExecuteNonQuery();
        }
        private void FormMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            worker.CancelAsync();
            cmd.Cancel();
            Connection.Close();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            if (worker.WorkerSupportsCancellation == true)
            {
                worker.CancelAsync();
                cmd.Cancel();
            }

        }
        
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            RefreshInfo("Wait...");
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Go();
        }
        private void RefreshInfo(string info)
        {
            if (statusStrip1.InvokeRequired)
            {
                RefreshInfo_d counterr = new RefreshInfo_d(RefreshInfo);
                this.Invoke(counterr, info);
            }
            else
            {
                tsslBusy.Text = "Wait...";
            }
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = e.Result;
            tsslBusy.Text = "Done.";
            //dataGridView1.Columns.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
        }

        private DataView Go()
        {
            DataTable ResultTable = new DataTable();    
            if (!worker.CancellationPending)
            {
                try
                {
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(ResultTable);
                    bindingSource1.DataSource = ResultTable;
                }
                catch (OracleException exc)
                {
                    MessageBox.Show(exc.Message);
                }
                
            }

            DataView dv = new DataView(ResultTable);
            
            return dv;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = interval*100;

            if(!worker.IsBusy)
                worker.RunWorkerAsync();
            else
                tsslBusy.Text = "Busy.";
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            interval = Convert.ToInt16(toolStripComboBox1.SelectedItem);
        }

        private void showSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            drv = (DataRowView)bindingSource1.Current;

            string sqlid = drv["sql_id"].ToString();

            if (sqlid != null && sqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(GetSql(Connection, sqlid));
                f.richTextBox1.Font = new System.Drawing.Font("Courier New", 10);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Session should have a valid sql_id", "Empty sqlid");
            }
        }
        public static string GetSql(OracleConnection connection, string sqlid)
        {
            string sql = string.Empty;
            string sqlString = "SELECT SQL_TEXT FROM v$sqltext_with_newlines where sql_id = :sql_id order by piece asc";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Add("sql_id", sqlid);

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sql += reader.GetValue(0).ToString();
            }
            reader.Close();

            return sql;
        }
    }
}
