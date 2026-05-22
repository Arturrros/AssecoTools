using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassViewWindow;
using Oracle.ManagedDataAccess.Client;

namespace ClassMonitor
{
    public partial class FormMonitor01 : Form
    {
        public delegate void RefreshInfo_d(string buff);
        string SpecialQuery;
        string Title;
        string WinType;

        OracleConnection Connection;

        OracleCommand cmd;

        DataTable TMain = new DataTable();
        int interval = 1;

        DataRowView drv;
        BackgroundWorker worker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wintype">normal</param>
        /// <param name="title"></param>
        /// <param name="specialQuery"></param>
        /// <param name="Connection"></param>
        /// <param name="Interval">Interval in seconds</param>
        /// <param name="width"></param>
        public FormMonitor01( string wintype ,string title, string specialQuery, OracleConnection Connection, int Interval, int width)
        {
            InitializeComponent();
            SpecialQuery = specialQuery;
            Title = title;
            this.Connection = Connection;
            cmd = new OracleCommand(specialQuery, Connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(TMain);

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            Text = title;
            WinType = wintype;
            if (WinType == "TMP")
                toolStripButtonShowHistorical.Visible = true;


            toolStripComboBox1.SelectedIndex = 7;
            interval = Interval;
            this.Width = width;
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
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = e.Result;
            tsslBusy.Text = "Done.";
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

        private void toolStripButtonShowHistorical_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormMonitorSessionTmpHistorical gw = new FormMonitorSessionTmpHistorical(conntmp);
            gw.StartPosition = FormStartPosition.CenterParent;
            gw.Show(this);
        }
    }
}
