using System;
using System.Collections.Generic;
using System.Text;


namespace ClassMonitor
{
    public class ClassMonitor
    {
        
    }
    public static class SQLStrings
    {
        /// <summary>
        /// Executions
        /// </summary>
        //public static string EXECUTIONS = "select b.sid sid, b.OSUSER name,\n" +
        //                                   "     sum(decode(c.name,'execute count',value,0)) val\n" +
        //                                   "from sys.v_$sesstat a,\n" +
        //                                   "     sys.v_$session b,\n" +
        //                                   "     sys.v_$statname c,\n" +
        //                                   "     sys.v_$process d,\n" +
        //                                   "     sys.v_$bgprocess e\n" +
        //                                   "where a.statistic#=c.statistic# and\n" +
        //                                   "      b.sid=a.sid and\n" +
        //                                   "      d.addr = b.paddr and\n" +
        //                                   "      e.paddr (+) = b.paddr and\n" +
        //                                   "      c.NAME in ('execute count')\n" +
        //                                   "group by b.sid, b.osuser";
        public static string EXECUTIONS = "select b.sid sid, b.OSUSER name,\n" +
                                          "     sum(decode(c.name,'execute count',value,0)) val\n" +
                                          "from sys.v_$sesstat a,\n" +
                                          "     sys.v_$session b,\n" +
                                          "     sys.v_$statname c,\n" +
                                          "     sys.v_$bgprocess e\n" +
                                          "where a.statistic#=c.statistic# and\n" +
                                          "      b.sid=a.sid and\n" +
                                          "      e.paddr (+) = b.paddr and\n" +
                                          "      c.NAME in ('execute count')\n" +
                                          "group by b.sid, b.osuser";
        public static string STATNAME_SID = "select b.sid sid, c.name,\n" +
                                          "     a.value val\n" +
                                          "from sys.v_$sesstat a,\n" +
                                          "     sys.v_$session b,\n" +
                                          "     sys.v_$statname c\n" +
                                          // "     sys.v_$bgprocess e\n" +
                                          "where a.statistic#=c.statistic# and\n" +
                                          "      b.sid=a.sid and\n" +
                                         // "      e.paddr (+) = b.paddr and\n" +
                                          "      c.NAME = :name and\n" +
                                          "      b.sid = :sid\n";
                                          //"group by b.sid, b.osuser";
        //public static string USER_CALLS = "select b.sid sid, b.OSUSER name,\n" +
        //                                   "     sum(decode(c.name,'user calls',value,0)) val\n" +
        //                                   "from sys.v_$sesstat a,\n" +
        //                                   "     sys.v_$session b,\n" +
        //                                   "     sys.v_$statname c,\n" +
        //                                   "     sys.v_$process d,\n" +
        //                                   "     sys.v_$bgprocess e\n" +
        //                                   "where a.statistic#=c.statistic# and\n" +
        //                                   "      b.sid=a.sid and\n" +
        //                                   "      d.addr = b.paddr and\n" +
        //                                   "      e.paddr (+) = b.paddr and\n" +
        //                                   "      c.NAME in ('user calls')\n" +
        //                                   "group by b.sid, b.osuser";
        public static string USER_CALLS = "select b.sid sid, b.OSUSER name,\n" +
                                           "     sum(decode(c.name,'user calls',value,0)) val\n" +
                                           "from sys.v_$sesstat a,\n" +
                                           "     sys.v_$session b,\n" +
                                           "     sys.v_$statname c,\n" +
                                           "     sys.v_$bgprocess e\n" +
                                           "where a.statistic#=c.statistic# and\n" +
                                           "      b.sid=a.sid and\n" +
                                           "      e.paddr (+) = b.paddr and\n" +
                                           "      c.NAME in ('user calls')\n" +
                                           "group by b.sid, b.osuser";
        //public static string PARSE_TOTAL = "select b.sid sid, b.OSUSER name,\n" +
        //                                  "     sum(decode(c.name,'parse count (hard)',value,0)) val\n" +
        //                                  "from sys.v_$sesstat a,\n" +
        //                                  "     sys.v_$session b,\n" +
        //                                  "     sys.v_$statname c,\n" +
        //                                  "     sys.v_$process d,\n" +
        //                                  "     sys.v_$bgprocess e\n" +
        //                                  "where a.statistic#=c.statistic# and\n" +
        //                                  "      b.sid=a.sid and\n" +
        //                                  "      d.addr = b.paddr and\n" +
        //                                  "      e.paddr (+) = b.paddr and\n" +
        //                                  "      c.NAME in ('parse count (hard)')\n" +
        //                                  "group by b.sid, b.osuser";

