using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ClassSession
{
    public class Sessions
    {
        /// <summary>
        /// kill session command . synonym 'kill_session' for procedure "kill_session" on the sys user is needed
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sid"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public string Kill(OracleConnection Connection,  int sid, int serial)
        {
            bool isBackgroundProces = false;

            //Kill check 
            OracleCommand cmdS = new OracleCommand();
            cmdS.Connection = Connection;
            cmdS.CommandText = "select upper(type) from v$session where sid=:sid and serial#=:serial#";
            cmdS.CommandType = CommandType.Text;
            cmdS.Parameters.Add("sid", sid);
            cmdS.Parameters.Add("serial#", serial);
            try
            {
                string typeProcess = cmdS.ExecuteScalar().ToString();
                if(typeProcess != "USER")
                    isBackgroundProces = true;

            }
            catch (OracleException ex)
            {
                return ex.Message.ToString();
            }


            if (isBackgroundProces)
            {
                return "Cannot kill BACKGROUND proces";
            }
            else
            {
                //string killResult = "";
                OracleParameter killResult = new OracleParameter();
                killResult.DbType = DbType.String;
                killResult.Direction = ParameterDirection.Output;
                killResult.Size = 2000;

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "kill_session";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("sid", sid);
                cmd.Parameters.Add("serial#", serial);
                cmd.Parameters.Add(killResult);

                try
                {
                    cmd.ExecuteNonQuery();
                    return killResult.Value.ToString();

                }
                catch (OracleException ex)
                {
                    return ex.Message.ToString();
                }
            }
        }

        /// <summary>
        /// Get Sql Plan for sql_id
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns>return (string)Explain</returns>
        public static string GetXPlain(OracleConnection connection, string sqlid, bool getFullSql)
        {
            string plan = string.Empty;
            string sqlString = "SELECT * FROM table(DBMS_XPLAN.DISPLAY_CURSOR(:sql_id, NULL, 'ALL ALLSTATS'))";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Add("sql_id", sqlid);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                plan += reader.GetValue(0).ToString() + "\n";
            }
            reader.Close();

            if (getFullSql)
            {
                plan = plan + "\n\n" + "-- FULL SQL --\n\n" + GetSql(connection, sqlid);
            }


            return plan;
        }

        /// <summary>
        /// Get Sql
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns></returns>
        public static string GetSql(OracleConnection connection, string sqlid)
        {
            string sql = string.Empty;
            string sqlString = "SELECT SQL_TEXT FROM v$sqltext_with_newlines where sql_id = :sql_id order by piece asc";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Add("sql_id", sqlid);

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sql += reader.GetValue(0).ToString();
            }
            reader.Close();

            return sql;
        }

        /// <summary>
        /// Flush corsor (sql_id)
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlid"></param>
        /// <returns>if command was send then true</returns>
        public static string FlushPlanCursor(OracleConnection connection, string sqlid)
        {
            string sqlString = "SELECT cast(ADDRESS as varchar2(30)) as ADDRESS, HASH_VALUE FROM gv$sqlarea where sql_id=:sql_id";
            OracleCommand cmd = new OracleCommand(sqlString, connection);
            cmd.Parameters.Add("sql_id",sqlid);

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sql = "SYS.DBMS_SHARED_POOL.PURGE";
                    OracleCommand cmd2 = new OracleCommand(sql, connection);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.Add("name", reader.GetValue(0).ToString() + "," + reader.GetValue(1).ToString());
                    cmd2.Parameters.Add("flags", "C");
                    cmd2.Parameters.Add("heaps", 1);

                    cmd2.ExecuteNonQuery();
                    
                }
                reader.Close();
                return "Purge completed";
            }
            catch (OracleException ex)
            {
                return ex.Message.ToString(); 
            }
        }

    }
}
