using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using ClassViewWindow;
using Oracle.ManagedDataAccess.Client;
using static System.Collections.Specialized.BitVector32;

namespace ClassSession
{

    public partial class FormSession : Form
    {
        readonly OracleConnection Connection;
        readonly Int32 sid;
        readonly Int32 serial; 
        bool colorGrid = false;
        DataRowView drv;
        Int32 timerCount = 0;
        Int32 timerCountMax = 100;

        public FormSession(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            ShowSessions(SQLStrings.SESSIONS_ALL_USER_IN_WORK);
            toolStripComboBox2.SelectedIndex = 1;
            SetTextFromConnectioNString(Connection);
        }
        public FormSession(OracleConnection Connection, int Sid, int Serial)
        {
            InitializeComponent();
            this.Connection = Connection;
            this.sid = Sid;
            this.serial = Serial;
            ShowSession( sid, serial);
            toolStripComboBox2.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndex = 2;
            toolStripTextBox1.Text = sid.ToString();
            SetTextFromConnectioNString(Connection);
        }
        public FormSession(OracleConnection Connection, int Sid)
        {
            InitializeComponent();
            this.Connection = Connection;
            this.sid = Sid;
            ShowSessionSid(sid);
            toolStripComboBox2.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndex = 2;
            toolStripTextBox1.Text = sid.ToString();
            SetTextFromConnectioNString(Connection);
        }

        private void SetTextFromConnectioNString(OracleConnection tmpConnection)
        {
            try {
                this.Text += String.IsNullOrEmpty(tmpConnection.Database.ToString()) ? "" : " DB: " + tmpConnection.Database.ToString();
                this.Text += String.IsNullOrEmpty(tmpConnection.PDBName.ToString())?"":" PDB: " + tmpConnection.PDBName.ToString();
            }
            catch { }
        }
        private void ShowSessions(string command)
        {
            DataTable table = new DataTable() { TableName = "SessionsTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = command };

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);
            
            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";

        }