        public static string PARSE_TOTAL = "select b.sid sid, b.OSUSER name,\n" +
                                          "     sum(decode(c.name,'parse count (hard)',value,0)) val\n" +
                                          "from sys.v_$sesstat a,\n" +
                                          "     sys.v_$session b,\n" +
                                          "     sys.v_$statname c,\n" +
                                          "     sys.v_$bgprocess e\n" +
                                          "where a.statistic#=c.statistic# and\n" +
                                          "      b.sid=a.sid and\n" +
                                          "      e.paddr (+) = b.paddr and\n" +
                                          "      c.NAME in ('parse count (hard)')\n" +
                                          "group by b.sid, b.osuser";

        //public static string ENQUENE_DEADLOCK = "select b.sid sid, b.OSUSER name,\n" +
        //                                     "     sum(decode(c.name,'enqueue deadlocks',value,0)) val\n" +
        //                                     "from sys.v_$sesstat a,\n" +
        //                                     "     sys.v_$session b,\n" +
        //                                     "     sys.v_$statname c,\n" +
        //                                     "     sys.v_$process d,\n" +
        //                                     "     sys.v_$bgprocess e\n" +
        //                                     "where a.statistic#=c.statistic# and\n" +
        //                                     "      b.sid=a.sid and\n" +
        //                                     "      d.addr = b.paddr and\n" +
        //                                     "      e.paddr (+) = b.paddr and\n" +
        //                                     "      c.NAME in ('enqueue deadlocks')\n" +
        //                                     "group by b.sid, b.osuser";
        public static string ENQUENE_DEADLOCK = "select b.sid sid, b.OSUSER name,\n" +
                                                "     sum(decode(c.name,'enqueue deadlocks',value,0)) val\n" +
                                                "from sys.v_$sesstat a,\n" +
                                                "     sys.v_$session b,\n" +
                                                "     sys.v_$statname c,\n" +
                                                "     sys.v_$bgprocess e\n" +
                                                "where a.statistic#=c.statistic# and\n" +
                                                "      b.sid=a.sid and\n" +
                                                "      e.paddr (+) = b.paddr and\n" +
                                                "      c.NAME in ('enqueue deadlocks')\n" +
                                                "group by b.sid, b.osuser";
        //public static string COMMITS = "select b.sid sid,\n" +
        //                                "       b.OSUSER name,\n" +
        //                                "       sum(decode(c.name, 'user commits', value, 0)) val\n" +
        //                                "  from sys.v_$sesstat   a,\n" +
        //                                "       sys.v_$session   b,\n" +
        //                                "       sys.v_$statname  c,\n" +
        //                                "       sys.v_$process   d,\n" +
        //                                "       sys.v_$bgprocess e\n" +
        //                                " where a.statistic# = c.statistic#\n" +
        //                                "   and b.sid = a.sid\n" +
        //                                "   and d.addr = b.paddr\n" +
        //                                "   and e.paddr(+) = b.paddr\n" +
        //                                "   and c.NAME in ('user commits')\n" +
        //                                " group by b.sid, b.osuser";
        public static string COMMITS = "select b.sid sid,\n" +
                                                "       b.OSUSER name,\n" +
                                                "       sum(decode(c.name, 'user commits', value, 0)) val\n" +
                                                "  from sys.v_$sesstat   a,\n" +
                                                "       sys.v_$session   b,\n" +
                                                "       sys.v_$statname  c,\n" +
                                                "       sys.v_$bgprocess e\n" +
                                                " where a.statistic# = c.statistic#\n" +
                                                "   and b.sid = a.sid\n" +
                                                "   and e.paddr(+) = b.paddr\n" +
                                                "   and c.NAME in ('user commits')\n" +
                                                " group by b.sid, b.osuser";
        //public static string CPU_BY_THIS = "select b.sid sid,\n" +
        //                                "       b.OSUSER name,\n" +
        //                                "       sum(decode(c.name, 'CPU used by this session', value, 0)) val\n" +
        //                                "  from sys.v_$sesstat   a,\n" +
        //                                "       sys.v_$session   b,\n" +
        //                                "       sys.v_$statname  c,\n" +
        //                                "       sys.v_$process   d,\n" +
        //                                "       sys.v_$bgprocess e\n" +
        //                                " where a.statistic# = c.statistic#\n" +
        //                                "   and b.sid = a.sid\n" +
        //                                "   and d.addr = b.paddr\n" +
        //                                "   and e.paddr(+) = b.paddr\n" +
        //                                "   and c.NAME in ('CPU used by this session')\n" +
        //                                " group by b.sid, b.osuser";
        public static string CPU_BY_THIS = "select b.sid sid,\n" +
                                         "       b.OSUSER name,\n" +
                                         "       sum(decode(c.name, 'CPU used by this session', value, 0)) val\n" +
                                         "  from sys.v_$sesstat   a,\n" +
                                         "       sys.v_$session   b,\n" +
                                         "       sys.v_$statname  c,\n" +
                                         "       sys.v_$bgprocess e\n" +
                                         " where a.statistic# = c.statistic#\n" +
                                         "   and b.sid = a.sid\n" +
                                         "   and e.paddr(+) = b.paddr\n" +
                                         "   and c.NAME in ('CPU used by this session')\n" +
                                         " group by b.sid, b.osuser";
        //public static string PHYSICAL_READS = "select b.sid sid,\n" +
        //                                        "       b.OSUSER name,\n" +
        //                                        "       sum(decode(c.name, 'physical read bytes', value, 0)) val\n" +
        //                                        "  from sys.v_$sesstat   a,\n" +
        //                                        "       sys.v_$session   b,\n" +
        //                                        "       sys.v_$statname  c,\n" +
        //                                        "       sys.v_$process   d,\n" +
        //                                        "       sys.v_$bgprocess e\n" +
        //                                        " where a.statistic# = c.statistic#\n" +
        //                                        "   and b.sid = a.sid\n" +
        //                                        "   and d.addr = b.paddr\n" +
        //                                        "   and e.paddr(+) = b.paddr\n" +
        //                                        "   and c.NAME in ('physical read bytes')\n" +
        //                                        " group by b.sid, b.osuser";
        public static string PHYSICAL_READS = "select b.sid sid,\n" +
                                                       "       b.OSUSER name,\n" +
                                                       "       sum(decode(c.name, 'physical read bytes', value, 0)) val\n" +
                                                       "  from sys.v_$sesstat   a,\n" +
                                                       "       sys.v_$session   b,\n" +
                                                       "       sys.v_$statname  c,\n" +
                                                       "       sys.v_$bgprocess e\n" +
                                                       " where a.statistic# = c.statistic#\n" +
                                                       "   and b.sid = a.sid\n" +
                                                       "   and e.paddr(+) = b.paddr\n" +
                                                       "   and c.NAME in ('physical read bytes')\n" +
                                                       " group by b.sid, b.osuser";
        public static string REDO_SIZE = "select b.sid sid,\n" +
                                                       "       b.OSUSER name,\n" +
                                                       "       sum(decode(c.name, 'physical read bytes', value, 0)) val\n" +
                                                       "  from sys.v_$sesstat   a,\n" +
                                                       "       sys.v_$session   b,\n" +
                                                       "       sys.v_$statname  c,\n" +
                                                       "       sys.v_$bgprocess e\n" +
                                                       " where a.statistic# = c.statistic#\n" +
                                                       "   and b.sid = a.sid\n" +
                                                       "   and e.paddr(+) = b.paddr\n" +
                                                       "   and c.NAME in ('redo size')\n" +
                                                       " group by b.sid, b.osuser";
        //public static string BYTES_NETWORK_SEND = "select b.sid sid,\n" +
        //                                        "       b.OSUSER name,\n" +
        //                                        "       sum(decode(c.name, 'bytes sent via SQL*Net to client', value, 0)) val\n" +
        //                                        "  from sys.v_$sesstat   a,\n" +
        //                                        "       sys.v_$session   b,\n" +
        //                                        "       sys.v_$statname  c,\n" +
        //                                        "       sys.v_$process   d,\n" +
        //                                        "       sys.v_$bgprocess e\n" +
        //                                        " where a.statistic# = c.statistic#\n" +
        //                                        "   and b.sid = a.sid\n" +
        //                                        "   and d.addr = b.paddr\n" +
        //                                        "   and e.paddr(+) = b.paddr\n" +
        //                                        "   and c.NAME in ('bytes sent via SQL*Net to client')\n" +
        //                                        " group by b.sid, b.osuser";
        public static string BYTES_NETWORK_SEND = "select b.sid sid,\n" +
                                                       "       b.OSUSER name,\n" +
                                                       "       sum(decode(c.name, 'bytes sent via SQL*Net to client', value, 0)) val\n" +
                                                       "  from sys.v_$sesstat   a,\n" +
                                                       "       sys.v_$session   b,\n" +
                                                       "       sys.v_$statname  c,\n" +
                                                       "       sys.v_$bgprocess e\n" +
                                                       " where a.statistic# = c.statistic#\n" +
                                                       "   and b.sid = a.sid\n" +
                                                       "   and e.paddr(+) = b.paddr\n" +
                                                       "   and c.NAME in ('bytes sent via SQL*Net to client')\n" +
                                                       " group by b.sid, b.osuser";
        public static string OPEN_CUROSORS = "select b.sid sid,\n" +
                                                       "       b.OSUSER name,\n" +
                                                       "       sum(decode(c.name, 'opened cursors current', value, 0)) val\n" +
                                                       "  from sys.v_$sesstat   a,\n" +
                                                       "       sys.v_$session   b,\n" +
                                                       "       sys.v_$statname  c,\n" +
                                                       "       sys.v_$bgprocess e\n" +
                                                       " where a.statistic# = c.statistic#\n" +
                                                       "   and b.sid = a.sid\n" +
                                                       "   and e.paddr(+) = b.paddr\n" +
                                                       "   and c.NAME in ('opened cursors current')\n" +
                                                       " group by b.sid, b.osuser\n" +
                                                       " order by val desc";
        public static string OBJECT_MONITOR = "select \n" +
                                                "       s.SQL_ID,\n" +
                                                "       (select object_name || ' (' || object_type || ')'\n" +
                                                "          from dba_objects\n" +
                                                "         where object_id = s.ROW_WAIT_OBJ#) as obj\n" +
                                                "  from v$session s\n" +
                                                " where sid = :sid";
        public static string SESSION_UNDO_BLOCK_USED = "select s.sid, t.used_ublk used_undo_blocks\n" +
                                                "  from v$session s, v$transaction t\n" +
                                                " where s.saddr = t.ses_addr\n" +
                                                "   and sid = :sid";


