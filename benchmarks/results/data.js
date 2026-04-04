window.BENCHMARK_DATA = {
  "lastUpdate": 1775267189911,
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
      }
    ]
  }
}