using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ClassWaiters
{
    public partial class FormHolds
    {
        /// <summary>
        /// Prz\ygotowanie danch dla formatki - schematy/uyżytkownicy 
        /// </summary>
        private void InitializeEviroment()
        {
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = Connection;

            string sqlString = "select username\n" +
            "from all_users\n" +
            "where username not in ('SYS','SYSTEM','OUTLN','SYSMAN','XDB','XS$NULL','SNOWINV','ORDSYS','ORDPLUGINS','FLOWS_FILES','ORDDATA','OLAPSYS','ORACLE_OCM','MGMT_VIEW','MDSYS','MDDATA','EXFSYS','DBSNMP','CTXSYS','DIP','APPQOSSYS', 'SCOTT', 'WMSYS')\n" +
            "      and username not like 'APEX%'\n" +
            "order by username";

            oracleCommand.CommandText = sqlString;
            List<string> SchemasList = new List<string>();

            OracleDataReader SchemaReader = oracleCommand.ExecuteReader();
            while (SchemaReader.Read())
            {
                SchemasList.Add(SchemaReader.GetValue(0).ToString().ToUpper());
            }
            tsSchemaComboBox.Items.Clear();
            tsSchemaComboBox.Items.AddRange(SchemasList.ToArray());
        }
    }
}
