------------------------------------------------------
-- Export file for user ASSECO_TOOLS                --
-- Created by asseco.artbal on 2026-10-02, 12:41:47 --
------------------------------------------------------

set define off
spool asseco_tools_last.log

prompt
prompt Creating table DDL_ACTIONS
prompt ==========================
prompt
create table ASSECO_TOOLS.DDL_ACTIONS
(
  counter     NUMBER(38),
  user_name   VARCHAR2(4000),
  dt          TIMESTAMP(6),
  ddl_type    VARCHAR2(4000),
  object_type VARCHAR2(4000),
  owner       VARCHAR2(4000),
  object_name VARCHAR2(4000),
  sqltext     CLOB,
  osuser      VARCHAR2(60),
  host        VARCHAR2(100),
  module      VARCHAR2(100),
  ip          VARCHAR2(16)
)
;
create index ASSECO_TOOLS.IDX_DDL_DATE on ASSECO_TOOLS.DDL_ACTIONS (DT);

prompt
prompt Creating table HOLDING_LOCKS
prompt ============================
prompt
create table ASSECO_TOOLS.HOLDING_LOCKS
(
  id               NUMBER(9) not null,
  dt               DATE,
  info             VARCHAR2(500),
  blo_sid          NUMBER(9),
  blo_osuser       VARCHAR2(30),
  blo_program      VARCHAR2(150),
  blo_machine      VARCHAR2(150),
  blo_last_call_et NUMBER(14),
  wai_sid          NUMBER(9),
  wai_osuser       VARCHAR2(30),
  wai_program      VARCHAR2(150),
  wai_machine      VARCHAR2(150),
  wai_wait_time    NUMBER(14),
  wai_sql_id       VARCHAR2(60),
  blo_client_info  VARCHAR2(250),
  wai_client_info  VARCHAR2(250),
  blo_status       VARCHAR2(8),
  blo_sql_id       VARCHAR2(60),
  wai_status       VARCHAR2(8)
)
;
create index ASSECO_TOOLS.IDX_HOLDING_LOCKS_01 on ASSECO_TOOLS.HOLDING_LOCKS (DT);
alter table ASSECO_TOOLS.HOLDING_LOCKS
  add constraint PK_HOLDING_LOCKS primary key (ID);

prompt
prompt Creating table HOLDING_LOCKS_DETAIL
prompt ===================================
prompt
create table ASSECO_TOOLS.HOLDING_LOCKS_DETAIL
(
  id         NUMBER(9) not null,
  id_detail  NUMBER(9),
  sid        NUMBER(9),
  table_name VARCHAR2(30),
  ctime      NUMBER(9),
  snap_date  DATE
)
;
alter table ASSECO_TOOLS.HOLDING_LOCKS_DETAIL
  add constraint PK_LOCK_DETAIL primary key (ID);
alter table ASSECO_TOOLS.HOLDING_LOCKS_DETAIL
  add constraint FK_LOCK_DETAIL foreign key (ID_DETAIL)
  references ASSECO_TOOLS.HOLDING_LOCKS (ID) on delete cascade;

prompt
prompt Creating table HOLDING_LOCKS_BINDS
prompt ==================================
prompt
create table ASSECO_TOOLS.HOLDING_LOCKS_BINDS
(
  id               NUMBER(9) not null,
  id_detail        NUMBER(9),
  sid              NUMBER(9),
  user_name        VARCHAR2(30),
  sql_hash_value   VARCHAR2(30),
  sql_id           VARCHAR2(20),
  sql_child_number NUMBER(9),
  name             VARCHAR2(30),
  value_string     VARCHAR2(4000),
  last_captured    DATE
)
;
alter table ASSECO_TOOLS.HOLDING_LOCKS_BINDS
  add constraint PK_LOCKS_BINDS primary key (ID);
alter table ASSECO_TOOLS.HOLDING_LOCKS_BINDS
  add constraint FK_LOCK_DETAIL_BIND foreign key (ID_DETAIL)
  references ASSECO_TOOLS.HOLDING_LOCKS_DETAIL (ID) on delete cascade;

prompt
prompt Creating table IOSTAT
prompt =====================
prompt
create table ASSECO_TOOLS.IOSTAT
(
  id            NUMBER(12) not null,
  dt            DATE,
  filetype_name VARCHAR2(64),
  filetype_id   NUMBER(9),
  srmb          NUMBER(16),
  swmb          NUMBER(16),
  lrmb          NUMBER(16),
  lwmb          NUMBER(16),
  diffsrmb      NUMBER(16),
  diffswmb      NUMBER(16),
  difflrmb      NUMBER(16),
  difflwmb      NUMBER(16)
)
;
create index ASSECO_TOOLS.IDX_FILETYPE_ID on ASSECO_TOOLS.IOSTAT (FILETYPE_ID, ID);
alter table ASSECO_TOOLS.IOSTAT
  add constraint PK_IOSTAT primary key (ID);

prompt
prompt Creating table LOGS
prompt ===================
prompt
create table ASSECO_TOOLS.LOGS
(
  id   NUMBER(9),
  dt   DATE default sysdate,
  info VARCHAR2(2000)
)
;

prompt
prompt Creating table MISSING_FK_INDEXES_TABLE
prompt =======================================
prompt
create table ASSECO_TOOLS.MISSING_FK_INDEXES_TABLE
(
  owner           VARCHAR2(64),
  constraint_name VARCHAR2(64),
  status          VARCHAR2(8),
  table_name      VARCHAR2(64),
  foreign_key     VARCHAR2(2000),
  info            VARCHAR2(2000)
)
;

prompt
prompt Creating table OBJ_INFO_TAB
prompt ===========================
prompt
create table ASSECO_TOOLS.OBJ_INFO_TAB
(
  id                     NUMBER(9),
  table_schema           VARCHAR2(64),
  table_name             VARCHAR2(64),
  table_created          DATE,
  num_rows               NUMBER(14),
  table_bytes            NUMBER(14),
  table_tablespace       VARCHAR2(1000),
  table_lobs             VARCHAR2(2000),
  table_lob_bytes        NUMBER(14),
  table_lobs_tablespaces VARCHAR2(1000),
  table_lob_cnt          NUMBER(9),
  indxes                 VARCHAR2(2000),
  indxes_bytes           NUMBER(14),
  indxes_tablespaces     VARCHAR2(1000),
  table_size_all         NUMBER(14),
  indxes_size_all        NUMBER(14),
  tab_indx_all           NUMBER(14)
)
;

prompt
prompt Creating table POOR_PARSING
prompt ===========================
prompt
create table ASSECO_TOOLS.POOR_PARSING
(
  id       NUMBER(9),
  dt       DATE default sysdate,
  sql_text VARCHAR2(2000),
  info     VARCHAR2(2000),
  example  CLOB,
  cnt      NUMBER(9)
)
;

prompt
prompt Creating table REP_ACTSESSHIS_TXTM
prompt ==================================
prompt
create table ASSECO_TOOLS.REP_ACTSESSHIS_TXTM
(
  id       NUMBER(9) not null,
  dt       DATE,
  locktype VARCHAR2(2),
  waiters  NUMBER(9)
)
;
alter table ASSECO_TOOLS.REP_ACTSESSHIS_TXTM
  add constraint PK_REP_ACTSESSHIS_TXTM primary key (ID);

prompt
prompt Creating table REP_AWRS
prompt =======================
prompt
create table ASSECO_TOOLS.REP_AWRS
(
  id   NUMBER(9) not null,
  dt   DATE,
  awr  CLOB,
  info VARCHAR2(200)
)
;
alter table ASSECO_TOOLS.REP_AWRS
  add constraint PK_REP_AWR primary key (ID);

prompt
prompt Creating table REP_CPULOAD
prompt ==========================
prompt
create table ASSECO_TOOLS.REP_CPULOAD
(
  id      NUMBER(9) not null,
  dt      DATE,
  cpuload NUMBER(9)
)
;
alter table ASSECO_TOOLS.REP_CPULOAD
  add constraint PK_REPORT_CPULOAD primary key (ID);

prompt
prompt Creating table REP_JOBS
prompt =======================
prompt
create table ASSECO_TOOLS.REP_JOBS
(
  id                  NUMBER(9) not null,
  dt                  DATE,
  owner               VARCHAR2(128),
  job_name            VARCHAR2(261),
  status              VARCHAR2(30),
  count               NUMBER,
  min_duration_in_min NUMBER,
  avg_duration_in_min NUMBER,
  max_duration_in_min NUMBER
)
;
alter table ASSECO_TOOLS.REP_JOBS
  add constraint PK_REP_JOBS primary key (ID);

prompt
prompt Creating table REP_TOPSQL
prompt =========================
prompt
create table ASSECO_TOOLS.REP_TOPSQL
(
  id       NUMBER(9) not null,
  sqlid    VARCHAR2(64),
  samplehr VARCHAR2(16),
  module   VARCHAR2(128),
  action   VARCHAR2(250),
  sqltext  CLOB
)
;
alter table ASSECO_TOOLS.REP_TOPSQL
  add constraint PK_REPORT_TOPSL primary key (ID);

prompt
prompt Creating table REP_WAITCLASS
prompt ============================
prompt
create table ASSECO_TOOLS.REP_WAITCLASS
(
  id              NUMBER(9),
  dt              DATE,
  ccpu            NUMBER,
  cbcpu           NUMBER,
  cscheduler      NUMBER,
  cuserio         NUMBER,
  csystemio       NUMBER,
  cconcurrency    NUMBER,
  capplication    NUMBER,
  ccommit         NUMBER,
  cconfiguration  NUMBER,
  cadministrative NUMBER,
  cnetwork        NUMBER,
  cqueueing       NUMBER,
  ccluster        NUMBER,
  cother          NUMBER
)
;

prompt
prompt Creating table ROWSCNT
prompt ======================
prompt
create table ASSECO_TOOLS.ROWSCNT
(
  id        NUMBER(9) not null,
  dt        DATE default sysdate,
  id_schema NUMBER(9)
)
;
alter table ASSECO_TOOLS.ROWSCNT
  add constraint PK_MAIN primary key (ID);

prompt
prompt Creating table ROWSCNT_LOG
prompt ==========================
prompt
create table ASSECO_TOOLS.ROWSCNT_LOG
(
  id      NUMBER(9),
  id_snap NUMBER(9),
  info    VARCHAR2(1000)
)
;

prompt
prompt Creating table ROWSCNT_MAIN
prompt ===========================
prompt
create table ASSECO_TOOLS.ROWSCNT_MAIN
(
  id      NUMBER(9) not null,
  id_snap NUMBER(9) not null,
  typ     VARCHAR2(60) not null,
  bytes   NUMBER(16)
)
;
create index ASSECO_TOOLS.IDX_SNAP_MAIN on ASSECO_TOOLS.ROWSCNT_MAIN (ID_SNAP);
alter table ASSECO_TOOLS.ROWSCNT_MAIN
  add constraint PKIDX_MAIN_DATE primary key (ID);
alter table ASSECO_TOOLS.ROWSCNT_MAIN
  add constraint FK_ROWSCNT_MAIN foreign key (ID_SNAP)
  references ASSECO_TOOLS.ROWSCNT (ID) on delete cascade;

prompt
prompt Creating table ROWSCNT_SCHEMAS
prompt ==============================
prompt
create table ASSECO_TOOLS.ROWSCNT_SCHEMAS
(
  id          NUMBER(9) not null,
  schema_name VARCHAR2(30)
)
;

prompt
prompt Creating table ROWSCNT_TAB
prompt ==========================
prompt
create table ASSECO_TOOLS.ROWSCNT_TAB
(
  id         NUMBER(9) not null,
  id_snap    NUMBER(9) not null,
  table_name VARCHAR2(30) not null,
  num_rows   NUMBER(16),
  bytes      NUMBER(16)
)
;
create index ASSECO_TOOLS.IDX_SNAP_TAB on ASSECO_TOOLS.ROWSCNT_TAB (ID_SNAP);
create index ASSECO_TOOLS.IDX_TABLE_NAME on ASSECO_TOOLS.ROWSCNT_TAB (TABLE_NAME);
alter table ASSECO_TOOLS.ROWSCNT_TAB
  add constraint PK_ROWSCNT primary key (ID);
alter table ASSECO_TOOLS.ROWSCNT_TAB
  add constraint FK_ROWSCNT_TAB foreign key (ID_SNAP)
  references ASSECO_TOOLS.ROWSCNT (ID) on delete cascade;

prompt
prompt Creating table SERVERERROR
prompt ==========================
prompt
create table ASSECO_TOOLS.SERVERERROR
(
  dt       DATE,
  osuser   VARCHAR2(100),
  host     VARCHAR2(100),
  terminal VARCHAR2(100),
  ip       VARCHAR2(15),
  protocol VARCHAR2(50),
  msg      VARCHAR2(1000),
  sql_code VARCHAR2(100),
  module   VARCHAR2(100),
  sqltext  VARCHAR2(1000),
  username VARCHAR2(30),
  language VARCHAR2(100)
)
;
create index ASSECO_TOOLS.IDX_SERVERERROR on ASSECO_TOOLS.SERVERERROR (DT);

prompt
prompt Creating table SESSIONS
prompt =======================
prompt
create table ASSECO_TOOLS.SESSIONS
(
  snap_date   DATE,
  sid         NUMBER(9),
  spid        NUMBER(9),
  osuser      VARCHAR2(30),
  machine     VARCHAR2(64),
  terminal    VARCHAR2(30),
  program     VARCHAR2(48),
  logontime   DATE,
  lastcallet  NUMBER(9),
  mem_used    NUMBER(9),
  mem_alloc   NUMBER(9),
  mem_max     NUMBER(9),
  username    VARCHAR2(30),
  client_info VARCHAR2(1000),
  status      VARCHAR2(8),
  sql_id      VARCHAR2(30)
)
;
create index ASSECO_TOOLS.IDX_SESSIONS_01 on ASSECO_TOOLS.SESSIONS (SNAP_DATE, PROGRAM);

prompt
prompt Creating table SESSIONS_OLD
prompt ===========================
prompt
create table ASSECO_TOOLS.SESSIONS_OLD
(
  snap_date   DATE,
  sid         NUMBER(9),
  spid        NUMBER(9),
  osuser      VARCHAR2(30),
  machine     VARCHAR2(64),
  terminal    VARCHAR2(30),
  program     VARCHAR2(48),
  logontime   DATE,
  lastcallet  NUMBER(9),
  mem_used    NUMBER(9),
  mem_alloc   NUMBER(9),
  mem_max     NUMBER(9),
  username    VARCHAR2(30),
  client_info VARCHAR2(1000)
)
;

prompt
prompt Creating table STATISTICS_CMD
prompt =============================
prompt
create table ASSECO_TOOLS.STATISTICS_CMD
(
  id          NUMBER(9),
  schema_name VARCHAR2(30),
  num_rows    NUMBER(12),
  cmd         VARCHAR2(1000),
  file_id     NUMBER(3)
)
;

prompt
prompt Creating table STATISTICS_INFO
prompt ==============================
prompt
create table ASSECO_TOOLS.STATISTICS_INFO
(
  id            NUMBER(9),
  user_schema   VARCHAR2(30),
  dt            DATE,
  t             VARCHAR2(30),
  p             VARCHAR2(30),
  s             VARCHAR2(30),
  reason        VARCHAR2(512),
  internal_code NUMBER(9),
  tempt         VARCHAR2(3),
  num_rows      NUMBER(16)
)
;

prompt
prompt Creating table TEMP_USAGE
prompt =========================
prompt
create table ASSECO_TOOLS.TEMP_USAGE
(
  id          NUMBER(9) not null,
  mb_in_temp  NUMBER(14),
  dt          DATE,
  session_sid NUMBER,
  serial#     NUMBER,
  username    VARCHAR2(30),
  status      VARCHAR2(30),
  schemaname  VARCHAR2(30),
  osuser      VARCHAR2(60),
  process     VARCHAR2(30),
  machine     VARCHAR2(64),
  terminal    VARCHAR2(64),
  program     VARCHAR2(64),
  event       VARCHAR2(128),
  sql_text    CLOB,
  is_active   CHAR(1) default 'Y'
)
nologging;
create index ASSECO_TOOLS.IDX_DT on ASSECO_TOOLS.TEMP_USAGE (DT);
alter table ASSECO_TOOLS.TEMP_USAGE
  add constraint PK_TEMP_USAGE primary key (ID);

prompt
prompt Creating sequence SQ_HOLDING_LOCKS_BINDS_ID
prompt ===========================================
prompt
create sequence ASSECO_TOOLS.SQ_HOLDING_LOCKS_BINDS_ID
minvalue 1
maxvalue 999999999
start with 61955244
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_HOLDING_LOCKS_DETAIL_ID
prompt ============================================
prompt
create sequence ASSECO_TOOLS.SQ_HOLDING_LOCKS_DETAIL_ID
minvalue 1
maxvalue 999999999
start with 23909042
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_HOLDING_LOCKS_ID
prompt =====================================
prompt
create sequence ASSECO_TOOLS.SQ_HOLDING_LOCKS_ID
minvalue 1
maxvalue 999999999
start with 434783
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_IOSTAT
prompt ===========================
prompt
create sequence ASSECO_TOOLS.SQ_IOSTAT
minvalue 1
maxvalue 999999999
start with 3501085
increment by 1
cache 2;

