using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using Oracle.ManagedDataAccess.Client;


using System.Windows.Forms;

namespace ClassSchemaStats
{
    public partial class FormSimpleRunStats : Form
    {
        public delegate void Worker_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);

        OracleConnection Connection;

        String owner = String.Empty;

        OracleCommand CmdGatherTable;
        //OracleCommand CmdGatherIndex;

        BackgroundWorker worker;

        bool autostop = true;
        String errorBuffer = String.Empty;
        Int32 errorBufferCount = 0;
        String execBuffer = String.Empty;
        Int32 execBufferCount = 0;
        DataTable runTables = new DataTable();

        bool noinvalidate;
        bool cascade;
        int degree;
        int estimapepercent;

        public FormSimpleRunStats(OracleConnection Connection, DataTable RunTables, int Degree, int estimatePercent, bool noInvalidate, bool Cascade)
        {
            InitializeComponent();
            this.Connection = Connection;
            runTables = RunTables;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;

             noinvalidate = noInvalidate;
             cascade = Cascade;
             degree = Degree;
             estimapepercent = estimatePercent;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMain.Value = e.ProgressPercentage;
        }

        private void FormTableStats_Load(object sender, EventArgs e)
        {
            tbtnStart_Click(null, null);
        }

        /// <summary>
        /// Data: 20-09-2022
        /// Opis zmian: Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// </summary>
        /// <param name="runTables"></param>
        /// <param name="degree"></param>
        /// <param name="extimatePrc"></param>
        /// <param name="noInvalidate"></param>
        /// <param name="Cascade"></param>
        /// <param name="runWorker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int runquery(DataTable runTables, int degree , int extimatePrc, bool noInvalidate, bool Cascade, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            Int32 tableCounter = 0;

            Worker_Info("Wait for prepare...", null, null, null);

            Int32 tableCount = runTables.Rows.Count;

            var result = from d in runTables.AsEnumerable() group d by d["USER_SCHEMA"];

            //foreach (var t in result)
            //{
            //   ClassSchemaStats.SetSchemaPrefs(Connection, t.Key.ToString());
            //}

           

