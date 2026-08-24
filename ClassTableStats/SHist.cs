using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassSchemaStats
{
    /// <summary>
    /// Author      : artur.balon@asseco.pl   
    /// Date        : 08-2026
    /// Descrition  : Modul do obsługi historii statystyk. Import/Export
    /// </summary>
    public class SHist
    {

        private OracleConnection connection;
        private string owner;
        private List<string> tablesNames;
        const string STATTABNAME = "STATTABHIST";

        public SHist(OracleConnection Connection, string Owner, List<string> TablesNames)
        {
           
            this.connection = Connection;
            owner = Owner;
            tablesNames = TablesNames;

        }

        private bool IsExportTbleExists()
        {
            string SqlFullText = string.Empty;
            try
            {
                string sqlString = "select count(*) from user_tables where table_name = :table_name";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("TABLE_NAME", STATTABNAME));

                Int32 tabex = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if (tabex > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }

        #region Drop/Create

        public void CreateStatsTable(string Owner, string TableName)
        {
            if (!IsExportTbleExists())
            {
                string sqlString = "DBMS_STATS.CREATE_STAT_TABLE";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("ownname", Owner));
                cmd.Parameters.Add(new OracleParameter("stattab", TableName));
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException exc) { }
            }
        }

        public void DropStatsTable()
        {
            if (!IsExportTbleExists())
            {
                string sqlString = "DBMS_STATS.DROP_STAT_TABLE";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("ownname", owner));
                cmd.Parameters.Add(new OracleParameter("stattab", STATTABNAME));
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException exc) { }
            }
        }
        #endregion

        public void ExportTableStats(  bool Cascade)
        {
            if (!IsExportTbleExists())
            {
                foreach (string tabName in tablesNames)
                {
                    string sqlString = "DBMS_STATS.EXPORT_TABLE_STATS";
                    OracleCommand cmd = new OracleCommand(sqlString, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("ownname", owner));
                    cmd.Parameters.Add(new OracleParameter("tabname", tabName));
                    cmd.Parameters.Add(new OracleParameter("stattab", STATTABNAME));
                    cmd.Parameters.Add(new OracleParameter("cascade", Cascade));
                    cmd.Parameters.Add(new OracleParameter("stat_category", "OBJECT_STATS, SYNOPSES"));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException exc) { }
                }
            }
        }

        /// <summary>
        /// dla zewnetrzych wywołan
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="TableName"></param>
        /// <param name="StatTable"></param>
        /// <param name="Cascade"></param>
        public void ExportTableStats(string Owner, string TableName, string StatTable, bool Cascade)
        {
            if (!IsExportTbleExists())
            {
                foreach (string tabName in tablesNames)
                {
                    string sqlString = "DBMS_STATS.EXPORT_TABLE_STATS";
                    OracleCommand cmd = new OracleCommand(sqlString, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("ownname", Owner));
                    cmd.Parameters.Add(new OracleParameter("tabname", TableName));
                    cmd.Parameters.Add(new OracleParameter("stattab", StatTable));
                    cmd.Parameters.Add(new OracleParameter("cascade", Cascade));
                    cmd.Parameters.Add(new OracleParameter("stat_category", "OBJECT_STATS, SYNOPSES"));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException exc) { }
                }
            }
        }

        public void ImportTableStats( bool Cascade, bool Invalidate)
        {
            if (!IsExportTbleExists())
            {
                foreach (string tabName in tablesNames)
                {
                    string sqlString = "DBMS_STATS.IMPORT_TABLE_STATS";
                    OracleCommand cmd = new OracleCommand(sqlString, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("ownname", owner));
                    cmd.Parameters.Add(new OracleParameter("tabname", tabName));
                    cmd.Parameters.Add(new OracleParameter("stattab", STATTABNAME));
                    cmd.Parameters.Add(new OracleParameter("cascade", Cascade));
                    cmd.Parameters.Add(new OracleParameter("no_invalidate", Invalidate));
                    cmd.Parameters.Add(new OracleParameter("stat_category", "OBJECT_STATS, SYNOPSES"));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException exc) { }
                }
            }
        }


        /// <summary>
        /// dla zewnętrezych wywołań
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="TableName"></param>
        /// <param name="StatTable"></param>
        /// <param name="Cascade"></param>
        /// <param name="Invalidate"></param>
        public void ImportTableStats(string Owner, string TableName, string StatTable, bool Cascade, bool Invalidate)
        {
            if (!IsExportTbleExists())
            {
                foreach (string tabName in tablesNames)
                {
                    string sqlString = "DBMS_STATS.IMPORT_TABLE_STATS";
                    OracleCommand cmd = new OracleCommand(sqlString, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("ownname", Owner));
                    cmd.Parameters.Add(new OracleParameter("tabname", TableName));
                    cmd.Parameters.Add(new OracleParameter("stattab", StatTable));
                    cmd.Parameters.Add(new OracleParameter("cascade", Cascade));
                    cmd.Parameters.Add(new OracleParameter("no_invalidate", Invalidate));
                    cmd.Parameters.Add(new OracleParameter("stat_category", "OBJECT_STATS, SYNOPSES"));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException exc) { }
                }
            }
        }

    }
}
