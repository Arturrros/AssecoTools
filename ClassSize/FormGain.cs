using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace ClassSize
{
    public partial class FormGain : Form
    {
        OracleConnection Connection;

        /// <summary>
        /// Author:         artur.balon@asseco.pl
        /// Cescription:    Forma do rozmiarow  
        /// ChangeLog:      2023-04-21: Dodane Rozmiary dla Tabel
        /// </summary>
        /// <param name="Connection"></param>
        public FormGain(OracleConnection Connection)
        {
            
            InitializeComponent();
            this.Connection = Connection;
            bindingSourceSchemas.DataSource = ClassSize.GetRowsCntSchemas(Connection);

            listBoxSchemas.DataSource = bindingSourceSchemas;
            listBoxSchemas.DisplayMember = "SCHEMA_NAME";
            listBoxSchemas.ValueMember = "ID";

            comboBoxDateFrom.DataSource = bindingSourceSnap;
            comboBoxDateFrom.DisplayMember = "DT";
            comboBoxDateFrom.ValueMember = "ID";
            //textBox1.DataBindings.Add("Text", bindingSourceSnap, "ID");
        }

        private void FormMainSize_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSchemas.SelectedItem != null)
            {
                textBox2.Text = string.Empty;
                Int32 id = Convert.ToInt32(((DataRowView)listBoxSchemas.SelectedItem)["ID"]);
                groupBox1.Text = String.Format("Schema ( id - {0} )", id);
                ReloadDates(id);

                DataTable tCahart = new DataTable();
                OracleCommand cmdChart1 = new OracleCommand(SQLStrings.GET_CHART_SCHEMA, Connection);
                cmdChart1.Parameters.Add("id", id);
                OracleDataReader reader = cmdChart1.ExecuteReader();
            }
        }

        private void ReloadDates(int idSchema)
        {
            bindingSourceSnap.DataSource = ClassSize.GetRowsCntDatesForSchema(Connection, idSchema);
        }

        private void comboBoxDateFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDateFrom.SelectedItem != null)
            {
                Int32 id = Convert.ToInt32(((DataRowView)comboBoxDateFrom.SelectedItem)["ID"]);
                groupBox3.Text = String.Format("Snaps ( id_snap - {0} )", id.ToString()); 
                textBox2.Text = ClassSize.GetSnapSize(Connection, id).ToString();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                Int64 size = Convert.ToInt64(textBox2.Text);
                decimal dec;

                if (size > 1099511627776)
                {
                    dec = size / 1024M / 1024M / 1024M / 1024M;
                    label3.Text = String.Format("{0:0.00} TB", (dec));
                    return;

                }
                if (size > 1073741824)
                {
                    dec = size / 1024M / 1024M / 1024M;
                    label3.Text = String.Format("{0:0.00} GB", (dec));
                    return;

                }
                if (size > 1048576)
                {
                    dec = size / 1024M / 1024M;
                    label3.Text = String.Format("{0:0.00} MB", (dec));
                    return;

                }
                if (size > 1024)
                {
                    dec = size / 1024M;
                    label3.Text = String.Format("{0:0.00} KB", (dec));
                    return;
                }
            }
        }

        private void comboBoxDateFrom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