        public static string SESSION_TMP = "select b.blocks * 8192 / 1024 / 1024 as MB_in_temp,\n" +
                                                "       a.sid,\n" +
                                                "       a.USERNAME,\n" +
                                                "       a.status,\n" +
                                                "       a.OSUSER,\n" +
                                                "       a.MACHINE,\n" +
                                                "       a.TERMINAL,\n" +
                                                "       a.PROGRAM,\n" +
                                                "       a.EVENT,\n" +
                                                "       a.SQL_ID,\n" +
                                                "       r.SQL_TEXT\n" +
                                                "  from V$SESSION a, V$SORT_USAGE b, v$sqlarea r\n" +
                                                " where a.saddr = b.session_addr\n" +
                                                "   and a.SQL_ID = r.SQL_ID\n" +
                                                " order by MB_in_temp desc";

        public static string UNDO_RECOVER = "select usn,\n" +
                                                "       state,\n" +
                                                "       undoblockstotal \"Total\",\n" +
                                                "       undoblocksdone \"Done\",\n" +
                                                "       undoblockstotal - undoblocksdone \"ToDo\",\n" +
                                                "       decode(cputime,\n" +
                                                "              0,\n" +
                                                "              'unknown',\n" +
                                                "              sysdate + (((undoblockstotal - undoblocksdone) /\n" +
                                                "              (undoblocksdone / cputime)) / 86400)) \"Estimated time to complete\"\n" +
                                                "  from v$fast_start_transactions \n" +
                                                "       where state != 'RECOVERED' \n";
        public static string UNDO_SIZE  = "SELECT s.sid,\n" +
                                                "       s.serial#,\n" +
                                                "       s.username,\n" +
                                                "       s.OSUSER,\n" +
                                                "       s.program,\n" +
                                                "       t.used_ublk * TO_NUMBER(p.value) / 1024  \"Undo Size (Kb)\"\n" +
                                                "  FROM v$session     s,\n" +
                                                "       v$parameter   p,\n" +
                                                "       v$transaction t\n" +
                                                " WHERE s.taddr = t.addr\n" +
                                                "   AND p.name = 'db_block_size' \n" +
                                                "   ORDER BY t.used_ublk DESC";

