using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;
using System.Resources;
using AssecoToolsOptions;

using System.Windows.Forms;
using System.Reflection;

namespace ClassSchemaStats
{
    public partial class FormSchemaStats : Form
    {
        SessionOptions sessionOptions;
        Assembly assembly;
        ResourceManager resman;
        CultureInfo culture; 

        public delegate void WorkerGather_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);
        public delegate void WorkerCheck_Info_d(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt);

        OracleConnection Connection;

        OracleCommand CmdGatherTable;
        OracleCommand CmdCheck;

        BackgroundWorker workerStats;
        BackgroundWorker workerCheck;

        bool autostop = true;
        String errorBuffer = String.Empty;
        Int32 errorBufferCount = 0;
        String execBuffer = String.Empty;
        Int32 execBufferCount = 0;


        public FormSchemaStats(OracleConnection Connection , CultureInfo Culture, SessionOptions sessionOptions)
        {
            InitializeComponent();
            this.Connection = Connection;

            workerStats = new BackgroundWorker();
            workerStats.WorkerSupportsCancellation = true;
            workerStats.WorkerReportsProgress = true;

            workerStats.DoWork += worker_DoWork;
            workerStats.RunWorkerCompleted += worker_RunWorkerCompleted;
            workerStats.ProgressChanged += worker_ProgressChanged;

            workerCheck = new BackgroundWorker();
            workerCheck.WorkerSupportsCancellation = true;
            workerCheck.WorkerReportsProgress = true;

            workerCheck.DoWork += workerCheck_DoWork;
            workerCheck.RunWorkerCompleted += workerCheck_RunWorkerCompleted;
            workerCheck.ProgressChanged += workerCheck_ProgressChanged;

            culture = Culture;
            InitializeLanguage(culture);
            CmdGatherTable = new OracleCommand();
            CmdGatherTable.Connection = Connection;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }
        
