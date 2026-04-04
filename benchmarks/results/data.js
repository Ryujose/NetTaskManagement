window.BENCHMARK_DATA = {
  "lastUpdate": 1775268614272,
  "repoUrl": "https://github.com/Ryujose/NetTaskManagement",
  "entries": {
    "Benchmark": [
      {
        "commit": {
          "author": {
            "name": "Jose Luis Guerra Infante",
            "username": "Ryujose",
            "email": "sora_ryu@hotmail.com"
          },
          "committer": {
            "name": "GitHub",
            "username": "web-flow",
            "email": "noreply@github.com"
          },
          "id": "77ac1aace760080960911c2117588dabaaaf0872",
          "message": "feat: update benchmarks publishing branch and related documentation (#21)\n\n- Changed `gh-pages-branch` from `main` to `gh-pages` in `benchmarks.yml`.\n- Updated `CLAUDE.md` to reflect the new branch for benchmark results publishing.\n- Refined benchmark chart URL in `README.md` for consistency.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>",
          "timestamp": "2026-04-04T01:39:45Z",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/77ac1aace760080960911c2117588dabaaaf0872"
        },
        "date": 1775267189295,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 80049,
            "unit": "ns",
            "range": "± 22626.041865514173"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 126669.25,
            "unit": "ns",
            "range": "± 56714.82955615166"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 115232,
            "unit": "ns",
            "range": "± 33153.98751784366"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5197.5,
            "unit": "ns",
            "range": "± 37.749172176353746"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 9475.5,
            "unit": "ns",
            "range": "± 2900.10258439249"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 10596.6,
            "unit": "ns",
            "range": "± 4153.054574647436"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 228590,
            "unit": "ns",
            "range": "± 10442.815975588193"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 291828.4,
            "unit": "ns",
            "range": "± 20117.522270399008"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 231.16502063274385,
            "unit": "ns",
            "range": "± 1.1945485500909263"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 418.36079654693606,
            "unit": "ns",
            "range": "± 2.0301972459681905"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1725.4550479888917,
            "unit": "ns",
            "range": "± 16.630324943857214"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 114847.6,
            "unit": "ns",
            "range": "± 21338.77918954128"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 47021,
            "unit": "ns",
            "range": "± 14863.136008258822"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 55471,
            "unit": "ns",
            "range": "± 6831.687968577019"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 4979.2,
            "unit": "ns",
            "range": "± 310.78963946695524"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 11263.5,
            "unit": "ns",
            "range": "± 200.98590331994265"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4547.2,
            "unit": "ns",
            "range": "± 505.6418693106812"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 228674.6,
            "unit": "ns",
            "range": "± 16831.599130801565"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 269283.25,
            "unit": "ns",
            "range": "± 19179.315374903246"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 249.99289093017578,
            "unit": "ns",
            "range": "± 1.2327463944046197"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 416.4913489341736,
            "unit": "ns",
            "range": "± 3.8875336199133157"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1699.718316268921,
            "unit": "ns",
            "range": "± 5.842861740312404"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 36661,
            "unit": "ns",
            "range": "± 14237.032193075447"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 51689.7,
            "unit": "ns",
            "range": "± 22517.28553134236"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 90729.25,
            "unit": "ns",
            "range": "± 36375.80947438375"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5789,
            "unit": "ns",
            "range": "± 452.33947428894595"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 5780.25,
            "unit": "ns",
            "range": "± 263.9461750685797"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 5037,
            "unit": "ns",
            "range": "± 77.08436936240706"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 249421.75,
            "unit": "ns",
            "range": "± 10045.869644618462"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 268724.8,
            "unit": "ns",
            "range": "± 28341.21672052913"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 196.9713213443756,
            "unit": "ns",
            "range": "± 0.9549040238980265"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 396.75517320632935,
            "unit": "ns",
            "range": "± 5.780288662080896"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1503.5677223205566,
            "unit": "ns",
            "range": "± 8.653153894340829"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "sora_ryu@hotmail.com",
            "name": "Jose Luis Guerra Infante",
            "username": "Ryujose"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "6059d7c8bb3be9452ec1d1199d1df84210c2d8f4",
          "message": "feat: enhance task deletion API and CI configuration, add dependabot and update documentation (#22)\n\n* feat: enhance task deletion API and CI configuration, add dependabot, and update documentation\n\n- Renamed `sendDataToInternalQueue` to `sendDisposedDataToInternalQueue` in `DeleteTask()` for clarity and consistency.\n- Updated `LifecycleBenchmarks` to align with the renamed parameter.\n- Integrated code coverage collection and upload to Codecov in CI workflow (`ci.yml`).\n- Added Codecov and CI status badges to `README.md`.\n- Introduced `dependabot.yml` for automated dependency updates.\n- Improved documentation with updated links and workflow details.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: ensure consistency in `DeleteTask` parameter naming\n\n- Corrected parameter name in `.claude/rules` and test file: `sendDataToInternalQueue` updated to `sendDisposedDataToInternalQueue`.\n- Improved clarity by ensuring consistent usage of parameter names across interface and implementation.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: add missing Codecov token to CI workflow configuration\n\n- Updated `.github/workflows/ci.yml` to include `CODECOV_TOKEN` for uploading coverage reports with Codecov.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n---------\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>",
          "timestamp": "2026-04-04T04:07:17+02:00",
          "tree_id": "d43fae9388bd6ec08aaffe3e961df23579b09000",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/6059d7c8bb3be9452ec1d1199d1df84210c2d8f4"
        },
        "date": 1775268614003,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 46677.7,
            "unit": "ns",
            "range": "± 14311.872578387498"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 92129.5,
            "unit": "ns",
            "range": "± 8197.576593115797"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 126003.5,
            "unit": "ns",
            "range": "± 14096.990151565453"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 6102.8,
            "unit": "ns",
            "range": "± 329.88967852905006"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 4867,
            "unit": "ns",
            "range": "± 364.017398851575"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 5284.75,
            "unit": "ns",
            "range": "± 159.34318309861894"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 254593.8,
            "unit": "ns",
            "range": "± 37795.58156319334"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 284906.3,
            "unit": "ns",
            "range": "± 27558.61082674524"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 218.83033237457275,
            "unit": "ns",
            "range": "± 1.4882450654604753"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 397.5635771751404,
            "unit": "ns",
            "range": "± 2.635853687520236"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1717.5664763450623,
            "unit": "ns",
            "range": "± 2.5762816391802006"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 85955.75,
            "unit": "ns",
            "range": "± 40995.31624771298"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 34634.25,
            "unit": "ns",
            "range": "± 11021.737351706399"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 99797.6,
            "unit": "ns",
            "range": "± 34995.41754715894"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5472.9,
            "unit": "ns",
            "range": "± 445.5090347007567"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10658.9,
            "unit": "ns",
            "range": "± 943.7771453049708"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4718.7,
            "unit": "ns",
            "range": "± 664.4183170262542"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 246115,
            "unit": "ns",
            "range": "± 23456.0535363475"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 283691.8,
            "unit": "ns",
            "range": "± 17301.536512691582"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 243.5427612066269,
            "unit": "ns",
            "range": "± 0.5025155954356892"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 421.1692644119263,
            "unit": "ns",
            "range": "± 1.7554806020528597"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1819.801019668579,
            "unit": "ns",
            "range": "± 3.612877755506953"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 69112.5,
            "unit": "ns",
            "range": "± 49109.47501755644"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 68031.3,
            "unit": "ns",
            "range": "± 26545.730385506442"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 111814.9,
            "unit": "ns",
            "range": "± 44845.920202399684"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5934.6,
            "unit": "ns",
            "range": "± 245.22397925162213"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 5424.5,
            "unit": "ns",
            "range": "± 198.27674262673034"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 5105,
            "unit": "ns",
            "range": "± 177.7076250474357"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 238106.4,
            "unit": "ns",
            "range": "± 13997.80708896933"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 276276.8,
            "unit": "ns",
            "range": "± 10138.888607732111"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 201.87876427173615,
            "unit": "ns",
            "range": "± 3.2259988003017384"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 415.3422471046448,
            "unit": "ns",
            "range": "± 1.0711880498371507"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1432.4106937408446,
            "unit": "ns",
            "range": "± 1.6431817064058312"
          }
        ]
      }
    ]
  }
}