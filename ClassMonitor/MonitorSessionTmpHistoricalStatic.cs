using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassMonitor
{
    /// <summary>
    /// Author:         artur.balon@asseco.pl
    /// Date created:   04-12-2023
    /// Chamge log:     
    /// Descritipn:     Dodanie monitorowania przestrzeni tymczasowych - dane historyczne
    /// </summary>

    internal static class MonitorSessionTmpHistoricalStatic
    {
        public static String DT_RANGE = "select to_char(dt, 'YYYY-MM-DD HH24:MI')\n" +
                                        "  from temp_usage\n" +
                                        " group by to_char(dt, 'YYYY-MM-DD HH24:MI')\n" +
                                        " order by 1";
        public static String SQL01 ="select to_char(dt, 'YYYY-MM-DD HH24:MI') as dt,\n" +
                                        "       mb_in_temp,\n" +
                                        "       session_sid,\n" +
                                        "       serial#,\n" +
                                        "       username,\n" +
                                        "       status,\n" +
                                        "       schemaname,\n" +
                                        "       osuser,\n" +
                                        "       process,\n" +
                                        "       machine,\n" +
                                        "       terminal,\n" +
                                        "       program,\n" +
                                        "       event,\n" +
                                        "       to_char(sql_text)\n" +
                                        "  from temp_usage\n" +
                                        " where dt >= to_date(:dt1, 'YYYY-MM-DD HH24:MI') \n" +
                                        "   and dt <= to_date(:dt2, 'YYYY-MM-DD HH24:MI')\n" +
                                        "   and is_active = :isActive\n" +
                                        " group by to_char(dt, 'YYYY-MM-DD HH24:MI'),\n" +
                                        "       mb_in_temp,\n" +
                                        "       session_sid,\n" +
                                        "       serial#,\n" +
                                        "       username,\n" +
                                        "       status,\n" +
                                        "       schemaname,\n" +
                                        "       osuser,\n" +
                                        "       process,\n" +
                                        "       machine,\n" +
                                        "       terminal,\n" +
                                        "       program,\n" +
                                        "       event,\n" +
                                        "       to_char(sql_text)\n" +
                                        " order by 1";

        public static string SQL02 = "select to_char(dt, 'YYYY-MM-DD HH24:MI') as dt,\n" +
                                        "       osuser,\n" +
                                        "       sum(mb_in_temp)\n" +
                                        "  from temp_usage\n" +
                                        " where dt >= to_date(dt1, 'YYYY-MM-DD HH24:MI')\n" +
                                        "   and dt <= to_date(dt2, 'YYYY-MM-DD HH24:MI')\n" +
                                        "   and is_active = :isActive\n" +
                                        " group by to_char(dt, 'YYYY-MM-DD HH24:MI'),\n" +
                                        "          osuser\n" +
                                        "          ";

        public static string SQL03 = "select to_char(dt, 'YYYY-MM-DD HH24:MI') as dt,\n" +
                                        "sum(mb_in_temp)\n" +
                                        "  from temp_usage\n" +
                                        " where dt >= to_date(dt1, 'YYYY-MM-DD HH24:MI')\n" +
                                        "   and dt <= to_date(dt2, 'YYYY-MM-DD HH24:MI')\n" +
                                        "   and is_active = :isActive\n" +
                                        " group by to_char(dt, 'YYYY-MM-DD HH24:MI')\n" +
                                        "";


    }
}