        void InitializeLanguage(CultureInfo ci)
        {
            assembly = Assembly.Load("AssecoTools");
            resman = new ResourceManager("AssecoTools.Lang.LangRes", assembly);
            tabPage1.Text = resman.GetString("FormSchemaStats_tabPage1_Text", ci);

            checkStatisticsToolStripMenuItem.Text = resman.GetString("FormSchemaStats_checkStatisticsToolStripMenuItem_Text", ci);
            gatherStatisticsToolStripMenuItem.Text = resman.GetString("FormSchemaStats_gatherStatisticsToolStripMenuItem_Text", ci);
            unlockSchemaToolStripMenuItem.Text = resman.GetString("FormSchemaStats_unlockSchemaToolStripMenuItem_Text", ci);
            lockSchemaToolStripMenuItem.Text = resman.GetString("FormSchemaStats_lockSchemaToolStripMenuItem_Text", ci);
            refreshToolStripMenuItem.Text = resman.GetString("FormSchemaStats_refreshToolStripMenuItem_Text", ci);
            tablesToolStripMenuItem.Text = resman.GetString("FormSchemaStats_tablesToolStripMenuItem_Text", ci);
            showStatTablesToolStripMenuItem.Text = resman.GetString("FormSchemaStats_showStatTablesToolStripMenuItem_Text", ci);
            toolStrip1.Text = resman.GetString("FormSchemaStats_toolStrip1_Text", ci);
            tbtnStop.Text = resman.GetString("FormSchemaStats_tbtnStop_Text", ci);
            toolStripDropDownButton1.Text = resman.GetString("FormSchemaStats_toolStripDropDownButton1_Text", ci);
            logToolStripMenuItem.Text = resman.GetString("FormSchemaStats_logToolStripMenuItem_Text", ci);
            errorToolStripMenuItem.Text = resman.GetString("FormSchemaStats_errorToolStripMenuItem_Text", ci);
            groupBox1.Text = resman.GetString("FormSchemaStats_groupBox1_Text", ci);
            tabPage1.Text = resman.GetString("FormSchemaStats_tabPage1_Text", ci);
            tabPage2.Text = resman.GetString("FormSchemaStats_tabPage2_Text", ci);
            label6.Text = resman.GetString("FormSchemaStats_label6_Text", ci);
            label5.Text = resman.GetString("FormSchemaStats_label5_Text", ci);
            label4.Text = resman.GetString("FormSchemaStats_label4_Text", ci);
            groupBox2.Text = resman.GetString("FormSchemaStats_groupBox2_Text", ci);
            toolStripButton3.Text = resman.GetString("FormSchemaStats_toolStripButton3_Text", ci);
            tbtnALL.Text = resman.GetString("FormSchemaStats_toolStripButton1_Text", ci);
            tbtnPRC.Text = resman.GetString("FormSchemaStats_toolStripButton2_Text", ci);
            tbtnCOLS.Text = resman.GetString("FormSchemaStats_toolStripButton7_Text", ci);
            tbtsMOD.Text = resman.GetString("FormSchemaStats_toolStripButton5_Text", ci);
            tbtnMISS.Text = resman.GetString("FormSchemaStats_toolStripButton6_Text", ci);
            tbtnCOUNT.Text = resman.GetString("FormSchemaStats_toolStripButton4_Text", ci);
            tbtnCreateScript.Text = resman.GetString("FormSchemaStats_tbtnCreateScript_Text", ci);
            label3.Text = resman.GetString("FormSchemaStats_label3_Text", ci);
            labelObject.Text = resman.GetString("FormSchemaStats_labelObject_Text", ci);
            label1.Text = resman.GetString("FormSchemaStats_label1_Text", ci);
            createScriptToolStripMenuItem.Text = resman.GetString("FormSchemaStats_createScriptToolStripMenuItem_Text", ci);
        }
        
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMain.Value = e.ProgressPercentage;
        }

        private void FormTableStats_Load(object sender, EventArgs e)
        {
            ReloadSchemas();
        }

        private void ReloadSchemas()
        {
            OracleCommand cmd = new OracleCommand(ClassSchemaStats.SQLStrings.GET_USERS_19, Connection);
            OracleDataReader reader = cmd.ExecuteReader();

            List<string> schemas = new List<string>();

            while (reader.Read())
            {
                schemas.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(schemas.ToArray());
        }

        /// <summary>
        /// Data: 20-09-2022
        /// Opis zmian: Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// </summary>
        /// <param name="schemas"></param>
        /// <param name="degree"></param>
        /// <param name="extimatePrc"></param>
        /// <param name="noInvalidate"></param>
        /// <param name="Cascade"></param>
        /// <param name="runWorker"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int runquery(List<string> schemas, int degree ,int extimatePrc,bool noInvalidate,bool Cascade, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            DataTable tables = new DataTable();
            Int32 schemaCounter = 0;

            Int32 tableCounter = 0;

            WorkerGather_Info("Wait for prepare...", null, null, null);

            labelError.ForeColor = SystemColors.ControlText;

            foreach (string schema in schemas)
            {
               // ClassSchemaStats.SetSchemaPrefs(Connection, schema);
                ClassSchemaStats.GetTables(Connection, schema, tables);
                schemaCounter++;
            }

            Int32 tableCount = tables.Rows.Count;

            foreach (DataRow dr in  tables.Rows)
            {
                if (autostop == true)
                {
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
                        noinvalidate = "TRUE";
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
                    "                                NO_INVALIDATE    => " + noinvalidate + ",\n" +
                    "                                GRANULARITY      => 'ALL',\n" +
                    "                                METHOD_OPT       => 'FOR ALL COLUMNS SIZE AUTO',\n" +
                    "                                CASCADE          => " + cascade + ");\n" +
                    "end;";

                    CmdGatherTable.CommandText = sqlString;
                    
                    CmdGatherTable.CommandType = CommandType.Text;

                    try
                    {
                        execBuffer += dr[0].ToString() + "." + dr[1].ToString();
                        WorkerGather_Info(dr[0].ToString() + "." + dr[1].ToString(), tableCounter, errorBufferCount, tableCount);
                        workerStats.ReportProgress(((tableCounter) * 100) / tableCount);
                        tableCounter++;
                        CmdGatherTable.ExecuteNonQuery();
                        execBuffer += "\t OK\t" + DateTime.Now + "\n";
                        execBufferCount++;
                    }
                    catch (OracleException ex)
                    {
                        workerStats.ReportProgress((tableCounter) * 100 / tableCount);
                        execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                        errorBuffer += dr[0].ToString() + "." + dr[1].ToString() + "\n" + ex.Message.ToString() + "\n";
                        WorkerGather_Info(dr[0].ToString() + "." + dr[1].ToString() + " - ERROR ", tableCounter, errorBufferCount, tableCount);
                        errorBufferCount++;
                    }

                }


            }
            return 0;
        }

        private void gatherStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            progressBarMain.Value = 0;
            errorBuffer = String.Empty;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is gathering statistics. Wait...");

            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }

            if (schemas.Count > 0)
            {
                labelTitle.Text = resman.GetString("FormSchemaStats_label1_Worker_DoWork", culture);

                if (!this.workerStats.IsBusy)
                {
                    autostop = false;
                    gatherStatisticsToolStripMenuItem.Enabled = false;
                    tbtnStop.Enabled = true; tbtnStop.BackColor = Color.Green; tbtnStop.ForeColor = Color.White;

                    workerStats.RunWorkerAsync(schemas);
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
                workerStats.CancelAsync();
                CmdGatherTable.Cancel();
            }

            if (workerStats.IsBusy)
            {
                workerStats.CancelAsync();
                CmdGatherTable.Cancel();
            }

            if (CmdCheck != null)
            {
                workerCheck.CancelAsync();
                CmdCheck.Cancel();
            }

            if (workerCheck.IsBusy)
            {
                workerCheck.CancelAsync();
                CmdCheck.Cancel();
            }

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gatherStatisticsToolStripMenuItem.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            progressBarMain.Value = 0;
            
            if (errorBufferCount > 0)
            {
                labelError.Text = errorBufferCount.ToString() + " errors occurred. Check errlor log";
                labelError.ForeColor = Color.Red;
            }
            else
            {
                labelObject.Text = string.Empty;
                labelDone.Text = string.Empty;
                labelError.Text = string.Empty; 
                labelError.ForeColor = SystemColors.ControlText;
            }
            ClassSchemaStats.SetClientInfo(Connection, String.Empty);
            labelTitle.Text = resman.GetString("FormSchemaStats_label1_Worker_Done", culture);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = runquery((List<String>)e.Argument,Convert.ToInt16(txtParallel.Text),Convert.ToInt16(txtEstimatePrc.Text),checkBox1.Checked, checkBox2.Checked, worker, e);
        }

        private void WorkerGather_Info(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt)
        {
            if (panel1.InvokeRequired)
            {
                WorkerGather_Info_d wd = new WorkerGather_Info_d(WorkerGather_Info);
                this.Invoke(wd, buff, doneCnt, errCnt, allCnt);
            }
            else
            {
                labelObject.Text = buff;
                labelDone.Text = (doneCnt == null ? "0" : doneCnt.ToString()) + "/" + (allCnt == null ? "0" : allCnt.ToString());
                labelError.Text = (errCnt == null ? "0" : errCnt.ToString());
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
            labelTitle.ForeColor = SystemColors.ControlText;
            labelTitle.Text = "Unlock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.UnlockSchemaStats(Connection, checkedListBox1.SelectedItem.ToString());

            labelTitle.Text = "Schema stats are unlocked";
        }

        private void lockSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelTitle.ForeColor = SystemColors.ControlText;
            labelTitle.Text = "Lock shema stats. Wait...";
            this.Refresh();
            ClassSchemaStats.LockSchemaStats(Connection, checkedListBox1.SelectedItem.ToString());

            labelTitle.Text = "Schema stats are locked";

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadSchemas();
        }

        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (String schema in checkedListBox1.CheckedItems)
            {
                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                //FormTableStats fts = new FormTableStats(conntmp, schema, Convert.ToInt16(txtParallel.Text), Convert.ToInt16(txtEstimatePrc.Text), checkBox1.Checked,checkBox2.Checked);
                FormTableStats fts = new FormTableStats(conntmp, schema, sessionOptions);
                fts.StartPosition = FormStartPosition.CenterScreen;
                fts.Show(this);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Check

        private void checkStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            progressBarMain.Value = 0;
            errorBufferCount = 0;
            execBuffer = String.Empty;
            execBufferCount = 0;
            labelTitle.Text = resman.GetString("FormSchemaStats_label1_Worker_DoWork", culture);
            labelError.Text = "";
            dataGridView1.DataSource = null;

            ClassSchemaStats.SetClientInfo(Connection, "ASSECO_TOOLS is checing statistics. Wait...");

            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }

            if (schemas.Count > 0)
            {
                if (!this.workerStats.IsBusy)
                {
                    autostop = false;
                    checkStatisticsToolStripMenuItem.Enabled = false;
                    tbtnStop.Enabled = true; tbtnStop.BackColor = Color.Green; tbtnStop.ForeColor = Color.White;

                    workerCheck.RunWorkerAsync(schemas);
                }
                else
                    MessageBox.Show("Busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void workerCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMain.Value = e.ProgressPercentage;
        }

        void workerCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            checkStatisticsToolStripMenuItem.Enabled = true;
            tbtnStop.Enabled = false; tbtnStop.BackColor = SystemColors.Control; tbtnStop.ForeColor = SystemColors.WindowText;
            progressBarMain.Value = 0;

            if (errorBufferCount > 0)
            {
                labelError.Text = errorBufferCount.ToString();
                labelError.ForeColor = Color.Red;
            }
            else
            {
                labelObject.Text = string.Empty;
                labelDone.Text = string.Empty;
                labelError.Text = "0";
                labelError.ForeColor = SystemColors.ControlText;
            }
            ClassSchemaStats.SetClientInfo(Connection, String.Empty);
            FillStatsInfo();
            labelTitle.Text = resman.GetString("FormSchemaStats_label1_Worker_Done", culture);
        }

        void workerCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = runCheck((List<String>)e.Argument, worker, e);
        }

        private void WorkerCheck_Info(string buff, Int32? doneCnt, Int32? errCnt, Int32? allCnt)
        {
            if (panel1.InvokeRequired)
            {
                WorkerCheck_Info_d wd = new WorkerCheck_Info_d(WorkerCheck_Info);
                this.Invoke(wd, buff, doneCnt, errCnt, allCnt);
            }
            else
            {
                labelObject.Text = buff;
                labelDone.Text = (doneCnt == null ? "0" : doneCnt.ToString()) + "/" + (allCnt == null ? "0" : allCnt.ToString());
            }
        }

        private int runCheck(List<string> schemas, BackgroundWorker runWorker, DoWorkEventArgs e)
        {
            DataTable TableTables = new DataTable();
            Int32 schemaCounter = 0;
            try
            {
                ClassSchemaStats.FlushMonitoringInfo(Connection);
                ClassSchemaStats.TruncateTableStatisticInfo(Connection);
            }
            catch (OracleException exc)
            {
                execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                errorBuffer += exc.Message.ToString() + "\n";
                errorBufferCount++;
            }
            

            foreach (string owner in schemas)
            {
                if (autostop == true)
                {
                   return 0;
                }

                if (runWorker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    WorkerCheck_Info(owner, schemaCounter, 0, schemas.Count);
                    string sqlString = "STATS.CHECK_STATS";
                    CmdCheck = new OracleCommand(sqlString, Connection);
                    CmdCheck.CommandType = CommandType.StoredProcedure;
                    CmdCheck.BindByName = true;
                    CmdCheck.Parameters.Add("P_OWNER", owner);
                    //CmdCheck.Parameters.Add("PP_PERCENTAGE_MOD_DIFF", Convert.ToInt16(txtModiff.Text));
                    execBuffer += resman.GetString("FormSchemaStats_label1_Worker_DoWork_StoredProc", culture);

                    try
                    {
                        CmdCheck.ExecuteNonQuery();
                    }
                    catch (OracleException exci)
                    {
                        execBuffer += "\t ERROR\t" + DateTime.Now + "\n";
                        errorBuffer += exci.Message.ToString() + "\n";
                        errorBufferCount++;
                    }
                }
                schemaCounter++;
            }

            if (autostop == true)
            {
                return 0;
            }

            if (runWorker.CancellationPending)
            {
                e.Cancel = true;
            }

            return 0;
        }

        private void FillStatsInfo()
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo(Connection);
            dataGridView1.DataSource = tab;
            if (tab.Rows.Count > 0)
                panel2.Visible = true;
            AppyGrifColour();

            labelError.Text = resman.GetString("FormSchemaStats_label1_Worker_FoundRcommendations", culture) + " : " + tab.Rows.Count.ToString();

        }

        private void AppyGrifColour()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string[] ctit = { "PRC", "COLS", "MISS", "MOD" };
                string[] warn = { "WARN" };

                string reason = dr.Cells["REASON"].Value.ToString();
                if (ctit.Any(reason.Contains))
                {
                    dr.DefaultCellStyle.BackColor = Color.LightPink;
                    continue;
                }

                if (warn.Any(reason.Contains))
                {
                    dr.DefaultCellStyle.BackColor = Color.LightYellow;

                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormSimpleRunStats fsrs = new FormSimpleRunStats(conntmp, (DataTable)dataGridView1.DataSource, Convert.ToInt16(txtParallel.Text), Convert.ToInt16(txtEstimatePrc.Text), checkBox1.Checked, checkBox2.Checked);
            fsrs.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo(Connection);
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo_Filter(Connection, "PRC");
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo_Filter(Connection, "COUNT");
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo_Filter(Connection, "MOD");
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo_Filter(Connection, "MISS");
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            DataTable tab = ClassSchemaStats.FillStatsInfo_Filter(Connection, "COL");
            dataGridView1.DataSource = tab;
            AppyGrifColour();
        }

        private void showStatTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }

            string schemasy = string.Concat("'", string.Join("','", schemas.ToArray()), "'");

            DataTable tab = ClassSchemaStats.GetTableStats(Connection, schemasy);

            ClassViewWindow.FormGridView gv = new ClassViewWindow.FormGridView(tab, "Tables Stats");
            gv.ShowDialog();
        }

        private void labelError_Click(object sender, EventArgs e)
        {
            ClassViewWindow.FormTextView textView = new ClassViewWindow.FormTextView();
            textView.richTextBox1.Text = errorBuffer;
            textView.Font = new Font("Courier New", 8);
            if (textView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void tbtnINFO_Click(object sender, EventArgs e)
        {
            tbtnINFO.ToolTipText = resman.GetString("FormSchemaStats_GridInfo", culture);
            toolTip1.Active = true;
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipTitle = "Info";
            toolTip1.Show(resman.GetString("FormSchemaStats_GridInfo", culture), this, this.PointToClient(Cursor.Position));
        }

        /// <summary>
        /// Data: 20-09-2022
        /// Opis zmian: Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// Zmieniony mechanizm wyhywania statystyk + dodane opcje do validacji i kaskada
        /// </summary>
        private void tbtnCreateScript_Click(object sender, EventArgs e)
        {
            string tmp = String.Empty;
            DataTable tab = ClassSchemaStats.FillStatsInfo(Connection);

            foreach (DataRow dr in tab.Rows)
            {
                string owner = dr["USER_SCHEMA"].ToString();
                string tableName = dr["TAB"].ToString();
                string partitionName = dr["PART"].ToString();
                string subpartitionName = dr["SUB"].ToString();

                int degree = Convert.ToInt16(txtParallel.Text);
                int esimatepercent = Convert.ToInt16(txtEstimatePrc.Text);


                string method = "'FOR ALL COLUMNS SIZE AUTO'";
                string noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";
                string cascade = "TRUE";

                if (checkBox1.Checked == true)
                    noinvalidate = "DBMS_STATS.AUTO_INVALIDATE";
                else
                    noinvalidate = "FALSE";
                

                if (checkBox2.Checked)
                    cascade = "TRUE";
                else
                    cascade = "FALSE";

                string granularity ;

                if (String.IsNullOrEmpty(partitionName))
                    granularity = "'ALL'";
                else
                    granularity = "'GLOBAL AND PARTITION'";

                


                string sqlString = String.Empty;
                sqlString += "BEGIN\n" +
                "  DBMS_STATS.GATHER_TABLE_STATS(OWNNAME          => '\"" + owner + "\"',\n" +
                "                                TABNAME          => '\"" + tableName + "\"',\n" +
                "                                ESTIMATE_PERCENT => " + esimatepercent + ",\n" +
                "                                DEGREE           => " + degree.ToString() + ",\n" +
                "                                NO_INVALIDATE    => " + noinvalidate + ",\n" +
                "                                GRANULARITY      => " + granularity + ",\n" +
                "                                METHOD_OPT       => " + method + ",\n" +
                "                                CASCADE          => " + cascade + ");\n" +
                "END;\n/\n";

                tmp += sqlString;

            }
            ClassViewWindow.FormTextView f1 = new ClassViewWindow.FormTextView(tmp);
            f1.Show(this);
        }

        private void createScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }
            DataTable tab = ClassSchemaStats.GenerateScriptForAll(Connection, schemas, 8, 3);
            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;

            foreach (DataRow dr in tab.Rows)
            {
                if (Convert.ToInt32(dr["file_id"]) == 1)
                {
                    s1 += dr["cmd"].ToString() + "\n\n";
                }
                if (Convert.ToInt32(dr["file_id"]) == 2)
                {
                    s2 += dr["cmd"].ToString() + "\n\n";
                }
                if (Convert.ToInt32(dr["file_id"]) == 3)
                {
                    s3 += dr["cmd"].ToString() + "\n\n";
                }
            }

            ClassViewWindow.FormTextView f1 = new ClassViewWindow.FormTextView(s1);
            f1.Show(this);
            ClassViewWindow.FormTextView f2 = new ClassViewWindow.FormTextView(s2);
            f2.Show(this);
            ClassViewWindow.FormTextView f3 = new ClassViewWindow.FormTextView(s3);
            f3.Show(this);

        }

        private void createScriptTableLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }
            DataTable tab = ClassSchemaStats.GenerateScriptForAll(Connection, schemas, 8, 1);
            string s1 = string.Empty;

            foreach (DataRow dr in tab.Rows)
            {
                s1 += dr["cmd"].ToString() + "\n\n";
            }

            ClassViewWindow.FormTextView f1 = new ClassViewWindow.FormTextView(s1);
            f1.Show(this);
        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
            string schema = checkedListBox1.SelectedItem.ToString();
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            //FormTableStats fts = new FormTableStats(conntmp, schema, Convert.ToInt16(txtParallel.Text), Convert.ToInt16(txtEstimatePrc.Text), checkBox1.Checked, checkBox2.Checked);
            FormTableStats fts = new FormTableStats(conntmp, schema, sessionOptions);
            fts.StartPosition = FormStartPosition.CenterScreen; 
            fts.Show(this);
        }

        private void FormSchemaStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workerStats.IsBusy)
            {
                workerStats.CancelAsync();
                CmdGatherTable.Cancel();
            }
            if(workerCheck.IsBusy) 
            {
                workerCheck.CancelAsync();
                CmdCheck.Cancel();
            }

            Connection.Close();
        }

        private void showWeakHYBRIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> schemas = new List<string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                schemas.Add(itemChecked.ToString());
            }

            string schemasy = string.Concat("'", string.Join("','", schemas.ToArray()), "'");

            DataTable tab = ClassSchemaStats.GetColStatsHybrid(Connection, schemasy);

            ClassViewWindow.FormGridView gv = new ClassViewWindow.FormGridView(tab, "Tables Stats");
            gv.ShowDialog();


        }

        private void showTablePrefsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string schema = checkedListBox1.SelectedItem.ToString();
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormShowTablePrefs fts = new FormShowTablePrefs(conntmp, schema, sessionOptions);
            fts.StartPosition = FormStartPosition.CenterScreen;
            fts.Show(this);
        }

        private void showTableTmieStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string datNam = (Connection.PDBName == null ? Connection.DatabaseName : Connection.PDBName);
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                DataTable tempTab = StatsTime.GetInfoAll(datNam, itemChecked.ToString());

                ClassViewWindow.FormGridView gv = new ClassViewWindow.FormGridView(tempTab, "Table Time Stats - " + itemChecked.ToString());
                gv.Show(this);
            }
        }
    }
}
