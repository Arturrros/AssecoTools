using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static ClassLog.Log;

namespace ClassLog
{
    public class Log
    {
        /// <summary>
        ///  LogLevel - more log files 
        /// </summary>
        public enum LogLevel { NORMAL = 1, INFO = 2, WARNING = 3, SETTINGSCHANGED = 4 }
        
        private static readonly string MUTEX_GUID = "e1ffff8f-c91d-4188-9e82-c92ca5b1d057";
        private static Mutex m_oLoggerMutex = null;
        private static string ApplicationStartupPath = AppDomain.CurrentDomain.BaseDirectory;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Informacja ktora ma byc zapisanoa do logu</param>
        public static void Add(LogLevel LogLVL, string message)
        {
            

            if (m_oLoggerMutex == null)
                m_oLoggerMutex = new Mutex(false, MUTEX_GUID);

            message = string.Format(message);//, parametry);

            m_oLoggerMutex.WaitOne();

            try
            {
                string path = string.Format("{0}", ApplicationStartupPath + "\\log_" + LogLVL.ToString().ToLower() + ".log");

                using (FileStream fs = File.Open(path, FileMode.Append))
                {

                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("windows-1250"));
                    sw.WriteLine(
                        string.Format(
                        "{0} {1}",
                        DateTime.Now, message));

                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {

            }
            finally
            {
                m_oLoggerMutex.ReleaseMutex();
            }
        }

        public static void Clear(LogLevel LogLVL)
        {
            string path = string.Format("{0}", ApplicationStartupPath + "\\log_" + LogLVL.ToString().ToLower() + ".log");

            Backup(LogLVL);

            FileStream fs;

            try
            {
                using (fs = File.Open(path, FileMode.Truncate))
                {
                    fs.Flush();
                    fs.Close();
                }
            }
            catch { }
        }

        public static Int64 CheckSize(LogLevel LogLVL)
        {
            Int64 size = 0;
            try
            {
                string path = string.Format("{0}", ApplicationStartupPath + "\\log_" + LogLVL.ToString().ToLower() + ".log");
                FileStream fs = File.Open(path, FileMode.Open);
                size = fs.Length;
                fs.Flush();
                fs.Close();
            }
            catch { }

            return size;

        }

        public static void Backup(LogLevel LogLVL)
        {
            try
            {
                string path = string.Format("{0}", ApplicationStartupPath + "\\log_" + LogLVL.ToString() + ".log");
                string path_backup = string.Format("{0}", ApplicationStartupPath + "\\log_"  + LogLVL.ToString().ToLower() + "_old.log");
                File.Copy(path, path_backup);
            }
            catch { }
        }
    }

}
