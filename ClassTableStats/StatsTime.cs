using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSchemaStats
{
    /// <summary>
    /// Moduł gromadzący czasy wykonań statstyk
    /// Autor:  artur.balon@asseco.pl
    /// Date:   04-2026
    /// </summary>
    internal class StatsTime
    {
        static string defautStatsTimePath = Application.StartupPath + "\\AssecoToolsStatsTime.xml";
        DataTable tableTimeStats;
        string database;
        string schema;
        string tableName;
        string command;
        string timeSec;

        public StatsTime(string Database, string Schema, string TableNmae, string Command, string TimeSec)
        {
            InitTimeStatsTable();
            database = Database;
            schema = Schema;
            tableName = TableNmae;
            command = Command;
            timeSec = TimeSec;
        }

        void InitTimeStatsTable()
        {
            tableTimeStats = new DataTable();
            tableTimeStats.TableName = "tableTimeStats";
            //tableTimeStats.Columns.Add("Id", typeof(int));
            tableTimeStats.Columns.Add("Date", typeof(DateTime));
            tableTimeStats.Columns.Add("Database", typeof(string));
            tableTimeStats.Columns.Add("Schema", typeof(string));
            tableTimeStats.Columns.Add("TableName", typeof(string));
            tableTimeStats.Columns.Add("Command", typeof(string));
            tableTimeStats.Columns.Add("TimeSec", typeof(decimal));
            try
            {
                tableTimeStats.ReadXml(defautStatsTimePath);
            }
            catch { }

        }
        public void Save()
        {
            DataRow dr = tableTimeStats.NewRow();
            dr["Date"] = DateTime.Now;
            dr["Database"] = database;
            dr["Schema"] = schema;
            dr["TableName"] = tableName;
            dr["Command"] = command;
            dr["TimeSec"] = timeSec;

            try
            {
                tableTimeStats.Rows.Add(dr);
                tableTimeStats.WriteXml(defautStatsTimePath, XmlWriteMode.WriteSchema);
            }
            catch { }

        }
        private bool checkFile()
        {
            if (!File.Exists(defautStatsTimePath))
            {
                return false;
            }
            return true;
        }
        private void ReloadFile()
        {
            try
            {
                tableTimeStats.Rows.Clear();
                tableTimeStats.ReadXml(defautStatsTimePath);
            }
            catch { }
        }

        public void ClearFile()
        {
            try
            {
                tableTimeStats.Rows.Clear();
                tableTimeStats.WriteXml(defautStatsTimePath, XmlWriteMode.WriteSchema, true);
            }
            catch { }
        }

        public DataTable GetSimpleNotes()
        {
            ReloadFile();
            return tableTimeStats;
        }

        /// <summary>
        /// Inofo o czasie statystyk dla tabeli - tylko pobranie z pliku
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="Schema"></param>
        /// <param name="TableNmae"></param>
        public static DataTable GetInfo(string Database, string Schema, string TableName)
        {
            DataTable temptableTimeStats = new DataTable();
            try
            {
                temptableTimeStats.ReadXml(defautStatsTimePath);

                string filter = "Database='" + Database + "' and " + " Schema='" + Schema + "' and " + " TableName='" + TableName + "'";
                DataView dv = new DataView(temptableTimeStats, filter, "Date asc", DataViewRowState.CurrentRows);
                return dv.ToTable(false, new string[] { "Date", "TimeSec", "Command" });
            }
            catch
            {
                //MessageBox.Show("No timestats file");
            }

            return null;
        }

        /// <summary>
        /// Inofo o czasie statystyk dla wszystkich tabel ze schematu - tylko pobranie z pliku
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="Schema"></param>
        /// <returns></returns>
        public static DataTable GetInfoAll(string Database, string Schema)
        {
            DataTable temptableTimeStats = new DataTable();
            try
            {
                temptableTimeStats.ReadXml(defautStatsTimePath);
            }
            catch { }

          ; string filter = "Database='" + Database + "' and " + " Schema='" + Schema + "'";

            DataView dv = new DataView(temptableTimeStats, filter, "Date asc", DataViewRowState.CurrentRows);

            return dv.ToTable(false, new string[] { "Date", "TableName", "TimeSec", "Command" });
        }
    }
}
