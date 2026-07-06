using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSchemaStats
{
    public class ClassSchemaStats
    {
        public static void SetClientInfo(OracleConnection Connection, string info)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DBMS_APPLICATION_INFO.SET_CLIENT_INFO", Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("client_info", info));
                cmd.ExecuteNonQuery();

            }
            catch (OracleException exc)
            {

            }
        }

        public static class SQLStrings
        {
            /// <summary>
            /// Dla baz 11
            /// </summary>
            public static string GET_USERS_11 = "SELECT USERNAME FROM DBA_USERS WHERE USERNAME NOT IN ('SYS','SYSTEM','DBSNMP') ORDER BY USERNAME";
            /// <summary>
            /// Dla baz 19
            /// </summary>
            public static string GET_USERS_19 = "SELECT USERNAME FROM DBA_USERS WHERE COMMON='NO' ORDER BY USERNAME";
            public static string GET_TABLES = "SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER = :owner and temporary = :temporary and nested ='NO' ORDER BY TABLE_NAME";
            public static string GET_TABLE_INDEXES = "SELECT INDEX_NAME FROM DBA_INDEXES WHERE OWNER = :owner and TABLE_NAME = :temporary ORDER BY INDEX_NAME";
            public static string GET_TABLE_STATS = "SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER = :owner and temporary = :temporary and nested ='NO' ORDER BY TABLE_NAME";
        }

        public static void UnlockSchemaStats(OracleConnection Connection, string Schema)
        {
            OracleCommand CmdUnlockSchema = new OracleCommand("DBMS_STATS.UNLOCK_SCHEMA_STATS", Connection);
            CmdUnlockSchema.CommandType = CommandType.StoredProcedure;
            CmdUnlockSchema.BindByName = true;
            CmdUnlockSchema.Parameters.Add("ownname", OracleDbType.Varchar2).Value = Schema;

            try
            {
                CmdUnlockSchema.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        public static void LockSchemaStats(OracleConnection Connection, string Schema)
        {
            OracleCommand CmdLockSchema = new OracleCommand("DBMS_STATS.LOCK_SCHEMA_STATS", Connection);
            CmdLockSchema.CommandType = CommandType.StoredProcedure;
            CmdLockSchema.BindByName = true;
            CmdLockSchema.Parameters.Add("ownname", OracleDbType.Varchar2).Value = Schema;

            try
            {
                CmdLockSchema.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        public static void UnlockTableStats(OracleConnection Connection, string Schema, string Table)
        {
            OracleCommand CmdUnlockSchema = new OracleCommand("DBMS_STATS.UNLOCK_TABLE_STATS", Connection);
            CmdUnlockSchema.CommandType = CommandType.StoredProcedure;
            CmdUnlockSchema.BindByName = true;
            CmdUnlockSchema.Parameters.Add("ownname", OracleDbType.Varchar2).Value = Schema;
            CmdUnlockSchema.Parameters.Add("tabname", OracleDbType.Varchar2).Value = Table;

            try
            {
                CmdUnlockSchema.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        public static void LockTableStats(OracleConnection Connection, string Schema, string Table)
        {
            OracleCommand CmdUnlockSchema = new OracleCommand("DBMS_STATS.LOCK_TABLE_STATS", Connection);
            CmdUnlockSchema.CommandType = CommandType.StoredProcedure;
            CmdUnlockSchema.BindByName = true;
            CmdUnlockSchema.Parameters.Add("ownname", OracleDbType.Varchar2).Value = Schema;
            CmdUnlockSchema.Parameters.Add("tabname", OracleDbType.Varchar2).Value = Table;

            try
            {
                CmdUnlockSchema.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }

        public static DataTable GetTables(OracleConnection Connection,String Schema, DataTable tables)
        {
            OracleCommand cmd = new OracleCommand("SELECT owner, table_name FROM ALL_TABLES where owner = :owner and temporary=:temporary order by owner, table_name", Connection);
            cmd.BindByName = true;
            cmd.Parameters.Add("owner", Schema);
            cmd.Parameters.Add("temporary", "N");
            OracleDataAdapter ada = new OracleDataAdapter(cmd);

            ada.Fill(tables);
            return tables;
        }
        public static DataTable GetTableStats(OracleConnection Connection, String Schemasy)
        {
            OracleCommand cmd = new OracleCommand("select OWNER,TABLE_NAME,LAST_ANALYZED,NUM_ROWS,SAMPLE_SIZE,TEMPORARY from all_tables t where owner in (" + Schemasy + ") order by OWNER,TABLE_NAME", Connection);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            DataTable tables = new DataTable();
            ada.Fill(tables);
            return tables;
        }
        public static DataTable GetColStatsHybrid(OracleConnection Connection, String Schemasy)
        {

            string sqlString = "select s.table_name,\n" +
            "       s.column_name,\n" +
            "       s.num_distinct,\n" +
            "       s.num_nulls,\n" +
            "       t.NUM_ROWS,\n" +
            "       s.sample_size,\n" +
            "       s.last_analyzed,\n" +
            "       round ((s.sample_size*100)/t.NUM_ROWS) as perc\n" +
            "  from dba_tab_col_statistics s, dba_tables t\n" +
            " where s.owner in (:owner)\n" +
            " and t.owner in (:owner)\n" +
            " and t.TABLE_NAME = s.TABLE_NAME\n" +
            "   and s.histogram = 'HYBRID'\n" +
          //  "   and round ((s.sample_size*100)/t.NUM_ROWS) < 100\n" +
            " order by table_name";

            OracleCommand cmd = new OracleCommand(sqlString, Connection);
            cmd.BindByName = true;
            cmd.Parameters.Add("owner", Schemasy);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            DataTable table = new DataTable();

            ada.Fill(table);
            return table;
        }
        //public static void SetSchemaPrefs(OracleConnection Connection, string Schema)
        //{
        //    try 
        //    {
        //    OracleCommand setCascade = new OracleCommand("DBMS_STATS.SET_SCHEMA_PREFS", Connection);
        //    setCascade.CommandType = CommandType.StoredProcedure;
        //    setCascade.BindByName = true;
        //    setCascade.Parameters.Add("ownname", OracleDbType.Varchar2).Value = Schema;
        //    setCascade.Parameters.Add("pname", OracleDbType.Varchar2).Value = "CASCADE";
        //    setCascade.Parameters.Add("pvalue", OracleDbType.Varchar2).Value = "TRUE";
        //    setCascade.ExecuteNonQuery();
        //    }
        //    catch (OracleException exc)
        //    {
        //        throw exc;
        //    }
        //}
        public static void FlushMonitoringInfo(OracleConnection Connection)
        {
            try
            {
                OracleCommand CmdFlushInfo = new OracleCommand("DBMS_STATS.FLUSH_DATABASE_MONITORING_INFO", Connection);
                CmdFlushInfo.CommandType = CommandType.StoredProcedure;
                CmdFlushInfo.ExecuteNonQuery();
                OracleCommand CmdTruncate = new OracleCommand("DELETE FROM STATISTICS_INFO", Connection);
                CmdTruncate.CommandType = CommandType.Text;
                CmdTruncate.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }

        public static void TruncateTableStatisticInfo(OracleConnection Connection)
        {
            try
            {
                OracleCommand CmdTruncate = new OracleCommand("DELETE FROM STATISTICS_INFO", Connection);
                CmdTruncate.CommandType = CommandType.Text;
                CmdTruncate.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        public static void TruncateTableStatisticCmd(OracleConnection Connection)
        {
            try
            {
                OracleCommand CmdTruncate = new OracleCommand("DELETE FROM STATISTICS_CMD", Connection);
                CmdTruncate.CommandType = CommandType.Text;
                CmdTruncate.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        public static DataTable GenerateScriptForAll(OracleConnection Connection, List<String> Schemas, Int32 Degree, Int32 NumFiles)
        {
            DataTable tab = new DataTable();

            TruncateTableStatisticCmd(Connection);
            foreach (string schema in Schemas)
            {
                OracleCommand CmdSplit = new OracleCommand("STATS.SPLIT_STATS_CMD", Connection);
                CmdSplit.CommandType = CommandType.StoredProcedure;
                CmdSplit.BindByName = true;
                CmdSplit.Parameters.Add("p_owner", OracleDbType.Varchar2).Value = schema;
                CmdSplit.Parameters.Add("p_degree", OracleDbType.Varchar2).Value = Degree;
                CmdSplit.Parameters.Add("p_num_files", OracleDbType.Varchar2).Value = NumFiles;
                try
                {
                    CmdSplit.ExecuteNonQuery();
                }
                catch (OracleException exc)
                {
                    throw exc;
                }
            }
            OracleCommand cmd = new OracleCommand("SELECT * FROM STATISTICS_CMD ORDER BY NUM_ROWS DESC", Connection);
            OracleDataAdapter ada = new OracleDataAdapter(cmd);
            ada.Fill(tab);
            return tab;

        }

        public static DataTable FillStatsInfo(OracleConnection Connection)
        {
            try
            {
                String sqlString = String.Empty;
                sqlString = "SELECT I.USER_SCHEMA, I.T AS TAB, I.P AS PART, I.S AS SUB, I.REASON FROM STATISTICS_INFO I ";

                OracleCommand Cmd = new OracleCommand(sqlString, Connection);
                OracleDataAdapter Ada = new OracleDataAdapter(Cmd);
                DataTable Tab = new DataTable();
                Ada.Fill(Tab);
                return Tab;
            }
            catch (OracleException exc)
            {
                
                throw exc;
            } 
        }
        public static DataTable FillStatsInfo_Filter(OracleConnection Connection, string Reason)
        {
            try
            {
                String sqlString = String.Empty;
                sqlString = "SELECT I.USER_SCHEMA, I.T AS TAB, I.P AS PART, I.S AS SUB, I.REASON FROM STATISTICS_INFO I WHERE I.REASON LIKE '%" + Reason + "%' ";

                OracleCommand Cmd = new OracleCommand(sqlString, Connection);
                OracleDataAdapter Ada = new OracleDataAdapter(Cmd);
                DataTable Tab = new DataTable();
                Ada.Fill(Tab);
                return Tab;
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
    }

}