        public static string GLOBAL_UNDO_SIZE = "SELECT tud.tablespace_name as name,\n" +
                                                "       round(SIZE_BYTES / 1024 / 1024) as SIZE_MB,\n" +
                                                "       round(USAGE_BYTES / 1024 / 1024) as USAGE_MB\n" +
                                                "  FROM (SELECT SUM(BYTES) as SIZE_BYTES, tbs.tablespace_name\n" +
                                                "          FROM dba_data_files ddf, dba_tablespaces tbs\n" +
                                                "         WHERE ddf.tablespace_name = tbs.tablespace_name\n" +
                                                "           AND tbs.contents like 'UNDO'\n" +
                                                "         GROUP BY tbs.tablespace_name) tud,\n" +
                                                "       (SELECT tablespace_name, SUM(BYTES) as USAGE_BYTES\n" +
                                                "          FROM DBA_UNDO_EXTENTS\n" +
                                                "         WHERE status <> 'EXPIRED'\n" +
                                                "         GROUP BY tablespace_name) tus\n" +
                                                " WHERE tud.tablespace_name = tus.tablespace_name";

        //public static string SESSION_WAIT_STATS = "select S.STATUS,\n" +
        //                                        "       S.STATE,\n" + 
        //                                        "       S.WAIT_CLASS,\n" + 
        //                                        "       S.LAST_CALL_ET,\n" + 
        //                                        "       H.EVENT,\n" + 
        //                                        "       H.P1TEXT,\n" + 
        //                                        "       H.P1,\n" + 
        //                                        "       H.P2TEXT,\n" + 
        //                                        "       H.P2,\n" + 
        //                                        "       H.TIME_SINCE_LAST_WAIT_MICRO\n" + 
        //                                        "  from V$SESSION S, V$SESSION_WAIT_HISTORY H\n" + 
        //                                        " where S.SID = H.SID\n" + 
        //                                        "   and S.sid = :sid\n" + 
        //                                        " order by H.seq#";

