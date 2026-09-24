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
    /// <summary>
    /// Description:    Wykresy pamięci wizualizacja
    /// Date:           09-2026
    /// Autor:          artur.balon@asseco.pl
    /// Changelog:  
    /// </summary>
    public partial class FormMemory : Form
    {
        private readonly SessionOptions sessionOptions;
        private readonly OracleConnection connection;

        public FormMemory(OracleConnection connection, SessionOptions sessionOptions)
        {
            InitializeComponent();

            this.connection = connection;
            this.sessionOptions = sessionOptions;
        }

        private void FormMemory_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMemoryParameters();
                LoadSgaChart();
                LoadPgaChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMemoryParameters()
        {
            ChartSGA.Legends["SGAINFOLegend"].CustomItems.Clear();
            ChartPGA.Legends["PGAINFOLegend"].CustomItems.Clear();

            using (OracleCommand commands = new OracleCommand(
                "select name, round(value/1024/1024)" + 
                " from v$parameter " + 
                " where name in ('memory_max_target', 'memory_target', 'sga_max_size', 'sga_target', 'db_cache_size', 'shared_pool_size', 'large_pool_size', 'java_pool_size', 'streams_pool_size')",
                connection))
            {
                using (OracleDataReader reader = commands.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChartSGA.Legends["SGAINFOLegend"].CustomItems.Add(
                            Color.Empty,
                            reader.GetValue(0).ToString() + " : " + reader.GetValue(1).ToString());
                    }
                }
            }

            using (OracleCommand commandPga = new OracleCommand(
                "select  name, round(value/1024/1024) " +
                "from v$parameter "+
                "where name in ('pga_aggregate_target')",
                connection))
            {
                using (OracleDataReader readerPga = commandPga.ExecuteReader())
                {
                    while (readerPga.Read())
                    {
                        ChartPGA.Legends["PGAINFOLegend"].CustomItems.Add(
                            Color.Empty,
                            readerPga.GetValue(0).ToString() + " : " + readerPga.GetValue(1).ToString());
                    }
                }
            }
        }

        private void LoadSgaChart()
        {
            using (OracleCommand cmd = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$sga", connection))
            using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                ChartSGA.DataSource = table;
                ChartSGA.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                ChartSGA.Series["SGA"].IsValueShownAsLabel = true;
                ChartSGA.Series["SGA"].XValueMember = "Name";
                ChartSGA.Series["SGA"].YValueMembers = "MBytes";
                ChartSGA.Legends["SGALegend"].Title = "SGA";
                ChartSGA.DataBind();
            }
        }

        private void LoadPgaChart()
        {
            using (OracleCommand cmd = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$pgastat where name in ('maximum PGA allocated','total PGA inuse')", connection))
            using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                ChartPGA.DataSource = table;
                ChartPGA.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                ChartPGA.Series["PGA"].IsValueShownAsLabel = true;
                ChartPGA.Series["PGA"].XValueMember = "Name";
                ChartPGA.Series["PGA"].YValueMembers = "MBytes";
                ChartPGA.Legends["PGALegend"].Title = "PGA";
                ChartPGA.DataBind();
            }
        }

        private void ChartSGA_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ChartSGA.Legends["SGALegend"].Title)
                {
                    case "SGA":
                        using (OracleCommand cmd = new OracleCommand("select Name, round(bytes/1024/1024) as MBytes from (select * from v$sgainfo order by bytes desc)", connection))
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            ChartSGA.DataSource = table;
                            ChartSGA.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                            ChartSGA.Series["SGA"].IsValueShownAsLabel = true;
                            ChartSGA.Series["SGA"].XValueMember = "Name";
                            ChartSGA.Series["SGA"].YValueMembers = "MBytes";
                            ChartSGA.Legends["SGALegend"].Title = "SGAINFO";
                            ChartSGA.DataBind();
                        }
                        break;

                    case "SGAINFO":
                        using (OracleCommand cmd = new OracleCommand("select sga_size,estd_physical_reads from v$sga_target_advice", connection))
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            ChartSGA.DataSource = table;
                            ChartSGA.Series["SGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                            ChartSGA.Series["SGA"].IsValueShownAsLabel = true;
                            ChartSGA.Series["SGA"].XValueMember = "sga_size";
                            ChartSGA.Series["SGA"].YValueMembers = "estd_physical_reads";
                            ChartSGA.Legends["SGALegend"].Title = "SGAADV";
                            ChartSGA.ChartAreas["SGAChartArea"].AxisX.Title = "MB of SGA";
                            ChartSGA.ChartAreas["SGAChartArea"].AxisY.Title = "Physical Reads";
                            ChartSGA.DataBind();
                        }
                        break;

                    case "SGAADV":
                        LoadSgaChart();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas odświeżania wykresu SGA: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChartPGA_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ChartPGA.Legends["PGALegend"].Title)
                {
                    case "PGAADV":
                        using (OracleCommand cmd = new OracleCommand("select Name, round(value/1024/1024) as MBytes from v$pgastat where name in ('maximum PGA allocated','total PGA inuse')", connection))
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            ChartPGA.DataSource = table;
                            ChartPGA.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                            ChartPGA.Series["PGA"].IsValueShownAsLabel = true;
                            ChartPGA.Series["PGA"].XValueMember = "Name";
                            ChartPGA.Series["PGA"].YValueMembers = "MBytes";
                            ChartPGA.Legends["PGALegend"].Title = "PGA";
                            ChartPGA.DataBind();
                        }
                        break;

                    case "PGA":
                        using (OracleCommand cmd = new OracleCommand("select round(PGA_TARGET_FOR_ESTIMATE/1024/1024) as PGA_TARGET_FOR_ESTIMATE,round(ESTD_EXTRA_BYTES_RW/1024/1024) as ESTD_EXTRA_BYTES_RW from v$pga_target_advice", connection))
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            ChartPGA.DataSource = table;
                            ChartPGA.Series["PGA"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                            ChartPGA.Series["PGA"].IsValueShownAsLabel = true;
                            ChartPGA.Series["PGA"].XValueMember = "PGA_TARGET_FOR_ESTIMATE";
                            ChartPGA.Series["PGA"].YValueMembers = "ESTD_EXTRA_BYTES_RW";
                            ChartPGA.Legends["PGALegend"].Title = "PGAADV";
                            ChartPGA.ChartAreas["PGAChartArea"].AxisX.Title = "MB of PGA";
                            ChartPGA.ChartAreas["PGAChartArea"].AxisY.Title = "MB Read/Writes";
                            ChartPGA.DataBind();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas odświeżania wykresu PGA: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
