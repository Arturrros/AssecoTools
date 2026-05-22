using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSession
{
    /// <summary>
    /// Klasa DML dla Sesji
    /// </summary>
    public static class SQLStrings
    {
        /// <summary>
        /// Show all sessions from v$session
        /// </summary>
        public static string SESSIONS_ALL = "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s";
        /// <summary>
        /// Show user sessions TYPE USER
        /// </summary>
        public static string SESSIONS_ALL_USER = "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s" +
                                            "  where TYPE='USER'";
        /// <summary>
        /// Show user sessions TYPE USER AND ACTIVE
        /// </summary>
        public static string SESSIONS_ALL_USER_ACTIVE = "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s\n" +
                                            "  where TYPE='USER'\n" +
                                            "    AND STATUS = 'ACTIVE'";
        /// <summary>
        /// Show user sessions TYPE USER AND ACTIVE
        /// </summary>
        public static string SESSIONS_ALL_USER_ACTIVE_AND_BLOCKED = "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s\n" +
                                            "  where s.TYPE='USER'\n" +
                                            "    AND s.STATUS = 'ACTIVE'\n" +
                                            "    OR s.BLOCKING_SESSION IS NOT NULL";

        public static string SESSIONS_ALL_USER_BLOCKED = "select\n" +
                                    "       s.OSUSER,\n" +
                                    "       s.USERNAME,\n" +
                                    "       s.PROGRAM,\n" +
                                    "       s.STATUS,\n" +
                                    "       s.EVENT,\n" +
                                    "       s.sid,\n" +
                                    "       s.MACHINE,\n" +
                                    "       s.LOGON_TIME,\n" +
                                    "       s.LAST_CALL_ET,\n" +
                                    "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                    "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                    "       s.BLOCKING_SESSION as BLO_SESS," +
                                    "       s.SQL_ID,\n" +
                                    "       s.PREV_SQL_ID,\n" +
                                    "       s.sql_child_number as sql_child,\n" +
                                    "       s.CLIENT_INFO," +
                                    "       s.ACTION," +
                                    "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                    "       s.serial#\n" +
                                    "  from v$session s\n" +
                                    "  where s.TYPE='USER'\n" +
                                    "    AND s.BLOCKING_SESSION IS NOT NULL";
        /// <summary>
        /// Show session with sid, serial parameters
        /// </summary>
        public static string SESSION =
                                            "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s" +
                                            "  where TYPE='USER'" +
                                            "    and sid=:sid" +
                                            "    and serial#=:serial#";
        /// <summary>
        /// Show session with sid parameters
        /// </summary>
        public static string SESSION_SID =
                                            "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s" +
                                            "  where TYPE='USER'" +
                                            "    and sid=:sid";
        /// <summary>
        /// Show session with osuser parameters
        /// </summary>
        public static string SESSION_OSUSER =
                                            "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s" +
                                            "  where TYPE='USER'" +
                                            "    and osuser like  '%' || :osuer || '%' ";
        /// <summary>
        /// Show session with osuser parameters
        /// </summary>
        public static string SESSION_PROGRAM =
                                            "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s" +
                                            "  where TYPE='USER'" +
                                            "    and program like '%' || :program || '%'";
        /// <summary>
        /// Show all active and working session ( LAST_CALL_ET 10 )
        /// </summary>
        public static string SESSIONS_ALL_USER_IN_WORK =
                                            "select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s\n" +
                                            " where s.TYPE = 'USER'\n" +
                                            "   and s.STATUS = 'ACTIVE'\n" +
                                            "    or (s.STATUS = 'INACTIVE' and s.LAST_CALL_ET < 1)";
        /// <summary>
        /// Show all session object holders ( string object_name - table)
        /// </summary>
        public static string SESSIONS_HOLDERS =
                                            " select\n" +
                                            "       s.OSUSER,\n" +
                                            "       s.USERNAME,\n" +
                                            "       s.PROGRAM,\n" +
                                            "       s.STATUS,\n" +
                                            "       s.EVENT,\n" +
                                            "       s.sid,\n" +
                                            "       s.MACHINE,\n" +
                                            "       s.LOGON_TIME,\n" +
                                            "       s.LAST_CALL_ET,\n" +
                                            "       s.SECONDS_IN_WAIT as SEC_IN_WAIT,\n" +
                                            "       s.BLOCKING_SESSION_STATUS as BLO_STATUS,\n" +
                                            "       s.BLOCKING_SESSION as BLO_SESS," +
                                            "       s.SQL_ID,\n" +
                                            "       s.PREV_SQL_ID,\n" +
                                            "       s.sql_child_number as sql_child,\n" +
                                            "       s.CLIENT_INFO," +
                                            "       s.ACTION," +
                                            "       s.BLOCKING_INSTANCE as BLO_INST,\n" +
                                            "       s.serial#\n" +
                                            "  from v$session s\n" +
                                            "  where s.SID in\n" +
                                            "       (select session_id\n" +
                                            "          from dba_locks l\n" +
                                            "         where l.lock_id1 in\n" +
                                            "               (select object_id\n" +
                                            "                  from dba_objects\n" +
                                            "                 where object_name = :objectname ))\n" +
                                            "  order by s.USERNAME";
        /// <summary>
        /// Zgupowana liczba sesji wg daty i programu
        /// </summary>
        public static string SESSIONS_HISTORICAL = "select snap_date, program, count(*) from sessions\n" +
                                            "where snap_date=to_date(:snap_date,'YYYY-MM-DD HH24:MI:SS')\n" +
                                            "group by snap_date, program\n" +
                                            "order by snap_date desc";



        public static string SESSION_SCC = "select o.sql_id, o.sql_text, o.last_sql_active_time\n" +
                                            "  from v$open_cursor o\n" +
                                            " where o.sid = :sid\n" +
                                            "   and o.last_sql_active_time is not null\n" +
                                            " order by o.last_sql_active_time desc";

        /// <summary>
        /// Pobranie sesji riodzica dla child-a
        /// </summary>
        public static string PX_SESSION = "select qcsid, qcserial# from v$px_session px where px.sid = :sid";
        public static string PX_SESSION_CNT = "select count(*) from v$px_session px where px.qcsid = :sid";
    }
}