        private void ShowSession(int sid, int serial)
        {
            DataTable table = new DataTable() { TableName = "SessionsTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = SQLStrings.SESSION };
            cmd.Parameters.Add("sid", sid);
            cmd.Parameters.Add("serial#", serial);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }
        private void ShowSessionSid(int sid)
        {
            DataTable table = new DataTable() { TableName = "SessionsTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = SQLStrings.SESSION_SID };
            cmd.Parameters.Add("sid", sid);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }

        private void ShowSessionOsuser(string osuser)
        {
            DataTable table = new DataTable() { TableName = "SessionsTable" }; 
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = SQLStrings.SESSION_OSUSER;
            cmd.Parameters.Add("osuser", osuser);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }

        private void ShowSessionProgram(string program)
        {
            DataTable table = new DataTable();
            table.TableName = "SessionsTable";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = SQLStrings.SESSION_PROGRAM;
            cmd.Parameters.Add("program", program);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }

        private void ShowSessionHolders(string objectName)
        {
            DataTable table = new DataTable();
            table.TableName = "SessionsTable";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = SQLStrings.SESSIONS_HOLDERS;
            cmd.Parameters.Add("objectname", objectName);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            bindingSource1.DataSource = table;

            dataGridView1.DataSource = bindingSource1;

            toolStripStatusLabel1.Text = table.Rows.Count.ToString() + " Rows selected";
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            drv = (DataRowView)bindingSource1.Current;
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            Go();
        }

        private void Go()
        {


            string filterek = String.Empty;
            bindingSource1.Filter = filterek;

            if (toolStripComboBox1.SelectedIndex == -1)
                toolStripComboBox1.SelectedIndex = 0;

            if (toolStripComboBox1.SelectedIndex >= 0)
            {
                if (string.IsNullOrEmpty(toolStripTextBox1.Text))
                {

                    if (toolStripComboBox2.SelectedIndex < 1 && toolStripComboBox3.SelectedIndex < 1)
                        ShowSessions(SQLStrings.SESSIONS_ALL_USER);
                    if (toolStripComboBox2.SelectedIndex == 1 && toolStripComboBox3.SelectedIndex < 1)
                        ShowSessions(SQLStrings.SESSIONS_ALL_USER_ACTIVE);
                    if (toolStripComboBox2.SelectedIndex == 1 && toolStripComboBox3.SelectedIndex == 1)
                        ShowSessions(SQLStrings.SESSIONS_ALL_USER_ACTIVE_AND_BLOCKED);
                    if (toolStripComboBox2.SelectedIndex < 1 && toolStripComboBox3.SelectedIndex == 1)
                        ShowSessions(SQLStrings.SESSIONS_ALL_USER_BLOCKED);
                }
                else
                {
                    filterek += (toolStripComboBox2.SelectedIndex == 1) ? " AND STATUS = 'ACTIVE'" : "";
                    filterek += (toolStripComboBox3.SelectedIndex == 1) ? " OR BLO_SESS IS NOT NULL" : "";

                    if (!string.IsNullOrEmpty(filterek))
                    {
                        if (filterek.Substring(0, 5) == " AND ")
                        {
                            //filterek = (filterek.Substring(0, 5) == " AND ") ? filterek.Substring(5, filterek.Length - 5) : filterek;
                            filterek = filterek.Substring(5, filterek.Length - 5);
                        }
                        else if (filterek.Substring(0, 4) == " OR ")
                        {
                            //filterek = (filterek.Substring(0, 5) == " OR ") ? filterek.Substring(4, filterek.Length - 4) : filterek;
                            filterek = filterek.Substring(4, filterek.Length - 4);
                        }
                    }
                    else
                        filterek = string.Empty;

                    bindingSource1.Filter = filterek;
                    ///
                    switch (toolStripComboBox1.SelectedItem.ToString())
                    {
                        case "sid":
                            ShowSessionSid(Convert.ToInt32(toolStripTextBox1.Text));
                            break;
                        case "osuser":
                            ShowSessionOsuser(toolStripTextBox1.Text);
                            break;
                        case "program":
                            ShowSessionProgram(toolStripTextBox1.Text);
                            break;
                        case "holders":
                            ShowSessionHolders(toolStripTextBox1.Text);
                            break;
                        default:
                            bindingSource1.Filter = string.Empty;
                            break;

                    }
                }
            }
            GrRepaint();
            bindingSource1.Sort = (toolStripComboBox4.SelectedIndex <= 0 ? string.Empty : (toolStripComboBox4.SelectedItem.ToString() + " DESC"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (toolStripButtonGo.Enabled == false)
                return;
            toolStripButtonGo.Enabled = false;
            Go();
            toolStripButtonGo.Enabled = true;
            timerCount++;
            toolStripDropDownButton2.Text = timerCount.ToString() + "/" + timerCountMax.ToString();
            if (timerCount >= timerCountMax)
                {
                    timer1.Stop();
                    toolStripDropDownButton2.Text = string.Empty;
                    timerCount = 0;
            }
        }

        #region secundy
        private void secToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerCount = 0;
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void secToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timerCount = 0;
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void secToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timerCount = 0;
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void secToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timerCount = 0;
            timer1.Interval = 10000;
            timer1.Start();
        }

        #endregion

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            toolStripButtonGo.Enabled = true;
        }

        private void aLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBox1.Text = String.Empty;
            bindingSource1.Filter = string.Empty;
            ShowSessions(SQLStrings.SESSIONS_ALL);
            GrRepaint();
        }

        private void uSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBox1.Text = String.Empty;
            bindingSource1.Filter = string.Empty;
            ShowSessions(SQLStrings.SESSIONS_ALL_USER);
            GrRepaint();
        }

        private void aCTIVEWORKINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripTextBox1.Text = String.Empty;
            bindingSource1.Filter = string.Empty;
            ShowSessions(SQLStrings.SESSIONS_ALL_USER_IN_WORK);
            GrRepaint();
        }

        private void xplainToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string sqlid = drv["sql_id"].ToString();
            string sessionInfo = "osuser: " + drv["osuser"].ToString();

            if (sqlid != null && sqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(Sessions.GetXPlain(Connection, sqlid, true), drv, Connection.HostName + " - " + Connection.DatabaseName + " - " + Connection.PDBName);
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
            //ShowSessions(SQLStrings.SESSIONS_ALL_USER_IN_WORK);
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

        private void FormSession_Load(object sender, EventArgs e)
        {
            toolStripComboBox3.SelectedIndex = 0;
            toolStripComboBox2.SelectedIndex = 1;
            toolStripComboBox4.SelectedIndex = 1;
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonGo_Click(sender, e);
            }
        }
        private void ColorGridSessionPx()
        {
            DataTable table = new DataTable();
            table.TableName = "SessionsPX";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "select sid, qcsid from v$px_session";
            

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            foreach (DataRow dr in table.Rows)
            {
                //kolorowanie dla pażdek PX-a 
            }
        }