        public static string SESSION_WAIT_STATS = "select e.event,\n" +
                                                "       e.TOTAL_WAITS,\n" +
                                                "       round(e.time_waited / 100, 1) time_waited_sec\n" +
                                                "  from v$session_event e\n" +
                                                " where e.sid = :sid\n" +
                                                " order by time_waited_sec desc";
        public static string SESION_TRANSACTION = "select\n" +
                                                "   t.sid,\n" +
                                                "   t.USERNAME,\n" +
                                                "   t.PROGRAM,\n" +
                                                "   t.LAST_CALL_ET,\n" +
                                                "   t.MACHINE,\n" +
                                                "   t.CLIENT_INFO,\n" +
                                                "   t.SQL_ID,\n" +
                                                "   t.STATUS,\n" +
                                                "   t.EVENT,\n" +
                                                "   to_char(x.start_date,'YYYY-MM-DD HH24:MI:SS') as start_date,\n" +
                                                "   x.status as xstatus,\n" +
                                                "   x.used_ublk as xused_block\n" +
                                                "from\n" +
                                                "   v$session t,\n" +
                                                "   v$transaction x\n" +
                                                "where\n" +
                                                "   t.saddr = x.ses_addr\n" +
                                                "   ORDER BY x.used_ublk DESC";
        public class GvMonitors 
        {
            public static string SESION_TRANSACTION = "select\n" +
                                                "   t.sid,\n" +
                                                "   t.USERNAME,\n" +
                                                "   t.PROGRAM,\n" +
                                                "   t.LAST_CALL_ET,\n" +
                                                "   t.MACHINE,\n" +
                                                "   t.CLIENT_INFO,\n" +
                                                "   t.SQL_ID,\n" +
                                                "   t.STATUS,\n" +
                                                "   t.EVENT,\n" +
                                                "   to_char(x.start_date,'YYYY-MM-DD HH24:MI:SS') as start_date,\n" +
                                                "   x.status as xstatus,\n" +
                                                "   x.used_ublk as xused_block\n" +
                                                "from\n" +
                                                "   v$session t,\n" +
                                                "   v$transaction x\n" +
                                                "where\n" +
                                                "   t.saddr = x.ses_addr\n" +
                                                "   ORDER BY x.used_ublk DESC";