prompt
prompt Creating sequence SQ_OBJ_INFO_ID
prompt ================================
prompt
create sequence ASSECO_TOOLS.SQ_OBJ_INFO_ID
minvalue 1
maxvalue 999999999
start with 30921
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_POOR_PARSE_ID
prompt ==================================
prompt
create sequence ASSECO_TOOLS.SQ_POOR_PARSE_ID
minvalue 1
maxvalue 999999999
start with 9941
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_ACTSESSHIS_TXTM
prompt ========================================
prompt
create sequence ASSECO_TOOLS.SQ_REP_ACTSESSHIS_TXTM
minvalue 1
maxvalue 999999999
start with 906721
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_AWRS
prompt =============================
prompt
create sequence ASSECO_TOOLS.SQ_REP_AWRS
minvalue 1
maxvalue 999999999
start with 1
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_CPULOAD
prompt ================================
prompt
create sequence ASSECO_TOOLS.SQ_REP_CPULOAD
minvalue 1
maxvalue 999999999
start with 45841
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_JOBS
prompt =============================
prompt
create sequence ASSECO_TOOLS.SQ_REP_JOBS
minvalue 1
maxvalue 999999999
start with 5661
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_TOPSQL
prompt ===============================
prompt
create sequence ASSECO_TOOLS.SQ_REP_TOPSQL
minvalue 1
maxvalue 999999999
start with 145861
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_REP_WAITCLASS
prompt ==================================
prompt
create sequence ASSECO_TOOLS.SQ_REP_WAITCLASS
minvalue 1
maxvalue 999999999
start with 24261
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_ROWSCNT
prompt ============================
prompt
create sequence ASSECO_TOOLS.SQ_ROWSCNT
minvalue 1
maxvalue 999999999
start with 10365
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_ROWSCNT_LOG_ID
prompt ===================================
prompt
create sequence ASSECO_TOOLS.SQ_ROWSCNT_LOG_ID
minvalue 1
maxvalue 999999999
start with 13982
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_ROWSCNT_MAIN_ID
prompt ====================================
prompt
create sequence ASSECO_TOOLS.SQ_ROWSCNT_MAIN_ID
minvalue 1
maxvalue 999999999
start with 16629
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_ROWSCNT_SCHEMAS
prompt ====================================
prompt
create sequence ASSECO_TOOLS.SQ_ROWSCNT_SCHEMAS
minvalue 1
maxvalue 999999999
start with 41
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_ROWSCNT_TAB_ID
prompt ===================================
prompt
create sequence ASSECO_TOOLS.SQ_ROWSCNT_TAB_ID
minvalue 1
maxvalue 999999999
start with 2634997
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_STATISTICS_INFO_ID
prompt =======================================
prompt
create sequence ASSECO_TOOLS.SQ_STATISTICS_INFO_ID
minvalue 1
maxvalue 999999999
start with 8301
increment by 1
cache 20;

prompt
prompt Creating sequence SQ_TEMP_USAGE_ID
prompt ==================================
prompt
create sequence ASSECO_TOOLS.SQ_TEMP_USAGE_ID
minvalue 1
maxvalue 999999999
start with 4461129
increment by 1
cache 20;

prompt
prompt Creating synonym KILL_SESS
prompt ==========================
prompt
create or replace synonym ASSECO_TOOLS.KILL_SESS
  for SYS.KILL_SESSION;

prompt
prompt Creating synonym KILL_SESSION_TABLE
prompt ===================================
prompt
create or replace synonym ASSECO_TOOLS.KILL_SESSION_TABLE
  for SYS.KILL_SESSION_TABLE;

prompt
prompt Creating package STATS
prompt ======================
prompt
CREATE OR REPLACE PACKAGE ASSECO_TOOLS.STATS IS

  -- Author  : ARTUR.BALON
  -- Created : 22.02.2018 14:54:07
  PROCEDURE CHECK_STATS(P_OWNER IN VARCHAR2);
  PROCEDURE CHECK_TAB_MOD(P_OWNER         IN VARCHAR2,
                          P_TABLE_NAME    IN VARCHAR2,
                          P_NUM_ROWS      IN NUMBER,
                          P_LAST_ANALYZED DATE,
                          P_NEED_STATS    IN OUT NUMBER,
                          P_REASON        IN OUT VARCHAR2);
  PROCEDURE CHECK_PART_MOD(P_OWNER          IN VARCHAR2,
                           P_TABLE_NAME     IN VARCHAR2,
                           P_PARTITION_NAME IN VARCHAR2,
                           P_NUM_ROWS       IN NUMBER,
                           P_LAST_ANALYZED  DATE,
                           P_NEED_STATS     IN OUT NUMBER,
                           P_REASON         IN OUT VARCHAR2);
  PROCEDURE CHECK_SUB_MOD(P_OWNER             IN VARCHAR2,
                          P_TABLE_NAME        IN VARCHAR2,
                          P_PARTITION_NAME    IN VARCHAR2,
                          P_SUBPARTITION_NAME IN VARCHAR2,
                          P_NUM_ROWS          IN NUMBER,
                          P_LAST_ANALYZED     DATE,
                          P_NEED_STATS        IN OUT NUMBER,
                          P_REASON            IN OUT VARCHAR2);
  PROCEDURE CHECK_COLL_STATS_MISS(P_OWNER      IN VARCHAR2,
                                  P_TABLE_NAME IN VARCHAR2,
                                  P_NEED_STATS IN OUT NUMBER,
                                  P_REASON     IN OUT VARCHAR2);
  PROCEDURE CHECK_COLL_STATS(P_OWNER         IN VARCHAR2,
                             P_TABLE_NAME    IN VARCHAR2,
                             P_NUM_ROWS      IN NUMBER,
                             P_LAST_ANALYZED DATE,
                             P_NEED_STATS    IN OUT NUMBER,
                             P_REASON        IN OUT VARCHAR2);
  PROCEDURE CHECK_IND_STATS(P_OWNER         IN VARCHAR2,
                            P_TABLE_NAME    IN VARCHAR2,
                            P_NUM_ROWS      IN NUMBER,
                            P_LAST_ANALYZED DATE,
                            P_NEED_STATS    IN OUT NUMBER,
                            P_REASON        IN OUT VARCHAR2);
  PROCEDURE GEN_TAB_LVL_CMD(P_DEGREE NUMBER, P_NUM_FILES NUMBER);
  PROCEDURE GEN_PART_LVL_CMD(P_DEGREE    NUMBER,
                             P_NUM_FILES NUMBER,
                             P_EXEC_TYPE VARCHAR2);
  PROCEDURE GEN_SUB_LVL_CMD(P_DEGREE    NUMBER,
                            P_NUM_FILES NUMBER,
                            P_EXEC_TYPE VARCHAR2);
  PROCEDURE SPLIT_STATS_CMD(P_OWNER     VARCHAR2,
                            P_DEGREE    NUMBER,
                            P_NUM_FILES NUMBER);
  PROCEDURE RUN_STATS_TAB_LVL(P_DEGREE NUMBER);
END STATS;
/

prompt
prompt Creating type SIZETABLETYPE
prompt ===========================
prompt
create or replace type asseco_tools.sizeTableType as object
 (
   tablename      varchar2(30),
   tablesize      number(16),
   tableindexsize number(16),
   avg_row_len    number(9),
   num_rows       number(16)
 )
/

prompt
prompt Creating function F_SIZE_SCHEMA_DETAIL
prompt ======================================
prompt
CREATE OR REPLACE FUNCTION ASSECO_TOOLS.F_SIZE_SCHEMA_DETAIL(p_owner IN VARCHAR2, p_tablename VARCHAR2) RETURN sizeTableType IS

  v_tab_bytes     NUMBER(19);
  v_num_rows      NUMBER(16);
  v_avg_row_len   NUMBER(9);
  v_tab_lob_bytes NUMBER(19);
  v_ind_bytes     NUMBER(19);
  v_ind_lob_bytes NUMBER(19);

  v_tab_size NUMBER(19);
  v_ind_size NUMBER(19);

BEGIN
  dbms_application_info.set_client_info(client_info => p_tablename);
  SELECT NUM_ROWS, AVG_ROW_LEN
    INTO v_num_rows, v_avg_row_len
    FROM dba_tables t
   WHERE table_name = p_tablename
     AND owner = p_owner;

  SELECT SUM(bytes)
    INTO v_tab_bytes
    FROM dba_segments
   WHERE segment_name = p_tablename
     AND owner = p_owner;

  SELECT SUM(s.bytes)
    INTO v_tab_lob_bytes
    FROM dba_lobs l, dba_segments s
   WHERE s.segment_name = l.segment_name
     AND s.owner = l.owner
     AND s.segment_type = 'LOBSEGMENT'
     AND l.table_name = p_tablename
     AND s.owner = p_owner
     AND l.OWNER = p_owner;

  SELECT SUM(s.bytes)
    INTO v_ind_bytes
    FROM dba_indexes i, dba_segments s
   WHERE s.segment_name = i.index_name
     AND s.owner = i.owner
     AND s.segment_type = 'INDEX'
     AND i.table_name = p_tablename
     AND s.owner = p_owner
     AND i.OWNER = p_owner;

  SELECT SUM(s.bytes)
    INTO v_ind_lob_bytes
    FROM dba_lobs l, dba_segments s
   WHERE s.segment_name = l.index_name
     AND s.owner = l.owner
     AND s.segment_type = 'LOBINDEX'
     AND l.table_name = p_tablename
     AND s.owner = p_owner
     AND l.OWNER = p_owner;

  v_tab_size := nvl(v_tab_bytes, 0) + nvl(v_tab_lob_bytes, 0);
  v_ind_size := nvl(v_ind_bytes, 0) + nvl(v_ind_lob_bytes, 0);

  RETURN sizeTableType(p_tablename, v_tab_size, v_ind_size, v_avg_row_len, v_num_rows);

END F_SIZE_SCHEMA_DETAIL;
/

prompt
prompt Creating procedure CLEAN_OLD_DATA
prompt =================================
prompt
create or replace procedure asseco_tools.CLEAN_OLD_DATA is
begin

  DELETE FROM SERVERERROR where DT < sysdate - 31;
  DELETE FROM DDL_ACTIONS where DT < sysdate - 180;
  DELETE FROM HOLDING_LOCKS where DT < sysdate - 31;
  DELETE FROM SESSIONS where SNAP_DATE < sysdate - 31;
  DELETE FROM TEMP_USAGE where DT < sysdate - 30;
  DELETE FROM IOSTAT where DT < sysdate - 90;
  DELETE FROM LOGS where DT < sysdate - 31;
  
end CLEAN_OLD_DATA;
/

prompt
prompt Creating procedure GATHER
prompt =========================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.GATHER(P_ID_SCHEMA NUMBER,
                                   P_SCHEMA    VARCHAR2,
                                   P_OPERATION VARCHAR2 DEFAULT 'GATHER') IS
  CURSOR C_ROWS_COUNT IS
    SELECT OWNER, TABLE_NAME, NUM_ROWS
      FROM DBA_TABLES
     WHERE OWNER = P_SCHEMA
       AND TEMPORARY = 'N';
  CR_ROWS_COUNT C_ROWS_COUNT%ROWTYPE;

  TYPE CURSOR_TYP IS REF CURSOR;
  C_GATHER CURSOR_TYP;

  V_ROWS_COUNT      NUMBER;
  V_QUERY           VARCHAR2(250);
  V_FOUND           BOOLEAN := FALSE;
  V_FOUND_STATS     NUMBER(5);
  V_TABLE_SIZE      NUMBER(14);
  V_DATA_SIZE       NUMBER(14);
  V_LOB_PART        NUMBER(14);
  V_LOB_SIZE        NUMBER(14);
  V_ID_SNAP         NUMBER(9);
  V_IS_VALID_SCHEMA NUMBER(1);
  V_ERROR           VARCHAR2(1000);

