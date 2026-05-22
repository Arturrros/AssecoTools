using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSchemaStats
{
    /// <summary>
    /// Wyświetlenie informacji o ustawionych preferencjach statystyk dla wszystkich tabel - na poziomie tabel
    /// Data:   12-2025
    /// Autor:  artur.balon@asseco.pl
    /// </summary>
    public partial class FormShowTablePrefs : Form
    {
        OracleConnection conn;
        string owner;
        BindingSource bsPerfs;
        DataTable dtPrefs = new DataTable();

        public FormShowTablePrefs(OracleConnection Connection, string Owner)
        {
            InitializeComponent();
            conn = Connection;
            owner = Owner;
            bsPerfs = new BindingSource();
        }

        private void FormShowTablePrefs_Load(object sender, EventArgs e)
        {
            
            dtPrefs.TableName = "TablePrefs";

            string sqlString = "SELECT table_name, preference_name, preference_value\n" +
                                "FROM dba_tab_stat_prefs\n" +
                                "WHERE owner = :owner\n" +
                                "ORDER BY 1, 2";
            
            OracleCommand cmdPrefs = new OracleCommand(sqlString, conn);
            cmdPrefs.Parameters.Add("owner", owner);

            OracleDataAdapter adaPrefs = new OracleDataAdapter(cmdPrefs);
            try
            {
                adaPrefs.Fill(dtPrefs);
            }
            catch (OracleException exc)
            {
                MessageBox.Show("Błąd pobrania danych o ustawionych preferencjach tabel");
            }
            bsPerfs.DataSource = dtPrefs;

            dataGridView1.DataSource = bsPerfs;
        }

        private void Filtruj()
        {
            string filtr = textBox1.Text;
            if (filtr.Trim().Length > 0)
            {
                dtPrefs.DefaultView.RowFilter = "table_name like '%" + filtr + "%'";
            }
            else
            {
                dtPrefs.DefaultView.RowFilter = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtruj();
        }
    }
}
