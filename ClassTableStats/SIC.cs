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
            List<string> UserViews = GetUserViews();

            var tabPattern = @"\b(" + string.Join("|", UserTables.Concat(UserTables).Select(Regex.Escape)) + @")\b";
            var viewPattern = @"\b(" + string.Join("|", UserViews.Concat(UserViews).Select(Regex.Escape)) + @")\b";

            var foundTab = Regex.Matches(SqlFullText, tabPattern, RegexOptions.IgnoreCase)
                             .Cast<Match>()
                             .Select(m => m.Value.ToUpper())
                             .Distinct()
                             .ToList();
                             
            var foundView = Regex.Matches(SqlFullText, viewPattern, RegexOptions.IgnoreCase)
                             .Cast<Match>()
                             .Select(m => m.Value.ToUpper())
                             .Distinct()
                             .ToList();                             
            foreach (string tableName in foundTab)
            {
                ListOfTables.Add(tableName);
            }

            foreach (string viewName in foundView)
            {
                List<string> depTab = GetUserViewsDependecies(viewName);
                foreach (string depViewName in depTab)
                {
                    ListOfTables.Add(depViewName);
                }
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

        private List<string> GetUserViews()
        {
            List<string> UserViews = new List<string>();
            try
            {
                string sqlString = "select view_name from dba_views v where v.owner = :1";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("owner", owner));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        UserViews.Add(rider.GetString(0));
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

            return UserViews;
        }

        private List<string> GetUserViewsDependecies(string ViewName)
        {
            List<string> DependTables = new List<string>();
            try
            {
                string sqlString = "select referenced_name from dba_dependencies where owner = :1 and name = :2";
                OracleCommand cmd = new OracleCommand(sqlString, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("owner", owner));
                cmd.Parameters.Add(new OracleParameter("name", ViewName));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        DependTables.Add(rider.GetString(0));
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

            return DependTables;
        }
    }
}