BEGIN
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'ASSECO GATHER PROCEDURE',
                                   action_name => 'GATHERING TABLE INFORMATION');
  EXECUTE IMMEDIATE 'ALTER SESSION SET NLS_DATE_FORMAT = "RRRR-MM-DD"';

  IF (P_SCHEMA is NULL) THEN
    DBMS_OUTPUT.PUT_LINE('EMPTY SCHEMA NAME');
    RETURN;
  ELSE
    SELECT COUNT(*)
      INTO V_IS_VALID_SCHEMA
      FROM DBA_USERS
     WHERE USERNAME = P_SCHEMA;
    IF V_IS_VALID_SCHEMA = 0 THEN
      DBMS_OUTPUT.PUT_LINE('SCHEMA NAME :' || P_SCHEMA ||
                           ' IS INVALID OR NO PERMISSION');
      RETURN;
    END IF;

  END IF;

  IF ((P_OPERATION IS NULL) OR
     (UPPER(P_OPERATION) NOT IN ('DELETE', 'GATHER', 'SHOW'))) THEN
    DBMS_OUTPUT.PUT_LINE('INVALID COMMAND! (ACCEPTED COMMANDS : DELETE,GATHER,SHOW)');
    RETURN;
  END IF;

  --
  -- DELETE GATHERED INFORMATIONS
  --

  DBMS_APPLICATION_INFO.SET_CLIENT_INFO(client_info => 'DELETE TABLE INFORMATION');
  IF (UPPER(P_OPERATION) = 'DELETE' AND P_SCHEMA IS NOT NULL) THEN
    DELETE FROM ROWSCNT
     WHERE ID_SCHEMA = P_ID_SCHEMA
       AND TRUNC(DT) = TRUNC(SYSDATE);

    IF SQL%FOUND THEN
      DBMS_OUTPUT.PUT_LINE('INFORMATION FOR USER :' || P_SCHEMA ||
                           ' HAS BEEN DELETED');
    ELSE
      DBMS_OUTPUT.PUT_LINE('NOTHING TO DELETE');
    END IF;

    COMMIT;
  END IF;

  --
  -- GATHER
  --

  IF (UPPER(P_OPERATION) = 'GATHER') THEN
    FOR ALL_USER_TABS IN (SELECT TABLE_NAME
                            FROM USER_TABLES
                           WHERE TABLE_NAME = 'ROWSCNT') LOOP
      V_FOUND := TRUE;

      --
      -- RUN TWICE LOCK
      --

      BEGIN
        SELECT COUNT(0)
          INTO V_FOUND_STATS
          FROM ROWSCNT
         WHERE ID_SCHEMA = P_ID_SCHEMA
           AND TRUNC(DT) = TRUNC(SYSDATE);

        IF V_FOUND_STATS > 0 THEN
          DBMS_OUTPUT.PUT_LINE('TODAY STATS FOR USER:' || P_SCHEMA ||
                               ' ALREDY EXIST. USE DELETE COMMAND');
          RETURN;
        END IF;

      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          DBMS_OUTPUT.PUT_LINE('GATHERING....');
      END;

      --
      -- SET SNAP_ID
      --

      BEGIN
        INSERT INTO ROWSCNT
          (DT, ID_SCHEMA)
        VALUES
          (sysdate, P_ID_SCHEMA)
        returning id into V_ID_SNAP;
        COMMIT;
      EXCEPTION
        WHEN OTHERS THEN
          V_ERROR := 'GENERATE SNAP_ID ERROR :' || substr(SQLERRM, 0, 1000);
          INSERT INTO ROWSCNT_LOG
            (ID_SNAP, INFO)
          VALUES
            (V_ID_SNAP, V_ERROR);
          V_ERROR := '';
          RETURN;
      END;

      --
      -- GATHER MAIN INFORMATIONS
      --

      BEGIN
        DBMS_APPLICATION_INFO.SET_CLIENT_INFO(client_info => 'GATHER MAIN INFORMATION');
        INSERT INTO ROWSCNT_MAIN
          (ID_SNAP, TYP, BYTES)
          SELECT V_ID_SNAP, segment_type, SUM(bytes)
            FROM dba_segments
           where owner = P_SCHEMA
           GROUP BY owner, segment_type;
        COMMIT;
      EXCEPTION
        WHEN OTHERS THEN
          V_ERROR := 'INSERT ERROR : ROWSCNT_MAIN :' ||
                     substr(SQLERRM, 0, 1000);
          INSERT INTO ROWSCNT_LOG
            (ID_SNAP, INFO)
          VALUES
            (V_ID_SNAP, V_ERROR);
          V_ERROR := '';
      END;

      --
      -- GATHER TABLE INFORMATIONS
      --

      FOR CR_ROWS_COUNT IN C_ROWS_COUNT LOOP
        DBMS_APPLICATION_INFO.SET_CLIENT_INFO(client_info => 'GATHER INFORMATION FOR TABLE :' ||
                                                             CR_ROWS_COUNT.OWNER || '.' ||
                                                             CR_ROWS_COUNT.TABLE_NAME);
        SELECT SUM(BYTES)
          INTO V_DATA_SIZE
          FROM DBA_SEGMENTS
         WHERE OWNER = P_SCHEMA
           AND SEGMENT_NAME = CR_ROWS_COUNT.TABLE_NAME;

        V_LOB_PART := 0;
        V_LOB_SIZE := 0;

        FOR LOB_CURSOR IN (SELECT SUM(S.BYTES) AS V_LOB_PART
                             FROM DBA_LOBS L, DBA_SEGMENTS S
                            WHERE L.OWNER = P_SCHEMA
                              AND S.OWNER = P_SCHEMA
                              AND L.SEGMENT_NAME = S.SEGMENT_NAME
                              AND L.TABLE_NAME = CR_ROWS_COUNT.TABLE_NAME
                            GROUP BY L.TABLE_NAME) LOOP

          V_LOB_SIZE := V_LOB_SIZE + LOB_CURSOR.V_LOB_PART;

        END LOOP;

        V_TABLE_SIZE := V_DATA_SIZE + V_LOB_SIZE;

        V_QUERY := 'SELECT COUNT(*) FROM ' || CR_ROWS_COUNT.OWNER || '.' ||
                   CR_ROWS_COUNT.TABLE_NAME;

        BEGIN

          OPEN C_GATHER FOR V_QUERY;
          FETCH C_GATHER
            INTO V_ROWS_COUNT;
          INSERT INTO ROWSCNT_TAB
            (ID_SNAP, TABLE_NAME, NUM_ROWS, BYTES)
          VALUES
            (V_ID_SNAP,
             CR_ROWS_COUNT.TABLE_NAME,
             V_ROWS_COUNT,
             V_TABLE_SIZE);
        EXCEPTION
          WHEN OTHERS THEN
            V_ERROR := 'INSERT INTO ROWSCNT_TAB ERROR :' ||
                       substr(SQLERRM, 0, 1000);
            INSERT INTO ROWSCNT_LOG
              (ID_SNAP, INFO)
            VALUES
              (V_ID_SNAP, V_ERROR);
            V_ERROR := '';
        END;

        EXIT WHEN C_GATHER%NOTFOUND;
        CLOSE C_GATHER;
        commit;
      END LOOP;
      COMMIT;
      V_ERROR := 'GATHER TABLE INFORMATION FOR USER :' || P_SCHEMA ||
                 ' HAS BEEN COMPLETED';
      INSERT INTO ROWSCNT_LOG (ID_SNAP, INFO) VALUES (V_ID_SNAP, V_ERROR);
      V_ERROR := '';

    END LOOP;
    IF NOT V_FOUND THEN
      V_ERROR := 'MAIN SPDB TABLE NOT EXIST';
      INSERT INTO ROWSCNT_LOG (ID_SNAP, INFO) VALUES (V_ID_SNAP, V_ERROR);
      V_ERROR := '';

    END IF;
  END IF;

  --
  -- SHOW GAIN INFORMATION
  --

  IF (UPPER(P_OPERATION) = 'SHOW') THEN
    DBMS_OUTPUT.ENABLE(1000000);
    DBMS_OUTPUT.PUT_LINE('REMEMBER ABOUT ACTUAL STATISTICS!');
    FOR SHOW_CURSOR IN (SELECT R.TABLE_NAME,
                               (MAX(NVL(R.NUM_ROWS, 0)) -
                               MIN(NVL(R.NUM_ROWS, 0))) AS DIFF_ROWS,
                               (MAX(NVL(R.NUM_ROWS, 0)) -
                               MIN(NVL(R.NUM_ROWS, 0))) *
                               NVL(T.AVG_ROW_LEN, 0) AS DIFF_SIZE,
                               (MAX(NVL(R.BYTES, 0)) - MIN(NVL(R.BYTES, 0))) AS DIFF_BYTES
                          FROM ROWSCNT_TAB R, DBA_TABLES T
                         WHERE T.OWNER = P_SCHEMA
                           AND R.TABLE_NAME = T.TABLE_NAME(+)
                           AND T.TEMPORARY = 'N'
                         GROUP BY R.TABLE_NAME, T.AVG_ROW_LEN
                        HAVING(MAX(NVL(R.NUM_ROWS, 0)) - MIN(NVL(R.NUM_ROWS, 0))) > 0
                         ORDER BY (MAX(NVL(R.NUM_ROWS, 0)) -
                                  MIN(NVL(R.NUM_ROWS, 0))) DESC) LOOP
      DBMS_OUTPUT.PUT_LINE(SHOW_CURSOR.TABLE_NAME || '; ROWS DIFF : ' ||
                           SHOW_CURSOR.DIFF_ROWS || '; GROWTH  : ' ||
                           SHOW_CURSOR.DIFF_SIZE || ' BYTES DIFF : ' ||
                           SHOW_CURSOR.DIFF_BYTES || ' BYTES');
    END LOOP;
    COMMIT;
  END IF;

  DBMS_APPLICATION_INFO.SET_MODULE(module_name => NULL,
                                   action_name => NULL);
  DBMS_APPLICATION_INFO.SET_CLIENT_INFO(client_info => NULL);

END GATHER;
/

prompt
prompt Creating procedure DELETE_GATHER_ALL
prompt ====================================
prompt
create or replace procedure asseco_tools.DELETE_GATHER_ALL is

  cursor c_schemas is
    select id,schema_name from rowscnt_schemas;
  cr_schemas c_schemas%rowtype;

begin
  /*
  Kasowanie statystyk dla wyszczególnionych schematow - tylko z dnia bierzacego
  */
  FOR cr_schemas IN c_schemas LOOP
    gather(P_ID_SCHEMA =>cr_schemas.id , P_SCHEMA => cr_schemas.schema_name, P_OPERATION => 'GATHER');
  END LOOP;

end DELETE_GATHER_ALL;
/

prompt
prompt Creating procedure GATHER_ALL
prompt =============================
prompt
create or replace procedure asseco_tools.GATHER_ALL is

  cursor c_schemas is
    select id, schema_name from rowscnt_schemas;
  cr_schemas c_schemas%rowtype;

begin
  /*
  Zbieranie statystyk dla wyszczegolnionych schematow
  */
  FOR cr_schemas IN c_schemas LOOP
    gather( P_ID_SCHEMA =>cr_schemas.id, P_SCHEMA => cr_schemas.schema_name, P_OPERATION => 'GATHER');
  END LOOP;

end GATHER_ALL;
/

prompt
prompt Creating procedure GATHER_IOSTAT
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.GATHER_IOSTAT IS
   V_ERROR VARCHAR2(1000);

   v_filetype_name VARCHAR2(64);
   v_filetype_id   NUMBER(16);
   v_srmb          NUMBER(16);
   v_swmb          NUMBER(16);
   v_lrmb          NUMBER(16);
   v_lwmb          NUMBER(16);

   cursor c2(v_filetype number) is
      select id, dt, filetype_name, filetype_id, srmb, swmb, lrmb, lwmb
        from iostat
       where id =
             (select max(id) from iostat where filetype_id = v_filetype);

   rc2 c2%rowtype;

   type fid_type is table of number;
   ft fid_type := fid_type(2, 3, 6);
   -- 2 data file
   -- 3 log file
   -- 6 temp file
BEGIN
   BEGIN
      FOR i IN 1 .. ft.count LOOP
         dbms_output.put_line(ft(i));

         OPEN C2(ft(i));
         LOOP
            BEGIN
               select f.FILETYPE_NAME, f.FILETYPE_ID,sum(f.SMALL_READ_MEGABYTES),sum(f.SMALL_WRITE_MEGABYTES), sum(f.LARGE_READ_MEGABYTES),sum(f.LARGE_WRITE_MEGABYTES)
                 into v_filetype_name,v_filetype_id,v_srmb,v_swmb,v_lrmb,v_lwmb
                 from v$iostat_file f
                where filetype_id = ft(i)
                group by f.FILETYPE_NAME, f.FILETYPE_ID;

               FETCH C2
                  into RC2;
               IF C2%FOUND THEN
                  insert into iostat
                     (dt,filetype_name,filetype_id,srmb,swmb,lrmb,lwmb,diffsrmb,diffswmb,difflrmb,difflwmb)
                  values
                     (sysdate,v_filetype_name,v_filetype_id,v_srmb,v_swmb,v_lrmb,v_lwmb,v_srmb - RC2.srmb,v_swmb - RC2.swmb,v_lrmb - RC2.lrmb,v_lwmb - RC2.lwmb);
                  EXIT;
               ELSE
                  insert into iostat
                     (dt,filetype_name,filetype_id,srmb,swmb,lrmb,lwmb,diffsrmb,diffswmb,difflrmb,difflwmb)
                  values
                     (sysdate,v_filetype_name,v_filetype_id, v_srmb,v_swmb,v_lrmb,v_lwmb,0,0,0,0);
                  EXIT;
               END IF;
            EXCEPTION
            WHEN OTHERS THEN
               V_ERROR := 'INSERT ERROR : IOSTAT : FOR VALUE:'|| ft(i)||' >'|| substr(SQLERRM, 0, 1000);
               INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
               EXIT;
            END;
         END LOOP;
         CLOSE C2;
      END LOOP;
   end;

   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : IOSTAT :' || substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure KILL_SESSION
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.KILL_SESSION(p_sid     IN NUMBER,
                                         p_serial# IN NUMBER,
                                         p_out     varchar2) IS
