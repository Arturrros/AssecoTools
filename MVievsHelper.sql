SELECT
    owner,
    mview_name,
    refresh_method,      -- FAST, COMPLETE, FORCE
    last_refresh_type,   -- Jak faktycznie wykonano ostatnie odświeżenie
    last_refresh_date,   -- Data i godzina ostatniego odświeżenia
    staleness,           -- FRESH, STALE, UNUSABLE, UNKNOWN itp.
    compile_state        -- VALID / NEEDS_COMPILE
FROM dba_mviews
ORDER BY last_refresh_date DESC NULLS LAST;

SELECT
    r.refresh_id,
    s.mv_owner,
    s.mv_name,
    r.start_time,
    r.end_time,
    r.elapsed_time,      -- Czas trwania w sekundach
    s.refresh_method,
    s.num_rows_ins,
    s.num_rows_upd,
    s.num_rows_del
FROM dba_mvref_run_stats r
JOIN dba_mvref_stats s ON r.refresh_id = s.refresh_id
ORDER BY r.start_time DESC;

SELECT
    s.sid,
    s.serial#,
    s.username,
    s.status,
    s.sql_id,
    s.event,             -- Na co czeka sesja (np. I/O, blokady)
    s.seconds_in_wait
FROM v$session s
WHERE s.program LIKE '%J0%' -- Procesy jobów DBMS_SCHEDULER / DBMS_JOB
   OR s.action LIKE '%MV%'
   OR s.module LIKE '%DBMS_MVIEW%';

   SELECT
    job_name,
    status,              -- SUCCEEDED, FAILED, STOPPED
    actual_start_date,
    run_duration,
    additional_info      -- Treść błędu w razie niepowodzenia
FROM dba_scheduler_job_run_details
WHERE job_name LIKE '%MVIEW%' -- Dopasuj do nazewnictwa swoich jobów
ORDER BY actual_start_date DESC;


SELECT
    rowner,
    rname,
    broken,              -- Y oznacza, że zadanie przestało się wykonywać po błędach
    failures,
    last_date,
    next_date
FROM dba_refresh;

