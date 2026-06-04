using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using ClassMonitor;
using ClassViewWindow;
using ClassSession;
using AssecoToolsOptions;

namespace ClassServerError
{
    public partial class FormServerError : Form
    {
        SessionOptions sessionOptions;
        public delegate void showTimeT_d(Int64 tim);
        public delegate void DisplayOdbcErrorCollection_d(OracleException exi);
        public delegate void ShowQueryInfo_d(Int32 sqlCnt, int rows_processed, string info);
        public delegate void datagrid_datasource_d(DataTable dttte);

        OracleCommand cmd;
        OracleDataAdapter adapter1;

        readonly OracleConnection Connection;
        DataRowView drv;
        DataTable dsdt;

        Int32 table_progres = 0;
        bool auto_stop = true;

        public FormServerError(OracleConnection Connection, string SQLOnStart, SessionOptions sessionOptions)
        {
            InitializeComponent();
            this.Connection = Connection;
            adapter1 = new OracleDataAdapter();
            dsdt = new DataTable();
            dsdt.RowChanging += new System.Data.DataRowChangeEventHandler(Changes);


            richTextBox1.Text = SQLOnStart;
            
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }

            Run(SQLOnStart);
        }

        private void FormServerError_Load(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            drv = (DataRowView)bindingSource1.Current;
        }

        private void Run(string polecenie)
        {
            
                auto_stop = false;
                table_progres = 0;


                dataGridView1.DataSource = null;
                toolStripDropDownButton1.Enabled = false;
                toolStripDropDownButton2.Enabled = false;
                toolStripButtonStop.Enabled = true;
                toolStripButtonStop.BackColor = Color.Red;
                toolStripStatusLabel1.Text = "0";
                
                if (!backgroundWorker.IsBusy)
                    backgroundWorker.RunWorkerAsync(polecenie);
                else
                    MessageBox.Show("Polecenie w trakcie wykonywania");
        }
        
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private int GetRows(string command, DataTable table, BackgroundWorker worker, DoWorkEventArgs ew)
        {
            if (worker.CancellationPending)
            {
                ew.Cancel = true;
            }
            else
            {
                try
                {
                    cmd = new OracleCommand();
                    cmd.Connection = Connection;
                    cmd.CommandText = command;


                    adapter1 = new OracleDataAdapter(cmd);
                    adapter1.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    table.Load(adapter1.SelectCommand.ExecuteReader(), LoadOption.OverwriteChanges);
                    datagrid_datasource_d dgds = datagrid_datasource;
                    Invoke(dgds, table);
                }
                catch (OracleException ex) 
                {
                    datagrid_datasource_d dgds = datagrid_datasource;
                    Invoke(dgds, table);

                    DisplayOdbcErrorCollection_d dl = DisplayOdbcErrorCollection;
                    Invoke(dl, ex);
                  
                }
            }
            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
            return -1;
        }

        private void counter()
        {
            toolStripStatusLabel1.Text = table_progres.ToString();
        }

        public void DisplayOdbcErrorCollection(OracleException myException)
        {
            string msg = null;
            for (int i = 0; i < myException.Errors.Count; i++)
            {
                msg += (myException.Errors[i].Message + "\n");
            }
            MessageBox.Show(msg);
        }
        private void datagrid_datasource(DataTable dttt)
        {
            bindingSource1.DataSource = dttt;
            dataGridView1.DataSource = bindingSource1;
            toolStripStatusLabel1.Text = dttt.Rows.Count.ToString();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            dsdt.Clear();
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = GetRows((string)e.Argument, dsdt, worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripDropDownButton1.Enabled = true;
            toolStripDropDownButton2.Enabled = true;
            toolStripButtonStop.Enabled = false;
            toolStripButtonStop.BackColor = SystemColors.Control;
            this.Refresh();
        }

        private void Changes(object sender, DataRowChangeEventArgs e)//Display Counter
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.ChangeCurrentAndOriginal)
            {
                table_progres++;
                if ((table_progres % 1) == 0)
                {
                    MethodInvoker counterrr = new MethodInvoker(counter);
                    Invoke(counterrr);
                }
                if (auto_stop)
                {
                    adapter1.SelectCommand.Cancel();
                    backgroundWorker.CancelAsync();
                    
                }
            }
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            auto_stop = true;
            backgroundWorker.CancelAsync();
            cmd.Cancel();
        }
        
        private void lAST24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.ERROR_LAST24H;
            Run(SQLStrings.ERROR_LAST24H);
        }

        private void tOP1000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.ERROR_LAST1000;
            Run(SQLStrings.ERROR_LAST1000);
        }

        private void aLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.ERRORS_ALL;
            Run(SQLStrings.ERRORS_ALL);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Run(richTextBox1.Text);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
            toolStripButtonRun.Enabled = !toolStripButtonRun.Enabled;
        }

        private void lAST1000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.DDL_LAST1000;
            Run(SQLStrings.DDL_LAST1000);
        }

        private void lAST24ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.DDL_LAST24;
            Run(SQLStrings.DDL_LAST24);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = SQLStrings.SESSION_LONGOPS;
            Run(SQLStrings.SESSION_LONGOPS);
        }

        private void showSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlid = drv["sql_id"].ToString();

            if (sqlid != null && sqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(Sessions.GetSql(Connection, sqlid));
                f.richTextBox1.Font = new System.Drawing.Font("Courier New", 10);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Session should have a valid sql_id", "Empty sqlid");
            }
        }

        private void xplainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlid = drv["sql_id"].ToString();

            if (sqlid != null && sqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(Sessions.GetXPlain(Connection, sqlid, true));
                f.richTextBox1.Font = new System.Drawing.Font("Courier New", 10);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Session should have a valid sql_id", "Empty sqlid");
            }
        }

        private void flushToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlid = drv["sql_id"].ToString();

            if (sqlid != null && sqlid.Trim().Length > 0)
            {
                MessageBox.Show(Sessions.FlushPlanCursor(Connection, sqlid));
            }
            else
            {
                MessageBox.Show("Session should have a valid sql_id", "Empty sqlid");
            }
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                if (MessageBox.Show("KIll session " + sid.ToString() + " - " + serial.ToString() + " ?", "Kill session", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    string res = string.Empty;
                    MessageBox.Show(new Sessions().Kill(Connection, sid, serial));
                }
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("KIll " + dataGridView1.SelectedRows.Count.ToString() + " sessions ?", "Kill session", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow drvi in dataGridView1.SelectedRows)
                    {
                        new Sessions().Kill(Connection, Convert.ToInt32(drvi.Cells["sid"].Value), Convert.ToInt32(drvi.Cells["serial#"].Value));

                    }
                    MessageBox.Show("Done.");
                }
            }
            //ShowSessions(SQLStrings.SESSIONS_ALL_USER);
        }

        private void sessionObiectMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                ClassMonitor.FormMonitorObj fm = new ClassMonitor.FormMonitorObj("Object Monitor sid: " + sid.ToString(), ClassMonitor.SQLStrings.OBJECT_MONITOR, sid, conntmp, sessionOptions);
                fm.Show(this);
            }
        }

        private void FormServerError_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }

        private void errorReportModuleCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run(SQLStrings.ERROR_REPORT_MODULE_COUNT);
        }
    }
}
