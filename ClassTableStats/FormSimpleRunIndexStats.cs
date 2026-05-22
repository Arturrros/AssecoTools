using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using Oracle.DataAccess.Client;


using System.Windows.Forms;

namespace ClassSchemaStats
{
    public partial class FormSimpleRunIndexStats : Form
    {
        public delegate void Worker_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);

        OracleConnection Connection;

        string owner = String.Empty;

        OracleCommand CmdGatherTable;
 
        BackgroundWorker worker;

        bool autostop = true;
        string errorBuffer = String.Empty;
        Int32 errorBufferCount = 0;
        string execBuffer = String.Empty;
        Int32 execBufferCount = 0;
        string objectOwner = String.Empty;
        string objectName = String.Empty;
        string objectType = String.Empty;

        bool noinvalidate;
        bool cascade;
        int degree;
        int estimapepercent;

        public FormSimpleRunIndexStats(string connectionString, string ObjectOwner, string ObjectName, string ObjectType, int Degree, int estimatePercent, bool noInvalidate, bool Cascade)
        {
            InitializeComponent();
            Connection = new OracleConnection(connectionString);
            objectOwner = ObjectOwner;
            objectName = ObjectName;
            objectType = ObjectType;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

             noinvalidate = noInvalidate;
             cascade = Cascade;
             degree = Degree;
             estimapepercent = estimatePercent;
        }

        private void FormTableStats_Load(object sender, EventArgs e)
        {
            Connection.Open();
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
        private int runquery( int degree , int extimatePrc, bool noInvalidate, bool Cascade, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            Int32 tableCounter = 0;

            Worker_Info("Wait for prepare...", null, null, null);

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

                if (objectType == "TABLE")
                {
                    if (Cascade == true)
                        cascade = "TRUE";
                    else
                        cascade = "FALSE";
                }

                string sqlString = "begin\n";
                sqlString += "  DBMS_STATS.GATHER_TABLE_STATS(ownname          => '" + objectOwner + "',\n";

                if (objectType == "TABLE")
                {
                    sqlString += "                                tabname          => '" + objectName + "',\n";
                }
                else if (objectType == "INDEX")
                {
                    sqlString += "                                indname          => '" + objectName + "',\n";
                }
                else
                {
                    return 0;
                }

                sqlString +=
                "                                ESTIMATE_PERCENT => " + extimatePrc + ",\n" +
                "                                DEGREE           => " + degree + ",\n" +
                "                                NO_INVALIDATE    => " + noinvalidate + ",\n";


                if (objectType == "TABLE")
                {
                    sqlString += "               METHOD_OPT       => 'FOR ALL COLUMNS SIZE AUTO',\n";
                    sqlString += "               CASCADE          => " + cascade + ");\n";
                }
                else if (objectType == "INDEX")
                {
                    sqlString += "               METHOD_OPT       => 'FOR ALL COLUMNS SIZE AUTO'\n";
                    sqlString += "               );\n";
                }
                else
                {
                    return 0;
                }


                sqlString += "end;";

                CmdGatherTable = new OracleCommand(sqlString, Connection);
                CmdGatherTable.CommandType = CommandType.Text;

                try
                {
                    execBuffer += owner + "." + objectName;
                    Worker_Info(owner + "." + objectName, tableCounter, errorBufferCount, 1);
                    //worker.ReportProgress(((tableCounter) * 100) / tableCount);
                    tableCounter++;
                    CmdGatherTable.ExecuteNonQuery();
                    execBuffer += "\t OK\t" + DateTime.Now + "\n";

                    execBufferCount++;
                }
                catch (OracleException ex)
                {
                    //worker.ReportProgress((tableCounter) * 100 / tableCount);
                    execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                    errorBuffer += owner + "." + objectName + "\n" + ex.Message.ToString() + "\n";
                    Worker_Info(owner + "." + objectName + " - ERROR ", tableCounter, errorBufferCount, 1);
                    errorBufferCount++;
                }
            }
            return 0;
        }

        private void tbtnStart_Click(object sender, EventArgs e)
        {
            errorBuffer = String.Empty;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            label1.ForeColor = SystemColors.ControlText;

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is gathering statistics. Wait...");
                if (!this.worker.IsBusy)
                {
                    autostop = false;
                    tbtnStart.Enabled = false;
                    tbtnStop.Enabled = true; tbtnStop.BackColor = Color.Green; tbtnStop.ForeColor = Color.White;

                    worker.RunWorkerAsync();
                }
                else
                    MessageBox.Show("Busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            

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

            if (worker.IsBusy)
                worker.CancelAsync();

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tbtnStart.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            
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
            e.Result = runquery( degree, estimapepercent, noinvalidate, cascade, worker, e);
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

    }
}
