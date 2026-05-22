using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClassViewWindow
{
    /// <summary>
    /// Obsługa logowania historycznycj planów zapytań
    /// Autor:  artur.balon@asseco.pl
    /// Date:   03-2026
    /// </summary>
    internal class StatsSimpleNote
    {
        string defautSimpleNotePath = Application.StartupPath + "\\AssecoToolsSimpleNote.xml";
        DataTable defaultSimpleNoteTable;
        private string databaseName;
        private DataRowView sessionInfo;

        public StatsSimpleNote(string Database) 
        {
            databaseName = Database;
            defaultSimpleNoteTable = new DataTable();
            if (checkSimpleNoteFile())
                defaultSimpleNoteTable.ReadXml(defautSimpleNotePath);
            else 
            {
                CreateSimpleNote();
            }
        }

        public StatsSimpleNote(string Database, DataRowView SessionInfo)
        {
            databaseName = Database;
            defaultSimpleNoteTable = new DataTable();
            sessionInfo = SessionInfo;
            if (checkSimpleNoteFile())
                defaultSimpleNoteTable.ReadXml(defautSimpleNotePath);
            else
            {
                CreateSimpleNote();
            }
        }

        private void CreateSimpleNote()
        {
            try
            {
                //defaultSimpleNoteTable = new DataTable();
                defaultSimpleNoteTable.TableName = "SimpleNoteTable";
                DataColumn dc1 = new DataColumn();
                dc1.ColumnName = "Date";
                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "Database";
                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "Info";
                defaultSimpleNoteTable.Columns.Add(dc1);
                defaultSimpleNoteTable.Columns.Add(dc2);
                defaultSimpleNoteTable.Columns.Add(dc3);
                defaultSimpleNoteTable.AcceptChanges();

                defaultSimpleNoteTable.WriteXml(defautSimpleNotePath, XmlWriteMode.WriteSchema,true);
            }
            catch (Exception ex) { }
        }
        public void SaveNote(string Note)
        {
            DataRow dr = defaultSimpleNoteTable.NewRow();
            dr["Date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dr["Database"] = databaseName;
            dr["Info"] = Note;
            defaultSimpleNoteTable.Rows.Add(dr);
            defaultSimpleNoteTable.WriteXml(defautSimpleNotePath,XmlWriteMode.WriteSchema);
            
        }
        private bool checkSimpleNoteFile()
        {
            if (!File.Exists(defautSimpleNotePath)) 
            {
                return false;
            }
            return true;
        }
        private void ReloadSimpleNoteFile()
        {
            try
            {
                defaultSimpleNoteTable.Rows.Clear();
                defaultSimpleNoteTable.ReadXml(defautSimpleNotePath);
            }
            catch { }   
        }

        public void ClearSimpleNotes()
        {
            try
            {
                defaultSimpleNoteTable.Rows.Clear();
                defaultSimpleNoteTable.WriteXml(defautSimpleNotePath, XmlWriteMode.WriteSchema, true);
            }
            catch { }
        }

        public DataTable GetSimpleNotes()
        {
            ReloadSimpleNoteFile();
            return defaultSimpleNoteTable;
        }
    }
}
