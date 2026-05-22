using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Oracle.ManagedDataAccess.Client;

namespace ClassVisual
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormChartOnline : Form
    {
        public delegate void Refresh_delegate(string buff);
        OracleConnection connection;
        OracleCommand cmd;
        string query;

        decimal value;

        BackgroundWorker worker;


        public FormChartOnline()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="Query">Simple query one value only</param>
        /// <param name="ChartTitle"></param>
        /// <param name="LegendText"></param>
        public FormChartOnline(OracleConnection Connection, string Query, string LegendText, string WindowText)
        {
            InitializeComponent();
            connection = Connection;
            query = Query;
            cmd = new OracleCommand(query, connection);
            
            try
            {
                decimal value = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            catch (Exception ex) {  }

            worker = new BackgroundWorker();
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;

            worker.WorkerSupportsCancellation = true;
            Text = WindowText;

            chart1.Series["Series1"].LegendText = LegendText;
            //chart1.Titles[0].Text = "cvcvb";

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Refresh("Wait...");
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Go();
            
        }

        private decimal Go()
        {
            decimal tempvalue = 0;
            if (!worker.CancellationPending)
            {
                try
                {
                    tempvalue = Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (OracleException exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }

            return tempvalue;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AddSimpleData(Convert.ToInt32(e.Result));
            tsslBusy.Text = "Done.";
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            try
            {
                SetClientInfo("ASSECO_TOOLS:MONITORING: Chart " + "xxxxx");
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //chart1.Series[0].LegendText = "sdfg";
            //chart1.Titles[0].Text = "cvcvb";
        }
        private void FormChartOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            worker.CancelAsync();
            cmd.Cancel();
            connection.Close();
        }
        void SetClientInfo(string info)
        {
            OracleCommand cmd = new OracleCommand("DBMS_APPLICATION_INFO.SET_CLIENT_INFO", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("client_info", info));
            cmd.ExecuteNonQuery();
        }
        private void AddSimpleData(Int32 val)
        {
            Series series = chart1.Series["Series1"];
            double next = 1;


            if (series.Points.Count > 0)
            {
                next = series.Points.Last().XValue + 1;
            }

            DataPoint dp = new DataPoint();

            dp.XValue = next;
            dp.YValues[0] = val;

            series.Points.Add(dp);

            // 500 limit do przewijania

            if (series.Points.Count > 50)
            {
                series.Points.Remove(series.Points[0]);
                chart1.ResetAutoValues();
            }
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            if (worker.WorkerSupportsCancellation == true)
            {
                worker.CancelAsync();
                cmd.Cancel();
            }
        }

        private void Refresh(string info)
        {
            if (statusStrip1.InvokeRequired)
            {
                Refresh_delegate counterr = new Refresh_delegate(Refresh);
                this.Invoke(counterr, info);
            }
            else
            {
                tsslBusy.Text = "Wait...";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Interval = 1000;

            if (!worker.IsBusy)
                worker.RunWorkerAsync();
            else
                tsslBusy.Text = "Busy.";
        }
    }
}
