---
schema_version: 2
type: library
file_count: 65
delete_recommendation_percent: 10
generated_date: 2026-09-30
generated_time: 15:11:23
last_build_ok: yes
last_build_date: 2026-10-02
last_tests_run_date: 2026-10-02
covered_lines: 0
total_lines: 2371
---

## Description

Podpora několika logovacích systémů podle výstupu: file logger, debug/dummy/sunamo loggery (včetně typovaných a template variant), `LogRouter` s providerem, `LoggingBootstrap`, `ConsoleTee` a `CrashHandler`. Staví na `Microsoft.Extensions.Logging`.
Balíček je self-contained: kód dříve referencovaných balíčků (SunamoCl, SunamoDependencyInjection, SunamoPlatformUwpInterop aj.) je zkopírován do `_sunamo\` jako internal a jiné Sunamo balíčky nereferencuje (SunamoCl používá jen pomocný projekt `RunnerLogging`).
