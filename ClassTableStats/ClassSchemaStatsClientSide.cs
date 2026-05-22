using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;



namespace ClassSchemaStats
{
    public class ClassSchemaStatsClientSide
    {


        public static class SQLStrings
        {
            public static string GET_USERS = "SELECT USERNAME FROM DBA_USERS WHERE USERNAME NOT IN ('SYS','SYSTEM','DBSNMP') ORDER BY USERNAME";
            public static string GET_TABLES = "SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER = :owner and temporary = :temporary ORDER BY TABLE_NAME";
            public static string GET_TABLE_STATS = "SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER = :owner and temporary = :temporary ORDER BY TABLE_NAME";
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

    }

}
