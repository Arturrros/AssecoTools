using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassMonitor
{
    public partial class FormMemory : Form
    {
        SessionOptions sessionOptions;
        OracleConnection connection;
        OracleCommand cmd;
        public FormMemory(OracleConnection Connection, SessionOptions sessionOptions)
        {
            InitializeComponent();
            connection = Connection;
            
        }

        private void FormMemory_Load(object sender, EventArgs e)
        {
            
            OracleCommand cmds = new OracleCommand("select name, round(value/1024/1024) from v$parameter where name in ('memory_max_target','memory_target','sga_max_size','sga_target','db_cache_size','shared_pool_size','large_pool_size','java_pool_size','streams_pool_size')", connection);
            OracleDataReader reader = cmds.ExecuteReader();

            while (reader.Read())
            {
                chart1.Legends["SGAINFOLegend"].CustomItems.Add(Color.Empty, reader.GetValue(0).ToString() + " : " + reader.GetValue(1).ToString());
            }
            reader.Close();

            OracleCommand cmdp = new OracleCommand("select name, round(value/1024/1024) from v$parameter where name in ('pga_aggregate_target')", connection);
            OracleDataReader readerp = cmdp.ExecuteReader();

            while (readerp.Read())
            {
                chart2.Legends["PGAINFOLegend"].CustomItems.Add(Color.Empty, readerp.GetValue(0).ToString() + " : " + readerp.GetValue(1).ToString());
            }

            readerp.Close();

            OracleCommand cmd = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$sga", connection);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            DataTable tab = new DataTable();
            ada.Fill(tab);
            

            chart1.DataSource = tab;
            chart1.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series["SGA"].IsValueShownAsLabel = true;
            chart1.Series["SGA"].XValueMember = "Name";
            chart1.Series["SGA"].YValueMembers = "MBytes";
            chart1.Legends["SGALegend"].Title = "SGA";
            chart1.DataBind();

            OracleCommand cmd1 = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$pgastat where name in ('maximum PGA allocated','total PGA inuse')", connection);
            OracleDataAdapter ada1 = new OracleDataAdapter(cmd1);
            DataTable tab1 = new DataTable();
            ada1.Fill(tab1);


            chart2.DataSource = tab1;
            chart2.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart2.Series["PGA"].IsValueShownAsLabel = true;
            chart2.Series["PGA"].XValueMember = "Name";
            chart2.Series["PGA"].YValueMembers = "MBytes";
            chart2.Legends["PGALegend"].Title = "PGA";
            chart2.DataBind();

            //toolStripComboBox1.SelectedIndex = 1;
        }
        private void chart1_Click(object sender, EventArgs e)
        {
            switch (chart1.Legends["SGALegend"].Title)
            {
                case "SGA":
                    OracleCommand cmd1 = new OracleCommand("select Name, round(bytes/1024/1024) as MBytes from (select * from v$sgainfo order by bytes desc)", connection);
                    OracleDataAdapter ada1 = new OracleDataAdapter(cmd1);
                    DataTable tab1 = new DataTable();
                    ada1.Fill(tab1);
                    chart1.DataSource = tab1;
                    chart1.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    chart1.Series["SGA"].IsValueShownAsLabel = true;
                    chart1.Series["SGA"].XValueMember = "Name";
                    chart1.Series["SGA"].YValueMembers = "MBytes";
                    chart1.Legends["SGALegend"].Title = "SGAINFO";
                    chart1.DataBind();
                    break;
                case "SGAINFO":
                    OracleCommand cmd = new OracleCommand("select sga_size,estd_physical_reads from v$sga_target_advice", connection);
                    OracleDataAdapter ada = new OracleDataAdapter(cmd);
                    DataTable tab = new DataTable();
                    ada.Fill(tab);
                    chart1.DataSource = tab;
                    chart1.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    chart1.Series["SGA"].IsValueShownAsLabel = true;
                    chart1.Series["SGA"].XValueMember = "sga_size";
                    chart1.Series["SGA"].YValueMembers = "estd_physical_reads";
                    chart1.Legends["SGALegend"].Title = "SGAADV";
                    chart1.ChartAreas["SGAChartArea"].AxisX.Title = "MB of SGA";
                    chart1.ChartAreas["SGAChartArea"].AxisY.Title = "Physical Reads";
                    chart1.DataBind();

                    break;
                case "SGAADV":
                    OracleCommand cmd2 = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$sga", connection);
                    OracleDataAdapter ada2 = new OracleDataAdapter(cmd2);
                    DataTable tab2 = new DataTable();
                    ada2.Fill(tab2);
                    chart1.DataSource = tab2;
                    chart1.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    chart1.Series["SGA"].IsValueShownAsLabel = true;
                    chart1.Series["SGA"].XValueMember = "Name";
                    chart1.Series["SGA"].YValueMembers = "MBytes";
                    chart1.Legends["SGALegend"].Title = "SGA";
                    chart1.DataBind();
                    break;
                default:
                    break;
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            switch (chart2.Legends["PGALegend"].Title)
            {
                case "PGAADV":
                    OracleCommand cmd1 = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$pgastat where name in ('maximum PGA allocated','total PGA inuse')", connection);
                    OracleDataAdapter ada1 = new OracleDataAdapter(cmd1);
                    DataTable tab1 = new DataTable();
                    ada1.Fill(tab1);
                    chart2.DataSource = tab1;
                    chart2.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                    chart2.Series["PGA"].IsValueShownAsLabel = true;
                    chart2.Series["PGA"].XValueMember = "Name";
                    chart2.Series["PGA"].YValueMembers = "MBytes";
                    chart2.Legends["PGALegend"].Title = "PGA";
                    chart1.DataBind();
                    break;
                case "PGA":
                    OracleCommand cmd = new OracleCommand("select round(PGA_TARGET_FOR_ESTIMATE/1024/1024) as PGA_TARGET_FOR_ESTIMATE,round(ESTD_EXTRA_BYTES_RW/1024/1024) as ESTD_EXTRA_BYTES_RW from v$pga_target_advice", connection);
                    OracleDataAdapter ada = new OracleDataAdapter(cmd);
                    DataTable tab = new DataTable();
                    ada.Fill(tab);
                    chart2.DataSource = tab;
                    chart2.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    chart2.Series["PGA"].IsValueShownAsLabel = true;
                    chart2.Series["PGA"].XValueMember = "PGA_TARGET_FOR_ESTIMATE";
                    chart2.Series["PGA"].YValueMembers = "ESTD_EXTRA_BYTES_RW";
                    chart2.Legends["PGALegend"].Title = "PGAADV";
                    chart2.ChartAreas["PGAChartArea"].AxisX.Title = "MB of PGA";
                    chart2.ChartAreas["PGAChartArea"].AxisY.Title = "MB Read/Writes";
                    chart2.DataBind();

                    break;
                default:
                    break;
            }
        }

    }
}
