using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using Oracle.ManagedDataAccess.Client;

namespace ClassAwr
{
    public partial class FormAwr : Form
    {
        OracleConnection Connection;
        DataTable tableOfSnapsBegin;
        DataTable tableOfSnapsEnd;

        BindingSource bindingSourceSnapBegin;
        BindingSource bindingSourceSnapEnd;

        BackgroundWorker worker;
        OracleCommand cmdGen;
        Oracle.ManagedDataAccess.Types.OracleClob awrClob;

        /// <summary>
        /// Generacja Awr-a
        /// Autor: Artur Bałon
        /// Changelog: 
        /// Created 05-2023
        /// </summary>
        /// <param name="connectionString">connectionSrting - otwiera nowe połącznie do bazy</param>
        public FormAwr(OracleConnection Connection)
        {
            InitializeComponent();
            InitializeTableOfIndexes();

            this.Connection = Connection;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            InitializeEviroment();
        }

        /// <summary>
        /// Inicjalizacja tabel snap
        /// </summary>
        private void InitializeTableOfIndexes()
        {
            tableOfSnapsBegin = new DataTable();
            tableOfSnapsBegin.TableName = "SNAP";

            tableOfSnapsBegin.Columns.Add("SNAP_ID");
            tableOfSnapsBegin.Columns.Add("SNAP_DATE");
            tableOfSnapsBegin.Columns.Add("SNAP_ALL");

            tableOfSnapsEnd = tableOfSnapsBegin.Copy();

            bindingSourceSnapBegin = new BindingSource();
            bindingSourceSnapBegin.DataSource = tableOfSnapsBegin;

            bindingSourceSnapEnd = new BindingSource();
            bindingSourceSnapEnd.DataSource = tableOfSnapsEnd;

        }

        /// <summary>
        /// Inicjalizacja SNAP_ID i dat migawek
        /// </summary>
        private void InitializeEviroment()
        {
            OracleCommand oracleCommand = new OracleCommand();
            oracleCommand.Connection = Connection;
            DataTable dt = new DataTable();


            string sqlString = "select SNAP_ID,\n" +
            "       to_char(BEGIN_INTERVAL_TIME, 'YYYY-MM-DD HH24:MI:SS') as SNAP_DATE,\n" +
            "       to_char(END_INTERVAL_TIME, 'YYYY-MM-DD HH24:MI:SS') || ' - ' || SNAP_ID as SNAP_ALL\n" +
            "  from dba_hist_snapshot h\n" +
            " where h.begin_interval_time > sysdate - 14\n" +
            " order by h.begin_interval_time desc";

            oracleCommand.CommandText = sqlString;
           
            OracleDataAdapter ada = new OracleDataAdapter(oracleCommand);
            ada.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                DataRow dr1 = tableOfSnapsBegin.NewRow();
                dr1 = dr;
                tableOfSnapsBegin.Rows.Add(dr1.ItemArray);

                DataRow dr2 = tableOfSnapsBegin.NewRow();
                dr2 = dr;
                tableOfSnapsEnd.Rows.Add(dr2.ItemArray);
            }

            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.ComboBox.DataSource = bindingSourceSnapBegin;
            toolStripComboBox1.ComboBox.DisplayMember = "SNAP_ALL";

            toolStripComboBox2.Items.Clear();
            toolStripComboBox2.ComboBox.DataSource = bindingSourceSnapEnd;
            toolStripComboBox2.ComboBox.DisplayMember = "SNAP_ALL";

        }

        private void FormAwr_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worker.IsBusy)
            {
                CancelAwr();
            }
            Connection.Close();
        }

        private void toolStripButtonGenerateAwr_Click_1(object sender, EventArgs e)
        {
            if (worker.IsBusy != true)
            {
                worker.RunWorkerAsync();
            }
            toolStripButton1.Enabled = false;
            toolStripButton3.Enabled = true;
            toolStripButton3.BackColor = System.Drawing.Color.Red;
        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, webBrowser1.Document.Body.Parent.OuterHtml, System.Text.Encoding.GetEncoding(webBrowser1.Document.Encoding));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void toolStripButtonCancelAwr_Click(object sender, EventArgs e)
        {
            CancelAwr();
        }

        /// <summary>
        /// Anulowanie wykonania Awr
        /// </summary>
        private void CancelAwr()
        {
            worker.CancelAsync();
            cmdGen.Cancel();
            toolStripButton1.Enabled = true;
            toolStripButton3.Enabled = false;
            toolStripButton3.BackColor = System.Drawing.SystemColors.Control;
        }

        /// <summary>
        /// Glowna funkcja generujaca raport
        /// do poprawnego działania potrzebna jest funkcja po stronie serwera
        /// </summary>
        private void GenerateAwr()
        {
            cmdGen = new OracleCommand();
            cmdGen.Connection = Connection;
           
            DataTable dt = new DataTable();

            Int32 beginSnap = Convert.ToInt32(((DataRowView)bindingSourceSnapBegin.Current)["SNAP_ID"]);
            Int32 endSnap = Convert.ToInt32(((DataRowView)bindingSourceSnapEnd.Current)["SNAP_ID"]);


            if (beginSnap >= endSnap)
            {
                MessageBox.Show("Wrong Begin Snap");

                return;

            }
            string sqlString = "select getawrf(p_beginsnap => :l_bid, p_endsnap => :l_eid) from dual";
            cmdGen.CommandText = sqlString;
            cmdGen.Parameters.Add("l_bid", beginSnap);
            cmdGen.Parameters.Add("l_eid", endSnap);

            cmdGen.InitialLOBFetchSize = 8192;
            OracleDataReader reader = cmdGen.ExecuteReader();

            try
            {
                reader.Read();
                awrClob = reader.GetOracleClob(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            };
 
        }
  
        #region worker
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBrowser1.DocumentStream = awrClob;
            toolStripButton1.Enabled = true;
            toolStripButton3.Enabled = false;
            toolStripButton3.BackColor = System.Drawing.SystemColors.Control;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;


            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                GenerateAwr();
            }

        }
        #endregion
       
    }
}
