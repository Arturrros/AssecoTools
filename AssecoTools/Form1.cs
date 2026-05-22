using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using ClassSqlId;
using System.Security;
using System.Net;
using System.IO;
using System.Diagnostics;
using ClassReports;
using ClassSize;
using ClassVisual;

namespace AssecoTools
{
    
    public partial class Form1 : Form
    {
        OracleConnection ConnectionMain = new OracleConnection();
        ResourceManager resman;
        CultureInfo culture;

        SecureString ss = new SecureString();

        public Form1(string[] Args)
        {
            try
            {
                InitializeComponent();
                String[] args = Environment.GetCommandLineArgs();

                if (args.Length > 1)
                {
                    ss = new NetworkCredential("", args[1].ToString()).SecurePassword;
                }
                else
                {
                    ss = new NetworkCredential("", "ATKI".ToString()).SecurePassword;
                }

                ConnectionMain.StateChange += ConnectionMain_StateChange;

                if (AssecoTools.Default.CultureInfo == "pl-PL")
                {
                    eNToolStripMenuItem.Checked = false;
                    pLToolStripMenuItem.Checked = true;
                }
                else
                {
                    eNToolStripMenuItem.Checked = true;
                    pLToolStripMenuItem.Checked = false;
                }

                culture = new CultureInfo(AssecoTools.Default.CultureInfo);
                InitializeLanguage(culture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

            ClassLog.Log.Add(ClassLog.Log.LogLevel.NORMAL, "Start Aplikacji");
            
            
        }
        void InitializeLanguage(CultureInfo ci)
        {
            Assembly assembly = Assembly.Load("AssecoTools");
            resman = new ResourceManager("AssecoTools.Lang.LangRes", assembly);
            btnGain.Text = resman.GetString("Form1_btnGain_Text", ci);
            btnSessions.Text = resman.GetString("Form1_btnSessions_Text", ci);
            btnLocks.Text = resman.GetString("Form1_btnLocks_Text", ci);
            btnSerwerror.Text = resman.GetString("Form1_btnSerwerror_Text", ci);
            btnDDL.Text = resman.GetString("Form1_btnDDL_Text", ci);
            btnLongOps.Text = resman.GetString("Form1_btnLongOps_Text", ci);
            fileToolStripMenuItem.Text = resman.GetString("Form1_fileToolStripMenuItem_Text", ci);

            connectToolStripMenuItem.Text = resman.GetString("Form1_connectToolStripMenuItem_Text", ci);
            disconnectToolStripMenuItem.Text = resman.GetString("Form1_disconnectToolStripMenuItem_Text", ci);
            aboutToolStripMenuItem.Text = resman.GetString("Form1_aboutToolStripMenuItem_Text", ci);
            infoToolStripMenuItem.Text = resman.GetString("Form1_infoToolStripMenuItem_Text", ci);
            menuStrip1.Text = resman.GetString("Form1_menuStrip1_Text", ci);
            statusStrip1.Text = resman.GetString("Form1_statusStrip1_Text", ci);
            btnExecutes.Text = resman.GetString("Form1_btnExecutes_Text", ci);
            btnParses.Text = resman.GetString("Form1_btnParses_Text", ci);
            btnUserCalls.Text = resman.GetString("Form1_btnUserCalls_Text", ci);
            btnDeadlocks.Text = resman.GetString("Form1_btnDeadlocks_Text", ci);
            groupBox2.Text = resman.GetString("Form1_groupBox2_Text", ci);
            btnBytesSend.Text = resman.GetString("Form1_btnBytesSend_Text", ci);
            btnPhysicalReads.Text = resman.GetString("Form1_btnPhysicalReads_Text", ci);
            btnCPUBYTHIS.Text = resman.GetString("Form1_btnCPUBYTHIS_Text", ci);
            btnCommits.Text = resman.GetString("Form1_btnCommits_Text", ci);
            btnTableStatistics.Text = resman.GetString("Form1_btnTableStatistics_Text", ci);
            toolTip1.SetToolTip(btnTableStatistics, resman.GetString("Form1_btnTableStatistics_ToolTip", ci));

            groupBox3.Text = resman.GetString("Form1_groupBox3_Text", ci);
            groupBox1.Text = resman.GetString("Form1_groupBox1_Text", ci);
            langToolStripMenuItem.Text = resman.GetString("Form1_langToolStripMenuItem_Text", ci);
            eNToolStripMenuItem.Text = resman.GetString("Form1_eNToolStripMenuItem_Text", ci);
            pLToolStripMenuItem.Text = resman.GetString("Form1_pLToolStripMenuItem_Text", ci);

            fileToolStripMenuItem.ToolTipText = resman.GetString("Form1_fileToolStripMenuItem_ToolTip", ci);
            toolTip1.SetToolTip(btnLocks, resman.GetString("Form1_btnLocks_ToolTip", ci));

            optionsToolStripMenuItem.Text = resman.GetString("Form1_optionsToolStripMenuItem_Text", ci);

            btnHolds.Text = resman.GetString("Form1_btnHolds_Text", ci);
            toolTip1.SetToolTip(btnHolds, resman.GetString("Form1_btnHolds_ToolTip", ci));

            button_GlobalUndo.Text = resman.GetString("Form1_button_GlobalUndo_Text", ci);

        }
        void ConnectionMain_StateChange(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState == ConnectionState.Open)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;

            }

            toolStripStatusLabel1.Text = e.CurrentState.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin(ss);
              
            if (fl.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ConnectionMain.Close();
                   
                ConnectionMain.ConnectionString = fl.connectionString;
                try 
                {
                    ConnectionMain.Open();
                    toolStripStatusLabel1.Text = toolStripStatusLabel1.Text + " Hostname: " + ConnectionMain.HostName.ToString() + " Instance: " + ConnectionMain.InstanceName + " PDBS: " + ConnectionMain.PDBName.ToString() + " ServiceName: " + ConnectionMain.ServiceName.ToString() + " ServerVersion: " + ConnectionMain.ServerVersion.ToString();
                    OracleGlobalization glob = ConnectionMain.GetSessionInfo();
                    toolStripStatusLabel1.ToolTipText = glob.ToString();

                    string globalINfo = "AssecoTools ClientLocale: \n";
                    globalINfo += "Language:\t\t" + glob.Language.ToString() + "\n";
                    globalINfo += "Territory:\t\t\t" + glob.Territory.ToString() + "\n";
                    globalINfo += "Calendar:\t\t" + glob.Calendar.ToString() + "\n";
                    globalINfo += "Currency:\t\t" + glob.Currency.ToString() + "\n";
                    globalINfo += "DateFormat:\t\t" + glob.DateFormat.ToString() + "\n";
                    globalINfo += "DateLanguage:\t\t" + glob.DateLanguage.ToString() + "\n";
                    globalINfo += "LengthSemantics:\t\t" + glob.LengthSemantics.ToString() + "\n";
                    globalINfo += "Sort:\t\t\t" + glob.Sort.ToString() + "\n";
                    globalINfo += "TimeStampFormat:\t" + glob.TimeStampFormat.ToString() + "\n";
                    globalINfo += "TimeZone:\t\t" + glob.TimeZone.ToString() + "\n";
                    //globalINfo += "Territory:\t" + glob..ToString() + "\n";


                    toolTip1.SetToolTip(statusStrip1, globalINfo);
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "Error";
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionMain.Close();
        }

        private void btnSessions_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassSession.FormSession fs = new ClassSession.FormSession(conntmp);
            fs.Show(this);
        }

        private void btnLocks_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassWaiters.FormWaiters fw = new ClassWaiters.FormWaiters(conntmp);
            fw.Show(this);
        }

