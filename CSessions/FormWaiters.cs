using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassSession;
using CSessions;
using Oracle.ManagedDataAccess.Client;

namespace ClassWaiters
{
    public partial class FormWaiters : Form
    {
        OracleConnection Connection;
        DataTable tableSessions;
        OracleCommand cmd;
        int[] SidSerialSaddr = { 0, 0 };

        List<Blockers> blocerList = new List<Blockers>();

        public FormWaiters(OracleConnection conn1)
        {
            InitializeComponent();
            Connection = conn1;
            tableSessions = new DataTable();
            cmd = new OracleCommand();
            cmd.Connection = conn1;
        }

        private void FormSessions_Load(object sender, EventArgs e)
        {
            Mon_Locks(tv1);
        }
        private void Mon_Locks(TreeView tv1)
        {
            tv1.Nodes.Clear();
            blocerList.Clear();
            //string sql = "select sid, serial#, '('||s.sid||','||serial#||') ' ||s.OSUSER||' - '||s.MACHINE||' '||s.PROGRAM|| ' ('|| l.mode_held|| ' - ' || l.lock_type||')' from gv$session s, dba_blockers b, dba_lock l where s.SID = b.holding_session and b.holding_session = l.session_id and blocking_others='Blocking'";
            string sql = "select sid, serial#, s.OSUSER||' - '||s.MACHINE||' '||s.PROGRAM|| ' ('|| l.mode_held|| ' - ' || l.lock_type||')'||' STATUS '||s.status||' LastCallEt '||s.last_call_et|| ' SecInWait :' || s.SECONDS_IN_WAIT ,cast(saddr as varchar2(60)) from gv$session s, dba_blockers b, dba_lock l where s.SID = b.holding_session and b.holding_session = l.session_id and blocking_others='Blocking'";
            OracleCommand cmd = new OracleCommand(sql, Connection);

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TreeNode tn = new TreeNode(reader.GetValue(2).ToString());
                    tn.ContextMenuStrip = contextMenuStrip1;


                    string sqlString = "SELECT sid,serial#, s.OSUSER || ' - ' || s.MACHINE ||' ' || s.PROGRAM || ' STATUS '||s.status||' LastCallEt '||s.last_call_et|| ' SecInWait :' || s.SECONDS_IN_WAIT, cast(saddr as varchar2(60))\n" +
                    "  FROM gv$session s, dba_waiters b\n" +
                    " WHERE s.SID = b.waiting_session\n" +
                    "   AND b.holding_session = " + ":sid" + "\n" +
                    " GROUP BY sid, serial#, s.OSUSER || ' - ' || s.MACHINE ||' ' || s.PROGRAM ||' STATUS '||s.status||' LastCallEt '||s.last_call_et|| ' SecInWait :' || s.SECONDS_IN_WAIT, cast(saddr as varchar2(60))";

                    OracleCommand cmd1 = new OracleCommand(sqlString, Connection);
                    cmd1.Parameters.Add(new OracleParameter("sid", reader.GetValue(0).ToString()));

                    tn.Tag = reader.GetValue(0).ToString() + "," + reader.GetValue(1).ToString() + "," + reader.GetValue(3).ToString();

                    OracleDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        TreeNode tnn = new TreeNode(reader1.GetValue(2).ToString());
                        tnn.Tag = reader1.GetValue(0).ToString() + "," + reader1.GetValue(1).ToString() + "," + reader.GetValue(3).ToString();
                        tnn.ContextMenuStrip = contextMenuStrip1;
                        tn.Nodes.Add(tnn);

                        tn.Expand();
                    }
                    reader1.Close();
                    tv1.Nodes.Add(tn);
                }
                reader.Close();
            }
            catch(OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private string PokazZablokowaneDane(OracleConnection connection1, string sid)
        {
            List<string> li = new List<string>();
            string sqlString = "SELECT 'select * from ' || d.object_name || ' where rowid = ' || '''' ||\n" +
            "        dbms_rowid.rowid_create(1,\n" +
            "                                ROW_WAIT_OBJ#,\n" +
            "                                ROW_WAIT_FILE#,\n" +
            "                                ROW_WAIT_BLOCK#,\n" +
            "                                ROW_WAIT_ROW#) || ''''\n" +
            "  FROM gv$session s, dba_objects d\n" +
            " WHERE sid in (SELECT bb.sid FROM gv$session_blockers bb where bb.SID = " + sid + ")\n" +
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
                    li.Add("--Query waiting session for sid " + sid + " :\n");


                    string sqlString1 = "SELECT cast(SQL_FULLTEXT as varchar2(4000))\n" +
                    "FROM V$SQLAREA\n" +
                    "WHERE (ADDRESS, HASH_VALUE) IN\n" +
                    "      (SELECT SQL_ADDRESS, SQL_HASH_VALUE\n" +
                    "       FROM GV$SESSION\n" +
                    "       WHERE SID =:sid)";
                    OracleCommand cmd2 = new OracleCommand(sqlString1, connection1);
                    cmd2.Parameters.Add(new OracleParameter("sid", sid));
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

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            Mon_Locks(tv1);
            
        }

        private void showSidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            
            ClassSession.FormSession fs = new ClassSession.FormSession(conntmp, SidSerialSaddr[0], SidSerialSaddr[1]);
            if (fs.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
 
            }
        }

        private void tv1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            string[] SidSerialSplited = tv1.SelectedNode.Tag.ToString().Split(',');
            SidSerialSaddr[0] = Convert.ToInt32(SidSerialSplited[0]);
            SidSerialSaddr[1] = Convert.ToInt32(SidSerialSplited[1]);
            toolStripLabelSidSerial.Text = tv1.SelectedNode.Tag.ToString();
        }

        private void tryToGetLockedDataRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            ClassViewWindow.FormTextView ft = new ClassViewWindow.FormTextView(PokazZablokowaneDane(conntmp, SidSerialSaddr[0].ToString()));
            if (ft.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void killSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KIll session " + SidSerialSaddr[0].ToString() + " - " + SidSerialSaddr[1].ToString() + " ?", "Kill session", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(new ClassSession.Sessions().Kill(Connection, SidSerialSaddr[0], SidSerialSaddr[1]));
                Mon_Locks(tv1);
                
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //OracleConnection conntmp = (OracleConnection)Connection.Clone();
            //conntmp.Open();
            //ClassViewWindow.FormGridView gw = new ClassViewWindow.FormGridView(conntmp, "select * from  HOLDING_LOCKS order by dt desc , wai_wait_time asc ", "Old holders");
            //gw.StartPosition = FormStartPosition.CenterParent;
            //gw.Show(this);

            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();
            FormWaitersHistory fwh = new FormWaitersHistory(conntmp);
            fwh.Show(this);


        }

        private void FormWaiters_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }
    }
}
