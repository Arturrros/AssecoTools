using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace AssecoTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(args));
            }
            catch (OracleException exc)
            {
                MessageBox.Show(exc.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
