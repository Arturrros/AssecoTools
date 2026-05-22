using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassVisual
{
    /// <summary>
    /// Forma do przegląd danych historycznych liczby sesji 
    /// autor:      artur.balon@asseco.pl
    /// date:       08-2024
    /// changelog:  23-08-2024 commited
    /// 
    /// </summary>
    public partial class FormSessHist : Form
    {
        OracleConnection connection;
        DataTable tableOfSnapData;
        BindingSource bindingSourceSnapData;

        DataTable tableOfSnapPrograms;
        BindingSource bindingSourceSnapPrograms;

        DataTable ChartTable;

        DataRowView currentSelectedRow;

        public FormSessHist(OracleConnection Connection)
        {
            InitializeComponent();
            connection = Connection;

            bindingSourceSnapData = new BindingSource();
            bindingSourceSnapPrograms = new BindingSource();
            bindingSourceSnapPrograms.CurrentChanged += BindingSourceSnapPrograms_CurrentChanged;
        }

        private void BindingSourceSnapPrograms_CurrentChanged(object sender, EventArgs e)
        {
            currentSelectedRow = (DataRowView)bindingSourceSnapPrograms.Current;
            
        }
        private void getDataChart(string programm) 
        {
            
            string sqlString = "select snap_date, count(*) as cnt\n" +
            "  from asseco_tools.sessions s\n" +
            " where program = :program\n" +
            " group by snap_date\n" +
            " order by snap_date";

            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Add("program", programm);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(ChartTable);

            chart1.DataSource = ChartTable;

            var oCh = chart1.ChartAreas[0];
            //oCh.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;

            //oCh.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;

            chart1.Series.Clear();


            chart1.Series.Add("SeriesProgram");
            chart1.Series["SeriesProgram"].XValueMember = "snap_date";
            chart1.Series["SeriesProgram"].YValueMembers = "cnt";
            //chart1.ChartAreas["ChartArea"].AxisX.MajorGrid.Enabled = true;
            //chart1.Series["SeriesProgram"].IsValueShownAsLabel = true;
            chart1.Series["SeriesProgram"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.DataBind();




        }

        private void FormSessions_Load(object sender, EventArgs e)
        {
            InitTableOfSnapData();
            getSnapDateList();
            
            comboBox1.Items.Clear();
            comboBox1.DataSource = bindingSourceSnapData;
            comboBox1.DisplayMember = "SNAPDATE";

            dataGridView1.DataSource = bindingSourceSnapPrograms;
            
        }
        private void InitTableOfSnapData()
        {
            tableOfSnapData = new DataTable();
            tableOfSnapData.TableName = "tableOfSnapData";

            tableOfSnapPrograms = new DataTable();
            tableOfSnapPrograms.TableName = "tableOfSnapPrograms";

            bindingSourceSnapData.DataSource = tableOfSnapData;
            bindingSourceSnapPrograms.DataSource = tableOfSnapPrograms;

            ChartTable = new DataTable();
        }
        private void getSnapDateList()
        {
            string sqlString = "select to_char(snap_date,'YYYY-MM-DD HH24:MI:SS') as SNAPDATE from asseco_tools.sessions group by to_char(snap_date,'YYYY-MM-DD HH24:MI:SS') order by to_char(snap_date,'YYYY-MM-DD HH24:MI:SS') desc";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(tableOfSnapData);
            
        }
        private void getDataFromSnap(string SnapDate)
        {
            string sqlString = "select program, count(*) as cnt\n" +
            "  from asseco_tools.sessions s\n" +
            " where s.snap_date =\n" +
            "       to_date(:snap_date, 'YYYY-MM-DD HH24:MI:SS')\n" +
            " group by program";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("snap_date", SnapDate);

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            tableOfSnapPrograms.Rows.Clear();
            adapter.Fill(tableOfSnapPrograms);

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dv = (DataRowView)comboBox1.SelectedItem;
            getDataFromSnap(dv["SNAPDATE"].ToString());
            

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            getDataChart(currentSelectedRow["program"].ToString());
        }
    }
}
