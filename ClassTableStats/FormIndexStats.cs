using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using Oracle.ManagedDataAccess.Client;


using System.Windows.Forms;

namespace ClassSchemaStats
{
    /// <summary>
    /// Dodana formatka (na podstawie statystyk tabel) do uszczegolowienia statystyk tylko dla poszczegolnych indeksow
    /// Data: 20-02-2023
    /// </summary>
    public partial class FormIndexStats : Form
    {
        public delegate void Worker_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);
        public delegate void Worker_InfoTxt_d(string buff);

        private readonly OracleConnection Connection;
        private readonly String owner = String.Empty;
        readonly string tableName = String.Empty;
        readonly string degree = string.Empty;
        readonly string estimatePercent = string.Empty;
        readonly string noInvalidate = string.Empty;
        private readonly OracleCommand cmdIndexGather;
        readonly BackgroundWorker worker;

        bool autostop = true;
        String errorBuffer = String.Empty;
        Int32 errorBufferCount = 0;
        String execBuffer = String.Empty;
        Int32 execBufferCount = 0;

        public FormIndexStats(OracleConnection Connection, string Owner, string Tablename, string Degree ,string EstimatePercent, string NoInvalidate)
        {
            InitializeComponent();
            this.Connection = Connection;

            cmdIndexGather = new OracleCommand();
            cmdIndexGather.Connection = Connection;

            owner = Owner;
            tableName = Tablename;

            degree = Degree;
            estimatePercent=EstimatePercent;
            noInvalidate = NoInvalidate;

            worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMain.Value = e.ProgressPercentage;
        }

        private void FormIndexStats_Load(object sender, EventArgs e)
        {
            ReloadIndexes(owner, tableName);
        }

        private void ReloadIndexes(string Owner, string TableName)
        {
            OracleCommand cmd = new OracleCommand(ClassSchemaStats.SQLStrings.GET_TABLE_INDEXES, Connection);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = Owner;
            cmd.Parameters.Add("table_name", OracleDbType.Varchar2).Value = TableName;
            
            OracleDataReader Indexreader = cmd.ExecuteReader();

            List<string> indexes = new List<string>();

            while (Indexreader.Read())
            {
                indexes.Add(Indexreader.GetValue(0).ToString());
            }
            Indexreader.Close();
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(indexes.ToArray());
        }

        /// <summary>
        /// Data: 20-02-2023
        /// Opis zmian: Statystyki tylko da Indeksów tabeli 
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Owner"></param>
        /// <param name="Degree"></param>
        /// <param name="estimatePercent"></param>
        /// <param name="noInvalidate"></param>
        /// <param name="runWorker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int Runquery(List<string> Indexes, string Owner, string Degree , string EstimatePercent, string NoInvalidate, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            Int32 indexCounter = 0;
            Worker_Info("Wait for Index prepare...", null, null, null);
            Int32 indexCount = Indexes.Count();

            foreach (string indexName in  Indexes)
            {
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
                    //string noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";


                    string sqlString = String.Empty;
                    sqlString += "BEGIN\n" +
                    "  DBMS_STATS.GATHER_INDEX_STATS(OWNNAME          => '" + Owner + "',\n" +
                    "                                INDNAME          => '" + indexName + "',\n" +
                    "                                ESTIMATE_PERCENT => " + EstimatePercent + ",\n" +
                    "                                DEGREE           => " + Degree + ",\n" +
                    "                                NO_INVALIDATE    => " + NoInvalidate + "\n" +
                    ");\n" +
                    "END;\n";

                    cmdIndexGather.CommandText = sqlString;

                    try
                    {
                        execBuffer += owner + "." + indexName;
                        Worker_Info(owner + "." + indexName, indexCounter, errorBufferCount, indexCount);
                        worker.ReportProgress(((indexCounter) * 100) / indexCount);
                        indexCounter++;
                        Worker_InfoTxt(sqlString);
                        cmdIndexGather.ExecuteNonQuery();
                        execBuffer += "\t OK\t" + DateTime.Now + "\n";
                        execBufferCount++;

                    }
                    catch (OracleException ex)
                    {
                        worker.ReportProgress((indexCounter) * 100 / indexCount);
                        execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                        errorBuffer += owner + "." + indexName + "\n" + ex.Message.ToString() + "\n";
                        Worker_Info(owner + "." + indexName + " - ERROR ", indexCounter, errorBufferCount, indexCount);
                        errorBufferCount++;
                    }
                }
            }
            return 0;
        }

        private void TbtnStop_Click(object sender, EventArgs e)
        {
            autostop = true;
            if (cmdIndexGather != null )
            {
                label1.Text = ("\n\nCanceling operation WAIT... \n");
                worker.CancelAsync();
                cmdIndexGather.Cancel();
            }

            if (cmdIndexGather != null)
            {
                label1.Text = ("\n\nCanceling operation WAIT... \n");
                worker.CancelAsync();
                cmdIndexGather.Cancel();
            }
            if (worker.IsBusy)
                worker.CancelAsync();

        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gatherStatisticToolStripMenuItem.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            progressBarMain.Value = 0;
            
            if (errorBufferCount > 0)
            {
                label1.Text = execBufferCount.ToString() + " Index statistics are updated \n" + errorBufferCount.ToString() + " errors occurred.\nCheck errlor log";
                label1.ForeColor = Color.Red;
            }
            else
            {
                 label1.Text = "Index statistics are updated succesfully";
                 label1.ForeColor = SystemColors.ControlText;
            }
            ClassSchemaStats.SetClientInfo(Connection, String.Empty);
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Runquery((List<String>)e.Argument, owner, degree, estimatePercent, noInvalidate, worker, e);
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
                string info = "Done/Error/All : " + (doneCnt == null ? "0" : doneCnt.ToString()) + "/" + (errCnt == null ? "0" : errCnt.ToString()) + "/" + (allCnt == null ? "0" : allCnt.ToString()) + " > " + buff;
                label1.Text = info;
            }

        }

        private void Worker_InfoTxt(string buff)
        {
            if (label1.InvokeRequired)
            {
                Worker_InfoTxt_d wdt = new Worker_InfoTxt_d(Worker_InfoTxt);
                this.Invoke(wdt, buff);
            }
            else
            {
                richTextBox1.AppendText(buff +"\n/\n");
            }

        }

        private void LogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassViewWindow.FormTextView textView = new ClassViewWindow.FormTextView();
            textView.richTextBox1.Text = execBuffer;
            textView.Font = new Font("Courier New", 8);
            if (textView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void ErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassViewWindow.FormTextView textView = new ClassViewWindow.FormTextView();
            textView.richTextBox1.Text = errorBuffer;
            textView.Font = new Font("Courier New", 8);
            if (textView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadIndexes(owner, tableName);
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void GatherStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBarMain.Value = 0;
            errorBuffer = String.Empty;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            label1.ForeColor = SystemColors.ControlText;

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is gathering index statistics. Wait...");

            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }

            if (schemas.Count > 0)
            {
                if (!this.worker.IsBusy)
                {
                    autostop = false;
                    gatherStatisticToolStripMenuItem.Enabled = false;
                    tbtnStop.Enabled = true; tbtnStop.BackColor = Color.Green; tbtnStop.ForeColor = Color.White;

                    worker.RunWorkerAsync(schemas);
                }
                else
                    MessageBox.Show("Busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FormIndexStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                cmdIndexGather.Cancel();
            }
            Connection.Close();
        }
    }
}
