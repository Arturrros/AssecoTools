using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSession
{

    public partial class FormSessionsHistorical : Form
    {
        OracleConnection Connection;
        DataRowView drv;
        List<string> SnapDates;
        BindingSource bindingSourceSnapDates;

        BindingSource bindingSourceSessions;

        /// <summary>
        /// Wyswietlenie liczby sesji archowalnych
        /// Autor: Artur Bałon
        /// Changelog: 
        /// Created 03-2024
        /// </summary>
        /// <param name="connectionString">connectionSrting - dla połączenia</param>
        public FormSessionsHistorical(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            InitializeEnv();
        }

        private void FormSessionsHistorical_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Ustawienie     zmiennych 
        /// </summary>
        private void InitializeEnv()
        {
            bindingSourceSessions = new BindingSource();
            try
            {
                SetNLSDateFormat();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.Message.ToString());
            }


            SnapDates = InitSnapDates();
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.AddRange(SnapDates.ToArray());
            toolStripComboBox1.SelectedIndex = 0;
            ShowSessions(toolStripComboBox1.SelectedItem.ToString());
        }

        /// <summary>
        /// ustawienie formatu dat
        /// </summary>
        private void SetNLSDateFormat()
        {
            string sqlstring = "alter session set nls_date_format = 'YYYY-MM-DD HH24:MI:SS'";
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = sqlstring };
            try
            {
               cmd.ExecuteNonQuery();
            }
            catch (OracleException exc) 
            {
                MessageBox.Show(exc.Message.ToString());
            }
        }
        /// <summary>
        /// Wyswietlenie/odsiezenie danych na formatce
        /// </summary>
        /// <param name="snapDate"></param>
        private void ShowSessions(string snapDate)
        {
            DataTable HistoricalSessionTable = new DataTable() { TableName = "HistoricalSessionTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = SQLStrings.SESSIONS_HISTORICAL };
            cmd.Parameters.Add(new OracleParameter("snap_date",snapDate));

            try
            {
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(HistoricalSessionTable);
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.Message.ToString());
            }
            bindingSourceSessions.DataSource = HistoricalSessionTable;
            dataGridView1.DataSource = bindingSourceSessions;
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 130;
        }

        /// <summary>
        /// Pobranie zakresu wartosci dat
        /// </summary>
        /// <returns></returns>
        private List<string> InitSnapDates()
        {
            List<string> list = new List<string>(); 
            string sqlString = "select to_char(snap_date,'YYYY-MM-DD HH24:MI:SS') from sessions group by snap_date order by snap_date desc";

            DataTable SnapDatesTable = new DataTable() { TableName = "SnapDatesTable" };
            OracleCommand cmd = new OracleCommand() { Connection = Connection, CommandText = sqlString };

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(Convert.ToString(reader.GetValue(0)));
            }



            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(SnapDatesTable);
            foreach (DataRow sdt in SnapDatesTable.Rows) 
            {
                list.Add(sdt[0].ToString());
            }
            return list;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSessions(toolStripComboBox1.SelectedItem.ToString());
        }

        private void FormSessionsHistorical_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }
    }
}