        private void sessionObiectMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                ClassMonitor.FormMonitorObj fm = new ClassMonitor.FormMonitorObj("Object Monitor sid: " + sid.ToString(), ClassMonitor.SQLStrings.OBJECT_MONITOR, sid, conntmp);
                fm.Show(this);
            }
        }

        private void sessionUndoMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);
                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                ClassMonitor.FormMonitorObj fm = new ClassMonitor.FormMonitorObj("Seesion Undo Monitor sid: " + sid.ToString(), ClassMonitor.SQLStrings.SESSION_UNDO_BLOCK_USED, sid, conntmp);
                fm.Show(this);
            }
        }

        private void sessionWaitStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                ClassMonitor.FormMonitorObj fm = new ClassMonitor.FormMonitorObj("Wait Stats sid: " + sid.ToString(), ClassMonitor.SQLStrings.SESSION_WAIT_STATS, sid, conntmp, 400, 300);
                fm.Show(this);
            }
        }

        private void historicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormSessionsHistorical fsh = new FormSessionsHistorical(conntmp);
            fsh.Show(this);
        }

        private void sqlIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlid = drv["sql_id"].ToString();
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            ClassSqlId.FormSqlId fid = new ClassSqlId.FormSqlId(conntmp, sqlid);
            fid.Show(this);
        }

        private void FormSession_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }

        private void blosessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drv["blo_sess"] == DBNull.Value)
            {
                return;
            }

            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            Int32 BloSid = Convert.ToInt32(drv["blo_sess"]);

             ClassSession.FormSession fs = new ClassSession.FormSession(conntmp, BloSid);
            fs.Show();
            //if (fs.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //{

            //}
        }
        private void sCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drv["sid"] == DBNull.Value)
            {
                return;
            }
            
            Int32 sid = Convert.ToInt32(drv["sid"]);


            DataTable table = new DataTable();
            table.TableName = "SessionsPX";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = SQLStrings.SESSION_SCC;
            cmd.Parameters.Clear();
            cmd.Parameters.Add("sid", sid);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);

            ClassViewWindow.FormGridView fs = new ClassViewWindow.FormGridView(table,"Session Casched Cursor");
            if (fs.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
                toolStripTextBox1.Text = string.Empty;
        }

        private void showSQLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string prebsqlid = drv["prev_sql_id"].ToString();

            if (prebsqlid != null && prebsqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(Sessions.GetSql(Connection, prebsqlid));
                f.richTextBox1.Font = new System.Drawing.Font("Courier New", 10);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Session should have a valid prev_sql_id", "Empty perv_sql_id");
            }
        }

        private void xplainToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string prevsqlid = drv["prev_sql_id"].ToString();

            if (prevsqlid != null && prevsqlid.Trim().Length > 0)
            {
                ClassViewWindow.FormTextView f = new ClassViewWindow.FormTextView(Sessions.GetXPlain(Connection, prevsqlid, true));
                f.richTextBox1.Font = new System.Drawing.Font("Courier New", 10);
                f.Show(this);
            }
            else
            {
                MessageBox.Show("Session should have a valid prev_sql_id", "Empty prev_sql_id");
            }
        }

        private void flushToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string prevsqlid = drv["prev_sql_id"].ToString();

            if (prevsqlid != null && prevsqlid.Trim().Length > 0)
            {
                MessageBox.Show(Sessions.FlushPlanCursor(Connection, prevsqlid));
            }
            else
            {
                MessageBox.Show("Session should have a valid prev_sql_id", "Empty prev_sql_id");
            }
        }

        private void executionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlid = drv["sql_id"].ToString();

            string sqlString = "select s.child_number as child, s.executions, s.module\n" +
            "  from v$sql s\n" +
            " where s.sql_id = '" + sqlid + "'";

            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            ClassMonitor.FormMonitor01 fm = new ClassMonitor.FormMonitor01("NORMAL", "Sql_id " + sqlid, sqlString, conntmp, 50, 400);
            fm.Show(this);
        }

        #region Statsy 

        private void SessionStats(string statName)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                OracleConnection conntmp = (OracleConnection)Connection.Clone();
                conntmp.Open();
                ClassMonitor.FormMonitor fm = new ClassMonitor.FormMonitor("Executes", ClassMonitor.SQLStrings.STATNAME_SID, conntmp, sid, statName);
                fm.Show(this);
            }
        }

        private void excuteCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionStats("execute count");
        }

        private void physicalReadBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionStats("physical read bytes");
        }

        #endregion

        private void sessionLogicalReadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessionStats("logical read bytes from cache");

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Checked)
            {
                colorGrid = true;
                toolStripButton1.BackColor = Color.MistyRose;
            }
            else
            {
                colorGrid = false;
                toolStripButton1.BackColor = SystemColors.Control;
            }
            GrRepaint();
        }

        private void GrRepaint()
        {
            if (colorGrid)
            {
                foreach (DataGridViewRow drv in dataGridView1.Rows)
                {
                    string status = drv.Cells["STATUS"].Value.ToString();
                    Int32 lastCallEt = Convert.ToInt32(drv.Cells["LAST_CALL_ET"].Value);
                    if (status == "ACTIVE" && lastCallEt < 1000)
                    {
                        drv.DefaultCellStyle.BackColor = SystemColors.Window;
                        //drv.DefaultCellStyle.BackColor = Color.MistyRose;

                    }
                    else if (status == "ACTIVE" && lastCallEt < 21600)
                    {
                        drv.DefaultCellStyle.BackColor = Color.Salmon;

                    }
                    else if (status == "ACTIVE" && lastCallEt < 430000)
                    {
                        //drv.DefaultCellStyle.BackColor = Color.Salmon;
                        drv.DefaultCellStyle.BackColor = Color.Red;
                        drv.Cells["LAST_CALL_ET"].Style.BackColor = Color.Black;
                        drv.Cells["LAST_CALL_ET"].Style.ForeColor = Color.White;
                        drv.Cells["STATUS"].Style.BackColor = Color.Black;
                        drv.Cells["STATUS"].Style.ForeColor = Color.White;
                    }
                    else if (status == "ACTIVE" && lastCallEt < 1000000)
                    {
                        //drv.DefaultCellStyle.BackColor = Color.Salmon;
                        drv.DefaultCellStyle.BackColor = Color.DarkRed;
                        drv.Cells["LAST_CALL_ET"].Style.BackColor = Color.Black;
                        drv.Cells["LAST_CALL_ET"].Style.ForeColor = Color.White;
                        drv.Cells["STATUS"].Style.BackColor = Color.Black;
                        drv.Cells["STATUS"].Style.ForeColor = Color.White;
                    }
                    else if (status == "ACTIVE" && lastCallEt < 10000000)
                    {
                        //drv.DefaultCellStyle.BackColor = Color.Salmon;
                        drv.DefaultCellStyle.BackColor = Color.DarkRed;
                        drv.Cells["LAST_CALL_ET"].Style.BackColor = Color.Black;
                        drv.Cells["LAST_CALL_ET"].Style.ForeColor = Color.Yellow;
                        drv.Cells["STATUS"].Style.BackColor = Color.Black;
                        drv.Cells["STATUS"].Style.ForeColor = Color.Yellow;
                    }
                    else //(status == "INACTIVE")
                    {
                        drv.DefaultCellStyle.BackColor = SystemColors.Window;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow drv in dataGridView1.Rows)
                {
                    drv.DefaultCellStyle.BackColor = SystemColors.Window;
                    drv.Cells["LAST_CALL_ET"].Style.ForeColor = SystemColors.ControlText;
                    drv.Cells["LAST_CALL_ET"].Style.BackColor = SystemColors.Window;
                    drv.Cells["STATUS"].Style.BackColor = SystemColors.Window;
                    drv.Cells["STATUS"].Style.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name.ToUpper() == "LAST_CALL_ET" || this.dataGridView1.Columns[e.ColumnIndex].Name.ToUpper() == "SEC_IN_WAIT")
            {
                if (e.Value != null) 
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "dni.godzin:minut:sekund\n\n" + TimeSpan.FromSeconds(Convert.ToInt32(e.Value)).ToString(@"dd\.hh\:mm\:ss");
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Up)
            {
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
                dataGridView1.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            }
            if (e.Control && e.KeyCode == Keys.Down)
            {
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", 7.25f, FontStyle.Regular);
                dataGridView1.RowsDefaultCellStyle.Font = new Font("Arial Narrow", 7.25f, FontStyle.Regular);
            }
        }

        private void sessionConnectInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(drv["sid"]);
            int serial = Convert.ToInt32(drv["serial#"]);
            string program = drv["program"].ToString();
            string machine = drv["machine"].ToString();

            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormSessionConnectInfo fm = new FormSessionConnectInfo(conntmp, sid, serial, program, machine);
            fm.Show(this);
        }

        private void toolStrip1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        //private void toolStripButtonGo_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        var item = toolStrip1.GetItemAt(e.Location);
        //        if (item == toolStripButtonGo)
        //        {
        //            contextMenuStripGo.Show(toolStrip1, e.Location);
        //        }
        //    }
        //}


    }

}
