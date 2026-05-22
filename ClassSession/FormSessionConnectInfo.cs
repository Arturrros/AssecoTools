using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSession
{
    /// <summary>
    /// Forma informacji o zmiennych danej sesji polaczonej
    /// Autor:  artur.balon@asseco.pl
    /// Date:   09-2025
    /// </summary>
    public partial class FormSessionConnectInfo : Form
    {
        private readonly OracleConnection connection;
        private readonly int sid;
        private readonly int serial;
        private readonly string program;
        private readonly string machine;

        public FormSessionConnectInfo(OracleConnection Connection, int Sid, int Serial, string Program, string Machine)
        {
            InitializeComponent();
            this.connection = Connection;
            this.sid = Sid;
            this.serial = Serial;
            this.program = Program;
            this.machine = Machine;
            this.Text = "Sid: " + sid + " Serial: " + serial;
        }

        private void FormSessionConnectInfo_Load(object sender, EventArgs e)
        {

            string sqlStringCi = "select ci.authentication_type,\n" +
            "       ci.client_charset,\n" +
            "       ci.client_oci_library,\n" +
            "       ci.client_version,\n" +
            "       ci.client_driver,\n" +
            "       ci.client_connection,\n" +
            "       ci.network_service_banner,\n" +
            "       ci.con_id,\n" +
            "       ci.osuser\n" +
            "  from v$session_connect_info ci\n" +
            " where sid = :sid\n" +
            "   and serial# = :serial";

            OracleCommand cmdCi = new OracleCommand() { Connection = connection, CommandText = sqlStringCi };
            cmdCi.Parameters.Add(new OracleParameter("sid", sid));
            cmdCi.Parameters.Add(new OracleParameter("serial", serial));

            OracleDataReader readerCi = cmdCi.ExecuteReader();
            while (readerCi.Read())
            {
                label10.Text = readerCi.GetValue(0).ToString();
                label11.Text = readerCi.GetValue(1).ToString();
                label12.Text = readerCi.GetValue(2).ToString();
                label13.Text = readerCi.GetValue(3).ToString();
                label14.Text = readerCi.GetValue(4).ToString();
                label15.Text = readerCi.GetValue(5).ToString();
                label16.Text = readerCi.GetValue(6).ToString();
                label17.Text = readerCi.GetValue(7).ToString();
                label18.Text = readerCi.GetValue(8).ToString();
                label19.Text = program;
                label21.Text = machine;
            }
            readerCi.Close();


            string sqlStringProc = "SELECT p.spid, p.tracefile, p.pga_used_mem as used_mem_bytes\n" +
            "  FROM V$SESSION S, V$PROCESS P\n" +
            " WHERE S.PADDR = P.ADDR\n" +
            "   AND sid = :sid\n" +
            "   and s.serial# = :serial";


            OracleCommand cmdProc = new OracleCommand() { Connection = connection, CommandText = sqlStringProc };
            cmdProc.Parameters.Add(new OracleParameter("sid", sid));
            cmdProc.Parameters.Add(new OracleParameter("serial", serial));

            OracleDataReader readerProc = cmdProc.ExecuteReader();
            while (readerProc.Read())
            {
                label24.Text = readerProc.GetValue(0).ToString();
                label23.Text = readerProc.GetValue(1).ToString();
                label22.Text = readerProc.GetValue(2).ToString();
            }
            readerProc.Close();


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OracleConnection conntmp = (OracleConnection)connection.Clone();
            conntmp.Open();

            string sqlString = "select c.sid,\n" +
            "       c.serial#,\n" +
            "       s.program,\n" +
            "       s.machine,\n" +
            "       s.terminal,\n" +
            "       s.osuser,\n" +
            "       c.authentication_type,\n" +
            "       c.client_charset,\n" +
            "       c.client_oci_library,\n" +
            "       c.client_version,\n" +
            "       c.client_driver,\n" +
            "       c.client_connection,\n" +
            "       c.network_service_banner,\n" +
            "       c.con_id\n" +
            "  from v$session s, v$session_connect_info c\n" +
            " where s.sid = c.sid\n" +
            "   and s.serial# = c.serial#";

            ClassViewWindow.FormGridView fgw = new ClassViewWindow.FormGridView(conntmp, sqlString, "Sessions Connect Info");
            fgw.ShowDialog();
        }
    }
}
