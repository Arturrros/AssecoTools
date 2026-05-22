using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassServerError
{
    public class ClassServerError
    {
        
    }

    public static class SQLStrings
    {
        /// <summary>
        /// Show errors
        /// </summary>
        public static string ERRORS_ALL = "select\n" +
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
        public static string ERROR_LAST24H = "select\n" +
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
                                            "  where s.DT > sysdate -1\n" +
                                            " order by s.DT desc ";
        public static string ERROR_LAST1H = "select\n" +
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
                                            "  where s.DT > sysdate -1/24\n" +
                                            " order by s.DT desc ";
        public static string ERROR_LAST1000 = "select * from (select\n" +
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
                                           "  order by s.DT desc )" +
                                           "  where rownum <= 1000 \n";

        public static string ERROR_REPORT_MODULE_COUNT = "select module, count(*), sqltext, msg\n" +
                                            "  from servererror\n" +
                                            " where dt > sysdate - 30\n" +
                                            "   and to_number(substr(MSG, 5, 5)) < 20000\n" +
                                            " group by module, sqltext, msg\n" +
                                            "having count(*) >= 9\n" +
                                            " order by count(*) desc";

        public static string DDL_LAST24 = "select *\n" +
                                            "  from (select user_name,\n" +
                                            "               dt,\n" +
                                            "               ddl_type,\n" +
                                            "               object_type,\n" +
                                            "               owner,\n" +
                                            "               object_name,\n" +
                                            "               DBMS_LOB.SUBSTR(sqltext ,4000,1) as SQL_TEXT_4000,\n" +
                                            "               osuser,\n" +
                                            "               host,\n" +
                                            "               module,\n" +
                                            "               ip\n" +
                                            "          from ddl_actions\n" +
                                            "         where dt > sysdate - 1\n" +
                                            "         order by dt desc)\n" +
                                            " where rownum < 1000";
        public static string DDL_LAST1000 = "select *\n" +
                                            "  from (select user_name,\n" +
                                            "               dt,\n" +
                                            "               ddl_type,\n" +
                                            "               object_type,\n" +
                                            "               owner,\n" +
                                            "               object_name,\n" +
                                            "               DBMS_LOB.SUBSTR(sqltext ,4000,1) as SQL_TEXT_4000,\n" +
                                            "               osuser,\n" +
                                            "               host,\n" +
                                            "               module,\n" +
                                            "               ip\n" +
                                            "          from ddl_actions\n" +
                                            "         order by dt desc)\n" +
                                            " where rownum < 1000";

        public static string SESSION_LONGOPS = "SELECT S.SID,\n" +
                                            "       S.SERIAL#,\n" +
                                            "       S.OSUSER,\n" +
                                            "       S.PROGRAM,\n" +
                                            "       S.MACHINE,\n" +
                                            "       ROUND(SL.ELAPSED_SECONDS / 60) || ':' || MOD(SL.ELAPSED_SECONDS, 60) ELAPSED,\n" +
                                            "       ROUND(SL.TIME_REMAINING / 60) || ':' || MOD(SL.TIME_REMAINING, 60) REMAINING,\n" +
                                            "       ROUND(SL.SOFAR / SL.TOTALWORK * 100, 2) PCT,\n" +
                                            "       S.SQL_ID\n" +
                                            "  FROM V$SESSION S, V$SESSION_LONGOPS SL\n" +
                                            " WHERE S.SID = SL.SID\n" +
                                            "   AND S.SERIAL# = SL.SERIAL#\n" +
                                            "   AND SL.TOTALWORK != 0\n" +
                                            "   AND SL.SOFAR <> SL.TOTALWORK\n" +
                                            "   AND SL.TIME_REMAINING > 0";

    }
}
