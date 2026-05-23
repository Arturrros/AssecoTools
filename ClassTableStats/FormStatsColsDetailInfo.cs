using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSchemaStats
{
    /// <summary>
    /// Dodana formatka (na podstawie statystyk tabel) Histogram - rozklad danych dla wszyskich kolumn
    /// Data: 05-2025
    /// </summary>
    public partial class FormStatsColsDetailInfo : Form
    {
        readonly BackgroundWorker worker;
        public delegate void Worker_Info_d(string info);
        private readonly OracleCommand cmdTabHist;
        private  OracleDataAdapter ada;
        private readonly OracleConnection Connection;
        private readonly String owner = String.Empty;
        readonly string tableName = String.Empty;
        private DataTable TabColsStats;
        private DataTable TabSizes;
        private DataTable TabInfo;
        private DataTable TabHistory;
        private DataTable TabModyfications;
        private DataTable IndStats;
        private DataTable IndUsage;
        private DataTable TabTimeStatsInfo;
        private Int64 TableDataBlocks;
        private string databaseName;


        bool autostop = true;
        public FormStatsColsDetailInfo(OracleConnection Connection, string Owner, string TableName)
        {
            InitializeComponent();
            this.Connection = Connection;
            this.Text = "Table Info for: " + Owner + "." + TableName;
            owner = Owner;
            tableName = TableName;

            databaseName = (Connection.PDBName == null ? Connection.DatabaseName : Connection.PDBName);

            worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };


            cmdTabHist = new OracleCommand();
            cmdTabHist.Connection = Connection;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Visible = false;
            dataGridView1.DataSource = TabColsStats;
            dataGridView2.DataSource = TabHistory;
            dataGridView3.DataSource = TabModyfications;
            dataGridView4.DataSource = IndStats;
            dataGridView5.DataSource = IndUsage;
            dataGridView6.DataSource = TabTimeStatsInfo;

            #region Coloring Indexex

            #endregion



            groupBox5.Text = "Indeks Statistic - DataBlocks: " + TableDataBlocks.ToString();

            #region Sizes
            Int64 intTables = 0;
            Int64 intLobs = 0;
            Int64 intIndexes = 0;
            Int64 intIndexesLob = 0;

            foreach (DataRow dr in TabSizes.Rows)
            {
                

                switch (dr["obj"].ToString())
                {
                    case "TABLES":
                        intTables = Convert.ToInt64(dr["bytes"]);
                        
                        break;
                    case "LOBS":
                        intLobs = Convert.ToInt64(dr["bytes"]);
                        
                        break;
                    case "INDEXES":
                        intIndexes = Convert.ToInt64(dr["bytes"]);
                        
                        break;
                    case "LOBSINDEXES":
                        intIndexesLob = Convert.ToInt64(dr["bytes"]);
                        break;
                    default:
                        break;
                }
            }
            label6.Text = intTables.ToString("#,##0");
            label7.Text = intLobs.ToString("#,##0");
            label8.Text = intIndexes.ToString("#,##0");
            label9.Text = intIndexesLob.ToString("#,##0");
            label12.Text = Pretty(intTables + intLobs);
            label13.Text = Pretty(intIndexes + intIndexesLob);

            Int64 summary = (intTables + intLobs + intIndexes + intIndexesLob);
            label15.Text = Pretty(summary);

            #endregion

            #region TabInfo
            foreach (DataRow dr in TabInfo.Rows) 
            {
                label34.Text = DBNull.Value.Equals(dr["num_rows"]) ? " " : Convert.ToInt64(dr["num_rows"]).ToString("#,##0");
                label33.Text = DBNull.Value.Equals(dr["sample_size"]) ? " " : Convert.ToInt64(dr["sample_size"]).ToString("#,##0");
                //label33.Text = Convert.ToInt64(dr["sample_size"]).ToString("#,##0");
                label32.Text = dr["last_analyzed"].ToString();
                label31.Text = dr["global_stats"].ToString();
                label30.Text = dr["user_stats"].ToString();
                label29.Text = dr["partitioned"].ToString();
                label28.Text = dr["temporary"].ToString();
                label27.Text = dr["tablespace_name"].ToString();
                label26.Text = dr["has_sensitive_column"].ToString();
                label37.Text = dr["stale_stats"].ToString();
            }
            #endregion

        }
        private string Pretty(Int64 summary)
        {
            string prettyString = string.Empty;
            if (summary < 1024)
                prettyString = (summary).ToString("#,##0") + " Bytes";
            else if (summary < 1048576)
                prettyString = (Decimal.Divide(summary , 1024)).ToString("N2") + " KB";
            else if (summary < 1073741824)
                prettyString = (Decimal.Divide(summary , 1048576)).ToString("N2") + " MB";
            else if (summary < 1099511627776)
                prettyString = (Decimal.Divide(summary , 1073741824)).ToString("N2") + " GB";
            else
                prettyString = (Decimal.Divide(summary , 1099511627776)).ToString("N2") + " TB";
                return prettyString;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = Runquery(owner, tableName, worker);
        }

        private void FormStatsInfoHistogram_Load(object sender, EventArgs e)
        {

        }

        private int Runquery(string Owner, string tableName, BackgroundWorker runWorker)
        {
            Worker_Info("Wait...");
            TableDataBlocks = GetTableBlocks(Owner, tableName);
            TabColsStats = GetTabColsStats(Owner, tableName);
            TabSizes = GetTabSizes(Owner, tableName);
            TabInfo = GetTabInfo(Owner, tableName);
            TabHistory = GetTabHistory(Owner, tableName);
            TabModyfications = GetTabModyfications(Owner, tableName);
            IndStats = GetIndexStats(Owner, tableName);
            IndUsage = GetIndexUsage(Owner, tableName);
            TabTimeStatsInfo = GetTabTimeStatsInfo();
            return 0;
        }

        private DataTable GetTabTimeStatsInfo()
        {
            DataTable tempTab = StatsTime.GetInfo(databaseName, owner, tableName);
            return tempTab;
        }
        private DataTable GetTabColsStats(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();
            string sqlString = "select s.column_name,\n" +
                        "       s.last_analyzed,\n" +
                        "       s.num_distinct as distincts,\n" +
                        "       s.num_nulls as nulls,\n" +
                        "       s.num_buckets as buckets,\n" +
                        "       s.sample_size,\n" +
                        "       s.histogram\n" +
                        "  from dba_tab_col_statistics s\n" +
                        " where owner = :owner\n" +
                        "   and table_name = :table_name";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }

        private DataTable GetTabSizes(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();

            string sqlString = "SELECT obj, sum(bytes) as bytes\n" +
            "  FROM (\n" +
            "\n" +
            "        SELECT 'TABLES' as obj, segment_name as table_name, owner, bytes\n" +
            "          FROM dba_segments\n" +
            "         WHERE segment_type IN\n" +
            "               ('TABLE', 'TABLE PARTITION', 'TABLE SUBPARTITION')\n" +
            "        UNION ALL\n" +
            "        SELECT 'LOBS' as obj, l.table_name, l.owner, s.bytes\n" +
            "          FROM dba_lobs l, dba_segments s\n" +
            "         WHERE s.segment_name = l.segment_name\n" +
            "           and s.owner = l.owner\n" +
            "           AND s.segment_type in\n" +
            "               ('LOBSEGMENT', 'LOB PARTITION', 'LOB SUBPARTITION')\n" +
            "        UNION ALL\n" +
            "        SELECT 'INDEXES' as obj, i.table_name, i.owner, s.bytes\n" +
            "          FROM dba_indexes i, dba_segments s\n" +
            "         WHERE s.segment_name = i.index_name\n" +
            "           AND s.owner = i.owner\n" +
            "           AND s.segment_type IN\n" +
            "               ('INDEX', 'INDEX PARTITION', 'INDEX SUBPARTITION')\n" +
            "        UNION ALL\n" +
            "        SELECT 'LOBSINDEXES' as obj, l.table_name, l.owner, s.bytes\n" +
            "          FROM dba_lobs l, dba_segments s\n" +
            "         WHERE s.segment_name = l.index_name\n" +
            "           AND s.owner = l.owner\n" +
            "           AND s.segment_type = 'LOBINDEX'\n" +
            "        )\n" +
            " WHERE owner in UPPER(:owner)\n" +
            "   and table_name in (:table_name)\n" +
            " GROUP BY obj, table_name\n";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }

        private DataTable GetTabInfo(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();



            string sqlString = "select t.table_name,\n" +
            "       t.num_rows,\n" +
            "       t.sample_size,\n" +
            "       t.last_analyzed,\n" +
            "       t.global_stats,\n" +
            "       t.user_stats,\n" +
            "       t.partitioned,\n" +
            "       t.temporary,\n" +
            "       t.tablespace_name,\n" +
            "       t.has_sensitive_column,\n" +
            "       s.stale_stats,\n" +
            "       s.global_stats\n" +
            "  from dba_tables t\n" +
            " right join dba_tab_statistics s\n" +
            "    on (t.OWNER = s.owner and t.TABLE_NAME = s.table_name)\n" +
            " where t.owner = :owner\n" +
            "   and t.table_name = :table_name";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }

        private DataTable GetTabHistory(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();

            string sqlString = "select dt, num_rows, bytes\n" +
            "  from rowscnt r, ROWSCNT_TAB t\n" +
            " where r.id = t.id_snap\n" +
            "   and t.table_name = :table_name\n" +
            "   and r.id_schema in\n" +
            "       (select s.id from rowscnt_schemas s where s.schema_name = :owner)\n" +
            " order by r.dt";


            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
               // Worker_Info("Done");
            }
            return tempTab;
        }

        private DataTable GetTabModyfications(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();

            string sqlString = "select m.inserts as ins, m.deletes as del, m.updates as upd, m.timestamp\n" +
            "  from dba_tab_modifications m\n" +
            " where table_owner = :owner\n" +
            "   and table_name = :table_name";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }

        private Int64 GetTableBlocks(string Owner, string tableName)
        {
            Int64 blocks = 0;

            string sqlString = "select blocks\n" +
            "  from dba_tables\n" +
            " where owner = :owner\n" +
            "   and table_name = :table_name";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                object o = cmdTabHist.ExecuteScalar();
                if (o != DBNull.Value && o != null)
                    blocks = Convert.ToInt64(o);
                else
                    blocks = 0;
                Worker_Info("Start");
            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return blocks;
        }

        private DataTable GetIndexStats(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();

            string sqlString = "select s.index_name,\n" +
            "       s.distinct_keys,\n" +
            "       s.clustering_factor,\n" +
            "       s.num_rows,\n" +
            "       s.sample_size,\n" +
            "       s.stale_stats,\n" +
            "       to_char(s.last_analyzed,'YYYY-MM-DD HH24:MI:SS') as last_analyzed\n" +
            "  from dba_ind_statistics s\n" +
            " where table_owner = :owner\n" +
            "   and table_name = :table_name\n"+
            " order by s.last_analyzed desc";


            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }

        private DataTable GetIndexUsage(string Owner, string tableName)
        {
            DataTable tempTab = new DataTable();

            string sqlString = "select i.index_name,\n" +
            "       u.total_access_count,\n" +
            "       u.total_exec_count,\n" +
            "       u.total_rows_returned,\n" +
            "       to_char(u.last_used,'YYYY-MM-DD HH24:MI:SS') as last_used\n" +
            "       from dba_index_usage u\n" +
            "       right join dba_indexes i\n" +
            "              on (i.index_name = u.name)\n" +
            "       where i.owner = :owner\n" +
            "       and i.table_name = :table_name\n"+
            "       order by u.last_used";

            cmdTabHist.CommandText = sqlString;
            cmdTabHist.Parameters.Clear();
            cmdTabHist.Parameters.Add(new OracleParameter("owner", Owner));
            cmdTabHist.Parameters.Add(new OracleParameter("table_name", tableName));
            try
            {
                ada = new OracleDataAdapter(cmdTabHist);
                Worker_Info("Start");
                ada.Fill(tempTab);

            }
            catch (OracleException ex)
            {
                Worker_Info("Done");
            }
            return tempTab;
        }


        private void Worker_Info(string Info)
        {
            if (label1.InvokeRequired)
            {
                Worker_Info_d wid = new Worker_Info_d(Worker_Info);
                this.Invoke(wid,"Wait...");
            }
            else
            {
                //label1.Text = "Done. ";
            }

        }

        private void FormStatsInfoHistogram_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                cmdTabHist.Cancel();
            }
            Connection.Close();
        }

        private void label37_TextChanged(object sender, EventArgs e)
        {
            if (label37.Text != "NO")
            {
                label37.ForeColor = Color.Red;
                errorProvider1.SetError(this.label37, "Należy odświeżyć statystyki");
            }
            else
            {
                label37.ForeColor = SystemColors.ControlText;
                errorProvider1.SetError(this.label37, "");
            }
        }

        private void dataGridView4_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    Color c;
                    if (e.Value.ToString() == "YES" )
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        errorProvider1.SetError(this.label38, "Należy odświeżyć statystyki");
                    }
                    else
                    {
                        c = dgv.DefaultCellStyle.BackColor;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }
    }
}
