using AssecoToolsOptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSchemaStats
{
    /// <summary>
    /// Author      : artur.balon@asseco.pl   
    /// Date        : 06-2026
    /// Descrition  : Modul do obslugi statystyka na tablicach tymczasowych
    /// </summary>
    public class GTT
    {
        private OracleConnection connection;
        private string owner;
        private string tempTable;

        public GTT(OracleConnection Connection)
        {
            connection = Connection;
        }
        public GTT(OracleConnection Connection, string Owner, string TemporaryTableName)
        {
            connection = Connection;
            tempTable = TemporaryTableName;
            owner = Owner;
        }

        /// <summary>
        /// Sprawdzenie poziomu zbierania statystyk
        /// </summary>
        /// <returns>Return scope: SESSION or SHARED </returns>
        public string CheckGttScope()
        {
            string tabLevel = string.Empty;
            string query = "SELECT DBMS_STATS.GET_PREFS('GLOBAL_TEMP_TABLE_STATS', '" + owner + "','" + tempTable + "') AS gtt_stats_scope FROM dual";
            OracleCommand cmd = new OracleCommand(query, connection);

                tabLevel = cmd.ExecuteScalar().ToString();
          
            return tabLevel;
        }

        public bool SetGttLevel(string GttLevel)
        {

            OracleCommand cmdSetPrefs = new OracleCommand("DBMS_STATS.SET_TABLE_PREFS", connection);
            cmdSetPrefs.CommandType = CommandType.StoredProcedure;
            cmdSetPrefs.BindByName = true;
            cmdSetPrefs.Parameters.Add("ownname", OracleDbType.Varchar2).Value = owner;
            cmdSetPrefs.Parameters.Add("tabname", OracleDbType.Varchar2).Value = tempTable;
            cmdSetPrefs.Parameters.Add("pname", OracleDbType.Varchar2).Value = "GLOBAL_TEMP_TABLE_STATS";
            cmdSetPrefs.Parameters.Add("pvalue", OracleDbType.Varchar2).Value = GttLevel;

            try
            {
                cmdSetPrefs.ExecuteNonQuery();
                return true;
            }
            catch (OracleException exc)
            {
                throw exc;
                
            }
        }

        /// <summary>
        /// Kasuje statystyki z tabeli tymczasowej tylko wtedy gdy są ustawione na SHARED
        /// </summary>
        /// <returns></returns>
        public void DeleteTableSharedStats()
        {
            if (needDeleteSharedStats()) 
            {
                SetGttLevel("SHARED");
                OracleCommand cmdSetPrefs = new OracleCommand("DBMS_STATS.DELETE_TABLE_STATS", connection);
                cmdSetPrefs.CommandType = CommandType.StoredProcedure;
                cmdSetPrefs.BindByName = true;
                cmdSetPrefs.Parameters.Add("ownname", OracleDbType.Varchar2).Value = owner;
                cmdSetPrefs.Parameters.Add("tabname", OracleDbType.Varchar2).Value = tempTable;

                try
                {
                    cmdSetPrefs.ExecuteNonQuery();
                    SetGttLevel("SESSION");
                    //return true;
                }
                catch (OracleException exc)
                {
                    throw exc;
                }
            }
            else { }  
        }

        public void DeleteTableStats()
        {

            OracleCommand cmdSetPrefs = new OracleCommand("DBMS_STATS.DELETE_TABLE_STATS", connection);
            cmdSetPrefs.CommandType = CommandType.StoredProcedure;
            cmdSetPrefs.BindByName = true;
            cmdSetPrefs.Parameters.Add("ownname", OracleDbType.Varchar2).Value = owner;
            cmdSetPrefs.Parameters.Add("tabname", OracleDbType.Varchar2).Value = tempTable;

            try
            {
                cmdSetPrefs.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
        /// <summary>
        /// sprawdź zakresy tabel tymczasowych typu SHARED wraz z przeanalizowanymi statystykami
        /// </summary>
        /// <returns></returns>
        public bool needDeleteSharedStats()
        {
            bool needDelete = false;
            string query = "select scope, last_analyzed from dba_tab_statistics where owner = :ownname and table_name = :tabname ";
            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.Parameters.Add("ownname", OracleDbType.Varchar2).Value = owner;
            cmd.Parameters.Add("tabname", OracleDbType.Varchar2).Value = tempTable;

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) == "SHARED" && !reader.IsDBNull(1) == true)
                        needDelete = true;
                }
            }

            return needDelete;
        }

        /// <summary>
        /// Zrob statystyki na tabeli tymczasowej - tylko Auto
        /// </summary>
        /// <returns></returns>
        public void GatherTableStats()
        {
            if (CheckGttScope() == "SESSION")
            {
                
            }
            OracleCommand cmdGather = new OracleCommand("DBMS_STATS.GATHER_TABLE_STATS", connection);
            cmdGather.CommandType = CommandType.StoredProcedure;
            cmdGather.BindByName = true;
            cmdGather.Parameters.Add("ownname", OracleDbType.Varchar2).Value = owner;
            cmdGather.Parameters.Add("tabname", OracleDbType.Varchar2).Value = tempTable;

            try
            {
                cmdGather.ExecuteNonQuery();
                //return "Stats done.";
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }
    }
}
