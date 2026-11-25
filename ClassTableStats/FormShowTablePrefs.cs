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
using AssecoToolsOptions;

namespace ClassSchemaStats
{
    /// <summary>
    /// Wyświetlenie informacji o ustawionych preferencjach statystyk dla wszystkich tabel - na poziomie tabel
    /// Data:   12-2025
    /// Autor:  artur.balon@asseco.pl
    /// </summary>
    public partial class FormShowTablePrefs : Form
    {
        SessionOptions sessionOptions;
        OracleConnection conn;
        string owner;
        BindingSource bsPerfs;
        DataTable dtPrefs = new DataTable();

        public FormShowTablePrefs(OracleConnection Connection, string Owner, SessionOptions sessionOptions)
        {
            InitializeComponent();
            conn = Connection;
            owner = Owner;
            bsPerfs = new BindingSource();
            this.sessionOptions = sessionOptions;

            this.sessionOptions = sessionOptions;
            if (sessionOptions.isActiveSessionColor)
            {
                panel1.BackColor = sessionOptions.SessionColor;
            }

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

            string filtr = string.Empty;
            string filtr1 = string.Empty;
            string filtr2 = string.Empty;



            if (textBox1.Text.Trim().Length > 0)
            {
                filtr1 += "table_name like '%" + textBox1.Text + "%'";
            }
            else
            {
                filtr1 = "";
            }

            if (textBox2.Text.Trim().Length > 0)
            {
                filtr2 += "preference_name like '%" + textBox2.Text + "%'";
            }
            else
            {
                filtr2 = "";
            }


            if (string.IsNullOrEmpty(filtr1) && string.IsNullOrEmpty(filtr2))
            {
                filtr = string.Empty;
            }
            else if (!string.IsNullOrEmpty(filtr1) && string.IsNullOrEmpty(filtr2))
            {
                filtr = filtr1;
            }
            else if (string.IsNullOrEmpty(filtr1) && !string.IsNullOrEmpty(filtr2))
            {
                filtr = filtr2;
            }
            else if (!string.IsNullOrEmpty(filtr1) && !string.IsNullOrEmpty(filtr2))
            {
                filtr = filtr1 + " and " + filtr2;
            }

            //filtr = filtr1 +  (string.IsNullOrEmpty(filtr2) ? "" : " and ") + filtr2;

            try
            {

                if (filtr.Trim().Length > 0)
                {
                    dtPrefs.DefaultView.RowFilter = filtr;
                }
                else
                {
                    dtPrefs.DefaultView.RowFilter = "";
                }
            }
            catch 
            {
                MessageBox.Show("Filtr:" + filtr);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtruj();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Filtruj();
        }
    }
}
