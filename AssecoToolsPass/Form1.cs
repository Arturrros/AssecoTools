using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssecoToolsPass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = ofd.FileName;
                }
            }

        }

        public string Encrypt(string PlTxt)
        {
            if (PlTxt == null) return "";
            return Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(PlTxt), null, DataProtectionScope.LocalMachine));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(textBox1.Text,Encrypt(textBox2.Text));
        }
    }
}
