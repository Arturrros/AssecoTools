using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClassSchemaStats
{
    public partial class FormTableStats : Form
    {
        SessionOptions sessionOptions;
        Assembly assembly;
        ResourceManager resman;
        bool temporaryMode = false;
        public delegate void Worker_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);
        public delegate void Worker_InfoTxt_d(string buff);

        OracleConnection Connection;

        string owner = string.Empty;
        string degree = string.Empty;
        string estimatePercent = string.Empty;
        string noinvalidate = string.Empty;
        string cascade = string.Empty;
        string granularity = string.Empty;
        string opt = string.Empty;
        string gatheropt = string.Empty;

        bool GatherStatisticsAuto = false;

        OracleCommand CmdGatherTable;
        //OracleCommand CmdGatherIndex;

        BackgroundWorker worker;

        bool autostop = true;
        String errorBuffer = String.Empty;
        Int32 errorBufferCount = 0;
        String execBuffer = String.Empty;
        Int32 execBufferCount = 0;

        public FormTableStats(OracleConnection Connection, string Owner, SessionOptions sessionOptions, bool TemporaryMode)
        {
            InitializeComponent();
            this.sessionOptions = sessionOptions;
            InitializeLanguage(sessionOptions.CI);

            this.Connection = Connection;
            temporaryMode = TemporaryMode;
            if (temporaryMode) 
            {
                groupBox4.Enabled = false;
            }
            

            if (temporaryMode == false)
                checkedListBox1.ContextMenuStrip = contextMenuStrip1;
            else
                checkedListBox1.ContextMenuStrip = contextMenuStripTemporary;


            CmdGatherTable = new OracleCommand();
            CmdGatherTable.Connection = Connection;

            owner = Owner;
            //degree = Degree;
            //estimatePercent=EstimatePercent;
            //noInvalidate = NoInvalidate;
            //cascade = Cascade;  

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
            
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }

            
        }
        
        void InitializeLanguage(CultureInfo ci)
        {
            assembly = Assembly.Load("AssecoTools");
            resman = new ResourceManager("AssecoTools.Lang.LangRes", assembly);

            tabPage1.Text = resman.GetString("FormTableStats_TabPage1_Text", ci);




        }
            void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMain.Value = e.ProgressPercentage;
        }

        private void FormTableStats_Load(object sender, EventArgs e)
        {
            ReloadTables(owner, temporaryMode);
        }

        private void ReloadTables(string Owner, bool temporary)
        {
            OracleCommand cmd = new OracleCommand(ClassSchemaStats.SQLStrings.GET_TABLES, Connection);
            cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = Owner;
            cmd.Parameters.Add("temporary", OracleDbType.Varchar2).Value = (temporary == true ? 'Y' : 'N');
            
            OracleDataReader reader = cmd.ExecuteReader();

            List<string> tables = new List<string>();

            while (reader.Read())
            {
                tables.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(tables.ToArray());
        }

        /// <summary>
        /// Data: 20-09-2022
        /// Opis zmian: Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// </summary>
        /// <param name="Tables"></param>
        /// <param name="Owner"></param>
        /// <param name="Degree"></param>
        /// <param name="estimatePercent"></param>
        /// <param name="noInvalidate"></param>
        /// <param name="Cascade"></param>
        /// <param name="runWorker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int runquery(List<string> Tables, string Owner, string Degree , string EstimatePercent, string NoInvalidate, string Cascade, string Granularity, string Opt, string GatherOpt, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            Int32 tableCounter = 0;

            Worker_Info("Wait for prepare...", null, null, null);

            //ClassSchemaStats.SetSchemaPrefs(Connection, owner);

            Int32 tableCount = Tables.Count();


            foreach (string tableName in  Tables)
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


                    string sqlString = String.Empty;
                    sqlString += "BEGIN\n" +
                    "  DBMS_STATS.GATHER_TABLE_STATS(OWNNAME          => '" + Owner + "',\n" +
                    "                                TABNAME          => '" + tableName + "',\n" +
                    "                                ESTIMATE_PERCENT => " + EstimatePercent + ",\n" +
                    "                                DEGREE           => " + Degree.ToString() + ",\n" +
                    "                                NO_INVALIDATE    => " + NoInvalidate + ",\n" +
                    "                                GRANULARITY      => " + "'" + Granularity + "',\n" +
                    "                                METHOD_OPT       => " + "'" + Opt + "',\n" +
                    "                                OPTIONS          => " + "'" + GatherOpt + "',\n" +
                    "                                CASCADE          => " + Cascade + ");\n" +
                    "END;\n";

                    CmdGatherTable.CommandText = sqlString;
                    //CmdGatherTable.CommandType = CommandType.Text;
                    //CmdGatherTable.BindByName = true;

                    try
                    {

                        execBuffer += owner + "." + tableName;
                        Worker_Info(owner + "." + tableName, tableCounter, errorBufferCount, tableCount);
                        worker.ReportProgress(((tableCounter) * 100) / tableCount);
                        tableCounter++;
                        Worker_InfoTxt(sqlString);

                        Int32 StartTime = System.Environment.TickCount;
                        
                        CmdGatherTable.ExecuteNonQuery();
                        
                        execBuffer += "\t OK\t" + DateTime.Now + "\n";
                        execBufferCount++;

                        decimal ElapsedTime = (Environment.TickCount - StartTime) / 1000M;

                        new StatsTime((Connection.PDBName == null ? Connection.DatabaseName : Connection.PDBName), Owner, tableName, sqlString, Convert.ToString(ElapsedTime)).Save();

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

        private int runqueryAuto(List<string> Tables, string Owner, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            Int32 tableCounter = 0;

            Worker_Info("Wait for prepare...", null, null, null);

            //ClassSchemaStats.SetSchemaPrefs(Connection, owner);

            Int32 tableCount = Tables.Count();


            foreach (string tableName in Tables)
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


                    string sqlString = String.Empty;
                    sqlString += "BEGIN\n" +
                    "  DBMS_STATS.GATHER_TABLE_STATS(OWNNAME          => '" + Owner + "',\n" +
                    "                                TABNAME          => '" + tableName + "');\n" +
                    "END;\n";

                    CmdGatherTable.CommandText = sqlString;

                    try
                    {

                        execBuffer += owner + "." + tableName;
                        Worker_Info(owner + "." + tableName, tableCounter, errorBufferCount, tableCount);
                        worker.ReportProgress(((tableCounter) * 100) / tableCount);
                        tableCounter++;
                        Worker_InfoTxt(sqlString);

                        Int32 StartTime = System.Environment.TickCount;
                        CmdGatherTable.ExecuteNonQuery();
                        
                        execBuffer += "\t OK\t" + DateTime.Now + "\n";
                        execBufferCount++;
                        decimal ElapsedTime = (Environment.TickCount - StartTime) / 1000M;

                        new StatsTime((Connection.PDBName == null ? Connection.DatabaseName : Connection.PDBName), Owner, tableName, sqlString, Convert.ToString(ElapsedTime)).Save();

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
            gatherStatisticToolStripMenuItem.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            progressBarMain.Value = 0;
            
            if (errorBufferCount > 0)
            {
                label1.Text = execBufferCount.ToString() + " Statistics are updated \n" + errorBufferCount.ToString() + " errors occurred.\nCheck errlor log";
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
            if(!GatherStatisticsAuto)
                e.Result = runquery((List<String>)e.Argument, owner, degree, estimatePercent, noinvalidate, cascade, granularity, opt, gatheropt, worker, e);
            else
                e.Result = runqueryAuto((List<String>)e.Argument, owner, worker, e);
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

        private void unlockSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ControlText;
            label1.Text = "Unlock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.UnlockTableStats(Connection, owner ,checkedListBox1.SelectedItem.ToString());

            label1.Text = "Schema stats are unlocked";
        }

        private void lockSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.ForeColor = SystemColors.ControlText;
            label1.Text = "Lock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.LockTableStats(Connection, owner, checkedListBox1.SelectedItem.ToString());
            
            label1.Text = "Schema stats are locked";
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadTables(owner, temporaryMode);
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }
        private void unSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }
        private void gatherStatisticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GatherStatistic(false);
        }
        private void gatherStatisticAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GatherStatistic(true);
        }
        private void GatherStatistic(bool auto)
        {
            GatherStatisticsAuto = auto;

            richTextBox1.Clear();
            progressBarMain.Value = 0;
            errorBuffer = String.Empty;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            label1.ForeColor = SystemColors.ControlText;

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is gathering statistics. Wait...");

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

                    //ustawienie opcji statystyk dla 
                    if (!auto) 
                        SetStatsValues();

                    worker.RunWorkerAsync(schemas);
                }
                else
                    MessageBox.Show("Busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void SetStatsValues()
        {
            if (radioButton5.Checked)
                estimatePercent = "DBMS_STATS.AUTO_SAMPLE_SIZE ";
            if (radioButton6.Checked)
                estimatePercent = textBox4.Text;

            if (radioButton19.Checked)
                noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";
            if (radioButton20.Checked)
                noinvalidate = "TRUE";
            if (radioButton21.Checked)
                noinvalidate = "FALSE";

            if (radioButton24.Checked)
                cascade = "DBMS_STATS.AUTO_CASCADE";
            if (radioButton23.Checked)
                cascade = "TRUE";
            if (radioButton22.Checked)
                cascade = "FALSE";

            if (radioButton8.Checked)
                degree = "DBMS_STATS.AUTO_DEGREE";
            if (radioButton9.Checked)
                degree = textBox5.Text;
            if (radioButton10.Checked)
                degree = "DBMS_STATS.DEFAULT_DEGREE";

            if (radioButton11.Checked)
                granularity = "AUTO";
            if (radioButton14.Checked)
                granularity = "ALL";
            if (radioButton15.Checked)
                granularity = "GLOBAL";
            if (radioButton16.Checked)
                granularity = "GLOBAL AND PARTITION";
            if (radioButton17.Checked)
                granularity = "PARTITION";
            if (radioButton18.Checked)
                granularity = "SUBPARTITION";

            if (checkBox8.Checked)
            {
                if (radioButton12.Checked)
                {
                    opt = "FOR ALL INDEXED COLUMNS SIZE AUTO";
                }
                else
                {
                    opt = "FOR ALL INDEXED COLUMNS SIZE " + numericUpDown1.Value.ToString();
                }
            }
            else
            {
                if (radioButton12.Checked)
                {
                    opt = "FOR ALL COLUMNS SIZE AUTO";

                }
                else
                {
                    opt = "FOR ALL COLUMNS SIZE " + numericUpDown1.Value.ToString();
                }
            }

            if (radioButton2.Checked)
                gatheropt = "GATHER";
            if (radioButton1.Checked)
                gatheropt = "GATHER AUTO";

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex >= 0)
            {
                string tableName = checkedListBox1.SelectedItem.ToString();
                OracleCommand cmd = new OracleCommand(ClassSchemaStats.SQLStrings.GET_TABLES, Connection);
                cmd.Parameters.Add("owner", OracleDbType.Varchar2).Value = Owner;
                cmd.Parameters.Add("temporary", OracleDbType.Varchar2).Value = 'N';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int x = checkedListBox1.FindString(textBox1.Text.ToUpper());
            if (x != -1)
                checkedListBox1.SetSelected(x, true);
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (String tablen in checkedListBox1.CheckedItems)
            {
                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                SetStatsValues();
                FormIndexStats fts = new FormIndexStats(conntmp, owner, tablen, degree, estimatePercent, noinvalidate, sessionOptions);
                
                fts.StartPosition = FormStartPosition.CenterParent;
                fts.Show(this);
            }
        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
            //string tablen = checkedListBox1.SelectedItem.ToString();
            //OracleConnection conntmp = (OracleConnection)Connection.Clone();
            //conntmp.Open();
            //FormIndexStats fts = new FormIndexStats(conntmp, owner, tablen, degree, estimatePercent, noInvalidate);

            //fts.Show(this);
        }

        private void FormTableStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                CmdGatherTable.Cancel();
            }
            
            Connection.Close();
        }

        private void statystykiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (String tablen in checkedListBox1.CheckedItems)
            {
                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();

                FormStatsColsDetailInfo fsih = new FormStatsColsDetailInfo(conntmp, owner, tablen, sessionOptions);
                fsih.StartPosition = FormStartPosition.CenterParent;
                fsih.Show(this);
            }
        }

        private void setTablePrefsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (String tablen in checkedListBox1.CheckedItems)
            {
                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                SetStatsValues();
                FormSetTablePrefs fts = new FormSetTablePrefs(conntmp, owner, tablen, sessionOptions);

                fts.StartPosition = FormStartPosition.CenterParent;
                fts.Show(this);
            }
        }


        //
        // GTT Stats Region
        //


        #region Temporary Tables Operations - GTT

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                richTextBox1.AppendText (tableName + " Level: " + new GTT(Connection, owner, tableName).CheckGttScope() + "\n");
            }
        }

        private void setSharedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                richTextBox1.AppendText(tableName + " Scope: " + new GTT(Connection, owner, tableName).CheckGttScope() + "\n");
                try
                {
                    new GTT(Connection, owner, tableName).SetGttLevel("SHARED");
                    richTextBox1.AppendText("New Scope: " + new GTT(Connection, owner, tableName).CheckGttScope() + "\n");
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                
            }
        }

        private void setSESSIONDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                richTextBox1.AppendText(tableName + " Scope: " + new GTT(Connection, owner, tableName).CheckGttScope() + "\n");
                try
                {
                    new GTT(Connection, owner, tableName).SetGttLevel("SESSION");
                    richTextBox1.AppendText("New Scope: " + new GTT(Connection, owner, tableName).CheckGttScope() + "\n");
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                
            }
        }

        private void deleteTableStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.Text = "";
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                try
                {
                    new GTT(Connection, owner, tableName).DeleteTableSharedStats();
                    richTextBox1.AppendText("Delete stats on " + tableName + " has been executed, and the scope has been set to SESSION" + "\n");
                }
                catch (Exception exc) 
                {
                    //throw exc;
                    label1.Text = exc.Message.ToString();
                    //richTextBox1.AppendText("Delete stats on " + tableName + " it isn’t necessary" + "\n");
                }
            }
        }

        private void deleteTableStatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.Text = "";
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
               new GTT(Connection, owner, tableName).DeleteTableStats();
               richTextBox1.AppendText("Delete stats on " + tableName + " has been executed" + "\n");
            }
        }


        private void needDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                bool ret = new GTT(Connection, owner, tableName).needDeleteSharedStats();
                if (ret)
                    richTextBox1.AppendText("Table " + tableName + " >>> YES <<< The statistics need to be deleted \n");
                else
                    richTextBox1.AppendText("Table " + tableName + " >>> NO <<< There's no need to delete the statistics" + "\n");
            }
        }

        private void gatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.Text = "";
            foreach (String tableName in checkedListBox1.CheckedItems)
            {
                GTT gt = new GTT(Connection, owner, tableName);
                if (gt.CheckGttScope() == "SHARED")
                {
                    try
                    {
                        gt.GatherTableStats();
                        label1.Text = "Statistics Done.";
                        richTextBox1.AppendText("INFO ONLY: Do you really need SHARED stats on temporary???");
                    }
                    catch (Exception exc)
                    {
                        label1.Text = exc.Message;
                    }


                }
                else
                {
                    richTextBox1.AppendText("Table scope is SESSION. There is no need to gather statistics at this stage\n");
                    richTextBox1.AppendText("If you need statistics on a temporary table, first set the SHARED scope, but remember... this is old style from Oracle 11 ");
                }
            }
        }


        #endregion

        private void unlockTableStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.ForeColor = SystemColors.ControlText;
            label1.Text = "Unlock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.UnlockTableStats(Connection, owner, checkedListBox1.SelectedItem.ToString());

            label1.Text = "Schema stats are unlocked";
        }

        private void lockTableStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label1.ForeColor = SystemColors.ControlText;
            label1.Text = "Unlock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.LockTableStats(Connection, owner, checkedListBox1.SelectedItem.ToString());

            label1.Text = "Schema stats are locked";
        }

       
    }
}
