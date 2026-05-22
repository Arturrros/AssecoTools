using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassMonitor
{
    /// <summary>
    /// Author:         artur.balon@asseco.pl
    /// Date created:   04-12-2023
    /// Chamge log:     
    /// Descritipn:     Dodanie monitorowania przestrzeni tymczasowych - dane historyczne
    /// </summary>
    public partial class FormMonitorSessionTmpHistorical : Form
    {
        DataTable source;
        OracleConnection Connection;

        public FormMonitorSessionTmpHistorical()
        {
            InitializeComponent();
        }
        public FormMonitorSessionTmpHistorical(DataTable source)
        {
            InitializeComponent();
            this.source = source;
            bindingSource1.DataSource = source;
            dataGridView1.DataSource = bindingSource1;
            this.Text = "Temporary table usage";
        }
        public FormMonitorSessionTmpHistorical(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            source = new DataTable();
            this.Text = "Temporary table usage";

            try
            {
                List<string> dtRange = MonitorSessionTmpHistorical.GetDtRANGE(Connection);

                toolStripComboBox1.Items.Clear();
                toolStripComboBox1.Items.AddRange(dtRange.ToArray());
                toolStripComboBox2.Items.Clear();
                toolStripComboBox2.Items.AddRange(dtRange.ToArray());
            }
            catch (OracleException exc)
            {
                throw exc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FormMonitorSessionTmpHistorical_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            toolStripComboBox2.SelectedIndex = 1;
            toolStripComboBox3.SelectedIndex = 0;
            dataGridView1.DataSource = MonitorSessionTmpHistorical.GetDTSql(Connection, MonitorSessionTmpHistoricalStatic.SQL01, toolStripComboBox1.Text, toolStripComboBox2.Text, toolStripComboBox3.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MonitorSessionTmpHistorical.GetDTSql(Connection, MonitorSessionTmpHistoricalStatic.SQL01, toolStripComboBox1.Text, toolStripComboBox2.Text, toolStripComboBox3.Text);
        }

        private void sumaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MonitorSessionTmpHistorical.GetDTSql(Connection, MonitorSessionTmpHistoricalStatic.SQL02, toolStripComboBox1.Text, toolStripComboBox2.Text, toolStripComboBox3.Text);
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MonitorSessionTmpHistorical.GetDTSql(Connection, MonitorSessionTmpHistoricalStatic.SQL03, toolStripComboBox1.Text, toolStripComboBox2.Text, toolStripComboBox3.Text);
        }

        private void FormMonitorSessionTmpHistorical_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }

        private void FormMonitorSessionTmpHistorical_HelpButtonClicked(object sender, CancelEventArgs e)
        {

        }
    }
}
