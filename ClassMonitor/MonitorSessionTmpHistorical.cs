using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ClassMonitor
{
    /// <summary>
    /// Author:         artur.balon@asseco.pl
    /// Date created:   04-12-2023
    /// Chamge log:     
    /// Descritipn:     Dodanie monitorowania przestrzeni tymczasowych - dane historyczne
    /// </summary>

    internal static class MonitorSessionTmpHistorical 
    {
        public static List<string>  GetDtRANGE(OracleConnection conn)
        {
            List<string> dtList = new List<string>();
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = MonitorSessionTmpHistoricalStatic.DT_RANGE;
                
                OracleDataReader reader =  cmd.ExecuteReader();
                while (reader.Read())
                {
                     dtList.Add(reader.GetString(0));
                }

                reader.Close();
                reader.Dispose();
            }
            catch (OracleException exc)
            { }
            return dtList;
        }

        public static DataTable GetDTSql(OracleConnection conn, string command, string dt1, string dt2, string isActive)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = command;
                cmd.Parameters.Add(new OracleParameter("dt1", dt1));
                cmd.Parameters.Add(new OracleParameter("dt2", dt2));
                cmd.Parameters.Add(new OracleParameter("isActive", isActive));
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);

                adapter.Fill(dt);
            }
            catch (OracleException exc)
            { }
            return dt;
        }
    }
}
