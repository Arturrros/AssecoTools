using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassSchemaStats
{
    /// <summary>
    /// Author      : artur.balon@asseco.pl   
    /// Date        : 07-2026
    /// Descrition  : Modul do wyszukiwania tabel z polecen
    /// </summary>
    public class SIC
    {
        private OracleConnection connection;
        private string owner;
        private string tempTable;
        private string sql_id;

        public SIC(OracleConnection Connection, string SqlId, string Owner)
        {
            this.connection = Connection;
            owner = Owner;
            sql_id = SqlId;
        }

        public List<string> FindTablesInSql()
        {
            List<string> ListOfTables = new List<string>();

            string SqlFullText = GetSqlTextFromSqlId();
            List<string> UserTables = GetUserTables();


            var pattern = @"\b(" + string.Join("|", UserTables.Select(Regex.Escape)) + @")\b";

            var found = Regex.Matches(SqlFullText, pattern, RegexOptions.IgnoreCase)
                             .Cast<Match>()
                             .Select(m => m.Value.ToUpper())
                             .Distinct()
                             .ToList();
            foreach (string str in found)
            {
                ListOfTables.Add(str);
            }

            return ListOfTables;
        }
        private string GetSqlTextFromSqlId()
        {
            string SqlFullText = string.Empty;
            try
            {
                string sqlString = "select q.sql_fulltext from v$sql q where q.sql_id = :1";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("sql_id", sql_id));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        SqlFullText = rider.GetString(0).ToString();
                    }
                }
                else
                {
                }
                rider.Close();
            }
            catch (OracleException exc)
            {
                throw exc;
            }

            return SqlFullText.ToUpper();
        }
        private List<string> GetUserTables()
        {
            List<string> UserTables = new List<string>();
            try
            {
                string sqlString = "select table_name from dba_tables t where t.owner = :1";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("owner", owner));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        UserTables.Add(rider.GetString(0));
                    }
                }
                else
                {}
                rider.Close();
            }
            catch (OracleException exc)
            {
                throw exc;
            }

            return UserTables;
        }
    }
}
