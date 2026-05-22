using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace ClassSqlId
{
    /// <summary>
    /// Klasy dla Formy sqlId 2023-11
    /// Author artur.balon@asseco.pl
    /// </summary>
    public class ClassSqlId
    { }

    public class TabSqlProfile
    {
        public DataTable TableProfile { get; set; }

        public TabSqlProfile()
        {
            TableProfile = new DataTable
            {
                TableName = "TableProfile"
            };
        }
    }
    public class TabSqlBaseLine
    {
        public DataTable TableBaseLine { get; set; }

        public TabSqlBaseLine()
        {
            TableBaseLine = new DataTable
            {
                TableName = "TableBaseLine"
            };
        }
    }

    /// <summary>
    /// Klasa informacji Sql_id
    /// </summary>
    public class VsqlId
    { 
        OracleConnection conn {  get; set; }
        public string SqlId { get; set; }
        public Int32 Executions { get; set; }
        public string SqlFullText { get; set; }
        public string HashValue { get; set; }
        public string PlanHashValue { get; set; }
        public string SqlProfile { get; set; }    
        public string SqlBaseline { get; set; }
        /// <summary>
        /// Klas informcji o sql_id
        /// </summary>
        /// <param name="Connection">Połączenie do bazy</param>
        /// <param name="Sql_Id">Sql_id</param>
        public VsqlId(OracleConnection Connection, string Sql_Id)
        {
            conn = Connection;
            SqlId = Sql_Id;
        }
        public void GetSqlInfo()
        {
            try
            {
                string sqlString = "select q.sql_fulltext, q.executions, q.hash_value, q.plan_hash_value, q.sql_plan_baseline, q.sql_profile from v$sql q where q.sql_id = :1";
                OracleCommand cmd = new OracleCommand(sqlString, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("sql_id", SqlId));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        SqlFullText = rider.GetString(0);
                        Executions = rider.GetInt32(1);
                        SqlFullText = rider.GetString(3);
                        HashValue = rider.GetString(4);
                        PlanHashValue = rider.GetString(5);
                        SqlProfile = rider.GetString(6);
                        SqlBaseline = rider.GetString(7);
                    }
                }
                else
                {
                    //throw up
                }
                rider.Close();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }

    }
    /// <summary>
    /// Klas infomacji o BaseLines
    /// </summary>
    public class VsqlIdBaseLine
    {
        OracleConnection conn { get; set; }
        public string PlanName { get; set; }
        public Int32 Executions { get; set; }
        public string Creator { get; set; }
        public string Descrition { get; set; }
        public string Enabled { get; set; }
        public string SqlHandle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Connection">Połączenie do bazy</param>
        /// <param name="Sql_Id">Sql_id</param>
        public VsqlIdBaseLine(OracleConnection Connection, string planName)
        {
            conn = Connection;
            PlanName = planName;
        }
        public void GetSqlInfo()
        {
            try
            {
                string sqlString = "select b.plan_name, b.creator, b.description, b.enabled, b.executions, b.sql_handle from dba_sql_plan_baselines b where b.plan_name = :1";
                OracleCommand cmd = new OracleCommand(sqlString, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new OracleParameter("plan_name", PlanName));

                OracleDataReader rider = cmd.ExecuteReader();

                if (rider.HasRows)
                {
                    while (rider.Read())
                    {
                        PlanName = rider.GetString(0);
                        Creator = rider.GetString(1);
                        Descrition = rider.GetString(3);
                        Enabled = rider.GetString(4);
                        Executions = rider.GetInt32(5);
                        SqlHandle = rider.GetString(6);
                    }
                }
                else
                {
                    //throw up
                }
                rider.Close();
            }
            catch (OracleException exc)
            {
                throw exc;
            }
        }

    }

}
