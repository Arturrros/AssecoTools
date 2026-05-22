using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassReports
{
    public class ClassReports
    {
    }

    public static class SQLStrings
    {
        /// <summary>
        /// Show errors
        /// </summary>
        public static string REPO_ALL = "select\n" +
                                            "       s.DT,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.HOST,\n" +
                                            "       s.TERMINAL,\n" +
                                            "       s.IP,\n" +
                                            "       s.PROTOCOL,\n" +
                                            "       s.MSG,\n" +
                                            "       s.SQL_CODE,\n" +
                                            "       s.MODULE,\n" +
                                            "       s.SQLTEXT,\n" +
                                            "       s.LANGUAGE\n" +
                                            "  from SERVERERROR s\n" +
                                            " order by s.DT desc ";
    }
    }