            public static string PHYSICAL_READS_BYTES = "select hsnap.BEGIN_INTERVAL_TIME as DT,\n" +
                                                "       hsys.VALUE - LAG(hsys.VALUE, 1, 0) OVER(ORDER BY hsnap.BEGIN_INTERVAL_TIME) AS \"VALUE_DIFF\"\n" +
                                                "  from dba_hist_sysstat hsys, dba_hist_snapshot hsnap\n" +
                                                " where hsys.snap_id = hsnap.snap_id\n" +
                                                "   and hsnap.instance_number in (select instance_number from v$instance)\n" +
                                                "   and hsnap.instance_number = hsys.instance_number\n" +
                                                "   and hsys.STAT_NAME = 'physical read bytes' --'user commits'\n" +
                                                "   and hsnap.BEGIN_INTERVAL_TIME between sysdate - 7 and sysdate \n" +
                                                " order by 1";
            public static string OSSTAT_LOAD = "select value from v$osstat where stat_name = 'LOAD'";
            public static string OSSTAT_BUSY_TIME = "select value from v$osstat where stat_name = 'BUSY_TIME'";
            public static string OSSTAT_TMP1 = "SELECT 1 - (SUM(decode(name, 'physical reads', value, 0)) / SUM(decode(name, 'db block gets', value, 0))) AS buffer_cache_hit_ratio FROM v$sysstat";

        }
    }
}
