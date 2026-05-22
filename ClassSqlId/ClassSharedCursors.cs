using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ClassSqlId
{
    /// <summary>
    /// Author:         artur.balon@asseco.pl
    /// Cescription:    klasa pomocnicza do informacji i sql_id
    /// Changelog:      2025-06
    /// </summary>
    internal class ClassSharedCursors
    {
        public static DataTable GetSCursor(OracleConnection connection, string  SqlId)
        {
            DataTable STable = new DataTable();
            OracleCommand cmd = new OracleCommand(SqlIdSqlstrings.SCURSOR, connection);
            cmd.Parameters.Add("sql_id", SqlId);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(STable);
            return STable;
        }
    }

    static class SqlIdSqlstrings
    {

        public static string MAIN_OVERWIEV = "select 'SQL_TYPE_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where SQL_TYPE_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'OPTIMIZER_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where OPTIMIZER_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'STATS_ROW_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where STATS_ROW_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'LITERAL_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where LITERAL_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'FORCE_HARD_PARSE' as name ,count(*) from V$SQL_SHARED_CURSOR where FORCE_HARD_PARSE = 'Y'\n" +
        "union all\n" +
        "select 'BIND_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where BIND_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'DESCRIBE_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where DESCRIBE_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'LANGUAGE_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where LANGUAGE_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'TRANSLATION_MISMATCH' as name ,count(*) from V$SQL_SHARED_CURSOR where TRANSLATION_MISMATCH = 'Y'\n" +
        "union all\n" +
        "select 'EXPLAIN_PLAN_CURSOR' as name ,count(*) from V$SQL_SHARED_CURSOR where EXPLAIN_PLAN_CURSOR = 'Y'";

        //public static string SCURSOR = "select c.* from v$sql_shared_cursor c where c.sql_id = :sql_id";

        public static string SCURSOR = "select sql_id,\n" +
        "       child_number,\n" +
        "       unbound_cursor,\n" +
        "       sql_type_mismatch,\n" +
        "       optimizer_mismatch,\n" +
        "       outline_mismatch,\n" +
        "       stats_row_mismatch,\n" +
        "       literal_mismatch,\n" +
        "       force_hard_parse,\n" +
        "       explain_plan_cursor,\n" +
        "       buffered_dml_mismatch,\n" +
        "       pdml_env_mismatch,\n" +
        "       inst_drtld_mismatch,\n" +
        "       slave_qc_mismatch,\n" +
        "       typecheck_mismatch,\n" +
        "       auth_check_mismatch,\n" +
        "       bind_mismatch,\n" +
        "       describe_mismatch,\n" +
        "       language_mismatch,\n" +
        "       translation_mismatch,\n" +
        "       bind_equiv_failure,\n" +
        "       insuff_privs,\n" +
        "       insuff_privs_rem,\n" +
        "       remote_trans_mismatch,\n" +
        "       logminer_session_mismatch,\n" +
        "       incomp_ltrl_mismatch,\n" +
        "       overlap_time_mismatch,\n" +
        "       edition_mismatch,\n" +
        "       mv_query_gen_mismatch,\n" +
        "       user_bind_peek_mismatch,\n" +
        "       typchk_dep_mismatch,\n" +
        "       no_trigger_mismatch,\n" +
        "       flashback_cursor,\n" +
        "       anydata_transformation,\n" +
        "       pddl_env_mismatch,\n" +
        "       top_level_rpi_cursor,\n" +
        "       different_long_length,\n" +
        "       logical_standby_apply,\n" +
        "       diff_call_durn,\n" +
        "       bind_uacs_diff,\n" +
        "       plsql_cmp_switchs_diff,\n" +
        "       cursor_parts_mismatch,\n" +
        "       stb_object_mismatch,\n" +
        "       crossedition_trigger_mismatch,\n" +
        "       pq_slave_mismatch,\n" +
        "       top_level_ddl_mismatch,\n" +
        "       multi_px_mismatch,\n" +
        "       bind_peeked_pq_mismatch,\n" +
        "       mv_rewrite_mismatch,\n" +
        "       roll_invalid_mismatch,\n" +
        "       optimizer_mode_mismatch,\n" +
        "       px_mismatch,\n" +
        "       mv_staleobj_mismatch,\n" +
        "       flashback_table_mismatch,\n" +
        "       litrep_comp_mismatch,\n" +
        "       plsql_debug,\n" +
        "       load_optimizer_stats,\n" +
        "       acl_mismatch,\n" +
        "       flashback_archive_mismatch,\n" +
        "       lock_user_schema_failed,\n" +
        "       remote_mapping_mismatch,\n" +
        "       load_runtime_heap_failed,\n" +
        "       hash_match_failed,\n" +
        "       purged_cursor,\n" +
        "       bind_length_upgradeable,\n" +
        "       use_feedback_stats,\n" +
        "       reason,\n" +
        "       con_id\n" +
        "  from v$sql_shared_cursor c\n" +
        " where c.sql_id = :sql_id";

    }

    internal class SCursors
    {
        SCursors() 
        {
            


        }  
        
    }
}
