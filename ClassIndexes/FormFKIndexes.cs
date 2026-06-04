using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using AssecoToolsOptions;

namespace ClassIndexes
{
    public partial class FormFKIndexes : Form
    {
        SessionOptions sessionOptions;
        OracleConnection Connection;
        DataTable tableOfIndexes;
        BindingSource bindingSource;

        /// <summary>
        /// Spradzanie brakujących indeksów na kluczach obcych.
        /// Dane generowane są po stronie bazy danych za pomocą procedury 
        /// Autor: Artur Bałon
        /// Changelog: 
        /// Created 12-2022
        /// </summary>
        /// <param name="connectionString">connectionSrting - otwiera nowe połącznie do bazy</param>
        public FormFKIndexes(OracleConnection Connection, SessionOptions sessionOptions)
        {
            InitializeComponent();
            InitializeTableOfIndexes();
            this.Connection = Connection;
            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                toolStrip1.BackColor = sessionOptions.SessionColor;
            }
        }

        /// <summary>
        /// Inicjalizacja tabeli MISSING_FK_INDEXES_TABLE
        /// </summary>
        private void InitializeTableOfIndexes()
        {
            tableOfIndexes = new DataTable();
            tableOfIndexes.TableName = "MISSING_FK_INDEXES_TABLE";

            tableOfIndexes.Columns.Add("TABLE_NAME");
            tableOfIndexes.Columns.Add("CONSTRAINT_NAME");
            tableOfIndexes.Columns.Add("STATUS");
            tableOfIndexes.Columns.Add("FOREIGN_KEY");
            tableOfIndexes.Columns.Add("INFO");
            bindingSource = new BindingSource();
            bindingSource.DataSource = tableOfIndexes;

            Binding b = new Binding("Text", bindingSource, "INFO");
            richTextBox1.DataBindings.Add(b);
        }

        private void FormFKIndexes_Load(object sender, EventArgs e)
        {
            InitializeEviroment();
        }

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string schemaName = tsSchemaComboBox.Text;

            ShowRefresch(schemaName);
        }

        /// <summary>
        /// Głowna funcja do odświeżenia danych od strony serwera i klienta
        /// </summary>
        /// <param name="schemaName"></param>
        private void ShowRefresch(string schemaName)
        {
            if (schemaName.Trim().Length > 0)
            {
                toolStripLabelWait.Visible = true;
                toolStrip1.Refresh();
                RefreshDataProcedure(schemaName);
                RefreshDataFromTable(schemaName);
                toolStripLabelWait.Visible = false;
            }
            else
            {
                MessageBox.Show("Wybierz schemat dla którego będą wyświetlone informacje o brakujących indeksach");
            }
        }

        /// <summary>
        /// Procedura składowana po stronie serwera - generuje dane do tabeli MISSING_FK_INDEXES_TABLE
        /// </summary>
        /// <param name="schemaName"></param>
        private void RefreshDataProcedure(string schemaName)
        {
            string sqlString = "MISSING_FK_INDEXES";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = sqlString;
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("p_schema", schemaName));
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Pobiera dane z tabeli MISSING_FK_INDEXES_TABLE i wyświetla na formatce
        /// </summary>
        /// <param name="schemaName"></param>
        private void RefreshDataFromTable(string schemaName)
        {

            string sqlString = "select t.table_name, t.constraint_name, t.status, t.foreign_key, t.info\n" +
            "  from MISSING_FK_INDEXES_TABLE t\n" +
            " where t.owner = :owner\n" +
            " order by table_name";


            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = sqlString;

            cmd.Parameters.Add("owner", schemaName);


            OracleDataAdapter ada = new OracleDataAdapter(cmd);

            tableOfIndexes.Rows.Clear();

            try
            {
                ada.Fill(tableOfIndexes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridViewSchema.DataSource = bindingSource;
            dataGridViewSchema.Columns[0].Width = 200;
            dataGridViewSchema.Columns[1].Width = 200;
            dataGridViewSchema.Columns[2].Width = 100;
            dataGridViewSchema.Columns[3].Width = 200;
            dataGridViewSchema.Columns[4].Width = 600;
        }

        private void FormFKIndexes_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Close();
        }

        /// <summary>
        /// Uniwersalne filtrowanie wierszy po dwóch kolumnach 
        /// </summary>
        /// <param name="schemaName"></param>
        private void Filter()
        {
            string filter = toolStripTextBox1.Text;
            if (filter.Trim().Length > 0)
            {
                string filterExpression = " table_name like '%" + filter + "%' or info like '%" + filter + "%' ";
                tableOfIndexes.DefaultView.RowFilter = filterExpression;
            }
            else
            {
                tableOfIndexes.DefaultView.RowFilter = "";
            }
        }

        private void FilterOld(string schemaName)
        {
            string filter = toolStripTextBox1.Text;
            string sqlString = "";

            if (filter.Trim().Length > 0)
            {
                sqlString = "select t.table_name, t.constraint_name, t.status, t.foreign_key, t.info\n" +
                            "  from MISSING_FK_INDEXES_TABLE t\n" +
                            " where t.owner = :owner and t.table_name like '%" + filter + "%' or t.info like '%" + filter + "%' \n" +
                            " order by table_name";
            }
            else
            {
                sqlString = "select t.table_name, t.constraint_name, t.status, t.foreign_key, t.info\n" +
                            "  from MISSING_FK_INDEXES_TABLE t\n" +
                            " where t.owner = :owner\n" +
                            " order by table_name";
            }

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Connection;
            cmd.CommandText = sqlString;
            cmd.Parameters.Add("owner", schemaName);


            OracleDataAdapter ada = new OracleDataAdapter(cmd);

            tableOfIndexes.Rows.Clear();

            try
            {
                ada.Fill(tableOfIndexes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
