using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Oracle.ManagedDataAccess.Client;

namespace ClassVisual
{
    public partial class FormChart : Form
    {
        OracleConnection conn;
        OracleCommand cmd;
        DataTable seriesData;
        public FormChart()
        {
            InitializeComponent();
            seriesData = new DataTable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SeriesData"></param>
        /// <param name="ChartTitle"></param>
        /// <param name="LegendText"></param>
        public FormChart(DataTable SeriesData,string ChartTitle, String LegendText)
        {
            InitializeComponent();
            seriesData = SeriesData;
            chart1.Series["Series1"].LegendText = "LegendText";
            chart1.Titles[0].Text = "cvcvb";

        }
        public FormChart(OracleConnection Conn, string Query, string ChartTitle, String LegendText)
        {
            InitializeComponent();
            conn = Conn;
            cmd = new OracleCommand(Query,conn);

            chart1.Series["Series1"].LegendText = "LegendText";
           // chart1.Titles[0].Text = "cvcvb";

        }
        private void FormChart_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter();
            oracleDataAdapter.SelectCommand = cmd;
            oracleDataAdapter.Fill(table);

            DataView dv = new DataView(table);
            dv.RowFilter = "";

            chart1.Series[0].Points.DataBind(dv, "DT","VALUE_DIFF","");



                       //chart1.Series[0].LegendText = "sdfg";
            //chart1.Titles[0].Text = "cvcvb";
            //if (seriesData.Rows.Count > 0)
            //{
            //    foreach (DataRow row in seriesData.Rows)
            //    {
            //        Int32 xValue = Convert.ToInt32(row[1].ToString());
            //        DateTime xTime = Convert.ToDateTime(row[0].ToString());

            //        AddSimpleData(xValue);
            //    }
            //}
            //AddSimpleData(12);
            //AddSimpleData(22);
        }

        private void AddSimpleData(Int32 val)
        {
            Series series = chart1.Series["Series1"];
            double next = 1;


            if (series.Points.Count > 0)
            {
                next = series.Points.Last().XValue + 1;
            }

            DataPoint dp = new DataPoint();

            dp.XValue = next;
            dp.YValues[0] = val;

            series.Points.Add(dp);

            // 500 limit do przewijania
            
            //if (series.Points.Count > 500)
            //{
            //    series.Points.Remove(series.Points[0]);
            //    chart1.ResetAutoValues();
            //}
        }


    }
}