begin

  sys.kill_session(p_sid => p_sid, p_serial# => p_serial#);

end KILL_SESSION;
/

prompt
prompt Creating procedure MISSING_FK_INDEXES
prompt =====================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.MISSING_FK_INDEXES(P_SCHEMA IN VARCHAR2) IS

  v_column          VARCHAR2(64);
  v_pl_foreign_key  VARCHAR2(2000);
  v_pl_ind_column   VARCHAR2(64);
  v_pl_ind_name     VARCHAR2(128);
  v_pl_ind_owner    VARCHAR2(64);
  v_pl_index        VARCHAR2(2000);
  v_f_owner         VARCHAR2(64);
  v_f_table_name    VARCHAR2(64);
  v_table_exists    VARCHAR2(1);
  v_output          VARCHAR2(2000);
  v_index_name_part VARCHAR2(64);

-- Ported fom PlSql Block script to procedure. Source support.oracle.com
/*
   Cursor c1 simply selects each Foreign Key constraint from the
   DBA View DBA_CONSTRAINTS. No need at this stage to limit the
   query to 'ENABLED' constraints, we'll simply report the status
   in the log file. For each constraint, we'll construct the column list,
   using cursor c2, which combine to form the foreign key constraint
   returned in cursor c1
*/

  CURSOR c1 IS
    SELECT constraint_name, owner, table_name, status, r_owner, r_constraint_name
      FROM dba_constraints
     WHERE constraint_type = 'R'
       AND owner = P_SCHEMA
     ORDER BY owner;

  CURSOR c2(cons_name VARCHAR2, cons_owner VARCHAR2) IS
    SELECT column_name
      FROM dba_cons_columns
     WHERE constraint_name = cons_name
       AND owner = cons_owner
     ORDER BY dba_cons_columns.position;

/*
   For each returned constraint, we need to fins a matching index, firstly
   we fetch each index name with c3, and then construct the index columns
   with cursor c4 in their correct order until we find a match with the
   foreign key constraint
*/

  CURSOR c3(ind_table VARCHAR2, tab_owner VARCHAR2) IS
    SELECT index_name, owner
      FROM dba_indexes
     WHERE table_name = ind_table
       AND table_owner = tab_owner;
  CURSOR c4(ind_name VARCHAR2, ind_owner VARCHAR2) IS
    SELECT column_name
      FROM dba_ind_columns
     WHERE INDEX_NAME = ind_name
       AND INDEX_OWNER = ind_owner
     ORDER BY dba_ind_columns.column_position;
  CURSOR c5(for_owner VARCHAR2, for_constraint VARCHAR2) IS
    SELECT owner, table_name
      FROM dba_constraints
     WHERE OWNER = for_owner
       AND CONSTRAINT_NAME = for_constraint;
BEGIN
  SELECT COUNT(*) INTO v_table_exists FROM user_tables WHERE table_name = 'MISSING_FK_INDEXES_TABLE';
  IF v_table_exists = 0 THEN
    -- CREATE TABLE MISSING_FK_INDEXES_TABLE(owner VARCHAR2(30), constraint_name VARCHAR2(30), status VARCHAR2(8), table_name VARCHAR2(30), foreign_key VARCHAR2(2000), info varchar2(2000));
    RETURN;
  ELSE
    DELETE FROM MISSING_FK_INDEXES_TABLE;
  END IF;

  FOR c1_rec IN c1 LOOP
    v_output := '';
    /* looping for each foreign key constraint */
    v_column         := NULL;
    v_pl_ind_column  := NULL;
    v_pl_ind_name    := NULL;
    v_pl_ind_owner   := NULL;
    v_pl_index       := NULL;
    v_f_owner        := NULL;
    v_F_table_name   := NULL;
    OPEN c5(c1_rec.r_owner, c1_rec.r_constraint_name);
    FETCH c5
      INTO v_f_owner, v_f_table_name;
    CLOSE c5;
    OPEN c2(c1_rec.constraint_name, c1_rec.owner);
    FETCH c2
      INTO v_column;
    v_pl_foreign_key := v_column; -- the first col in the foreign key
    <<constraint_names>>
    LOOP
      /*
       constructing the foreign key columns, delimiting each column with a ','
      */
      FETCH c2
        INTO v_column;
      EXIT WHEN c2%NOTFOUND;
      v_pl_foreign_key := v_pl_foreign_key || ',' || v_column;
    END LOOP constraint_names;
    /*
    we now have a table and foreign key definition for which we need an index */
    CLOSE c2;
    OPEN c3(c1_rec.table_name, c1_rec.owner);
    <<index_name>>
    LOOP
      /* for each index found for this table */
      FETCH c3
        INTO v_pl_ind_name, v_pl_ind_owner;
      EXIT WHEN c3%NOTFOUND;
      OPEN c4(v_pl_ind_name, v_pl_ind_owner);
      FETCH c4
        INTO v_pl_ind_column;
      v_pl_index := v_pl_ind_column; -- the first column in the index
      IF v_pl_index = v_pl_foreign_key THEN
        -- check this doesn't already match
        CLOSE c4; -- the foreign key
        EXIT index_name;
      END IF;
      IF v_pl_index = SUBSTR(v_pl_foreign_key, 1, LENGTH(v_pl_index)) THEN
        <<index_columns>>
        LOOP
          FETCH c4
            INTO v_pl_ind_column;
          EXIT WHEN c4%NOTFOUND;
          v_pl_index := v_pl_index || ',' || v_pl_ind_column;
          IF v_pl_index = v_pl_foreign_key THEN
            CLOSE c4;
            EXIT index_name;
          END IF;
          IF v_pl_index != SUBSTR(v_pl_foreign_key, 1, LENGTH(v_pl_index)) THEN
            EXIT index_columns;
          END IF;
        END LOOP index_columns;
      END IF;
      CLOSE c4;
    END LOOP index_name;
    CLOSE c3;
    IF v_pl_index != v_pl_foreign_key OR v_pl_index IS NULL THEN
      v_output := v_output || ('Constraint  ' || c1_rec.constraint_name || '(' || c1_rec.status || ') : Changing data in table ' || v_f_owner || '.' || v_f_table_name || ' will lock table ' ||
                  c1_rec.owner || '.' || c1_rec.table_name) || chr(13);
      v_output := v_output || 'Create index for table '||c1_rec.owner||'.'||c1_rec.table_name||' on columns '|| v_pl_foreign_key || chr(13)|| chr(10);

      SELECT to_char(current_timestamp, 'SSFF') INTO v_index_name_part FROM dual;

     -- v_output := v_output ||'-->> CREATE INDEX ' || c1_rec.owner || '.' || 'IDX_AUMS_' || v_index_name_part || ' on ' || c1_rec.owner || '.' || v_f_table_name || ' (' ||
     --             v_pl_foreign_key || ')' || chr(13);

      INSERT INTO MISSING_FK_INDEXES_TABLE VALUES (c1_rec.owner, c1_rec.constraint_name, c1_rec.status, c1_rec.table_name, v_pl_foreign_key, v_output);

      COMMIT;
    END IF;
  END LOOP;
commit;
END MISSING_FK_INDEXES;
/

prompt
prompt Creating procedure MONITOR_LOCKS
prompt ================================
prompt
create or replace procedure asseco_tools.MONITOR_LOCKS(p_sec_in_wait number) is

  cursor clock is
    select sid,
           serial#,
           s.OSUSER,
           s.MACHINE,
           s.PROGRAM,
           l.mode_held,
           l.lock_type,
           l.lock_id1,
           a.SQL_ID,
           s.LAST_CALL_ET,
           s.CLIENT_INFO,
           s.status
      from v$session s, dba_blockers b, dba_lock l, v$sql a
     where s.SID = b.holding_session
       and b.holding_session = l.session_id
       and l.blocking_others = 'Blocking'
       and s.SQL_ID = a.sql_id(+);

  crlock clock%rowtype;

  cursor clockdetail(pc_sid number) is
    SELECT s.sid,
           serial#,
           s.OSUSER,
           s.MACHINE,
           s.PROGRAM,
           s.SECONDS_IN_WAIT,
           s.SQL_ID,
           s.CLIENT_INFO,
           s.ROW_WAIT_OBJ#,
           s.EVENT,
           s.status
      FROM v$session s, dba_waiters b
     WHERE s.SID = b.waiting_session
       AND b.holding_session = pc_sid
     GROUP BY s.sid,
              serial#,
              s.OSUSER,
              s.MACHINE,
              s.PROGRAM,
              s.SECONDS_IN_WAIT,
              s.SQL_ID,
              s.CLIENT_INFO,
              s.ROW_WAIT_OBJ#,
              s.EVENT,
              s.status;
  crlockdetail clockdetail%rowtype;

  cursor ctables(ps_sid number) is
    select l.sid, o.OBJECT_NAME, l.CTIME
      from v$lock l, dba_objects o
     where l.ID1 = o.OBJECT_ID
       and l.sid = ps_sid;

  crtables ctables%rowtype;

  cursor clocksbinds(ps_sid number) is
    select s.sid,
           s.username,
           s.sql_hash_value,
           s.sql_id,
           s.sql_child_number,
           spc.name,
           spc.value_string,
           last_captured
      from v$sql_bind_capture spc, v$session s, v$sql sq
     where s.sql_hash_value = spc.hash_value
       and s.sql_address = spc.address
       and sq.sql_id = s.sql_id
       and spc.was_captured = 'YES'
       and s.type <> 'BACKGROUND'
       and s.status = 'ACTIVE'
       and s.SID = ps_sid;

  crlocksbinds clocksbinds%rowtype;

  v_licznik_bokujacych  number(9) := 0;
  v_licznik_czekajacych number(9) := 0;
  v_snap_date           date;
  v_error               VARCHAR2(1000);
  v_sq_id               NUMBER(9);
  v_info                VARCHAR2(500);

BEGIN
  select sysdate into v_snap_date from dual;
  FOR crlock in clock LOOP
    v_licznik_bokujacych := v_licznik_bokujacych + 1;
    FOR crlockdetail in clockdetail(crlock.sid) LOOP
      IF crlockdetail.seconds_in_wait > p_sec_in_wait THEN
        v_licznik_czekajacych := v_licznik_czekajacych + 1;

        v_info := 'waiting users:' || v_licznik_czekajacych || ' ' ||
                  'ROW_WAIT_OBJ(' || crlockdetail.ROW_WAIT_OBJ# ||
                  ') EVENT (' || crlockdetail.event || ')';

        insert into holding_locks
          (dt,
           info,
           blo_sid,
           blo_osuser,
           blo_program,
           blo_machine,
           blo_last_call_et,
           blo_client_info,
           wai_sid,
           wai_osuser,
           wai_program,
           wai_machine,
           wai_wait_time,
           wai_sql_id,
           wai_client_info,
           blo_sql_id,
           blo_status,
           wai_status)
        values
          (v_snap_date,
           v_info,
           crlock.sid,
           crlock.osuser,
           crlock.program,
           crlock.machine,
           crlock.last_call_et,
           crlock.client_info,
           crlockdetail.sid,
           crlockdetail.osuser,
           crlockdetail.program,
           crlockdetail.machine,
           crlockdetail.seconds_in_wait,
           crlockdetail.sql_id,
           crlockdetail.client_info,
           crlock.sql_id,
           crlock.status,
           crlockdetail.status)
        returning ID into v_sq_id;
        --blocker
        FOR crtables in ctables(crlock.sid) LOOP
          insert into holding_locks_detail
            (id_detail, sid, table_name, ctime)
          values
            (v_sq_id, crtables.sid, crtables.object_name, crtables.ctime);

        END LOOP;

        --waiter
        FOR crtables in ctables(crlockdetail.sid) LOOP
          insert into holding_locks_detail
            (id_detail, sid, table_name, ctime)
          values
            (v_sq_id, crtables.sid, crtables.object_name, crtables.ctime);

          FOR crlocksbinds IN clocksbinds(crlockdetail.sid) LOOP
            insert into holding_locks_binds
              (id_detail,
               sid,
               user_name,
               sql_hash_value,
               sql_id,
               sql_child_number,
               name,
               value_string,
               last_captured)
            values
              (v_sq_id,
               crlocksbinds.sid,
               crlocksbinds.username,
               crlocksbinds.sql_hash_value,
               crlocksbinds.sql_id,
               crlocksbinds.sql_child_number,
               crlocksbinds.name,
               crlocksbinds.value_string,
               crlocksbinds.last_captured);

          END LOOP;

        END LOOP;

      END IF;
    END LOOP;
  END LOOP;

  commit;
exception
  when others then
    v_error := 'HOLDING_LOCKS: ' || substr(SQLERRM, 0, 2000);
    insert into logs (dt, info) values (sysdate, v_error);
end MONITOR_LOCKS;
/

prompt
prompt Creating procedure MONITOR_TEMP_USAGE
prompt =====================================
prompt
create or replace procedure asseco_tools.MONITOR_TEMP_USAGE is

  V_ERROR varchar2(1000);
  V_DT    date;

begin
  V_DT := sysdate;

  --
  -- Aktywne
  --

  insert into temp_usage
    (mb_in_temp,
     dt,
     session_sid,
     serial#,
     username,
     status,
     schemaname,
     osuser,
     process,
     machine,
     terminal,
     program,
     event,
     sql_text,
     is_active)
    select sum(round(b.blocks * 8192 / 1024 / 1024)) as mb_in_temp,
           v_dt,
           a.sid,
           a.serial#,
           a.USERNAME,
           a.status,
           a.SCHEMANAME,
           a.OSUSER,
           a.PROCESS,
           a.MACHINE,
           a.TERMINAL,
           a.PROGRAM,
           a.EVENT,
           r.SQL_TEXT,
           'Y'
      from V$SESSION a, V$SORT_USAGE b, v$sqlarea r
     where a.saddr = b.session_addr
       and a.SQL_ID = r.SQL_ID
       and round(b.blocks * 8192 / 1024 / 1024) > 1
     group by a.sid,
              a.SERIAL#,
              a.USERNAME,
              a.status,
              a.SCHEMANAME,
              a.OSUSER,
              a.PROCESS,
              a.MACHINE,
              a.TERMINAL,
              a.PROGRAM,
              a.EVENT,
              r.SQL_TEXT
     order by MB_in_temp desc;
     --
     -- Wszystkie
     --
       insert into temp_usage
    (mb_in_temp,
     dt,
     session_sid,
     serial#,
     username,
     status,
     schemaname,
     osuser,
     process,
     machine,
     terminal,
     program,
     event,
     sql_text,
     is_active)
    select sum(round(b.blocks * 8192 / 1024 / 1024)) as mb_in_temp,
           v_dt,
           a.sid,
           a.serial#,
           a.USERNAME,
           a.status,
           a.SCHEMANAME,
           a.OSUSER,
           a.PROCESS,
           a.MACHINE,
           a.TERMINAL,
           a.PROGRAM,
           a.EVENT,
           null,
           'N'
      from V$SESSION a, V$SORT_USAGE b
     where a.saddr = b.session_addr
       and round(b.blocks * 8192 / 1024 / 1024) > 1
     group by a.sid,
              a.SERIAL#,
              a.USERNAME,
              a.status,
              a.SCHEMANAME,
              a.OSUSER,
              a.PROCESS,
              a.MACHINE,
              a.TERMINAL,
              a.PROGRAM,
              a.EVENT
     order by MB_in_temp desc;

  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    V_ERROR := substr(SQLERRM, 0, 1000);
    insert into logs (dt, info) values (sysdate, v_ERROR);

end MONITOR_TEMP_USAGE;
/

prompt
prompt Creating procedure OBJ_INFO
prompt ===========================
prompt
create or replace procedure asseco_tools.OBJ_INFO(p_owner varchar2) is

  v_owner varchar2(64);
  cursor c_obj(p_owner varchar2) is
    select o.OWNER, o.OBJECT_NAME, o.CREATED
      from dba_objects o
     where o.OWNER = p_owner
       and o.OBJECT_TYPE in ('TABLE')
       and o.TEMPORARY = 'N';
  cr_obj c_obj%rowtype;

  cursor c_tab(p_owner varchar2, p_table_name varchar2) is
    select t.OWNER, t.TABLE_NAME, t.NUM_ROWS
      from dba_tables t
     where t.OWNER = p_owner
       and t.TABLE_NAME = p_table_name;
  cr_tab c_tab%rowtype;

  cursor c_indx(p_owner varchar2, p_table_name varchar2) is
    select i.OWNER, i.INDEX_NAME
      from dba_indexes i
     where i.OWNER = p_owner
       and i.TABLE_NAME = p_table_name;
  cr_indx c_indx%rowtype;

  cursor c_lob(p_owner varchar2, p_table_name varchar2) is
    select l.OWNER,
           l.table_name,
           l.column_name,
           l.segment_name,
           l.index_name
      from dba_lobs l
     where l.owner = p_owner
       and l.table_name = p_table_name;
  cr_lob c_lob%rowtype;

  cursor c_seg(p_owner varchar2, p_segment_name varchar2) is
    select s.segment_name, s.tablespace_name, sum(abs(s.BYTES)) as bytes
      from dba_segments s
     where s.owner = p_owner
       and s.segment_name = p_segment_name
     group by s.segment_name, s.tablespace_name;
  cr_seg c_seg%rowtype;

  v_table_created          DATE;
  v_table_name             VARCHAR2(64);
  v_num_rows               NUMBER(14);
  v_table_lobs             VARCHAR2(2000);
  v_table_bytes            NUMBER(14);
  v_table_tablespace       VARCHAR2(1000);
  v_table_lob_bytes        NUMBER(14);
  v_table_lob_cnt          NUMBER(14);
  v_table_lobs_tablespaces VARCHAR2(1000);
  v_indxes                 VARCHAR2(2000);
  v_indxes_bytes           NUMBER(14);
  v_indxes_tablespaces     VARCHAR2(1000);
  v_table_size_all         NUMBER(14);
  v_indxes_size_all        NUMBER(14);
  v_tab_indx_all           NUMBER(14);
begin
  v_owner := p_owner;

  commit;

  FOR cr_obj IN c_obj(v_owner) LOOP
    v_num_rows               := 0;
    v_table_lob_bytes        := 0;
    v_table_lob_cnt          := 0;
    v_table_bytes            := 0;
    v_indxes                 := '';
    v_indxes_bytes           := 0;
    v_table_lobs             := '';
    v_table_tablespace       := '';
    v_table_lobs_tablespaces := '';
    v_indxes_tablespaces     := '';
    v_table_size_all         := 0;
    v_indxes_size_all        := 0;
    v_tab_indx_all           := 0;

    v_table_name    := cr_obj.object_name;
    v_table_created := cr_obj.created;
    FOR cr_tab IN c_tab(cr_obj.owner, cr_obj.object_name) LOOP
      v_num_rows := cr_tab.num_rows;
      -- table bytes
      FOR cr_seg IN c_seg(cr_tab.owner, cr_tab.table_name) LOOP
        v_table_bytes      := cr_seg.bytes;
        v_table_tablespace := cr_seg.tablespace_name;
      END LOOP;

      FOR cr_lob IN c_lob(cr_tab.owner, cr_tab.table_name) LOOP
        -- table lob bytes

        FOR cr_seg IN c_seg(cr_lob.owner, cr_lob.segment_name) LOOP
          v_table_lobs             := v_table_lobs || cr_lob.column_name || ' (' ||
                                      cr_seg.bytes || ')' || chr(13);
          v_table_lob_bytes        := v_table_lob_bytes + cr_seg.bytes;
          v_table_lob_cnt          := v_table_lob_cnt + 1;
          v_table_lobs_tablespaces := v_table_lobs_tablespaces ||
                                      cr_seg.tablespace_name || chr(13);
        END LOOP;
      END LOOP;
      -- indexes
      FOR cr_indx IN c_indx(cr_tab.owner, cr_tab.table_name) LOOP
        FOR cr_seg IN c_seg(cr_indx.owner, cr_indx.index_name) LOOP
          v_indxes             := v_indxes || cr_indx.index_name || ' (' ||
                                  cr_seg.bytes || ')' || chr(13);
          v_indxes_bytes       := v_indxes_bytes + cr_seg.bytes;
          v_indxes_tablespaces := trim(v_indxes_tablespaces ||
                                  cr_seg.tablespace_name) || chr(13);
        END LOOP;
      END LOOP;

    END LOOP;
    v_table_size_all  := v_table_bytes + v_table_lob_bytes;
    v_indxes_size_all := v_indxes_bytes;
    v_tab_indx_all    := v_table_size_all + v_indxes_size_all;
    insert into obj_info_tab
      (table_schema,
       table_name,
       table_created,
       num_rows,
       table_bytes,
       table_tablespace,
       table_lobs,
       table_lob_bytes,
       table_lob_cnt,
       table_lobs_tablespaces,
       indxes,
       indxes_bytes,
       indxes_tablespaces,
       table_size_all,
       indxes_size_all,
       tab_indx_all)
    values
      (p_owner,
       v_table_name,
       v_table_created,
       v_num_rows,
       v_table_bytes,
       v_table_tablespace,
       v_table_lobs,
       v_table_lob_bytes,
       v_table_lob_cnt,
       v_table_lobs_tablespaces,
       v_indxes,
       v_indxes_bytes,
       v_indxes_tablespaces,
       v_table_size_all,
       v_indxes_size_all,
       v_tab_indx_all);
    commit;
  END LOOP;

end OBJ_INFO;
/

prompt
prompt Creating procedure POOR_PARSING_PRC
prompt ===================================
prompt
create or replace procedure asseco_tools.POOR_PARSING_PRC is

  cursor cpars is
    with stmp as
     (select dbms_lob.substr(s.sql_fulltext, 3900, 1) as sql_text1, s.MODULE
        from v$sql s
       where force_matching_signature <> exact_matching_signature
         and executions > 0
         and command_type in ('3', '6', '7', '2'))
    select substr(sql_text1, 0, round(length(sql_text1) / 3)) as sql_text1,
           module
      from stmp
     group by substr(sql_text1, 0, round(length(sql_text1) / 3)), module
    having count (*) > 10;

  -- crpars cpars%rowtype;

  cursor cparsdetail(v_sql varchar2) is
    select substr(sql_text, 0, length(v_sql)), MODULE, count(*) as cnt
      from v$sql
     where sql_text like v_sql || '%'
     group by substr(sql_text, 0, length(v_sql)), MODULE
     order by cnt desc;

  crparsdetail cparsdetail%rowtype;

  v_snap_date date;
  v_error     VARCHAR2(4000);
  v_info      VARCHAR2(4000);
  v_example   clob;

BEGIN
  select sysdate into v_snap_date from dual;

  FOR crpars in cpars LOOP
    v_info := '';
    v_example := '';
    
    FOR crparsdetail in cparsdetail(crpars.sql_text1) LOOP
    
      v_info := v_info || crparsdetail.module || '(' || crparsdetail.cnt || ')' ||  chr(13);
    
    END LOOP;
  
    begin
      select sql_fulltext
        into v_example
        from v$sql
       where sql_text like crpars.sql_text1 || '%'
         and rownum = 1;
      --v_example:='';
    exception
      when others then
        v_error := '' || substr(SQLERRM, 0, 1000)||' '|| substr(crpars.sql_text1, 0,990);
        insert into logs (dt, info) values (sysdate, v_error);
        
    end;
  
    insert into poor_parsing
      (dt, sql_text, info, example, cnt)
    values
      (v_snap_date, crpars.sql_text1, v_info, v_example, null);
    commit;
  END LOOP;

  commit;
exception
  when others then
    v_error := '' || substr(SQLERRM, 0, 2000);
    insert into logs (dt, info) values (sysdate, v_error);
end POOR_PARSING_PRC;
/

prompt
prompt Creating procedure REPORT_ACTSESSHIS_TXTM
prompt =========================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.REPORT_ACTSESSHIS_TXTM IS
   V_ERROR VARCHAR2(1000);
BEGIN
   insert into rep_actsesshis_txtm
      (dt, locktype, waiters)
      SELECT SQL_EXEC_START, 'TX' as LOCKTYPE, COUNT(*) WAITERS
        FROM DBA_HIST_ACTIVE_SESS_HISTORY S
       WHERE SAMPLE_TIME > SYSDATE - 7
         AND EVENT = 'enq: TX - row lock contention'
       GROUP BY SQL_EXEC_START
       ORDER BY SQL_EXEC_START;

   insert into rep_actsesshis_txtm
      (dt, locktype, waiters)
      SELECT SQL_EXEC_START, 'TM' as LOCKTYPE, COUNT(*) WAITERS
        FROM DBA_HIST_ACTIVE_SESS_HISTORY S
       WHERE SAMPLE_TIME > SYSDATE - 7
         AND EVENT = 'enq: TM - contention'
       GROUP BY SQL_EXEC_START
       ORDER BY SQL_EXEC_START;

   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : REP_ACTSESSHIST :' ||
                 substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure REPORT_AWRS
prompt ==============================
prompt
create or replace procedure asseco_tools.REPORT_AWRS is
begin

null;
end REPORT_AWRS;
/

prompt
prompt Creating procedure REPORT_CPULOAD
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.REPORT_CPULOAD IS
   V_ERROR VARCHAR2(1000);
BEGIN
   insert into rep_cpuload
      (dt, cpuload)
      SELECT s.begin_interval_time, round(os.value)
        FROM dba_hist_snapshot s, dba_hist_osstat os
       WHERE s.snap_id = os.snap_id
         AND s.instance_number = os.instance_number
         AND s.dbid = os.dbid
         AND os.stat_name = 'LOAD'
         AND s.begin_interval_time > sysdate - 7
       ORDER BY s.begin_interval_time;

   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : REP_CPULOAD :' ||
                 substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure REPORT_JOBS
prompt ==============================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.REPORT_JOBS IS
   V_ERROR VARCHAR2(1000);
BEGIN

insert into rep_jobs
  (dt, owner, job_name, status, count, min_duration_in_min, avg_duration_in_min, max_duration_in_min)
      SELECT sysdate,
						       owner,
             job_name,
             status,
             COUNT(*) AS COUNT,
             ROUND(MIN(EXTRACT(DAY FROM run_duration) * 24 * 60 +
                       EXTRACT(HOUR FROM run_duration) * 60 +
                       EXTRACT(MINUTE FROM run_duration) +
                       EXTRACT(SECOND FROM run_duration) / 60)) AS Min_duration_in_min,
             ROUND(AVG(EXTRACT(DAY FROM run_duration) * 24 * 60 +
                       EXTRACT(HOUR FROM run_duration) * 60 +
                       EXTRACT(MINUTE FROM run_duration) +
                       EXTRACT(SECOND FROM run_duration) / 60)) AS AVG_duration_in_min,
             ROUND(MAX(EXTRACT(DAY FROM run_duration) * 24 * 60 +
                       EXTRACT(HOUR FROM run_duration) * 60 +
                       EXTRACT(MINUTE FROM run_duration) +
                       EXTRACT(SECOND FROM run_duration) / 60)) AS Max_duration_in_min
        FROM dba_scheduler_job_run_details
       WHERE OWNER NOT IN ('SYS', 'EXFSYS')
         AND log_date > SYSDATE - 7
       GROUP BY owner, job_name, status
      HAVING AVG(EXTRACT(DAY
        FROM run_duration) * 24 * 60 * 60 + EXTRACT(HOUR
        FROM run_duration) * 60 * 60 + EXTRACT(MINUTE
        FROM run_duration) * 60 + EXTRACT(SECOND
        FROM run_duration)) > 0
       ORDER BY owner, job_name;

   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : REP_JOBS :' ||
                 substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure REPORT_TOPSQL
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.REPORT_TOPSQL IS
   V_ERROR VARCHAR2(1000);
BEGIN
   insert into rep_topsql
      (sqlid, samplehr, module, action, sqltext)
      select *
        from (WITH x AS (SELECT *
                           FROM (SELECT sql_id,
                                        sample_hr,
                                        module,
                                        action,
                                        cnt_on_cpu,
                                        cnt_waiting,
                                        dense_rank() over(PARTITION BY sample_hr ORDER BY cnt_on_cpu DESC) cpu_rnk,
                                        dense_rank() over(PARTITION BY sample_hr ORDER BY cnt_waiting DESC) wait_rnk
                                   FROM (SELECT sql_id,
                                                to_char(sample_time,
                                                        'YYYY-MM-DD HH24') sample_hr,
                                                SUM(decode(ash.session_state,
                                                           'ON CPU',
                                                           1,
                                                           0)) cnt_on_cpu,
                                                SUM(decode(ash.session_state,
                                                           'WAITING',
                                                           1,
                                                           0)) cnt_waiting,
                                                module,
                                                action
                                           FROM dba_hist_active_sess_history ash
                                          WHERE sample_time > SYSDATE - 7
                                            AND sql_id IS NOT NULL
                                          GROUP BY sql_id,
                                                   module,
                                                   action,
                                                   to_char(sample_time,
                                                           'YYYY-MM-DD HH24')
                                          ORDER BY 3 DESC))
                          WHERE wait_rnk <= 5
                             OR cpu_rnk <= 5
                          ORDER BY sample_hr, cpu_rnk ASC)
                SELECT x.sql_id, x.sample_hr, x.module, x.action, q.sql_text
                  FROM x, v$sql q
                 WHERE x.sql_id = q.sql_id
                 GROUP BY x.sql_id,
                          x.sample_hr,
                          x.module,
                          x.action,
                          q.sql_text
                 order by x.sample_hr);

   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : REP_TOPSQL :' ||
                 substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure REPORT_WAITCLASS
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.REPORT_WAITCLASS IS
   V_ERROR VARCHAR2(1000);
BEGIN

   insert into REP_WAITCLASS
      (dt,
       ccpu,
       cbcpu,
       cscheduler,
       cuserio,
       csystemio,
       cconcurrency,
       capplication,
       ccommit,
       cconfiguration,
       cadministrative,
       cnetwork,
       cqueueing,
       ccluster,
       cother)
      select *
        from (WITH x AS (SELECT sample_time,
                                cpu / 60 * 10 AS cpu,
                                bcpu / 60 * 10 AS bcpu,
                                scheduler / 60 * 10 AS scheduler,
                                uio / 60 * 10 AS uio,
                                sio / 60 * 10 AS sio,
                                concurrency / 60 * 10 AS concurrency,
                                application / 60 * 10 AS application,
                                COMMIT / 60 * 10 AS COMMIT,
                                configuration / 60 * 10 AS configuration,
                                administrative / 60 * 10 AS administrative,
                                network / 60 * 10 AS network,
                                queueing / 60 * 10 AS queueing,
                                clust / 60 * 10 AS clust,
                                other / 60 * 10 AS other
                           FROM (SELECT TRUNC(sample_time, 'MI') AS sample_time,
                                        DECODE(session_state,
                                               'ON CPU',
                                               DECODE(session_type,
                                                      'BACKGROUND',
                                                      'BCPU',
                                                      'ON CPU'),
                                               wait_class) AS wait_class
                                   FROM dba_hist_active_sess_history
                                  WHERE sample_time > SYSDATE - INTERVAL '168'
                                  HOUR
                                    AND sample_time <= TRUNC(SYSDATE, 'MI')) ash
                         PIVOT(COUNT(*)
                            FOR wait_class IN('ON CPU' AS cpu,
                                             'BCPU' AS bcpu,
                                             'Scheduler' AS scheduler,
                                             'User I/O' AS uio,
                                             'System I/O' AS sio,
                                             'Concurrency' AS concurrency,
                                             'Application' AS application,
                                             'Commit' AS COMMIT,
                                             'Configuration' AS
                                             configuration,
                                             'Administrative' AS
                                             administrative,
                                             'Network' AS network,
                                             'Queueing' AS queueing,
                                             'Cluster' AS clust,
                                             'Other' AS other)) ash

                         )
                SELECT trunc(sample_time, 'HH24') as Time,
                       SUM(cpu) / 60 AS CPU,
                       SUM(bcpu) / 60 AS BCPU,
                       SUM(scheduler) / 60 AS SCHEDULER,
                       SUM(uio) / 60 AS User_IO,
                       SUM(sio) / 60 AS System_IO,
                       SUM(concurrency) / 60 AS Concurrency,
                       SUM(application) / 60 AS Application,
                       SUM(COMMIT) / 60 AS COMMIT,
                       SUM(configuration) / 60 AS Configuration,
                       SUM(administrative) / 60 AS Administrative,
                       SUM(network) / 60 AS Network,
                       SUM(queueing) / 60 AS Queueing,
                       SUM(clust) / 60 AS Clust,
                       SUM(other) / 60 AS Other
                  FROM x
                 GROUP BY trunc(sample_time, 'HH24')
                 ORDER BY trunc(sample_time, 'HH24'));


   commit;
EXCEPTION
   WHEN OTHERS THEN
      V_ERROR := 'INSERT ERROR : REP_WAITCLASS :' ||
                 substr(SQLERRM, 0, 1000);
      INSERT INTO LOGS (DT, INFO) VALUES (sysdate, V_ERROR);
      rollback;
END;
/

prompt
prompt Creating procedure RUN_STATS
prompt ============================
prompt
create or replace procedure asseco_tools.RUN_STATS (P_SCHEMA IN VARCHAR2) IS
begin
  DELETE FROM STATISTICS_INFO;
  STATS.CHECK_STATS(P_OWNER => P_SCHEMA);
  COMMIT;
  STATS.RUN_STATS_TAB_LVL(8);
end RUN_STATS;
/

prompt
prompt Creating procedure SESSIONS_SAVE
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE ASSECO_TOOLS.sessions_save IS
  v_snap_date date;

BEGIN

  select sysdate into v_snap_date from dual;

  insert into sessions
    (snap_date,
     sid,
     spid,
     osuser,
     machine,
     terminal,
     program,
     logontime,
     lastcallet,
     mem_used,
     mem_alloc,
     mem_max,
     username,
     client_info,
     status,
     sql_id)
    select /*+ ORDERED */
     v_snap_date,
     s.sid,
     p.spid,
     s.OSUSER,
     s.MACHINE,
     s.TERMINAL,
     s.PROGRAM,
     s.LOGON_TIME,
     s.LAST_CALL_ET,
     round(p.PGA_USED_MEM / 1024 / 1024),
     round(p.PGA_ALLOC_MEM / 1024 / 1024),
     round(p.PGA_MAX_MEM / 1024 / 1024),
     s.USERNAME,
     s.CLIENT_INFO,
     s.STATUS,
     s.SQL_ID
      from v$process p, v$session s
     where s.PADDR = p.ADDR
       and s.TYPE != 'BACKGROUND';
  commit;
EXCEPTION
  WHEN OTHERS THEN
    rollback;
END;
/

prompt
prompt Creating package body STATS
prompt ===========================
prompt
CREATE OR REPLACE PACKAGE BODY ASSECO_TOOLS.STATS IS

  V_PERCENTAGE_MOD_DIFF NUMBER(3);
  V_CHECK_SUBPARTITIONS NUMBER(1);

  PROCEDURE CHECK_STATS(P_OWNER IN VARCHAR2) IS
  
    CURSOR C_TABLES IS
      SELECT T.OWNER,
             T.TABLE_NAME,
             T.NUM_ROWS,
             T.SAMPLE_SIZE,
             T.LAST_ANALYZED,
             T.PARTITIONED,
             T.TEMPORARY,
             T.GLOBAL_STATS
        FROM ALL_TABLES T
       WHERE T.OWNER = P_OWNER
         AND (T.TABLE_NAME NOT LIKE 'TMP%' AND T.TEMPORARY = 'N') -- nasze pseudo temp
       ORDER BY T.TABLE_NAME;
  
    CR_TABLES C_TABLES%ROWTYPE;
  
    CURSOR C_TAB_PARTITIONS(PP_OWNER VARCHAR2, PP_TABLE_NAME VARCHAR2) IS
      SELECT P.TABLE_NAME,
             P.PARTITION_NAME,
             P.NUM_ROWS,
             P.SAMPLE_SIZE,
             P.LAST_ANALYZED,
             P.SUBPARTITION_COUNT,
             P.GLOBAL_STATS
        FROM ALL_TAB_PARTITIONS P
       WHERE P.TABLE_OWNER = PP_OWNER
         AND P.TABLE_NAME = PP_TABLE_NAME;
  
    CR_TAB_PARTITIONS C_TAB_PARTITIONS%ROWTYPE;
  
    CURSOR C_TAB_SUBPARTITIONS(PP_OWNER          VARCHAR2,
                               PP_TABLE_NAME     VARCHAR2,
                               PP_PARTITION_NAME VARCHAR2) IS
      SELECT S.SUBPARTITION_NAME,
             S.NUM_ROWS,
             S.SAMPLE_SIZE,
             S.LAST_ANALYZED,
             S.GLOBAL_STATS
        FROM ALL_TAB_SUBPARTITIONS S
       WHERE S.TABLE_OWNER = PP_OWNER
         AND S.TABLE_NAME = PP_TABLE_NAME
         AND S.PARTITION_NAME = PP_PARTITION_NAME;
  
    CR_TAB_SUBPARTITIONS C_TAB_SUBPARTITIONS%ROWTYPE;
  
    V_PERCENTAGE NUMBER(16) := 0;
  
    V_NEED_STATS NUMBER(9) := 0;
  
    V_TABLE_NAME VARCHAR2(30);
    V_PART_NAME  VARCHAR2(30);
    V_SUB_NAME   VARCHAR2(30);
    V_REASON     VARCHAR2(200);
    V_TEMPT      VARCHAR2(90);
    --    V_NUM_ROWS   NUMBER(16);
    V_CHECK_DATE DATE;
    V_COUNT_SQL  VARCHAR2(300);
    V_COUNT      NUMBER(16);
    V_CHECKED    NUMBER(9);
  BEGIN
  
    V_CHECK_DATE          := SYSDATE;
    V_CHECKED             := 0;
    V_PERCENTAGE_MOD_DIFF := V_PERCENTAGE_MOD_DIFF;
    DBMS_STATS.FLUSH_DATABASE_MONITORING_INFO();
    --DELETE FROM STATISTICS_INFO;
    --COMMIT;
    FOR CR_TABLES IN C_TABLES LOOP
      V_NEED_STATS := 0;
      V_REASON     := '';
      dbms_application_info.set_client_info(CR_TABLES.TABLE_NAME);
    
      IF CR_TABLES.TEMPORARY = 'YES' AND
         CR_TABLES.Last_Analyzed IS NOT NULL THEN
        INSERT INTO STATISTICS_INFO
          (USER_SCHEMA,
           DT,
           T,
           P,
           S,
           REASON,
           INTERNAL_CODE,
           TEMPT,
           NUM_ROWS)
        VALUES
          (P_OWNER,
           V_CHECK_DATE,
           V_TABLE_NAME,
           V_PART_NAME,
           V_SUB_NAME,
           CR_TABLES.TEMPORARY,
           NULL,
           NULL,
           'DELETE STATISTICS');
        CONTINUE;
      END IF;
    
      IF CR_TABLES.PARTITIONED = 'YES' THEN
      
        FOR CR_TAB_PARTITIONS IN C_TAB_PARTITIONS(CR_TABLES.OWNER,
                                                  CR_TABLES.TABLE_NAME) LOOP
          V_NEED_STATS := 0;
          V_REASON     := '';
        
          CHECK_COLL_STATS(P_OWNER         => CR_TABLES.OWNER,
                           P_TABLE_NAME    => CR_TABLES.TABLE_NAME,
                           P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                           P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                           P_NEED_STATS    => V_NEED_STATS,
                           P_REASON        => V_REASON);
          CHECK_IND_STATS(P_OWNER         => CR_TABLES.OWNER,
                          P_TABLE_NAME    => CR_TABLES.TABLE_NAME,
                          P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                          P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                          P_NEED_STATS    => V_NEED_STATS,
                          P_REASON        => V_REASON);
          CHECK_COLL_STATS_MISS(P_OWNER      => CR_TABLES.OWNER,
                                P_TABLE_NAME => CR_TABLES.TABLE_NAME,
                                P_NEED_STATS => V_NEED_STATS,
                                P_REASON     => V_REASON);
        
          IF V_CHECK_SUBPARTITIONS = 1 THEN
            IF CR_TAB_PARTITIONS.SUBPARTITION_COUNT > 0 THEN
              -- SUBPARTYCJA
              FOR CR_TAB_SUBPARTITIONS IN C_TAB_SUBPARTITIONS(CR_TABLES.OWNER,
                                                              CR_TAB_PARTITIONS.TABLE_NAME,
                                                              CR_TAB_PARTITIONS.PARTITION_NAME) LOOP
                V_NEED_STATS := 0;
                V_REASON     := '';
              
                BEGIN
                
                  IF (CR_TAB_SUBPARTITIONS.SAMPLE_SIZE = 0 OR
                     CR_TAB_SUBPARTITIONS.NUM_ROWS = 0) OR
                     (CR_TAB_SUBPARTITIONS.SAMPLE_SIZE IS NULL OR
                     CR_TAB_SUBPARTITIONS.NUM_ROWS IS NULL) THEN
                    V_COUNT_SQL := 'SELECT COUNT(*) FROM ' || P_OWNER || '.' ||
                                   CR_TABLES.TABLE_NAME ||
                                   ' SUBPARTITION (' ||
                                   CR_TAB_SUBPARTITIONS.SUBPARTITION_NAME || ')';
                    EXECUTE IMMEDIATE V_COUNT_SQL
                      INTO V_COUNT;
                    IF V_COUNT = 0 THEN
                      CHECK_SUB_MOD(P_OWNER             => CR_TABLES.OWNER,
                                    P_TABLE_NAME        => CR_TABLES.Table_Name,
                                    P_PARTITION_NAME    => CR_TAB_PARTITIONS.PARTITION_NAME,
                                    P_SUBPARTITION_NAME => CR_TAB_SUBPARTITIONS.SUBPARTITION_NAME,
                                    P_NUM_ROWS          => CR_TABLES.NUM_ROWS,
                                    P_LAST_ANALYZED     => CR_TAB_SUBPARTITIONS.LAST_ANALYZED,
                                    P_NEED_STATS        => V_NEED_STATS,
                                    P_REASON            => V_REASON);
                      IF V_NEED_STATS > 0 THEN
                        INSERT INTO STATISTICS_INFO
                          (DT,
                           USER_SCHEMA,
                           T,
                           P,
                           S,
                           REASON,
                           INTERNAL_CODE,
                           TEMPT,
                           NUM_ROWS)
                        VALUES
                          (V_CHECK_DATE,
                           P_OWNER,
                           CR_TABLES.TABLE_NAME,
                           CR_TAB_PARTITIONS.PARTITION_NAME,
                           CR_TAB_SUBPARTITIONS.SUBPARTITION_NAME,
                           V_REASON,
                           V_NEED_STATS,
                           V_TEMPT,
                           CR_TAB_SUBPARTITIONS.NUM_ROWS);
                      END IF;
                      CONTINUE;
                    END IF;
                  END IF;
                  IF CR_TAB_SUBPARTITIONS.NUM_ROWS = 0 AND
                     CR_TAB_SUBPARTITIONS.SAMPLE_SIZE > 0 THEN
                    V_PERCENTAGE := (CR_TAB_SUBPARTITIONS.SAMPLE_SIZE /
                                    CR_TAB_SUBPARTITIONS.NUM_ROWS) * 100;
                    V_NEED_STATS := V_NEED_STATS + 1;
                    V_REASON     := 'S_PRC_NOT ' || V_PERCENTAGE;
                  END IF;
                
                  IF CR_TAB_SUBPARTITIONS.NUM_ROWS > 0 AND
                     CR_TAB_SUBPARTITIONS.SAMPLE_SIZE > 0 AND
                     CR_TAB_SUBPARTITIONS.NUM_ROWS !=
                     CR_TAB_SUBPARTITIONS.SAMPLE_SIZE THEN
                    V_PERCENTAGE := (CR_TAB_SUBPARTITIONS.SAMPLE_SIZE /
                                    CR_TAB_SUBPARTITIONS.NUM_ROWS) * 100;
                    V_NEED_STATS := V_NEED_STATS + 1;
                    V_REASON     := 'S_PRC ' || V_PERCENTAGE;
                  END IF;
                
                  IF V_PERCENTAGE < 100 THEN
                    V_NEED_STATS := V_NEED_STATS + 1;
                    V_REASON     := V_REASON || 'S_PRC ' || V_PERCENTAGE;
                  END IF;
                
                  CHECK_SUB_MOD(P_OWNER             => CR_TABLES.OWNER,
                                P_TABLE_NAME        => CR_TABLES.Table_Name,
                                P_PARTITION_NAME    => CR_TAB_PARTITIONS.PARTITION_NAME,
                                P_SUBPARTITION_NAME => CR_TAB_SUBPARTITIONS.SUBPARTITION_NAME,
                                P_NUM_ROWS          => CR_TAB_PARTITIONS.NUM_ROWS,
                                P_LAST_ANALYZED     => CR_TAB_PARTITIONS.LAST_ANALYZED,
                                P_NEED_STATS        => V_NEED_STATS,
                                P_REASON            => V_REASON);
                
                  IF V_NEED_STATS > 0 THEN
                    INSERT INTO STATISTICS_INFO
                      (DT,
                       USER_SCHEMA,
                       T,
                       P,
                       S,
                       REASON,
                       INTERNAL_CODE,
                       TEMPT,
                       NUM_ROWS)
                    VALUES
                      (V_CHECK_DATE,
                       P_OWNER,
                       CR_TABLES.TABLE_NAME,
                       CR_TAB_PARTITIONS.PARTITION_NAME,
                       CR_TAB_SUBPARTITIONS.SUBPARTITION_NAME,
                       V_REASON,
                       V_NEED_STATS,
                       V_TEMPT,
                       CR_TAB_SUBPARTITIONS.Num_Rows);
                  END IF;
                
                EXCEPTION
                  WHEN OTHERS THEN
                    dbms_output.put_line(SQLERRM);
                    dbms_output.put_line(CR_TAB_SUBPARTITIONS.SAMPLE_SIZE || ':' ||
                                         CR_TAB_SUBPARTITIONS.NUM_ROWS);
                  
                END;
              END LOOP;
            END IF;
          ELSE
            -- PARTYCJA
            BEGIN
              IF (CR_TAB_PARTITIONS.SAMPLE_SIZE = 0 OR
                 CR_TAB_PARTITIONS.NUM_ROWS = 0) OR
                 (CR_TAB_PARTITIONS.SAMPLE_SIZE IS NULL OR
                 CR_TAB_PARTITIONS.NUM_ROWS IS NULL) THEN
                V_COUNT_SQL := 'SELECT COUNT(*) FROM ' || P_OWNER || '.' ||
                               CR_TABLES.TABLE_NAME || ' PARTITION(' ||
                               CR_TAB_PARTITIONS.PARTITION_NAME || ')';
                EXECUTE IMMEDIATE V_COUNT_SQL
                  INTO V_COUNT;
                IF V_COUNT = 0 THEN
                  CHECK_PART_MOD(P_OWNER          => CR_TABLES.OWNER,
                                 P_TABLE_NAME     => CR_TABLES.TABLE_NAME,
                                 P_PARTITION_NAME => CR_TAB_PARTITIONS.PARTITION_NAME,
                                 P_NUM_ROWS       => CR_TABLES.NUM_ROWS,
                                 P_LAST_ANALYZED  => CR_TAB_PARTITIONS.LAST_ANALYZED,
                                 P_NEED_STATS     => V_NEED_STATS,
                                 P_REASON         => V_REASON);
                  IF V_NEED_STATS > 0 THEN
                    INSERT INTO STATISTICS_INFO
                      (DT,
                       USER_SCHEMA,
                       T,
                       P,
                       S,
                       REASON,
                       INTERNAL_CODE,
                       TEMPT,
                       NUM_ROWS)
                    VALUES
                      (V_CHECK_DATE,
                       P_OWNER,
                       CR_TABLES.TABLE_NAME,
                       CR_TAB_PARTITIONS.PARTITION_NAME,
                       NULL,
                       V_REASON,
                       V_NEED_STATS,
                       V_TEMPT,
                       CR_TAB_PARTITIONS.Num_Rows);
                  END IF;
                
                  CONTINUE;
                END IF;
              END IF;
              V_PERCENTAGE := (CR_TAB_PARTITIONS.SAMPLE_SIZE /
                              CR_TAB_PARTITIONS.NUM_ROWS) * 100;
            
              IF V_PERCENTAGE < 100 THEN
                V_NEED_STATS := V_NEED_STATS + 1;
                V_REASON     := 'P_PRC ' || V_PERCENTAGE;
              END IF;
            
              CHECK_PART_MOD(P_OWNER          => CR_TABLES.OWNER,
                             P_TABLE_NAME     => CR_TABLES.TABLE_NAME,
                             P_PARTITION_NAME => CR_TAB_PARTITIONS.PARTITION_NAME,
                             P_NUM_ROWS       => CR_TABLES.NUM_ROWS,
                             P_LAST_ANALYZED  => CR_TAB_PARTITIONS.LAST_ANALYZED,
                             P_NEED_STATS     => V_NEED_STATS,
                             P_REASON         => V_REASON);
            
              IF V_NEED_STATS > 0 THEN
                INSERT INTO STATISTICS_INFO
                  (DT,
                   USER_SCHEMA,
                   T,
                   P,
                   S,
                   REASON,
                   INTERNAL_CODE,
                   TEMPT,
                   NUM_ROWS)
                VALUES
                  (V_CHECK_DATE,
                   P_OWNER,
                   CR_TABLES.TABLE_NAME,
                   CR_TAB_PARTITIONS.PARTITION_NAME,
                   NULL,
                   V_REASON,
                   V_NEED_STATS,
                   V_TEMPT,
                   CR_TAB_PARTITIONS.Num_Rows);
              END IF;
            
            EXCEPTION
              WHEN OTHERS THEN
                dbms_output.put_line(SQLERRM);
                dbms_output.put_line(CR_TAB_PARTITIONS.SAMPLE_SIZE || ':' ||
                                     CR_TAB_PARTITIONS.NUM_ROWS);
              
            END;
          
          END IF;
        
        END LOOP;
      
      ELSE
        -- TABELA
        BEGIN
          IF (CR_TABLES.SAMPLE_SIZE = 0 OR CR_TABLES.NUM_ROWS = 0) OR
             (CR_TABLES.SAMPLE_SIZE IS NULL OR CR_TABLES.NUM_ROWS IS NULL) THEN
            V_COUNT_SQL := 'SELECT COUNT(*) FROM ' || P_OWNER || '.' ||
                           CR_TABLES.TABLE_NAME;
            EXECUTE IMMEDIATE V_COUNT_SQL
              INTO V_COUNT;
            IF V_COUNT = 0 THEN
              CHECK_TAB_MOD(P_OWNER         => CR_TABLES.OWNER,
                            P_TABLE_NAME    => CR_TABLES.Table_Name,
                            P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                            P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                            P_NEED_STATS    => V_NEED_STATS,
                            P_REASON        => V_REASON);
              IF V_NEED_STATS > 0 THEN
                INSERT INTO STATISTICS_INFO
                  (DT,
                   USER_SCHEMA,
                   T,
                   P,
                   S,
                   REASON,
                   INTERNAL_CODE,
                   TEMPT,
                   NUM_ROWS)
                VALUES
                  (V_CHECK_DATE,
                   P_OWNER,
                   CR_TABLES.TABLE_NAME,
                   NULL,
                   NULL,
                   V_REASON,
                   V_NEED_STATS,
                   V_TEMPT,
                   CR_TABLES.Num_Rows);
              END IF;
              CONTINUE;
            ELSE
              V_NEED_STATS := V_NEED_STATS + 1;
              V_REASON     := 'T_COUNT ' || V_COUNT;
            END IF;
          END IF;
          V_PERCENTAGE := (CR_TABLES.SAMPLE_SIZE / CR_TABLES.NUM_ROWS) * 100;
        
          IF V_PERCENTAGE < 100 THEN
            V_NEED_STATS := V_NEED_STATS + 1;
            V_REASON     := 'T_PRC ' || V_PERCENTAGE;
          END IF;
        
          CHECK_TAB_MOD(P_OWNER         => CR_TABLES.OWNER,
                        P_TABLE_NAME    => CR_TABLES.Table_Name,
                        P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                        P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                        P_NEED_STATS    => V_NEED_STATS,
                        P_REASON        => V_REASON);
          CHECK_COLL_STATS(P_OWNER         => CR_TABLES.OWNER,
                           P_TABLE_NAME    => CR_TABLES.Table_Name,
                           P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                           P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                           P_NEED_STATS    => V_NEED_STATS,
                           P_REASON        => V_REASON);
          CHECK_IND_STATS(P_OWNER         => CR_TABLES.OWNER,
                          P_TABLE_NAME    => CR_TABLES.Table_Name,
                          P_NUM_ROWS      => CR_TABLES.NUM_ROWS,
                          P_LAST_ANALYZED => CR_TABLES.LAST_ANALYZED,
                          P_NEED_STATS    => V_NEED_STATS,
                          P_REASON        => V_REASON);
          CHECK_COLL_STATS_MISS(P_OWNER      => CR_TABLES.OWNER,
                                P_TABLE_NAME => CR_TABLES.TABLE_NAME,
                                P_NEED_STATS => V_NEED_STATS,
                                P_REASON     => V_REASON);
          IF V_NEED_STATS > 0 THEN
            INSERT INTO STATISTICS_INFO
              (DT,
               USER_SCHEMA,
               T,
               P,
               S,
               REASON,
               INTERNAL_CODE,
               TEMPT,
               NUM_ROWS)
            VALUES
              (V_CHECK_DATE,
               P_OWNER,
               CR_TABLES.TABLE_NAME,
               NULL,
               NULL,
               V_REASON,
               V_NEED_STATS,
               V_TEMPT,
               CR_TABLES.Num_Rows);
          END IF;
        
        EXCEPTION
          WHEN OTHERS THEN
            dbms_output.put_line(SQLERRM);
            dbms_output.put_line(CR_TABLES.SAMPLE_SIZE || ':' ||
                                 CR_TABLES.NUM_ROWS);
        END;
      END IF;
      --      CHECK_COLL_STATS_MISS(P_OWNER => CR_TABLES.OWNER, P_TABLE_NAME => CR_TABLES.TABLE_NAME, P_NEED_STATS => V_NEED_STATS, P_REASON => V_REASON);
      COMMIT;
    
      V_CHECKED := V_CHECKED + 1;
    END LOOP;
  
    dbms_output.put_line('CHECKED OBJECT:' || V_CHECKED);
    --GEN_TAB_LVL_CMD(P_DETAIL => 'T', P_NUM_FILES => 4);
  
  END CHECK_STATS;

  PROCEDURE CHECK_TAB_MOD(P_OWNER         IN VARCHAR2,
                          P_TABLE_NAME    IN VARCHAR2,
                          P_NUM_ROWS      IN NUMBER,
                          P_LAST_ANALYZED DATE,
                          P_NEED_STATS    IN OUT NUMBER,
                          P_REASON        IN OUT VARCHAR2) IS
    CURSOR C_TAB_MODIFICATION IS
      SELECT M.TABLE_NAME,
             M.PARTITION_NAME,
             M.SUBPARTITION_NAME,
             (M.INSERTS + M.UPDATES + M.DELETES) AS SUM_MODIFIED
        FROM ALL_TAB_MODIFICATIONS M
       WHERE M.TABLE_OWNER = P_OWNER
         AND M.TABLE_NAME = P_TABLE_NAME
         AND M.PARTITION_NAME IS NULL
         AND M.SUBPARTITION_NAME IS NULL;
  
    CR_TAB_MODIFICATION C_TAB_MODIFICATION%ROWTYPE;
  
    V_MOD NUMBER;
    V_D   NUMBER(9);
  
  BEGIN
    V_D := 0;
    FOR CR_TAB_MODIFICATION IN C_TAB_MODIFICATION LOOP
      V_MOD := ROUND((P_NUM_ROWS / 100) * V_PERCENTAGE_MOD_DIFF);
      IF (CR_TAB_MODIFICATION.SUM_MODIFIED > V_MOD) THEN
        P_NEED_STATS := P_NEED_STATS + 1;
        P_REASON     := P_REASON || ' T_MOD ' || V_MOD;
      ELSE
        V_D := SYSDATE - P_LAST_ANALYZED;
      
        IF (CR_TAB_MODIFICATION.SUM_MODIFIED > 0 AND ABS(V_D) > 30) THEN
          P_NEED_STATS := P_NEED_STATS + 1;
          P_REASON     := P_REASON || ' T_WARN_STATS_OLD_DATE ' || V_D;
        END IF;
      END IF;
    END LOOP;
  
    -- CHECK_COLL_STATS_MISS(P_OWNER => P_OWNER, P_TABLE_NAME => P_TABLE_NAME, P_NEED_STATS => P_NEED_STATS, P_REASON => P_REASON);
  
  END CHECK_TAB_MOD;

  PROCEDURE CHECK_PART_MOD(P_OWNER          IN VARCHAR2,
                           P_TABLE_NAME     IN VARCHAR2,
                           P_PARTITION_NAME IN VARCHAR2,
                           P_NUM_ROWS       IN NUMBER,
                           P_LAST_ANALYZED  DATE,
                           P_NEED_STATS     IN OUT NUMBER,
                           P_REASON         IN OUT VARCHAR2) IS
    CURSOR C_PART_MODIFICATION(PP_OWNER          VARCHAR2,
                               PP_TABLE_NAME     VARCHAR2,
                               PP_PARTITION_NAME VARCHAR2) IS
      SELECT M.TABLE_NAME,
             M.PARTITION_NAME,
             M.SUBPARTITION_NAME,
             (M.INSERTS + M.UPDATES + M.DELETES) AS SUM_MODIFIED
        FROM ALL_TAB_MODIFICATIONS M
       WHERE M.TABLE_OWNER = PP_OWNER
         AND M.TABLE_NAME = PP_TABLE_NAME
         AND M.PARTITION_NAME = PP_PARTITION_NAME
         AND M.SUBPARTITION_NAME IS NULL;
  
    CR_PART_MODIFICATION C_PART_MODIFICATION%ROWTYPE;
  
    V_MOD NUMBER;
    V_D   NUMBER(9);
  
  BEGIN
    FOR CR_PART_MODIFICATION IN C_PART_MODIFICATION(P_OWNER,
                                                    P_TABLE_NAME,
                                                    P_PARTITION_NAME) LOOP
      V_MOD := ROUND((P_NUM_ROWS / 100) * V_PERCENTAGE_MOD_DIFF);
      IF (CR_PART_MODIFICATION.SUM_MODIFIED > V_MOD) THEN
        P_NEED_STATS := P_NEED_STATS + 1;
        P_REASON     := P_REASON || ' P_MOD ';
      ELSE
        V_D := SYSDATE - P_LAST_ANALYZED;
        IF (CR_PART_MODIFICATION.SUM_MODIFIED > 0 AND ABS(V_D) > 30) THEN
          P_NEED_STATS := P_NEED_STATS + 1;
          P_REASON     := P_REASON || ' P_WARN_STATS_OLD_DATE ' || V_D;
        END IF;
      END IF;
    END LOOP;
    --CHECK_COLL_STATS_MISS(P_OWNER => P_OWNER, P_TABLE_NAME => P_TABLE_NAME, P_NEED_STATS => P_NEED_STATS, P_REASON => P_REASON);
  END;

  PROCEDURE CHECK_SUB_MOD(P_OWNER             IN VARCHAR2,
                          P_TABLE_NAME        IN VARCHAR2,
                          P_PARTITION_NAME    IN VARCHAR2,
                          P_SUBPARTITION_NAME IN VARCHAR2,
                          P_NUM_ROWS          IN NUMBER,
                          P_LAST_ANALYZED     DATE,
                          P_NEED_STATS        IN OUT NUMBER,
                          P_REASON            IN OUT VARCHAR2) IS
    CURSOR C_SUB_MODIFICATION(PP_OWNER             VARCHAR2,
                              PP_TABLE_NAME        VARCHAR2,
                              PP_PARTITION_NAME    VARCHAR2,
                              PP_SUBPARTITION_NAME VARCHAR2) IS
      SELECT M.TABLE_NAME,
             M.PARTITION_NAME,
             M.SUBPARTITION_NAME,
             (M.INSERTS + M.UPDATES + M.DELETES) AS SUM_MODIFIED
        FROM ALL_TAB_MODIFICATIONS M
       WHERE M.TABLE_OWNER = PP_OWNER
         AND M.TABLE_NAME = PP_TABLE_NAME
         AND M.PARTITION_NAME = PP_PARTITION_NAME
         AND M.SUBPARTITION_NAME = PP_SUBPARTITION_NAME;
  
    CR_SUB_MODIFICATION C_SUB_MODIFICATION%ROWTYPE;
    V_MOD               NUMBER;
    V_D                 NUMBER(9);
  BEGIN
    FOR CR_SUB_MODIFICATION IN C_SUB_MODIFICATION(P_OWNER,
                                                  P_TABLE_NAME,
                                                  P_PARTITION_NAME,
                                                  P_SUBPARTITION_NAME) LOOP
      V_MOD := ROUND((P_NUM_ROWS / 100) * V_PERCENTAGE_MOD_DIFF);
      IF (CR_SUB_MODIFICATION.SUM_MODIFIED > V_MOD) THEN
        P_NEED_STATS := P_NEED_STATS + 1;
        P_REASON     := P_REASON || ' S_MOD ';
      ELSE
        V_D := SYSDATE - P_LAST_ANALYZED;
        IF (CR_SUB_MODIFICATION.SUM_MODIFIED > 0 AND ABS(V_D) > 30) THEN
          P_NEED_STATS := P_NEED_STATS + 1;
          P_REASON     := P_REASON || ' S_WARN_STATS_OLD_DATE ' || V_D;
        END IF;
      END IF;
    END LOOP;
  END;

  PROCEDURE CHECK_COLL_STATS_MISS(P_OWNER      IN VARCHAR2,
                                  P_TABLE_NAME IN VARCHAR2,
                                  P_NEED_STATS IN OUT NUMBER,
                                  P_REASON     IN OUT VARCHAR2) IS
    --
    -- Sprawdzenia brakujacych statystyk kolumn
    --
    V_COL_COUNT NUMBER(9);
  BEGIN
    V_COL_COUNT := 0;
    SELECT COUNT(*)
      INTO V_COL_COUNT
      FROM (SELECT C.COLUMN_NAME
              FROM ALL_TAB_COLS C
             WHERE C.OWNER = P_OWNER
               AND C.HIDDEN_COLUMN = 'NO'
               AND C.TABLE_NAME = P_TABLE_NAME
               AND C.DATA_TYPE NOT IN ('LONG', 'XML')
            MINUS
            SELECT S.COLUMN_NAME
              FROM ALL_TAB_COL_STATISTICS S
             WHERE S.OWNER = P_OWNER
               AND S.TABLE_NAME = P_TABLE_NAME);
    IF (V_COL_COUNT > 0) THEN
      P_NEED_STATS := P_NEED_STATS + 1;
      P_REASON     := P_REASON || ' T_COL_STAT_MISS ' || V_COL_COUNT;
    END IF;
  END;

  PROCEDURE CHECK_COLL_STATS(P_OWNER         IN VARCHAR2,
                             P_TABLE_NAME    IN VARCHAR2,
                             P_NUM_ROWS      IN NUMBER,
                             P_LAST_ANALYZED DATE,
                             P_NEED_STATS    IN OUT NUMBER,
                             P_REASON        IN OUT VARCHAR2) IS
    --
    -- Sprawdzenia dat statystyk kolumn
    --
    CURSOR C_COLL_STATS IS
      SELECT S.COLUMN_NAME,
             S.NUM_DISTINCT,
             S.NUM_NULLS,
             S.LAST_ANALYZED,
             S.SAMPLE_SIZE,
             S.GLOBAL_STATS,
             S.HISTOGRAM
        FROM ALL_TAB_COL_STATISTICS S
       WHERE OWNER = P_OWNER
         AND TABLE_NAME = P_TABLE_NAME;
    CR_COLL_STATS     C_COLL_STATS%ROWTYPE;
    V_COOL_NEED_STATS NUMBER(9);
    V_D               NUMBER;
  BEGIN
    V_COOL_NEED_STATS := 0;
    FOR CR_COLL_STATS IN C_COLL_STATS LOOP
      V_D := round((P_LAST_ANALYZED - CR_COLL_STATS.LAST_ANALYZED) * 24 * 60);
      IF V_D > 1444 THEN
        V_COOL_NEED_STATS := V_COOL_NEED_STATS + 1;
      ELSE
        NULL;
      END IF;
    
    END LOOP;
    IF V_COOL_NEED_STATS > 0 THEN
      P_NEED_STATS := P_NEED_STATS + 1;
      P_REASON     := P_REASON || ' T_COL_STAT_OLD ' || V_COOL_NEED_STATS;
    END IF;
  END;

  PROCEDURE CHECK_IND_STATS(P_OWNER         IN VARCHAR2,
                            P_TABLE_NAME    IN VARCHAR2,
                            P_NUM_ROWS      IN NUMBER,
                            P_LAST_ANALYZED DATE,
                            P_NEED_STATS    IN OUT NUMBER,
                            P_REASON        IN OUT VARCHAR2) IS
    --
    -- Sprawdzenia statystyk indeksow
    --
    CURSOR C_IND_STATS IS
      SELECT I.INDEX_NAME,
             I.NUM_ROWS,
             I.LAST_ANALYZED,
             I.SAMPLE_SIZE,
             I.GLOBAL_STATS,
             I.PARTITIONED
        FROM ALL_INDEXES I
       WHERE OWNER = P_OWNER
         AND TABLE_NAME = P_TABLE_NAME;
    CR_IND_STATS     C_IND_STATS%ROWTYPE;
    V_IND_NEED_STATS NUMBER(9);
    V_D              NUMBER;
  
  BEGIN
    V_IND_NEED_STATS := 0;
    FOR CR_IND_STATS IN C_IND_STATS LOOP
      V_D := round((P_LAST_ANALYZED - CR_IND_STATS.LAST_ANALYZED) * 24 * 60);
      IF V_D > 1440 THEN
        V_IND_NEED_STATS := V_IND_NEED_STATS + 1;
      ELSE
        NULL;
      END IF;
    END LOOP;
    IF V_IND_NEED_STATS > 0 THEN
      P_NEED_STATS := P_NEED_STATS + 1;
      P_REASON     := P_REASON || ' T_WARN_IND_DATE_DIFF ' ||
                      V_IND_NEED_STATS;
    END IF;
  END;

  PROCEDURE GEN_TAB_LVL_CMD(P_DEGREE NUMBER, P_NUM_FILES NUMBER) IS
    --
    -- Generowanie skryptu statystyk dla tabel
    --
    CURSOR C_TAB IS
      SELECT S.USER_SCHEMA, S.T, S.TEMPT, SUM(S.NUM_ROWS)
        FROM STATISTICS_INFO S
       GROUP BY S.USER_SCHEMA, S.T, S.TEMPT
       ORDER BY S.T;
    CR_TAB C_TAB%ROWTYPE;
  
    V_SUM_ROWS   NUMBER(16);
    V_MAX_LINE   NUMBER(19);
    V_FILE_COUNT NUMBER(3);
    V_TMP_SUM    NUMBER(19);
    V_CMD        VARCHAR2(1000);
  BEGIN
    EXECUTE IMMEDIATE 'TRUNCATE TABLE STATISTICS_CMD';
    V_FILE_COUNT := 1;
    V_TMP_SUM    := 0;
  
    SELECT SUM(NUM_ROWS) INTO V_SUM_ROWS FROM STATISTICS_INFO;
    V_MAX_LINE := ROUND(V_SUM_ROWS / P_NUM_FILES);
    FOR CR_TAB IN C_TAB LOOP
      V_CMD := 'BEGIN ';
    
      V_CMD := V_CMD || 'DBMS_STATS.GATHER_TABLE_STATS(OWNNAME=> ' || '''' ||
               CR_TAB.USER_SCHEMA || '''' || ',';
      V_CMD := V_CMD || 'TABNAME=> ' || '''' || CR_TAB.T || '''' || ',';
      V_CMD := V_CMD || 'ESTIMATE_PERCENT=>NULL' || ',';
      V_CMD := V_CMD || 'DEGREE =>' || P_DEGREE || ',';
      V_CMD := V_CMD || 'NO_INVALIDATE=>DBMS_STATS.AUTO_INVALIDATE' || ',';
      V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'ALL' || '''' || ',';
      V_CMD := V_CMD || 'METHOD_OPT=>' || '''' ||
               'FOR ALL COLUMNS SIZE AUTO' || '''' || ',';
      V_CMD := V_CMD || 'CASCADE=>TRUE);';
    
      V_CMD := V_CMD || 'END;';
    
      INSERT INTO STATISTICS_CMD
        (CMD, FILE_ID)
      VALUES
        (V_CMD, V_FILE_COUNT);
      V_TMP_SUM := V_TMP_SUM + 1;
      IF V_TMP_SUM >= V_MAX_LINE THEN
        V_TMP_SUM    := 0;
        V_FILE_COUNT := V_FILE_COUNT + 1;
      END IF;
    END LOOP;
    COMMIT;
  
  END;

  PROCEDURE GEN_PART_LVL_CMD(P_DEGREE    NUMBER,
                             P_NUM_FILES NUMBER,
                             P_EXEC_TYPE VARCHAR2) IS
    --
    -- Generowanie skryptu statystyk dla partycji
    --  
    CURSOR C_PART IS
      SELECT S.USER_SCHEMA, S.T, S.P, S.TEMPT, SUM(S.NUM_ROWS) AS SUM
        FROM STATISTICS_INFO S
       GROUP BY S.USER_SCHEMA, S.T, S.P, S.TEMPT
       ORDER BY S.T;
    CR_PART C_PART%ROWTYPE;
  
    V_SUM_ROWS   NUMBER(16);
    V_MAX_LINE   NUMBER(19);
    V_FILE_COUNT NUMBER(3);
    V_TMP_SUM    NUMBER(19);
    V_CMD        VARCHAR2(1000);
  BEGIN
    EXECUTE IMMEDIATE 'TRUNCATE TABLE STATISTICS_CMD';
    V_FILE_COUNT := 1;
    V_TMP_SUM    := 0;
  
    SELECT SUM(NUM_ROWS) INTO V_SUM_ROWS FROM STATISTICS_INFO;
    V_MAX_LINE := ROUND(V_SUM_ROWS / P_NUM_FILES);
  
    FOR CR_PART IN C_PART LOOP
      IF P_EXEC_TYPE = 'BEGIN' THEN
        V_CMD := 'BEGIN ';
      END IF;
      IF P_EXEC_TYPE = 'EXEC' THEN
        V_CMD := 'PROMPT ' || CR_PART.T || ':' || CR_PART.P || CHR(13);
        V_CMD := V_CMD || 'EXEC ';
      END IF;
      V_CMD := V_CMD || 'DBMS_STATS.GATHER_TABLE_STATS(OWNNAME=> ' || '''' ||
               CR_PART.USER_SCHEMA || '''' || ',';
      V_CMD := V_CMD || 'TABNAME=> ' || '''' || CR_PART.T || '''' || ',';
    
      IF CR_PART.P IS NULL THEN
        V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'ALL' || '''' || ',';
      ELSE
        V_CMD := V_CMD || 'PARTNAME=> ' || '''' || CR_PART.P || '''' || ',';
        V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'PARTITION' || '''' || ',';
      END IF;
    
      V_CMD := V_CMD || 'ESTIMATE_PERCENT=>NULL' || ',';
      V_CMD := V_CMD || 'DEGREE =>' || P_DEGREE || ',';
      V_CMD := V_CMD || 'NO_INVALIDATE=>DBMS_STATS.AUTO_INVALIDATE' || ',';
      V_CMD := V_CMD || 'METHOD_OPT=>' || '''' ||
               'FOR ALL COLUMNS SIZE AUTO' || '''' || ',';
      V_CMD := V_CMD || 'CASCADE=>TRUE);';
    
      IF P_EXEC_TYPE = 'BEGIN' THEN
        V_CMD := V_CMD || 'END;';
      END IF;
    
      INSERT INTO STATISTICS_CMD
        (CMD, FILE_ID)
      VALUES
        (V_CMD, V_FILE_COUNT);
      V_TMP_SUM := V_TMP_SUM + CR_PART.SUM;
      IF V_TMP_SUM >= V_MAX_LINE THEN
        V_TMP_SUM    := 0;
        V_FILE_COUNT := V_FILE_COUNT + 1;
      END IF;
    END LOOP;
    COMMIT;
  END;

  PROCEDURE GEN_SUB_LVL_CMD(P_DEGREE    NUMBER,
                            P_NUM_FILES NUMBER,
                            P_EXEC_TYPE VARCHAR2) IS
    --
    -- Generowanie skryptu statystyk dla subpartycji
    -- 
    CURSOR C_SUB IS
      SELECT S.USER_SCHEMA, S.T, S.P, S.S, S.TEMPT, SUM(S.NUM_ROWS) AS SUM
        FROM STATISTICS_INFO S
       GROUP BY S.USER_SCHEMA, S.T, S.P, S.S, S.TEMPT
       ORDER BY S.T;
    CR_SUB C_SUB%ROWTYPE;
  
    V_SUM_ROWS   NUMBER(16);
    V_MAX_LINE   NUMBER(19);
    V_FILE_COUNT NUMBER(3);
    V_TMP_SUM    NUMBER(19);
    V_CMD        VARCHAR2(1000);
  BEGIN
    EXECUTE IMMEDIATE 'TRUNCATE TABLE STATISTICS_CMD';
    V_FILE_COUNT := 1;
    V_TMP_SUM    := 0;
  
    SELECT SUM(NUM_ROWS) INTO V_SUM_ROWS FROM STATISTICS_INFO;
    V_MAX_LINE := ROUND(V_SUM_ROWS / P_NUM_FILES);
  
    FOR CR_SUB IN C_SUB LOOP
      IF P_EXEC_TYPE = 'BEGIN' THEN
        V_CMD := 'BEGIN ';
      END IF;
      IF P_EXEC_TYPE = 'EXEC' THEN
        V_CMD := 'PROMPT ' || CR_SUB.T || ':' || CR_SUB.P || ':' ||
                 CR_SUB.S || CHR(13);
        V_CMD := V_CMD || 'EXEC ';
      END IF;
      V_CMD := V_CMD || 'DBMS_STATS.GATHER_TABLE_STATS(OWNNAME=> ' || '''' ||
               CR_SUB.USER_SCHEMA || '''' || ',';
      V_CMD := V_CMD || 'TABNAME=> ' || '''' || CR_SUB.T || '''' || ',';
    
      IF CR_SUB.P IS NULL THEN
        V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'ALL' || '''' || ',';
        --V_CMD := V_CMD || 'PARTNAME=> ' || '''' || CR_SUB.S || '''' || ',';
      ELSE
        IF CR_SUB.S IS NULL THEN
          V_CMD := V_CMD || 'PARTNAME=> ' || '''' || CR_SUB.P || '''' || ',';
          V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'PARTITION' || '''' || ',';
        ELSE
          V_CMD := V_CMD || 'PARTNAME=> ' || '''' || CR_SUB.S || '''' || ',';
          V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'SUBPARTITION' || '''' || ',';
        END IF;
      END IF;
    
      V_CMD := V_CMD || 'ESTIMATE_PERCENT=>NULL' || ',';
      V_CMD := V_CMD || 'DEGREE =>' || P_DEGREE || ',';
      V_CMD := V_CMD || 'NO_INVALIDATE=>DBMS_STATS.AUTO_INVALIDATE' || ',';
    
      V_CMD := V_CMD || 'METHOD_OPT=>' || '''' ||
               'FOR ALL COLUMNS SIZE AUTO' || '''' || ',';
      V_CMD := V_CMD || 'CASCADE=>TRUE);';
    
      IF P_EXEC_TYPE = 'BEGIN' THEN
        V_CMD := V_CMD || 'END;';
      END IF;
    
      INSERT INTO STATISTICS_CMD
        (CMD, FILE_ID)
      VALUES
        (V_CMD, V_FILE_COUNT);
      V_TMP_SUM := V_TMP_SUM + CR_SUB.SUM;
      IF V_TMP_SUM >= V_MAX_LINE THEN
        V_TMP_SUM    := 0;
        V_FILE_COUNT := V_FILE_COUNT + 1;
      END IF;
    END LOOP;
    COMMIT;
  END;

  PROCEDURE SPLIT_STATS_CMD(P_OWNER     VARCHAR2,
                            P_DEGREE    NUMBER,
                            P_NUM_FILES NUMBER) IS
    --
    -- Generowanie skryptu statystyk dla wszystkich tabel z podzialem na pliki
    --
    CURSOR C_TAB IS
      SELECT S.OWNER, S.TABLE_NAME, S.NUM_ROWS
        FROM ALL_TABLES S
       WHERE OWNER = P_OWNER
       ORDER BY S.NUM_ROWS DESC;
    CR_TAB C_TAB%ROWTYPE;
  
    V_SUM_ROWS   NUMBER(16);
    V_MAX_LINE   NUMBER(19);
    V_FILE_COUNT NUMBER(3);
    V_TMP_SUM    NUMBER(19);
    V_CMD        VARCHAR2(1000);
  BEGIN
    --EXECUTE IMMEDIATE 'TRUNCATE TABLE STATISTICS_CMD';
    V_FILE_COUNT := 1;
    V_TMP_SUM    := 0;
  
    SELECT SUM(NUM_ROWS)
      INTO V_SUM_ROWS
      FROM ALL_TABLES
     WHERE OWNER = P_OWNER
       AND TEMPORARY = 'N';
    V_MAX_LINE := ROUND(V_SUM_ROWS / P_NUM_FILES);
    FOR CR_TAB IN C_TAB LOOP
      V_CMD := 'PROMPT ' || CR_TAB.OWNER || '.' || CR_TAB.TABLE_NAME || ';' ||
               chr(13);
      V_CMD := V_CMD || 'BEGIN ' || chr(13);
    
      V_CMD := V_CMD || 'DBMS_STATS.GATHER_TABLE_STATS(' || chr(13);
      V_CMD := V_CMD || chr(9) || 'OWNNAME=> ' || '''' || CR_TAB.OWNER || '''' || ',' ||
               chr(13);
      V_CMD := V_CMD || chr(9) || 'TABNAME=> ' || '''' || CR_TAB.TABLE_NAME || '''' || ',' ||
               chr(13);
      V_CMD := V_CMD || chr(9) || 'ESTIMATE_PERCENT=>NULL' || ',' ||
               chr(13);
      V_CMD := V_CMD || chr(9) || 'DEGREE =>' || P_DEGREE || ',' || chr(13);
      V_CMD := V_CMD || chr(9) ||
               'NO_INVALIDATE=>DBMS_STATS.AUTO_INVALIDATE' || ',' ||
               chr(13);
      V_CMD := V_CMD || chr(9) || 'GRANULARITY=>' || '''' || 'ALL' || '''' || ',' ||
               chr(13);
      V_CMD := V_CMD || chr(9) || 'METHOD_OPT=>' || '''' ||
               'FOR ALL COLUMNS SIZE AUTO' || '''' || ',' || chr(13);
      V_CMD := V_CMD || chr(9) || 'CASCADE=>TRUE);' || chr(13);
      V_CMD := V_CMD || 'END;' || chr(13);
      V_CMD := V_CMD || '/' || chr(13);
    
      INSERT INTO STATISTICS_CMD
        (SCHEMA_NAME, NUM_ROWS, CMD, FILE_ID)
      VALUES
        (CR_TAB.OWNER, CR_TAB.Num_Rows, V_CMD, V_FILE_COUNT);
      V_TMP_SUM := V_TMP_SUM + nvl(CR_TAB.NUM_ROWS, 0);
      IF V_TMP_SUM >= V_MAX_LINE THEN
        V_TMP_SUM    := 0;
        V_FILE_COUNT := V_FILE_COUNT + 1;
      END IF;
    END LOOP;
    COMMIT;
  
  END;

  PROCEDURE RUN_STATS_TAB_LVL(P_DEGREE NUMBER) IS
    --
    -- Wykonanie statystyk 
    --
    CURSOR C_TAB IS
      SELECT S.USER_SCHEMA, S.T, S.TEMPT, SUM(S.NUM_ROWS)
        FROM STATISTICS_INFO S
       WHERE S.TEMPT IS NOT NULL
       GROUP BY S.USER_SCHEMA, S.T, S.TEMPT
       ORDER BY S.NUM_ROWS;
    CR_TAB C_TAB%ROWTYPE;
  
    V_CMD      VARCHAR2(1000);
    V_DATETIME DATE;
  BEGIN
    V_DATETIME := SYSDATE;
    FOR CR_TAB IN C_TAB LOOP
      V_CMD := 'BEGIN ';
    
      V_CMD := V_CMD || 'DBMS_STATS.GATHER_TABLE_STATS(OWNNAME=> ' || '''' ||
               CR_TAB.USER_SCHEMA || '''' || ',';
      V_CMD := V_CMD || 'TABNAME=> ' || '''' || CR_TAB.T || '''' || ',';
      V_CMD := V_CMD || 'ESTIMATE_PERCENT=>NULL' || ',';
      V_CMD := V_CMD || 'DEGREE =>' || P_DEGREE || ',';
      V_CMD := V_CMD || 'NO_INVALIDATE=>DBMS_STATS.AUTO_INVALIDATE' || ',';
      V_CMD := V_CMD || 'GRANULARITY=>' || '''' || 'ALL' || '''' || ',';
      V_CMD := V_CMD || 'METHOD_OPT=>' || '''' ||
               'FOR ALL COLUMNS SIZE AUTO' || '''' || ',';
      V_CMD := V_CMD || 'CASCADE=>TRUE);';
    
      V_CMD := V_CMD || 'END;';
    
      EXECUTE IMMEDIATE V_CMD;
      COMMIT;
      EXIT WHEN SYSDATE >(V_DATETIME + 2 / 24);
    
    END LOOP;
    COMMIT;
  
  END;

BEGIN
  V_PERCENTAGE_MOD_DIFF := 10;
  V_CHECK_SUBPARTITIONS := 0;
END STATS;
/

prompt
prompt Creating trigger DDLTRIGGER
prompt ===========================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.DDLTrigger
  AFTER DDL ON DATABASE
declare
  l_cnt      BINARY_INTEGER := 0;
  l_len      integer := 0;
  l_no       integer := 1;
  l_s        varchar2(32767) := '';
  v_os_user  varchar2(200);
  v_module   varchar2(200);
  v_ip       varchar2(20);
  v_host     varchar2(100);
  v_error    varchar2(2000);
  l_sql_text ora_name_list_t;
BEGIN

  if (ora_dict_obj_type IN ('INDEX',
                            'TABLE',
                            'SYNONYM',
                            'VIEW',
                            'PROCEDURE',
                            'FUNCTION',
                            'PACKAGE',
                            'TRIGGER',
                            'PACKAGE BODY',
                            'DATABASE LINK') AND
     ora_sysevent in ('CREATE', 'DROP', 'ALTER') AND
     ((ora_dict_obj_name not like ('WYD%')) AND
     (ora_dict_obj_name not like ('TMP%')))) THEN

    select SYS_CONTEXT('USERENV', 'OS_USER'),
           SYS_CONTEXT('USERENV', 'MODULE'),
           SYS_CONTEXT('USERENV', 'IP_ADDRESS'),
           SYS_CONTEXT('USERENV', 'HOST')
      into v_os_user, v_module, v_ip, v_host
      from dual;

    l_cnt := nvl(ora_sql_txt(l_sql_text), 0);
    for i in 1 .. l_cnt loop
      if l_cnt = 1 then
        insert into DDL_ACTIONS
          (counter,
           user_name,
           dt,
           ddl_type,
           object_type,
           owner,
           object_name,
           sqltext,
           osuser,
           host,
           module,
           ip)
        VALUES
          (i,
           ora_login_user,
           systimestamp,
           ora_sysevent,
           ora_dict_obj_type,
           ora_dict_obj_owner,
           ora_dict_obj_name,
           l_sql_text(i),
           v_os_user,
           v_host,
           v_module,
           v_ip);
      else
        if l_len + length(l_sql_text(i)) > 32767 then
          -- insert
          insert into DDL_ACTIONS
            (counter,
             user_name,
             dt,
             ddl_type,
             object_type,
             owner,
             object_name,
             sqltext,
             osuser,
             host,
             module,
             ip)
          VALUES
            (l_no,
             ora_login_user ,
             systimestamp,
             ora_sysevent,
             ora_dict_obj_type,
             ora_dict_obj_owner,
             ora_dict_obj_name,
             l_s,
             v_os_user,
             v_host,
             v_module,
             v_ip);

          l_len := length(l_sql_text(i));
          l_s   := l_sql_text(i);
          l_no  := l_no + 1;
        else
          -- concat
          l_s   := l_s || l_sql_text(i);
          l_len := l_len + length(l_sql_text(i));
        end if;
      end if;
    end loop;
    if l_s != '' then
      insert into DDL_ACTIONS
        (counter,
         user_name,
         dt,
         ddl_type,
         object_type,
         owner,
         object_name,
         sqltext,
         osuser,
         host,
         module,
         ip)
      VALUES
        (l_no,
         ora_login_user,
         systimestamp,
         ora_sysevent,
         ora_dict_obj_type,
         ora_dict_obj_owner,
         ora_dict_obj_name,
         l_s,
         v_os_user,
         v_host,
         v_module,
         v_ip);
    end if;
  END IF;
exception
  when others then
    v_error := 'DDL_ACTION_ERROR : ' || substr(SQLERRM, 0, 2000);
    insert into logs (dt, info) values (sysdate, v_error);

END;
/

prompt
prompt Creating trigger SERVERERROR_TRIGGER
prompt ====================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.servererror_trigger
  AFTER SERVERERROR ON DATABASE DECLARE
  l_cnt      BINARY_INTEGER := 0;
  l_sql_text ora_name_list_t;
  v_cmd      varchar2(2000);

  v_osuser  varchar2(100);
  v_sqlcode varchar2(100);

BEGIN
  v_osuser := SYS_CONTEXT('USERENV', 'OS_USER');
  IF v_osuser != ('oraprd') THEN

    l_cnt := nvl(ora_sql_txt(l_sql_text), 0);
    FOR i in 1 .. l_cnt LOOP
      v_cmd := v_cmd || l_sql_text(i);
    END LOOP;

    v_sqlcode := SQLERRM;
    insert into servererror
      (dt,
       osuser,
       host,
       terminal,
       ip,
       protocol,
       msg,
       sql_code,
       module,
       sqltext,
       username,
       language)
    values
      (sysdate,
       v_osuser,
       SYS_CONTEXT('USERENV', 'HOST'),
       SYS_CONTEXT('USERENV', 'TERMINAL'),
       SYS_CONTEXT('USERENV', 'IP_ADDRESS'),
       SYS_CONTEXT('USERENV', 'NETWORK_PROTOCOL'),
       substr(ora_server_error_msg(1), 0, 1000),
       v_sqlcode,
       SYS_CONTEXT('USERENV', 'MODULE'),
       substr(v_cmd, 0, 1000),
       SYS_CONTEXT('USERENV', 'EXTERNAL_NAME'),
       SYS_CONTEXT('USERENV', 'LANGUAGE'));

  END IF;
EXCEPTION
  WHEN others THEN
    NULL;

END ;
/

prompt
prompt Creating trigger TIB_HOLDING_LOCKS
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_HOLDING_LOCKS
  BEFORE INSERT ON HOLDING_LOCKS
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_HOLDING_LOCKS_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_HOLDING_LOCKS_BINDS
prompt ========================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_HOLDING_LOCKS_BINDS
  BEFORE INSERT ON HOLDING_LOCKS_BINDS
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_HOLDING_LOCKS_BINDS_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_HOLDING_LOCKS_DETAIL
prompt =========================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_HOLDING_LOCKS_DETAIL
  BEFORE INSERT ON HOLDING_LOCKS_DETAIL
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := sq_holding_locks_detail_id.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_IOSTAT
prompt ===========================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_IOSTAT
  BEFORE INSERT ON IOSTAT
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_IOSTAT.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_OBJ_INFO
prompt =============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_OBJ_INFO
  BEFORE INSERT ON OBJ_INFO_TAB
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_OBJ_INFO_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_POOR_PARSING
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_POOR_PARSING
  BEFORE INSERT ON POOR_PARSING
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_POOR_PARSE_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_ACTSESSHIS_TXTM
prompt ========================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_ACTSESSHIS_TXTM
  BEFORE INSERT ON REP_ACTSESSHIS_TXTM
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_ACTSESSHIS_TXTM.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_AWRS
prompt =============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_AWRS
  BEFORE INSERT ON REP_AWRS
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_AWRS.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_CPULOAD
prompt ================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_CPULOAD
  BEFORE INSERT ON REP_CPULOAD
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_CPULOAD.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_JOBS
prompt =============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_JOBS
  BEFORE INSERT ON REP_JOBS
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_JOBS.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_TOPSQL
prompt ===============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_TOPSQL
  BEFORE INSERT ON REP_TOPSQL
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_TOPSQL.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_REP_WAITCLASS
prompt ==================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_REP_WAITCLASS
  BEFORE INSERT ON REP_WAITCLASS
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_REP_WAITCLASS.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_ROWSCNT
prompt ============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_ROWSCNT
  BEFORE INSERT ON ROWSCNT
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_ROWSCNT.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_ROWSCNT_LOG
prompt ================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_ROWSCNT_LOG
  BEFORE INSERT ON ROWSCNT_LOG
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_ROWSCNT_LOG_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_ROWSCNT_MAIN
prompt =================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_ROWSCNT_MAIN
  BEFORE INSERT ON ROWSCNT_MAIN
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_ROWSCNT_MAIN_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_ROWSCNT_SCHEMAS
prompt ====================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_ROWSCNT_SCHEMAS
  BEFORE INSERT ON ROWSCNT_SCHEMAS
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_ROWSCNT_SCHEMAS.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_ROWSCNT_TAB
prompt ================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_ROWSCNT_TAB
  BEFORE INSERT ON ROWSCNT_TAB
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_ROWSCNT_TAB_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_STATISTICS_INFO
prompt ====================================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.
TIB_STATISTICS_INFO
  BEFORE INSERT ON STATISTICS_INFO
  FOR EACH ROW

WHEN(NEW.ID IS NULL)
BEGIN
  :NEW.ID := SQ_STATISTICS_INFO_ID.NEXTVAL;
END;
/

prompt
prompt Creating trigger TIB_TEMP_USAGE
prompt ===============================
prompt
CREATE OR REPLACE TRIGGER ASSECO_TOOLS.TIB_TEMP_USAGE
  BEFORE INSERT ON ASSECO_TOOLS.TEMP_USAGE
  FOR EACH ROW

 WHEN (NEW.ID IS NULL) BEGIN
  :NEW.ID := SQ_TEMP_USAGE_ID.NEXTVAL;
END;
/


spool off
