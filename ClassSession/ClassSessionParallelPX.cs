using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSession
{
    public partial class FormSession 
    {
        /// <summary>
        /// Wyświetlanie sesji rodzica - dla sesji podrzędnych
        /// Parallel
        /// Autor:      artur.balon@asseco.pl
        /// Date:       11-2025
        /// ChangeLog   Created
        /// </summary>
        private void showParentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 sid = Convert.ToInt32(drv["sid"]);
            Int32 serial = Convert.ToInt32(drv["serial#"]);

            OracleConnection conntmp = (OracleConnection)Connection.Clone();
            conntmp.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conntmp;
            cmd.CommandText = SQLStrings.PX_SESSION;
            cmd.Parameters.Clear();
            cmd.Parameters.Add("sid", sid);

            Int32 qsid = 0;
            Int32 qserial = 0;

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetValue(0) != DBNull.Value)
                        qsid = Convert.ToInt32(reader.GetValue(0));
                    if (reader.GetValue(1) != DBNull.Value)
                        qserial = Convert.ToInt32(reader.GetValue(1));
                }
                reader.Close();
            }
            if (qsid != 0 && qserial != 0)
            {
                ClassSession.FormSession fs = new ClassSession.FormSession(conntmp, qsid, qserial, sessionOptions);
                fs.Show();
            }
            else if (qsid != 0 && qserial == 0)
            {
                MessageBox.Show("This is a parent");
            }
            else
            {
                MessageBox.Show("Not a prallel session");
            }
            
            Int32 pxSessionCount = GetPXVCount(sid);

            if (pxSessionCount < 0)
            {
                MessageBox.Show("Something went wroong");
            }

        }
        private Int32 GetPXVCount(Int32 sid)
        {
            Int32 sesCnt = 0;
            OracleCommand cmdtmp = new OracleCommand();
            cmdtmp.Connection = Connection;
            cmdtmp.CommandText = SQLStrings.PX_SESSION_CNT;
            cmdtmp.Parameters.Clear();
            cmdtmp.Parameters.Add("sid", sid);
            try
            {
                sesCnt = Convert.ToInt32(cmdtmp.ExecuteScalar());
            }
            catch (Exception ex) 
            { 
                sesCnt = 0; 
            }
            return sesCnt;
        }
    }
}
