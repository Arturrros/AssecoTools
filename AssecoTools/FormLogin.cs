using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ClassMonitor;
using static System.Formats.Asn1.AsnWriter;
using AssecoToolsOptions;

namespace AssecoTools
{
    public partial class FormLogin : Form
    {
        public string connectionString;
        SecureString ss;

        SessionOptions sessionOptions;

        public FormLogin(SecureString Ss, AssecoToolsOptions.SessionOptions So)
        {
            InitializeComponent();
            ss = Ss;
            sessionOptions = So;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectionString = "Data Source = (DESCRIPTION = (CID = GTU_APP)(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtIP.Text + ")(PORT = " + txtPORT.Text + ")))(CONNECT_DATA = (SERVICE_NAME = " + txtSERVICE_NAME.Text + ")(SERVER = DEDICATED))); User Id = " + txtUSER.Text + "; Password = " + txtPASSWORD.Text + ";Pooling=False;Persist Security Info=False; ";
            //connectionString = "Data Source=BOHEMA; User Id = " + txtUSER.Text + "; Password = " + txtPASSWORD.Text + ";Pooling=False;Persist Security Info=True; ";
            AssecoTools.Default.Ip = txtIP.Text;
            AssecoTools.Default.Port = txtPORT.Text;
            AssecoTools.Default.ServiceName = txtSERVICE_NAME.Text;
            AssecoTools.Default.UserName = txtUSER.Text;
            AssecoTools.Default.ConnName = txtConnName.Text;
            AssecoTools.Default.Save();
           
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\login.txt"))
            {
                String[] lines = File.ReadAllLines(Application.StartupPath + "\\login.txt");
                String[] line = lines[0].Split(';');
                if (line.Length == 4)
                {
                    txtIP.Text = line[0];
                    txtPORT.Text = line[1];
                    txtSERVICE_NAME.Text = line[2];
                    txtUSER.Text = line[3];
                    txtConnName.Text = line[4];
                    sessionOptions.userConnectionName = line[4];
                }
            }
            else
            {
                txtIP.Text = AssecoTools.Default.Ip;
                txtPORT.Text = AssecoTools.Default.Port;
                txtSERVICE_NAME.Text = AssecoTools.Default.ServiceName;
                txtUSER.Text = AssecoTools.Default.UserName;
                txtConnName.Text = AssecoTools.Default.ConnName;
                sessionOptions.userConnectionName = AssecoTools.Default.ConnName;
            }
            txtPASSWORD.Select();
            txtPASSWORD.Focus();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btn_more_Click(object sender, EventArgs e)
        {
            Point screenPoint = button1.PointToScreen(new Point(btn_more.Left, button1.Bottom));
            if (screenPoint.Y + contextMenuStrip1.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                contextMenuStrip1.Show(btn_more, new Point(0, -contextMenuStrip1.Size.Height));
            }
            else
            {
                contextMenuStrip1.Show(btn_more, new Point(0, button1.Height));
            }    
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string tmpSystemsFile = Application.StartupPath + "\\systems.txt";
            if (!File.Exists(tmpSystemsFile))
            {
                string[] startupLines = { "# # Comment line", "# Terminator ';'", "# Structure:","# IP;PORT;SERVICE_NAME(SID);USERNAME;FRIENDLY_CONNECTION_NAME","# Example:", "# 1.1.1.1;1521;DB01,TIGER,MyconnectionDB01"};

                using (StreamWriter outputFile = new StreamWriter(tmpSystemsFile))
                {
                    foreach (string line in startupLines)
                        outputFile.WriteLine(line);
                }
            }


            try
            {
                contextMenuStrip1.Items.Clear();
                using (var reader = File.OpenText(Application.StartupPath + "\\systems.txt"))
                {
                    string line = String.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line.Trim()))
                            continue;

                        if (line.Trim().Substring(0, 1) == "#")
                            continue;

                        string[] li = line.Split(';');

                        if (li.Length < 4)
                            continue;

                        ToolStripMenuItem tmi = new ToolStripMenuItem();
                        tmi.Name = li[2];
                        tmi.Text = li[2];
                        tmi.Tag = line;
                        if (li.Length > 4)
                            tmi.Text += " ("+li[4].ToString()+")";

                        tmi.Click += tmi_Click;
                        contextMenuStrip1.Items.Add(tmi);

                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message.ToString() + "\n" + "Error parsing file systems.txt");
            }
        }

        void tmi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tmi = ((ToolStripMenuItem)sender);
            string tag = tmi.Tag.ToString();
            string [] li = tag.Split(';');

            txtIP.Text = li[0];
            txtPORT.Text = li[1];
            txtSERVICE_NAME.Text = li[2];
            txtUSER.Text = li[3];
            // dodana nazwa connectionName
            if (li.Length >4)
                txtConnName.Text = li[4];
            else
                txtConnName.Text = string.Empty;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                txtPASSWORD.Text = Decrypt(File.ReadAllText(new NetworkCredential("", ss).Password));
            }
            catch (Exception exc) { MessageBox.Show("Brak pliku"); };
        }

        public string Decrypt(string cipher)
        {
            if (cipher == null) return "";
            return Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(cipher), null, DataProtectionScope.LocalMachine));
        }

        private void buttonSaveConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConnName.Text.Trim().Length > 0)
                {
                    string[] readConns = File.ReadAllLines(Application.StartupPath + "\\systems.txt");
                    bool existsConn = false;
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(Application.StartupPath, "systems.txt"), true))
                    {
                        string tmpLine = String.Empty;
                        tmpLine = txtIP.Text + ";" + txtPORT.Text + ";" + txtSERVICE_NAME.Text + ";" + txtUSER.Text + ";" + txtConnName.Text;
                        foreach (string connline in readConns)
                        {
                            if (connline.ToUpper().Trim() == tmpLine.ToUpper().Trim())
                            {
                                MessageBox.Show("Duplicated.");
                                existsConn = true;
                                break;
                            }
                            if (connline.EndsWith(Environment.NewLine))
                                MessageBox.Show("asdf");
                        }
                        if (!existsConn)
                            outputFile.WriteLine(tmpLine);
                    }
                }
                else
                {
                    MessageBox.Show("Empty name");
                }
            }
            catch (Exception ex) { MessageBox.Show("Saved"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\systems.txt");
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.SolidColorOnly = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = cd.Color;
                sessionOptions.SessionColor = cd.Color;
                sessionOptions.isActiveSessionColor = true;
            }
        }
    }
}
