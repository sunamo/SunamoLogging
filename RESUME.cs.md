---
schema_version: 11
type: my-library
category_override: none
file_count: 65
file_extensions: cs:46, md:8, csproj:3, noext:3, json:1, slnx:1, yml:1
file_extensions_updated: 2026-10-04
avg_lines_per_file: 58
total_lines: 3655
metrics_lm: 2026-09-30 15:11:23
move_to_legacy_percent: 10
description_updated: 2026-09-30
links_updated: 2026-09-30
github_source_url: not run
origin_status: pending
origin_checked: not run
article_source_url: not run
article_status: pending
article_checked: not run
last_build_ok: not run
last_build_date: not run
last_tests_run_date: not run
covered_lines: not run
---

## Description

Podpora několika logovacích systémů podle výstupu: file logger, debug/dummy/sunamo loggery (včetně typovaných a template variant), `LogRouter` s providerem, `LoggingBootstrap`, `ConsoleTee` a `CrashHandler`. Staví na `Microsoft.Extensions.Logging`.
Balíček je self-contained: kód dříve referencovaných balíčků (SunamoCl, SunamoDependencyInjection, SunamoPlatformUwpInterop aj.) je zkopírován do `_sunamo\` jako internal a jiné Sunamo balíčky nereferencuje (SunamoCl používá jen pomocný projekt `RunnerLogging`).

## Původ zdrojáků

Původ zdrojáků se zatím nezjišťoval (`origin_status: pending`).

## Doporučení přesunu do legacy

Doporučení přesunu do legacy: **10 %** — hodnota převzata ze starší verze souboru, důvod nebyl zapsán.

## Vazby na moje repa

- Submoduly: not run
- ProjectReference / PackageReference: not run
