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

namespace ClassReports
{
    public partial class FormReportMain : Form
    {
        readonly OracleConnection Connection;
        OracleCommand cmd;
        OracleDataAdapter adapter1;

        public FormReportMain(OracleConnection Connection, string SQLOnStart)
        {
            InitializeComponent();
        }

        private void FormReportMain_Load(object sender, EventArgs e)
        {

        }

        private void errorReportModuleCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }
    }
}
