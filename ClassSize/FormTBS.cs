using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassSize
{
    public partial class FormTBS : Form
    {
        OracleConnection Connection;

        /// <summary>
        /// Author:         artur.balon@asseco.pl
        /// Cescription:    Forma przestrzeni
        /// ChangeLog:      XXXXX1: Dodane Info i dodawanie plikow do przestrzeni
        ///                 Nie brac tego 
        /// </summary>
        /// <param name="Connection"></param>
        /// 
        public FormTBS(OracleConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
        }

        private void FormTBS_Load(object sender, EventArgs e)
        {
            OracleCommand cmdFillTbs = new OracleCommand("select t.TABLESPACE_NAME, t.BIGFILE from dba_tablespaces t", Connection);
            OracleDataAdapter adaFillTbs = new OracleDataAdapter(cmdFillTbs);
            DataTable tabTablespaces = new DataTable();
            adaFillTbs.Fill(tabTablespaces);

            bindingSourceTablespaces.DataSource = tabTablespaces;
            dataGridView1.DataSource = bindingSourceTablespaces;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["TABLESPACE_NAME"].Index)
            {
                String ts = (String)dataGridView1.Rows[e.RowIndex].Cells["TABLESPACE_NAME"].Value;


                OracleCommand cmdFillDataFiles = new OracleCommand("select t.FILE_NAME, t.BYTES from dba_data_files t where TABLESPACE_NAME=:TABLESPACE_NAME", Connection);
                cmdFillDataFiles.Parameters.Add(new OracleParameter("TABLESPACE_NAME", ts));
                OracleDataAdapter adaFillDataFiles = new OracleDataAdapter(cmdFillDataFiles);
                DataTable tabDatafiles = new DataTable();
                adaFillDataFiles.Fill(tabDatafiles);

                bindingSourceDataFiles.DataSource = tabDatafiles;
                dataGridView2.DataSource = bindingSourceDataFiles;
                dataGridView2.Columns[1].Width = 120;
                dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            DataRowView dv_tbs = (DataRowView)bindingSourceTablespaces.Current;
            String tablespace = dv_tbs["TABLESPACE_NAME"].ToString();

            richTextBox1.Text = "DROP TABLESPACE " + tablespace + " INCLUDING CONTENTS AND DATAFILES";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataRowView dv_tbs = (DataRowView)bindingSourceTablespaces.Current;
            DataRowView dv_file = (DataRowView)bindingSourceDataFiles.Current;
            String tablespace = dv_tbs["TABLESPACE_NAME"].ToString();
            String bigfile = dv_tbs["BIGFILE"].ToString();

            String datafile = dv_file["FILE_NAME"].ToString();
            if (bigfile == "YES")
            {
                richTextBox1.Text = "Cannot add file to bigfile tablespace";
                return;
            }

            String Dir = System.IO.Path.GetDirectoryName(datafile);
            String Fil = System.IO.Path.GetFileNameWithoutExtension(datafile);
            String Ext = System.IO.Path.GetExtension(datafile);

            string filnam = System.Text.RegularExpressions.Regex.Replace(Fil, @"\d", "");
            string filnum = System.Text.RegularExpressions.Regex.Replace(Fil, @"\D", "");

            if (filnum == "")
            {
                filnum = "_01";
            }
            else
            {
                try
                {
                    int n = Convert.ToInt16(filnum);
                    n++;
                    filnum = n.ToString("D2");

                }
                catch { }
            }


            richTextBox1.Text = "ALTER TABLESPACE " + tablespace + " ADD DATAFILE '" + Dir.Replace('\\', '/') + "/" + filnam + filnum + Ext + "' SIZE 10M AUTOEXTEND ON";

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRowView dv_file = (DataRowView)bindingSourceDataFiles.Current;
            String datafile = dv_file["FILE_NAME"].ToString();

            richTextBox1.Text = "ALTER DATABASE DATAFILE '" + datafile.Replace('\\', '/') + "' RESIZE 10M";

        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            DataRowView dv_tbs = (DataRowView)bindingSourceTablespaces.Current;
            String tablespace = dv_tbs["TABLESPACE_NAME"].ToString();

            DataRowView dv_file = (DataRowView)bindingSourceDataFiles.Current;
            String datafile = dv_file["FILE_NAME"].ToString();

            richTextBox1.Text = "ALTER TABLESPACE " + tablespace + " DROP DATAFILE '" + datafile.Replace('\\', '/') + "'";
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(richTextBox1.Text, Connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataRowView dv_tbs = (DataRowView)bindingSourceTablespaces.Current;
            String tablespace = dv_tbs["TABLESPACE_NAME"].ToString();

            DataRowView dv_file = (DataRowView)bindingSourceDataFiles.Current;
            String datafile = dv_file["FILE_NAME"].ToString();

            richTextBox1.Text = "ALTER TABLESPACE " + tablespace + " DROP DATAFILE '" + datafile.Replace('\\', '/') + "'";
        }
    }
}