        private void btnServererror_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassServerError.FormServerError fg = new ClassServerError.FormServerError(conntmp, ClassServerError.SQLStrings.ERROR_LAST24H);
            fg.Show(this);
        }

        private void btnDDL_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassServerError.FormServerError fg = new ClassServerError.FormServerError(conntmp, ClassServerError.SQLStrings.DDL_LAST1000);
            fg.Show(this);
        }

        private void btnLongOps_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassServerError.FormServerError fg = new ClassServerError.FormServerError(conntmp, ClassServerError.SQLStrings.SESSION_LONGOPS);
            fg.Show(this);
        }

        private void btnGain_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassSize.FormGain fs = new ClassSize.FormGain(conntmp);
            fs.Show(this);
        }

        private void btnExecutes_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Executes", ClassMonitor.SQLStrings.EXECUTIONS, conntmp);
            fm.Show(this);
        }

        private void btnParses_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Parse Hard", ClassMonitor.SQLStrings.PARSE_TOTAL, conntmp);
            fm.Show(this);
        }

        private void btnUserCalls_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("User Calls", ClassMonitor.SQLStrings.USER_CALLS, conntmp);
            fm.Show(this);
        }

        private void btnDeadlocks_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("ENQUENE DEADLOCK", ClassMonitor.SQLStrings.ENQUENE_DEADLOCK, conntmp);
            fm.Show(this);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog();
        }

        private void btnTableStatistics_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassSchemaStats.FormSchemaStats ft = new ClassSchemaStats.FormSchemaStats(conntmp, culture);
            ft.Show(this);
        }

        private void btnCommits_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Commits", ClassMonitor.SQLStrings.COMMITS, conntmp);
            fm.Show(this);
        }

        private void btnCPUBYTHIS_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("CPU", ClassMonitor.SQLStrings.CPU_BY_THIS, conntmp);
            fm.Show(this);
        }

        private void btnPhysicalReads_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Physical Reads", ClassMonitor.SQLStrings.PHYSICAL_READS, conntmp);
            fm.Show(this);
        }

        private void btnBytesSend_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Bytes send to client", ClassMonitor.SQLStrings.BYTES_NETWORK_SEND, conntmp);
            fm.Show(this);
        }

        private void eNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            culture = new CultureInfo("en-US");
            InitializeLanguage(culture);
            AssecoTools.Default.CultureInfo = culture.Name;
            AssecoTools.Default.Save();
            eNToolStripMenuItem.Checked = true;
            pLToolStripMenuItem.Checked = false;
        }

        private void pLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            culture = new CultureInfo("pl-PL");
            AssecoTools.Default.CultureInfo = culture.Name;
            InitializeLanguage(culture);
            
            AssecoTools.Default.Save();
            eNToolStripMenuItem.Checked = false;
            pLToolStripMenuItem.Checked = true;
        }

        private void showTipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showTipsToolStripMenuItem.Checked = !showTipsToolStripMenuItem.Checked;
            if (showTipsToolStripMenuItem.Checked)
                toolTip1.Active = true;
            else
                toolTip1.Active = false;
            
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChangeLog fch = new FormChangeLog();
            fch.ShowDialog();
        }

        private void btnMissingInedxes_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassIndexes.FormFKIndexes ft = new ClassIndexes.FormFKIndexes(conntmp);
            ft.StartPosition = FormStartPosition.CenterParent; 
            ft.Show(this);
        }

        private void btnHolds_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassWaiters.FormHolds fw = new ClassWaiters.FormHolds(conntmp);
            fw.Show(this);
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("TMP", "Temporary Tables", ClassMonitor.SQLStrings.SESSION_TMP, conntmp, 50, 1200);
            fm.Show(this);
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("REC", "Undo Recover", ClassMonitor.SQLStrings.UNDO_RECOVER, conntmp, 50, 700);
            fm.Show(this);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("UND", "Undo Size", ClassMonitor.SQLStrings.UNDO_SIZE, conntmp, 50, 700);
            fm.Show(this);
        }

        private void button_GlobalUndo_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("UND", "Global Undo Size", ClassMonitor.SQLStrings.GLOBAL_UNDO_SIZE, conntmp, 50, 400);
            fm.Show(this);
        }

        private void btnAWR_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassAwr.FormAwr fm = new ClassAwr.FormAwr(conntmp);
            fm.Show(this);
        }

        private void btnSqlId_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            FormSqlId fm = new FormSqlId(conntmp);
            fm.Show(this);
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("", "Transactions", ClassMonitor.SQLStrings.SESION_TRANSACTION, conntmp, 50, 1200);
            fm.Show(this);
        }

        private void btn_Session_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassVisual.FormSessHist fs = new ClassVisual.FormSessHist(conntmp);
            fs.Show(this);
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectionMain.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("", "Open Cursors", ClassMonitor.SQLStrings.OPEN_CUROSORS, conntmp, 150, 350);
            fm.Show(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && e.Control && e.Shift && e.Alt)
            {
                buttonRepors.Visible = !buttonRepors.Visible;
                button3.Visible = !button3.Visible;
            }
        }

        private void buttonRepors_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            ClassReports.FormReportMain fm = new ClassReports.FormReportMain(conntmp, ClassReports.SQLStrings.REPO_ALL);
            fm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            FormTBS fm = new FormTBS(conntmp);
            fm.ShowDialog(this);
        }

        private void btnActiveSessionHistory_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            FormASH fash = new FormASH(conntmp);
            fash.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)ConnectionMain.Clone();
            conntmp.Open();
            FormChartOnline fch = new FormChartOnline(conntmp,ClassMonitor.SQLStrings.GvMonitors.OSSTAT_BUSY_TIME, "OSSTAT_BUSY_TIME","global system");
            fch.Show(this);
        }
    }
}
