using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using AssecoToolsOptions;

namespace ClassMonitor
{
    public partial class FormMonitor : Form
    {
        SessionOptions sessionOptions;
        public delegate void RefreshInfo_d(string buff);
        string SpecialQuery;
        string Title;
        OracleConnection Connection;

        OracleCommand cmd;

        DataTable TPrimary = new DataTable();
        DataTable TSecondary = new DataTable();
        int interval = 1;
        int diffBorder = 1;

        Int64 sum = 0;

        Int64 sid = 0;
        string statName = string.Empty;

        BackgroundWorker worker;

        public FormMonitor(string title, string specialQuery, OracleConnection Connection, SessionOptions sessionOptions)
        {
            InitializeComponent();
            SpecialQuery = specialQuery;
            Title = title;
            this.Connection = Connection;
            cmd = new OracleCommand(specialQuery, Connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(TSecondary);

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            Text = title;
            toolStripComboBox1.SelectedIndex = 1;
            toolStripComboBox2.SelectedIndex = 1;
            sid = 0;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }
        public FormMonitor(string title, string specialQuery, OracleConnection Connection, Int32 Sid, string StatName, SessionOptions sessionOptions)
        {
            InitializeComponent();
            sid = Sid;
            statName = StatName;
            this.Text = " \"" + StatName + "\" for sid " + sid.ToString();

            SpecialQuery = specialQuery;
            Title = title;
            this.Connection = Connection;
            cmd = new OracleCommand(specialQuery, Connection);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("name", statName);
            cmd.Parameters.Add("sid", sid);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(TSecondary);

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            //Text = title;
            toolStripComboBox1.SelectedIndex = 1;
            toolStripComboBox2.SelectedIndex = 1;
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
        private void FormMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            
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

            sum = 0;
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
            tsslSum.Text = "Total: " + sum.ToString();
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private DataView Go()
        {
            sum = 0;
            DataTable TResult = new DataTable();
            TResult.TableName = "TResult";
            DataColumn dc1 = new DataColumn("sid");
            dc1.DataType = System.Type.GetType("System.Int32");
            
            TResult.Columns.Add(dc1);
            
            DataColumn dc2 = new DataColumn("name");
            TResult.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn("diff");
            dc3.DataType = System.Type.GetType("System.Int64");
            TResult.Columns.Add(dc3);

            TPrimary = TSecondary.Copy();
            TSecondary.Clear();

            if (!worker.CancellationPending)
            {
                try
                {
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(TSecondary);

                    foreach (DataRow drS in TSecondary.Rows)
                    {
                        string filter = "sid = " + drS["sid"].ToString() + " and name = '" + drS["Name"].ToString() + "'";
                        DataRow[] drP = TPrimary.Select(filter);

                        foreach (DataRow drPP in drP)
                        {
                            Int64 diff = (Convert.ToInt64(drS[2]) - Convert.ToInt64(drPP[2]));
                            if (diff > diffBorder)
                            {
                                DataRow dro = TResult.NewRow();
                                dro[0] = drPP[0];
                                dro[1] = drPP[1];
                                dro[2] = diff / interval;
                                TResult.Rows.Add(dro);
                            }
                            sum += diff / interval;
                            break;
                        }
                    }
                }
                catch (OracleException exc)
                {
                    
                }
                
            }

            DataView dv = new DataView(TResult);
            dv.Sort = "diff Desc";
            
            return dv;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = interval*1000;

            if(!worker.IsBusy)
                worker.RunWorkerAsync();
            else
                tsslBusy.Text = "Busy.";
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            interval = Convert.ToInt16(toolStripComboBox1.SelectedItem);
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            diffBorder = Convert.ToInt16(toolStripComboBox2.SelectedItem);
        }

       
       
    }
}
