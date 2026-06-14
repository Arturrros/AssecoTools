using AssecoToolsOptions;
using ClassTableStats;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClassSchemaStats
{
    /// <summary>
    /// Ustawienie parametrów statystyk dla tabel
    /// Data:   06-2025
    /// Autor:  artur.balon@asseco.pl
    /// </summary>
    public partial class FormSetTablePrefs : Form
    {
        SessionOptions sessionOptions;
        OracleConnection connection;
        string owner = string.Empty;
        string tableName = string.Empty;
        private OracleCommand cmdPref;
        private OracleCommand cmdActivePref;
        private OracleDataAdapter adaPref;
        DataTable dtPref = new DataTable();
        DataTable dtActivePref = new DataTable();
        BindingSource bs = new BindingSource();

        public FormSetTablePrefs(OracleConnection Connection, string Owner, string TableName, SessionOptions sessionOptions)
        {
            InitializeComponent();
            owner = Owner;
            connection = Connection;
            tableName = TableName;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                panel3.BackColor = sessionOptions.SessionColor;
            }
        }

        private void FormSetTablePrefs_Load(object sender, EventArgs e)
        {
            GetTablePrefs();
            GetaTableActivePrefs();

            bs = new BindingSource(dtPref, null);
            bs.CurrentChanged += Bs_CurrentChanged;
            dataGridView1.DataSource = bs;

            textBox1.DataBindings.Add("Text", bs, "val");
            GColor();

        }

        private void Bs_CurrentChanged(object sender, EventArgs e)
        {
            if (bs.DataSource != null && bs.Current != null)
            {
                DataRowView drt = (DataRowView)bs.Current;
                if (drt["opts"].ToString() == "METHOD_OPT")
                {
                    groupBox1.Enabled = true;
                    buttonColumns.Visible = true;
                    buttonColumns.Enabled = true;
                }
                else
                {
                    groupBox1.Enabled = false;
                    buttonColumns.Visible = false;
                    buttonColumns.Enabled = false;
                }

                switch (drt["opts"].ToString())
                {
                    case "METHOD_OPT":
                        groupBox1.Enabled = true;
                        break;
                    case "ESTIMATE_PERCENT":
                        groupBox1.Enabled = true;
                        break;
                    case "DEGREE":
                        groupBox1.Enabled = true;
                        break;
                    case "STALE_PERCENT":
                        groupBox1.Enabled = true;
                        break;
                    default:
                        groupBox1.Enabled = false;
                        break;
                }
            }
        }

        private void GColor()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string opts = row.Cells["OPTS"].Value.ToString();
                foreach (DataRow dr in dtActivePref.Rows)
                {
                    if (opts == dr[0].ToString())
                    {
                        dataGridView1.Rows[row.Index].Cells[0].Style.BackColor = Color.LightCyan;
                        continue;
                    }
                    else
                    {
                        //dataGridView1.Rows[row.Index].Cells[0].Style.BackColor = SystemColors.Window;
                    }
                }
            }
        }

        private void GetTablePrefs()
        {
            string sqlString = "select 'APPROXIMATE_NDV_ALGORITHM' as opts,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'APPROXIMATE_NDV_ALGORITHM') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        "select 'AUTO_STAT_EXTENSIONS' as opts,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'AUTO_STAT_EXTENSIONS') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        //"select 'AUTO_TASK_STATUS' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'AUTO_TASK_STATUS') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'AUTO_TASK_MAX_RUN_TIME' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'AUTO_TASK_MAX_RUN_TIME') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'AUTO_TASK_INTERVAL' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'AUTO_TASK_INTERVAL') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'AUTOSTATS_TARGET' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'AUTOSTATS_TARGET') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        "select 'CASCADE' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'CASCADE') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        //"select 'CONCURRENT' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'CONCURRENT') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        "select 'DEGREE' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'DEGREE') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        "select 'ESTIMATE_PERCENT' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'ESTIMATE_PERCENT') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        //"select 'GLOBAL_TEMP_TABLE_STATS' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'GLOBAL_TEMP_TABLE_STATS') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'INCREMENTAL' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'INCREMENTAL') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'INCREMENTAL_STALENESS' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'INCREMENTAL_STALENESS') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        //"select 'INCREMENTAL_LEVEL' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'INCREMENTAL_LEVEL') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        "select 'METHOD_OPT' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'METHOD_OPT') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        "select 'NO_INVALIDATE' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'NO_INVALIDATE') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        "select 'OPTIONS' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'OPTIONS') as val\n" +
                        "  from dual\n" +
                        "union all\n" +
                        //"select 'PREFERENCE_OVERRIDES_PARAMETER' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'PREFERENCE_OVERRIDES_PARAMETER') as val\n" +
                        //"  from dual\n" +
                        //"union all\n" +
                        "select 'GRANULARITY' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'GRANULARITY') as val\n" +
                        "  from dual\n" +
                        //"union all\n" +
                        //"select 'PUBLISH' as opt,\n" +
                        //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        //"                            tabname => '<TABLE_NAME>',\n" +
                        //"                            pname   => 'PUBLISH') as val\n" +
                        //"  from dual\n" +
                        "union all\n" +
                        "select 'STALE_PERCENT' as opt,\n" +
                        "       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
                        "                            tabname => '<TABLE_NAME>',\n" +
                        "                            pname   => 'STALE_PERCENT') as val\n" +
                        "  from dual\n";
            //"union all\n" +
            //"select 'STAT_CATEGORY' as opt,\n" +
            //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
            //"                            tabname => '<TABLE_NAME>',\n" +
            //"                            pname   => 'STAT_CATEGORY') as val\n" +
            //"  from dual\n" +
            //"union all\n" +
            //"select 'TABLE_CACHED_BLOCKS' as opt,\n" +
            //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
            //"                            tabname => '<TABLE_NAME>',\n" +
            //"                            pname   => 'TABLE_CACHED_BLOCKS') as val\n" +
            //"  from dual\n" +
            //"union all\n" +
            //"select 'WAIT_TIME_TO_UPDATE_STATS' as opt,\n" +
            //"       DBMS_STATS.get_prefs(ownname => '<OWNER>',\n" +
            //"                            tabname => '<TABLE_NAME>',\n" +
            //"                            pname   => 'WAIT_TIME_TO_UPDATE_STATS') as val\n" +
            //"  from dual";

            string sqlStringReplaced = sqlString.Replace("<OWNER>", owner).Replace("<TABLE_NAME>", tableName);
            cmdPref = new OracleCommand(sqlStringReplaced, connection);


            try
            {
                dtPref.Rows.Clear();
                adaPref = new OracleDataAdapter(cmdPref);
                adaPref.Fill(dtPref);

            }
            catch (OracleException ex)
            {

            }
        }
        private void GetaTableActivePrefs()
        {
            string sqlString = "select preference_name, preference_value\n" +
                                "  from DBA_TAB_STAT_PREFS\n" +
                                " where owner = :owner\n" +
                                "   and table_name = :table_name";

            cmdActivePref = new OracleCommand(sqlString, connection);
            cmdActivePref.Parameters.Clear();
            cmdActivePref.Parameters.Add("owner", owner);
            cmdActivePref.Parameters.Add("table_name", tableName);

            try
            {
                dtActivePref.Rows.Clear();
                adaPref = new OracleDataAdapter(cmdActivePref);
                adaPref.Fill(dtActivePref);
            }
            catch (OracleException ex)
            {
            }
        }



        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                DataRowView dr = (DataRowView)bs.Current;
                string opts = dr["opts"].ToString();
                string val = dr["val"].ToString();
                RefreshListOptsValues(opts);
            }
        }
        private void RefreshListOptsValues(string opts)
        {
            contextMenuStripGridSet.Items.Clear();

            switch (opts)
            {
                case "APPROXIMATE_NDV_ALGORITHM":
                    ToolStripMenuItem tsi = new ToolStripMenuItem("REPEAT OR HYPERLOGLOG");
                    tsi.ForeColor = Color.Blue;
                    tsi.ToolTipText = "Default value";
                    contextMenuStripGridSet.Items.Add(tsi);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("ADAPTIVE SAMPLING");
                    contextMenuStripGridSet.Items.Add("HYPERLOGLOG");
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                case "AUTO_STAT_EXTENSIONS":
                    ToolStripMenuItem tsi3 = new ToolStripMenuItem("OFF");
                    tsi3.ForeColor = Color.Blue;
                    tsi3.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi3);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("ON");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                case "CASCADE":

                    ToolStripMenuItem tsi5 = new ToolStripMenuItem("DBMS_STATS.AUTO_CASCADE");
                    tsi5.ForeColor = Color.Blue;
                    tsi5.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi5);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("TRUE");
                    contextMenuStripGridSet.Items.Add("FALSE");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");
                    break;
                case "DEGREE":
                    ToolStripMenuItem tsi6 = new ToolStripMenuItem("NULL");
                    tsi6.ForeColor = Color.Blue;
                    tsi6.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi6);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("BOTTOM EDIT");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                case "ESTIMATE_PERCENT":
                    ToolStripMenuItem tsi7 = new ToolStripMenuItem("DBMS_STATS.AUTO_SAMPLE_SIZE");
                    tsi7.ForeColor = Color.Blue;
                    tsi7.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi7);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("BOTTOM EDIT");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");
                    break;

                case "GRANULARITY":


                    ToolStripMenuItem tsi8 = new ToolStripMenuItem("AUTO");
                    tsi8.ForeColor = Color.Blue;
                    tsi8.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi8);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("ALL");
                    contextMenuStripGridSet.Items.Add("DEFAULT");
                    contextMenuStripGridSet.Items.Add("GLOBAL");
                    contextMenuStripGridSet.Items.Add("GLOBAL AND PARTITION");
                    contextMenuStripGridSet.Items.Add("PARTITION");
                    contextMenuStripGridSet.Items.Add("SUBPARTITION");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                case "METHOD_OPT":
                    ToolStripMenuItem tsi12 = new ToolStripMenuItem("FOR ALL COLUMNS SIZE AUTO");
                    tsi12.ForeColor = Color.Blue;
                    tsi12.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi12);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("BOTTOM EDIT");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");
                    break;
                case "NO_INVALIDATE":
                    ToolStripMenuItem tsi9 = new ToolStripMenuItem("DBMS_STATS.AUTO_INVALIDATE");
                    tsi9.ForeColor = Color.Blue;
                    tsi9.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi9);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("TRUE");
                    contextMenuStripGridSet.Items.Add("FALSE");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                case "OPTIONS":
                    ToolStripMenuItem tsi10 = new ToolStripMenuItem("GATHER");
                    tsi10.ForeColor = Color.Blue;
                    tsi10.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi10);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("GATHER AUTO");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");
                    break;
                case "STALE_PERCENT":

                    ToolStripMenuItem tsi11 = new ToolStripMenuItem("10");
                    tsi11.ForeColor = Color.Blue;
                    tsi11.ToolTipText = "Default value";

                    contextMenuStripGridSet.Items.Add(tsi11);
                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());

                    contextMenuStripGridSet.Items.Add("BOTTOM EDIT");

                    contextMenuStripGridSet.Items.Add(new ToolStripSeparator());
                    contextMenuStripGridSet.Items.Add("UNSET");

                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)bs.Current;
            string sqlString = "begin\n" +
            "  DBMS_STATS.SET_TABLE_PREFS(ownname => '" + owner + "',\n" +
            "                             Tabname => '" + tableName + "',\n" +
            "                             pname   => '" + drv["opts"] + "',\n" +
            "                             pvalue  => '" + textBox1.Text + "');\n" +
            "end;";

            try
            {
                OracleCommand tmpcmd = new OracleCommand(sqlString, connection);
                tmpcmd.ExecuteNonQuery();
                ClassLog.Log.Add(ClassLog.Log.LogLevel.SETTINGSCHANGED, "Zmieniono parametr dla tabeli " + owner + "." + tableName + ": " + drv["opts"].ToString() + " na wartość " + textBox1.Text);
            }
            catch (OracleException exx)
            {
                MessageBox.Show(exx.Message.ToString());
            }
            GetTablePrefs();
            GetaTableActivePrefs();
            dataGridView1.Refresh();
            GColor();
        }

        private void SetParameter(string ParmValue)
        {
            if (ParmValue.Trim().Length < 0)
                return;
            textBox1.Text = ParmValue;

            DataRowView drv = (DataRowView)bs.Current;
            string sqlString = "begin\n" +
            "  DBMS_STATS.SET_TABLE_PREFS(ownname => '" + owner + "',\n" +
            "                             Tabname => '" + tableName + "',\n" +
            "                             pname   => '" + drv["opts"] + "',\n" +
            "                             pvalue  => '" + ParmValue + "');\n" +
            "end;";

            try
            {
                OracleCommand tmpcmd = new OracleCommand(sqlString, connection);
                tmpcmd.ExecuteNonQuery();
                ClassLog.Log.Add(ClassLog.Log.LogLevel.SETTINGSCHANGED, "Zmieniono parametr dla tabeli " + owner + "." + tableName + ": " + drv["opts"].ToString() + " na wartość " + ParmValue);
            }
            catch (OracleException exx)
            {
                MessageBox.Show(exx.Message.ToString());
            }
            GetTablePrefs();
            GetaTableActivePrefs();
            dataGridView1.Refresh();
            GColor();
        }
        private void UnsetParameter(string ParamValue)
        {
            string sqlString = "begin\n" +
            "  DBMS_STATS.delete_table_prefs(ownname => '" + owner + "',\n" +
            "                             Tabname => '" + tableName + "',\n" +
            "                             pname   => '" + ParamValue + "');\n" +
            "end;";

            try
            {
                OracleCommand tmpcmd = new OracleCommand(sqlString, connection);
                tmpcmd.ExecuteNonQuery();
                ClassLog.Log.Add(ClassLog.Log.LogLevel.SETTINGSCHANGED, "Zresetowano parametr dla tabeli " + owner + "." + tableName + ": " + ParamValue);
            }
            catch (OracleException exx)
            {
                MessageBox.Show(exx.Message.ToString());
            }
            GetTablePrefs();
            GetaTableActivePrefs();
            dataGridView1.Refresh();
            GColor();
        }
        private void contextMenuStripGridSet_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "BOTTOM EDIT")
            {
                textBox1.Select();
                textBox1.Focus();
                return;

            }
            else
            {
                groupBox1.Enabled = false;
            }

            if (e.ClickedItem.Text != "UNSET")
                SetParameter(e.ClickedItem.Text);
            else
            {
                DataRowView drv = (DataRowView)bs.Current;
                UnsetParameter(drv["opts"].ToString());
            }

        }

        private void buttonColumns_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)connection.Clone();
            conntmp.Open();

            FormSchemaStatsInternalTable fssit = new FormSchemaStatsInternalTable(conntmp, owner, tableName, textBox1.Text, sessionOptions);
            fssit.StartPosition = FormStartPosition.CenterParent;

            //if (fssit.ShowDialog(this) == DialogResult.OK)
            fssit.ShowDialog(this);
            {
                GetTablePrefs();
                GetaTableActivePrefs();
                GColor();
            }
        }

        private void FormSetTablePrefs_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }
    }
}
