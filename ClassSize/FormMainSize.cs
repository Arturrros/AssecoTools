using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace ClassSize
{
    public partial class FormMainSize : Form
    {
        OracleConnection Connection;

        public FormMainSize(OracleConnection Connection)
        {
            
            InitializeComponent();
            this.Connection = Connection;
            bindingSourceSchemas.DataSource = ClassSize.GetRowsCntSchemas(Connection);

            listBox1.DataSource = bindingSourceSchemas;
            listBox1.DisplayMember = "SCHEMA_NAME";
            listBox1.ValueMember = "ID";

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
            if (listBox1.SelectedItem != null)
            {
                textBox2.Text = string.Empty;
                Int32 id = Convert.ToInt32(((DataRowView)listBox1.SelectedItem)["ID"]);
                groupBox1.Text = String.Format("Schema ( id - {0} )", id);
                ReloadDates(id);

                DataTable tCahart = new DataTable();
                OracleCommand cmdChart1 = new OracleCommand(SQLStrings.GET_CHART_SCHEMA, Connection);
                cmdChart1.Parameters.Add("id", id);
                //OracleDataAdapter adapter = new OracleDataAdapter(cmdChart1);
                //adapter.Fill(tCahart);

                //chartMain.Series.Clear();
                OracleDataReader reader = cmdChart1.ExecuteReader();

                //chartMain.DataSource = reader;
                //chartMain.DataBindTable(reader,"DT");
                //chartMain.Series.Add("Series1");
                //chartMain.Series["Series1"].XValueMember = "DT";
                //chartMain.Series["Series1"].YValueMembers = "MB_SIZE";

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
