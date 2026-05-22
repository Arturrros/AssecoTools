using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ClassSize
{
    /// <summary>
    /// Author:         artur.balon@asseco.pl
    /// Cescription:    klasa pomocnicza do rozmiarow
    /// Changelog:      2023-04
    /// </summary>
    public class ClassSize
    {
        public static DataTable GetRowsCntSchemas(OracleConnection connection)
        {
            DataTable STable = new DataTable();
            OracleCommand cmd = new OracleCommand(SQLStrings.GET_SCHEMAS, connection);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(STable);
            return STable;
        }

        public static DataTable GetRowsCntDatesForSchema(OracleConnection connection, int idSchema)
        {
            DataTable STable = new DataTable();
            OracleCommand cmd = new OracleCommand(SQLStrings.GET_SCHEMA_DATE, connection);
            cmd.Parameters.Add("id_schema", idSchema);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(STable);
            return STable;
        }

        public static Int64 GetSnapSize(OracleConnection connection, int idSnap)
        {
            DataTable STable = new DataTable();
            OracleCommand cmd = new OracleCommand(SQLStrings.GET_SNAP_SIZE, connection);
            cmd.Parameters.Add("id_snap", idSnap);
            string size = cmd.ExecuteScalar().ToString();
            return Convert.ToInt64(size);
        }
    }

    public static class SQLStrings
    {
        public static string GET_SCHEMAS = "SELECT ID, SCHEMA_NAME FROM ROWSCNT_SCHEMAS";
        public static string GET_SCHEMA_DATE = "SELECT ID, DT FROM ROWSCNT WHERE ID_SCHEMA = :id_schema ORDER BY DT ASC";
        public static string GET_SNAP_SIZE = "SELECT SUM(BYTES) FROM ROWSCNT_MAIN WHERE ID_SNAP =:id_snap";


        public static string GET_CHART_SCHEMA = "SELECT TO_CHAR(R.DT,'YYYY-MM-DD') as DT , ROUND(SUM(M.BYTES)/1024/1024) AS MB_SIZE\n" +
        "  FROM ROWSCNT_SCHEMAS S, ROWSCNT_MAIN M, ROWSCNT R\n" +
        " WHERE S.ID = R.ID_SCHEMA\n" +
        "   AND M.ID_SNAP = R.ID\n" +
        "   AND S.ID = :id\n" +
        " GROUP BY R.DT\n" +
        " ORDER BY R.DT";


        /// <summary>
        /// DATABASE SIZE
        /// </summary>
        public static string SIZE_DB = "select sum(SIZE_GB)\n" +
                                        "  from (select round(sum(bytes) / 1024 / 1024 / 1024) SIZE_GB\n" +
                                        "          from dba_data_files\n" +
                                        "        union\n" +
                                        "        select round(sum(bytes) / 1024 / 1024 / 1024) SIZE_GB\n" +
                                        "          from dba_temp_files)";

        
        /// <summary>
        /// TABLESPACES SIZE
        /// </summary>
        public static string SIZE_TABLESPACES = "select *\n" +
                                                "  from (select tablespace_name,\n" + 
                                                "               round(sum(bytes) / 1024 / 1024 / 1024) SIZE_GB\n" + 
                                                "          from dba_data_files\n" + 
                                                "         group by tablespace_name\n" + 
                                                "        union\n" + 
                                                "        select tablespace_name,\n" + 
                                                "               round(sum(bytes) / 1024 / 1024 / 1024) SIZE_GB\n" + 
                                                "          from dba_temp_files\n" + 
                                                "         group by tablespace_name)";


        /// <summary>
        /// SEGMENTS SIZE BY OWNER
        /// </summary>
        public static string SIZE_SEGMENTS_OWNER = "select owner, round(sum(bytes) / 1024 / 1024 / 1024)\n" +
                                            "  from dba_segments\n" + 
                                            " group by owner";

      

    }
}
