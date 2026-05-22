using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassWaiters
{
    public partial class FormBlockedObjects : Form
    {
        OracleConnection Connection;
        DataTable tableSessions;
        OracleCommand cmd;
        int[] SidSerialSaddr = { 0, 0 };

        List<Blockers> blocerList = new List<Blockers>();

        public FormBlockedObjects(OracleConnection conn1)
        {
            InitializeComponent();
            Connection = conn1;
            tableSessions = new DataTable();
            cmd = new OracleCommand();
            cmd.Connection = conn1;
        }

        private void FormSessions_Load(object sender, EventArgs e)
        {
        }

        private string PokazZablokowaneDane(OracleConnection connection1, int objectId)
        {
            List<string> li = new List<string>();
            string sqlString = "SELECT 'select * from ' || d.object_name || ' where rowid = ' || '''' ||\n" +
            "        dbms_rowid.rowid_create(1,\n" +
            "                                ROW_WAIT_OBJ#,\n" +
            "                                ROW_WAIT_FILE#,\n" +
            "                                ROW_WAIT_BLOCK#,\n" +
            "                                ROW_WAIT_ROW#) || ''''\n" +
            "  FROM gv$session s, dba_objects d\n" +
            " WHERE sid in (SELECT bb.sid FROM gv$session_blockers bb where bb.SID = " + objectId.ToString() + ")\n" +
            "   AND s.ROW_WAIT_OBJ# = d.OBJECT_ID";


            OracleCommand cmd = new OracleCommand(sqlString, connection1);
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string query = reader.GetValue(0).ToString();
                    li.Add("Locked data:\n");
                    li.Add(query + "\n");
                    li.Add("--Query waiting session for sid " + objectId.ToString() + " :\n");


                    string sqlString1 = "SELECT cast(SQL_FULLTEXT as varchar2(4000))\n" +
                    "FROM V$SQLAREA\n" +
                    "WHERE (ADDRESS, HASH_VALUE) IN\n" +
                    "      (SELECT SQL_ADDRESS, SQL_HASH_VALUE\n" +
                    "       FROM GV$SESSION\n" +
                    "       WHERE SID =:sid)";
                    OracleCommand cmd2 = new OracleCommand(sqlString1, connection1);
                    cmd2.Parameters.Add(new OracleParameter("sid", objectId.ToString()));
                    OracleDataReader reader1 = cmd2.ExecuteReader();
                    while (reader1.Read())
                    {
                        li.Add(reader1.GetValue(0).ToString());
                    }
                    reader1.Close();
                }
                reader.Close();

                string result = string.Empty;
                foreach (string str in li)
                {
                    result += str +"\n";
                }
                return result;
            }
            catch 
            {
                return string.Empty;
            }

        }

        private void showSidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassSession.FormSession fs = new ClassSession.FormSession(Connection, SidSerialSaddr[0], SidSerialSaddr[1]);
            if (fs.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
 
            }
        }

        private void killSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KIll session " + SidSerialSaddr[0].ToString() + " - " + SidSerialSaddr[1].ToString() + " ?", "Kill session", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(new ClassSession.Sessions().Kill(Connection, SidSerialSaddr[0], SidSerialSaddr[1]));
               
            }
        }

        private void FormBlockedObjects_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }
    }
}
