using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;

namespace ClassWaiters
{
    /// <summary>
    /// Forma pokazująca zablokowany obiekt do modyfikacji przez inne sesje
    /// </summary>
    public partial class FormHolds : Form
    {
        readonly OracleConnection Connection;
        DataTable tableHoldings;
        OracleCommand cmd;
 
        DataRowView drv;

        SessionOptions sessionOptions;

        public FormHolds(OracleConnection conn1, SessionOptions sessionOptions)
        {
            InitializeComponent();
            Connection = conn1;
            tableHoldings = new DataTable();
            cmd = new OracleCommand();
            cmd.Connection = conn1;
            this.sessionOptions = sessionOptions;
        }

        

        private void FormHolds_Load(object sender, EventArgs e)
        {
            InitializeEviroment();
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {

            string sqlString = "select s.SID,\n" +
                                 "       s.SERIAL#,\n" +
                                 "       s.USERNAME,\n" +
                                 "       s.OSUSER,\n" +
                                 "       s.STATUS,\n" +
                                 "       s.PROGRAM,\n" +
                                 "       s.EVENT,\n" +
                                 "       l.blocking_others,\n" +
                                 "       l.mode_held\n" +
                                 "  from dba_lock l, v$session s\n" +
                                " where l.session_id = s.SID\n" +
                                "   and l.lock_id1 = (select object_id\n" +
                                "                       from dba_objects\n" +
                                "                      where owner = :owner \n" +
                                "                        and object_name = :object_name )\n" +
                                " ";

            DataTable dt = new DataTable();
            cmd.CommandText = sqlString;
            cmd.Parameters.Clear();
            cmd.Parameters.Add("owner", tsSchemaComboBox.Text);
            cmd.Parameters.Add("object_name", toolStripTextBox1.Text.ToUpper());
            try
            {
                OracleDataAdapter ada = new OracleDataAdapter(cmd);
                ada.Fill(dt);

                bindingSource1.DataSource = dt;
                dataGridView1.DataSource = bindingSource1;

            }
            catch (Exception ex)
            { }
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void showSidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drv != null)
            {
                int sid = Convert.ToInt32(drv["sid"]);
                int serial = Convert.ToInt32(drv["serial#"]);

                ClassSession.FormSession fs = new ClassSession.FormSession(Connection, sid, serial, sessionOptions);
                if (fs.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {}
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            drv = (DataRowView)bindingSource1.Current;
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonGo_Click(sender, e);
            }
        }

        private void FormHolds_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }
    }
}