            foreach (DataRow dr in runTables.Rows)
            {
                string owner = dr["USER_SCHEMA"].ToString();
                string tableName = dr["TAB"].ToString();
                string partitionName = dr["PART"].ToString();
                string subpartitionName  = dr["SUB"].ToString();

                string parname = String.Empty;

                if (autostop == true)
                {
                    Worker_Info("\n\nAnulowano na żądanie użytkownika\n", null, null, null);
                    return 0;
                }

                if (runWorker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    string noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";

                    string cascade = "TRUE";

                    if (noInvalidate == true)
                        noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";
                    else
                        noinvalidate = "FALSE";

                    if (Cascade == true)
                        cascade = "TRUE";
                    else
                        cascade = "FALSE";


                    string sqlString = "begin\n" +
                    "  DBMS_STATS.GATHER_TABLE_STATS(ownname          => '" + dr[0] + "',\n" +
                    "                                tabname          => '" + dr[1] + "',\n" +
                    "                                ESTIMATE_PERCENT => " + extimatePrc + ",\n" +
                    "                                DEGREE           => " + degree + ",\n" +
                    "                                NO_INVALIDATE    => " + noinvalidate + ",\n";

                    if (String.IsNullOrEmpty(partitionName))
                    {
                        sqlString += "                                GRANULARITY      => 'ALL',\n";
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(subpartitionName))
                        {
                            sqlString += "                                PARTNAME      => '" + partitionName + "',\n";
                            sqlString += "                                GRANULARITY      => 'PARTITION',\n";

                        }
                        else
                        {
                            sqlString += "                                PARTNAME      => '" + subpartitionName + "',\n";
                            sqlString += "                                GRANULARITY      => 'SUBPARTITION',\n";
                        }

                    }


                    sqlString += "                                METHOD_OPT       => 'FOR ALL COLUMNS SIZE AUTO',\n" +
                               "                                CASCADE          => " + cascade + ");\n" +
                               "end;";
                    
                    CmdGatherTable = new OracleCommand(sqlString, Connection);
                    CmdGatherTable.CommandType = CommandType.Text;

                    try
                    {
                        execBuffer += owner + "." + tableName;
                        Worker_Info(owner + "." + tableName, tableCounter, errorBufferCount, tableCount);
                        worker.ReportProgress(((tableCounter) * 100) / tableCount);
                        tableCounter++;
                        CmdGatherTable.ExecuteNonQuery();
                        execBuffer += "\t OK\t" + DateTime.Now + "\n";

                        execBufferCount++;
                    }
                    catch (OracleException ex)
                    {
                        worker.ReportProgress((tableCounter) * 100 / tableCount);
                        execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                        errorBuffer += owner + "." + tableName + "\n" + ex.Message.ToString() + "\n";
                        Worker_Info(owner + "." + tableName + " - ERROR ", tableCounter, errorBufferCount, tableCount);
                        errorBufferCount++;
                    }

                }


            }
            return 0;
        }

        private void tbtnStart_Click(object sender, EventArgs e)
        {
            progressBarMain.Value = 0;
            errorBuffer = String.Empty;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            label1.ForeColor = SystemColors.ControlText;

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is gathering statistics. Wait...");
            if (runTables.Rows.Count > 0)
            {
                if (!this.worker.IsBusy)
                {
                    autostop = false;
                    tbtnStart.Enabled = false;
                    tbtnStop.Enabled = true; tbtnStop.BackColor = Color.Green; tbtnStop.ForeColor = Color.White;

                    worker.RunWorkerAsync(runTables);
                }
                else
                    MessageBox.Show("Busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void tbtnStop_Click(object sender, EventArgs e)
        {
            autostop = true;
            if (CmdGatherTable != null )
            {
                label1.Text = ("\n\nCanceling operation WAIT... \n");
                worker.CancelAsync();
                CmdGatherTable.Cancel();
            }

            //if (CmdGatherIndex != null)
            //{
            //    label1.Text = ("\n\nCanceling operation WAIT... \n");
            //    worker.CancelAsync();
            //    CmdGatherIndex.Cancel();
            //}
            if (worker.IsBusy)
                worker.CancelAsync();

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tbtnStart.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            progressBarMain.Value = 0;
            
            if (errorBufferCount > 0)
            {
                label1.Text = execBufferCount.ToString() + " Statistics are updated " + errorBufferCount.ToString() + " errors occurred.";
                label1.ForeColor = Color.Red;
            }
            else
            {
                 label1.Text = "Statistics are updated succesfully";
                 label1.ForeColor = SystemColors.ControlText;
            }
            ClassSchemaStats.SetClientInfo(Connection, String.Empty);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = runquery((DataTable)e.Argument, degree, estimapepercent, noinvalidate, cascade, worker, e);
        }

        private void Worker_Info(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt)
        {
            if (label1.InvokeRequired)
            {
                Worker_Info_d wd = new Worker_Info_d(Worker_Info);
                this.Invoke(wd, buff, doneCnt, errCnt, allCnt);
            }
            else
            {
                label1.Text = "Done/Error/All : " +  (doneCnt == null ? "0" :  doneCnt.ToString()) + "/" + (errCnt == null ? "0" : errCnt.ToString()) + "/" + (allCnt == null ? "0" : allCnt.ToString()) + " > " + buff;
            }

        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassViewWindow.FormTextView textView = new ClassViewWindow.FormTextView();
            textView.richTextBox1.Text = execBuffer;
            textView.Font = new Font("Courier New", 8);
            if (textView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void errorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassViewWindow.FormTextView textView = new ClassViewWindow.FormTextView();
            textView.richTextBox1.Text = errorBuffer;
            textView.Font = new Font("Courier New", 8);
            if (textView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void FormSimpleRunStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                CmdGatherTable.Cancel();
            }
            Connection.Close();
        }
    }
}
