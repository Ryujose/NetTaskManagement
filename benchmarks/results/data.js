window.BENCHMARK_DATA = {
  "lastUpdate": 1775341171025,
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
      },
      {
        "commit": {
          "author": {
            "email": "49699333+dependabot[bot]@users.noreply.github.com",
            "name": "dependabot[bot]",
            "username": "dependabot[bot]"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "1936f1d716bbcf5ea8810951f7d7c5c7a29963f3",
          "message": "Bump actions/checkout from 4 to 6 (#23)\n\nBumps [actions/checkout](https://github.com/actions/checkout) from 4 to 6.\n- [Release notes](https://github.com/actions/checkout/releases)\n- [Changelog](https://github.com/actions/checkout/blob/main/CHANGELOG.md)\n- [Commits](https://github.com/actions/checkout/compare/v4...v6)\n\n---\nupdated-dependencies:\n- dependency-name: actions/checkout\n  dependency-version: '6'\n  dependency-type: direct:production\n  update-type: version-update:semver-major\n...\n\nSigned-off-by: dependabot[bot] <support@github.com>\nCo-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>",
          "timestamp": "2026-04-04T04:10:20+02:00",
          "tree_id": "1ae672671a8c61a651cdd1338a38b4178f53853e",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/1936f1d716bbcf5ea8810951f7d7c5c7a29963f3"
        },
        "date": 1775268784327,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 48888.8,
            "unit": "ns",
            "range": "± 20585.539966199576"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 54116.25,
            "unit": "ns",
            "range": "± 16109.350811976668"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 132988.2,
            "unit": "ns",
            "range": "± 46884.915470756685"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5386.4,
            "unit": "ns",
            "range": "± 107.86241235944986"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10297,
            "unit": "ns",
            "range": "± 4950.172320232903"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 6003.25,
            "unit": "ns",
            "range": "± 581.4329282041051"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 260137.2,
            "unit": "ns",
            "range": "± 24777.37277638612"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 293794.2,
            "unit": "ns",
            "range": "± 26872.15629978361"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 233.9755097389221,
            "unit": "ns",
            "range": "± 0.8279010816393696"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 399.79782581329346,
            "unit": "ns",
            "range": "± 1.995964835396246"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1723.3251964569092,
            "unit": "ns",
            "range": "± 4.0720292064575005"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 25530.75,
            "unit": "ns",
            "range": "± 958.4262708558581"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 41478.25,
            "unit": "ns",
            "range": "± 7826.55149155744"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 102531.2,
            "unit": "ns",
            "range": "± 26663.69835937993"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 4836.2,
            "unit": "ns",
            "range": "± 165.38198208994837"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 4841,
            "unit": "ns",
            "range": "± 149.29389360140175"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4933.3,
            "unit": "ns",
            "range": "± 272.3191142758804"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 246306.9,
            "unit": "ns",
            "range": "± 12021.027194046272"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 246699,
            "unit": "ns",
            "range": "± 10058.54669754367"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 244.1607092857361,
            "unit": "ns",
            "range": "± 1.0060660316913312"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 412.03048458099363,
            "unit": "ns",
            "range": "± 3.1012345909630445"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1699.1770787239075,
            "unit": "ns",
            "range": "± 1.188449896720207"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 29402.75,
            "unit": "ns",
            "range": "± 986.0163538197528"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 44345.25,
            "unit": "ns",
            "range": "± 5769.045869985781"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 1112345,
            "unit": "ns",
            "range": "± 1392061.4139061896"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5228.2,
            "unit": "ns",
            "range": "± 46.916947897321705"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10891,
            "unit": "ns",
            "range": "± 2656.9288661911896"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 5206,
            "unit": "ns",
            "range": "± 70.92249290598858"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 294198.2,
            "unit": "ns",
            "range": "± 48004.360054061755"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 270916.2,
            "unit": "ns",
            "range": "± 24377.432467345694"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 190.43844175338745,
            "unit": "ns",
            "range": "± 0.9375994883301947"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 436.7627709388733,
            "unit": "ns",
            "range": "± 3.239295315670166"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1381.3637409210205,
            "unit": "ns",
            "range": "± 4.9598791887219145"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "49699333+dependabot[bot]@users.noreply.github.com",
            "name": "dependabot[bot]",
            "username": "dependabot[bot]"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "4efa7643ab81decbdf16bd49acdc31422977cda4",
          "message": "Bump actions/setup-dotnet from 4 to 5 (#24)\n\nBumps [actions/setup-dotnet](https://github.com/actions/setup-dotnet) from 4 to 5.\n- [Release notes](https://github.com/actions/setup-dotnet/releases)\n- [Commits](https://github.com/actions/setup-dotnet/compare/v4...v5)\n\n---\nupdated-dependencies:\n- dependency-name: actions/setup-dotnet\n  dependency-version: '5'\n  dependency-type: direct:production\n  update-type: version-update:semver-major\n...\n\nSigned-off-by: dependabot[bot] <support@github.com>\nCo-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>",
          "timestamp": "2026-04-04T04:10:37+02:00",
          "tree_id": "7058a56c96412c5f058800348180a4118c6151fe",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/4efa7643ab81decbdf16bd49acdc31422977cda4"
        },
        "date": 1775268807760,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 57458.5,
            "unit": "ns",
            "range": "± 15220.822119714821"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 168786.1,
            "unit": "ns",
            "range": "± 126429.22199910905"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 188045.7,
            "unit": "ns",
            "range": "± 81697.25119733221"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5932.2,
            "unit": "ns",
            "range": "± 368.26444302973374"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 11336.75,
            "unit": "ns",
            "range": "± 515.5717699796993"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 17106,
            "unit": "ns",
            "range": "± 9285.339439137377"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 262840.8,
            "unit": "ns",
            "range": "± 28668.759891212594"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 281268.8,
            "unit": "ns",
            "range": "± 15367.283777558088"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 228.68481101989747,
            "unit": "ns",
            "range": "± 1.963076769445143"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 417.92208070755004,
            "unit": "ns",
            "range": "± 2.4316873024334393"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1911.0504070281982,
            "unit": "ns",
            "range": "± 12.766448949148437"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 36348,
            "unit": "ns",
            "range": "± 12620.941308000763"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 62895.25,
            "unit": "ns",
            "range": "± 15556.388106391107"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 107645.5,
            "unit": "ns",
            "range": "± 34656.89095980769"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5370,
            "unit": "ns",
            "range": "± 123.38962679253066"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 4522,
            "unit": "ns",
            "range": "± 244.41903908383787"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4615.5,
            "unit": "ns",
            "range": "± 731.0444582923805"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 237962.1,
            "unit": "ns",
            "range": "± 12205.230509908448"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 267202.8,
            "unit": "ns",
            "range": "± 21539.964640175247"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 236.80119833946227,
            "unit": "ns",
            "range": "± 0.6211568255084243"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 398.33303594589233,
            "unit": "ns",
            "range": "± 0.24970433926511884"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1959.3357460021973,
            "unit": "ns",
            "range": "± 14.66804917902482"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 36577.9,
            "unit": "ns",
            "range": "± 10399.835445813555"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 60916.5,
            "unit": "ns",
            "range": "± 27047.60851720536"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 52121,
            "unit": "ns",
            "range": "± 1386.1420081170136"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5380.25,
            "unit": "ns",
            "range": "± 59.55599605973077"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 5486,
            "unit": "ns",
            "range": "± 449.0827689116265"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4637.4,
            "unit": "ns",
            "range": "± 347.64248877258945"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 242476.6,
            "unit": "ns",
            "range": "± 14804.612163106469"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 297307.3,
            "unit": "ns",
            "range": "± 42053.29811917253"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 178.96200001239777,
            "unit": "ns",
            "range": "± 0.3684112471922305"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 312.41389858722687,
            "unit": "ns",
            "range": "± 1.5332337019696847"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1372.7234191894531,
            "unit": "ns",
            "range": "± 11.376789235489522"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "49699333+dependabot[bot]@users.noreply.github.com",
            "name": "dependabot[bot]",
            "username": "dependabot[bot]"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "399dc150f643d7d1e3e7060966652defacab6f7b",
          "message": "Bump codecov/codecov-action from 4 to 6 (#25)\n\nBumps [codecov/codecov-action](https://github.com/codecov/codecov-action) from 4 to 6.\n- [Release notes](https://github.com/codecov/codecov-action/releases)\n- [Changelog](https://github.com/codecov/codecov-action/blob/main/CHANGELOG.md)\n- [Commits](https://github.com/codecov/codecov-action/compare/v4...v6)\n\n---\nupdated-dependencies:\n- dependency-name: codecov/codecov-action\n  dependency-version: '6'\n  dependency-type: direct:production\n  update-type: version-update:semver-major\n...\n\nSigned-off-by: dependabot[bot] <support@github.com>\nCo-authored-by: dependabot[bot] <49699333+dependabot[bot]@users.noreply.github.com>",
          "timestamp": "2026-04-04T04:10:48+02:00",
          "tree_id": "5fd36eb7fac5c2854d78a4a53b0849286bd85130",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/399dc150f643d7d1e3e7060966652defacab6f7b"
        },
        "date": 1775268819310,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 157631.6,
            "unit": "ns",
            "range": "± 45669.10053088412"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 133376.5,
            "unit": "ns",
            "range": "± 21656.00925840216"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 190093.25,
            "unit": "ns",
            "range": "± 13459.431795213348"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5515.5,
            "unit": "ns",
            "range": "± 58.25518574913882"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 5685,
            "unit": "ns",
            "range": "± 379.07870774638167"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 19066,
            "unit": "ns",
            "range": "± 6592.459075843146"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 239232,
            "unit": "ns",
            "range": "± 11130.385273355694"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 325482.2,
            "unit": "ns",
            "range": "± 41461.40016569629"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 273.4687896728516,
            "unit": "ns",
            "range": "± 2.4167294063045723"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 460.8224680900574,
            "unit": "ns",
            "range": "± 3.3887859717466458"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1993.9301383972168,
            "unit": "ns",
            "range": "± 14.124868131108615"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 45166.8,
            "unit": "ns",
            "range": "± 19299.891442699878"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 83095,
            "unit": "ns",
            "range": "± 36242.19541501315"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 109883.5,
            "unit": "ns",
            "range": "± 37260.09663612446"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5096.5,
            "unit": "ns",
            "range": "± 86.5544144839919"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 12795.6,
            "unit": "ns",
            "range": "± 4469.301544984406"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4894.25,
            "unit": "ns",
            "range": "± 592.2405901883682"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 258232.4,
            "unit": "ns",
            "range": "± 13798.347303209903"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 289888,
            "unit": "ns",
            "range": "± 18103.12184863889"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 257.2006983757019,
            "unit": "ns",
            "range": "± 1.2041811748956524"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 409.2461220026016,
            "unit": "ns",
            "range": "± 2.3222394483906754"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1888.358854675293,
            "unit": "ns",
            "range": "± 6.158712341640502"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 49744.6,
            "unit": "ns",
            "range": "± 16681.200145672974"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 72904.9,
            "unit": "ns",
            "range": "± 32027.74514073696"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 87949.25,
            "unit": "ns",
            "range": "± 10542.414219870765"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 7232,
            "unit": "ns",
            "range": "± 461.6232229860192"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 11037.5,
            "unit": "ns",
            "range": "± 2582.0951764022952"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 8212.75,
            "unit": "ns",
            "range": "± 263.9259681552133"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 239977,
            "unit": "ns",
            "range": "± 21965.000261780104"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 312190.1,
            "unit": "ns",
            "range": "± 42483.785486936074"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 204.27749955654144,
            "unit": "ns",
            "range": "± 1.056061423057324"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 428.0164765357971,
            "unit": "ns",
            "range": "± 5.564467329402689"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1523.2980766296387,
            "unit": "ns",
            "range": "± 26.31136232741992"
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
          "id": "8ae511376da04fc4561a28b5d9de9b3c9530a7c9",
          "message": "feat: add Dataflow vs NetTaskManagement benchmarks and optimize task cancellation logic (#26)\n\n* feat: add Dataflow vs NetTaskManagement benchmarks and optimize task cancellation logic\n\n- Introduced `DataflowComparisonBenchmarks`:\n  - Compares NetTaskManagement and TPL Dataflow for starting, canceling, and processing N workers/items.\n  - Helps developers choose between tools based on specific use cases.\n  - Added details and execution instructions to `README.md`.\n- Enhanced `CancelAllTasks()`:\n  - Introduced `CancelAllTasksParallelThreshold` for selective parallelization based on task count.\n  - Optimized logic for sequential vs parallel task cancellation to minimize overhead.\n  - Improved handling for completed tasks and cancellation token sources.\n- Updated CI benchmark workflow to include the new comparison benchmarks.\n- Extended documentation with detailed benchmark descriptions and updated example output.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: add unit tests for completed tasks and task cancellation edge cases\n\n- Introduced `AlreadyCompletedTaskCheckTaskStatusCompleted_CompletedStatus` to validate handling of completed tasks in `CheckTaskStatusCompleted()`.\n- Added `AllTasksInExceptListCancelAllTasks_TasksNotFoundToBeCancelledStatus` test to confirm behavior when all tasks are excluded from cancellation.\n- Implemented `AlreadyCompletedTaskCancelAllTasks_AllTasksCancelPetitionAcceptedStatus` to verify cancellation behavior for already completed tasks.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: add internal visibility for parallel cancellation testing and new unit test\n\n- Exposed `CancelAllTasksParallelThreshold` as `internal` to facilitate testing of parallel cancellation logic.\n- Added `ParallelPathCancelAllTasks_AllTasksCancelPetitionAcceptedStatus` unit test to validate parallel task cancellation behavior with a reduced threshold.\n- Modified project file to include `InternalsVisibleTo` for test assembly access.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: add unit tests for additional edge cases in `CancelAllTasks()`\n\n- Added three new unit tests to validate behavior under diverse scenarios:\n  - `ParallelPathWithExceptListCancelAllTasks_AllTasksCancelPetitionAcceptedStatus`: Verifies cancellation of tasks excluding specific exceptions.\n  - `ParallelPathAlreadyCompletedTaskCancelAllTasks_AllTasksCancelPetitionAcceptedStatus`: Confirms behavior when all tasks are already completed.\n  - `ParallelPathAllTasksExceptedCancelAllTasks_TasksNotFoundToBeCancelledStatus`: Ensures correct status when all tasks are excluded from cancellation.\n- Refactored `CancelAllTasks()` to remove redundant checks for `CancellationTokenSource`, improving code readability and maintainability.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n* feat: expand Dataflow vs NetTaskManagement benchmarks with detailed scenarios and threading model distinctions\n\n- Enhanced `DataflowComparisonBenchmarks` with new benchmarking scenarios:\n  - Added separate benchmarks for `LongRunning` and `Pool` task creation options.\n  - Introduced `ParallelBlockingWork` benchmark to measure throughput for blocking workloads.\n- Implemented `CancelWaitDeleteAll()` to ensure proper teardown of tasks in benchmarks.\n- Detailed threading model trade-offs for different task behaviors (e.g., high-throughput anonymous items vs long-running dedicated workers).\n- Improved clarity with updated benchmark naming and comments:\n  - Split benchmarks to explicitly track `LongRunning` vs `Pool` configurations.\n  - Expanded documentation of benchmarks' purpose, implementation, and tradeoffs.\n- Updated iteration setup and cleanup logic to support task type distinctions.\n- Ensured accurate benchmarking by accounting for thread-pool injection delays in `ParallelBlockingWork`.\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>\n\n---------\n\nSigned-off-by: Jose Luis Guerra Infante <sora_ryu@hotmail.com>",
          "timestamp": "2026-04-04T23:47:07+02:00",
          "tree_id": "1d57a7b9df70c94afbdb6b0a466c32ddf278013a",
          "url": "https://github.com/Ryujose/NetTaskManagement/commit/8ae511376da04fc4561a28b5d9de9b3c9530a7c9"
        },
        "date": 1775341170219,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 23875.75,
            "unit": "ns",
            "range": "± 5140.475618396933"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 25416.75,
            "unit": "ns",
            "range": "± 6929.592069523284"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 49986.25,
            "unit": "ns",
            "range": "± 5313.786056099737"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 6273.75,
            "unit": "ns",
            "range": "± 132.46729156034456"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10202.3,
            "unit": "ns",
            "range": "± 1936.5170280686923"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 16394.4,
            "unit": "ns",
            "range": "± 8246.766899822014"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 188527,
            "unit": "ns",
            "range": "± 10452.688155047326"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 229682.5,
            "unit": "ns",
            "range": "± 18975.269523777522"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 210.32649705410003,
            "unit": "ns",
            "range": "± 0.7878578252604737"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 430.8369497299194,
            "unit": "ns",
            "range": "± 2.048530064036321"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1809.3766170501708,
            "unit": "ns",
            "range": "± 6.694194480820234"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 10)",
            "value": 39166844.4,
            "unit": "ns",
            "range": "± 6003883.177148129"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 10)",
            "value": 22181.5,
            "unit": "ns",
            "range": "± 196.4306493396588"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 10)",
            "value": 19482.5,
            "unit": "ns",
            "range": "± 1695.204019186678"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 10)",
            "value": 35728.3,
            "unit": "ns",
            "range": "± 3109.9606428377833"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 10)",
            "value": 34522.9,
            "unit": "ns",
            "range": "± 4537.576037930384"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 10)",
            "value": 1673.25,
            "unit": "ns",
            "range": "± 165.2803174408052"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 10)",
            "value": 2483543.1,
            "unit": "ns",
            "range": "± 107300.16370816962"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 10)",
            "value": 1594827.2,
            "unit": "ns",
            "range": "± 33475.41500414894"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 10)",
            "value": 65235.75,
            "unit": "ns",
            "range": "± 1074.6417620149828"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 10)",
            "value": 23980590,
            "unit": "ns",
            "range": "± 675209.8216339865"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 10)",
            "value": 51223808.5,
            "unit": "ns",
            "range": "± 11917147.198315522"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 10)",
            "value": 40368629.8,
            "unit": "ns",
            "range": "± 96433.90085286397"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 50)",
            "value": 2705477973,
            "unit": "ns",
            "range": "± 161790911.34098136"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 50)",
            "value": 65814.9,
            "unit": "ns",
            "range": "± 12265.164238606836"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 50)",
            "value": 31090,
            "unit": "ns",
            "range": "± 3175.4525661706866"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 50)",
            "value": 42083.8,
            "unit": "ns",
            "range": "± 6098.621090049783"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 50)",
            "value": 44341.5,
            "unit": "ns",
            "range": "± 3514.2992189055276"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 50)",
            "value": 1656.1,
            "unit": "ns",
            "range": "± 468.7283861683651"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 50)",
            "value": 11030348.6,
            "unit": "ns",
            "range": "± 250253.25547392986"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 50)",
            "value": 6830781.9,
            "unit": "ns",
            "range": "± 105033.27791371648"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 50)",
            "value": 96691.7,
            "unit": "ns",
            "range": "± 15516.925362326132"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 50)",
            "value": 33585131.75,
            "unit": "ns",
            "range": "± 1012662.3501695502"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 50)",
            "value": 226152351.2,
            "unit": "ns",
            "range": "± 25157176.607849017"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 50)",
            "value": 237766174,
            "unit": "ns",
            "range": "± 33017666.943886578"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 100)",
            "value": 24853266786.75,
            "unit": "ns",
            "range": "± 655893328.9252142"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 100)",
            "value": 109447.9,
            "unit": "ns",
            "range": "± 27033.51983741666"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 100)",
            "value": 58265.3,
            "unit": "ns",
            "range": "± 12223.207259144387"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 100)",
            "value": 121617.4,
            "unit": "ns",
            "range": "± 91790.03549841345"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 100)",
            "value": 1172043.4,
            "unit": "ns",
            "range": "± 1529892.3593154522"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 100)",
            "value": 1522,
            "unit": "ns",
            "range": "± 144.64093473149293"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 100)",
            "value": 20386196.4,
            "unit": "ns",
            "range": "± 245365.6833509935"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 100)",
            "value": 14943514.3,
            "unit": "ns",
            "range": "± 493847.6631631459"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 100)",
            "value": 153776,
            "unit": "ns",
            "range": "± 26895.746810601857"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 100)",
            "value": 47557201.4,
            "unit": "ns",
            "range": "± 1841636.4361677903"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 100)",
            "value": 410677081.9,
            "unit": "ns",
            "range": "± 18715397.39329413"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 100)",
            "value": 378486548.1,
            "unit": "ns",
            "range": "± 32909324.40588245"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 10300,
            "unit": "ns",
            "range": "± 71.6147098483731"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 14028.8,
            "unit": "ns",
            "range": "± 643.1537141306112"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 19467.75,
            "unit": "ns",
            "range": "± 377.2226354113266"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 4889.75,
            "unit": "ns",
            "range": "± 285.46146850319394"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10443.75,
            "unit": "ns",
            "range": "± 189.44370315918835"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 5063.25,
            "unit": "ns",
            "range": "± 848.2685797159609"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 189208.5,
            "unit": "ns",
            "range": "± 11214.06099501871"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 244601.3,
            "unit": "ns",
            "range": "± 26714.448818944406"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 204.11503462791444,
            "unit": "ns",
            "range": "± 0.491769034898613"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 419.3168659210205,
            "unit": "ns",
            "range": "± 2.2767806957895935"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1783.3301429748535,
            "unit": "ns",
            "range": "± 4.127378029795923"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 10)",
            "value": 38708150.4,
            "unit": "ns",
            "range": "± 5588039.343439772"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 10)",
            "value": 19356.8,
            "unit": "ns",
            "range": "± 2831.2916133807203"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 10)",
            "value": 17933,
            "unit": "ns",
            "range": "± 1234.7222359705036"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 10)",
            "value": 31319.25,
            "unit": "ns",
            "range": "± 24649.10454593973"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 10)",
            "value": 22436.25,
            "unit": "ns",
            "range": "± 753.3442661448571"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 10)",
            "value": 1147,
            "unit": "ns",
            "range": "± 162.75902432737792"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 10)",
            "value": 2000244.5,
            "unit": "ns",
            "range": "± 10292.154957377326"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 10)",
            "value": 1484059.8,
            "unit": "ns",
            "range": "± 27662.652542733496"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 10)",
            "value": 54966,
            "unit": "ns",
            "range": "± 5843.093843732673"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 10)",
            "value": 23866713.7,
            "unit": "ns",
            "range": "± 406919.0199753509"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 10)",
            "value": 50851086.6,
            "unit": "ns",
            "range": "± 11727260.906047085"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 10)",
            "value": 40419867,
            "unit": "ns",
            "range": "± 127539.9341676951"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 50)",
            "value": 2797955804.1,
            "unit": "ns",
            "range": "± 170198337.53581494"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 50)",
            "value": 58009,
            "unit": "ns",
            "range": "± 10525.212048536916"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 50)",
            "value": 27458.4,
            "unit": "ns",
            "range": "± 7014.692495042103"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 50)",
            "value": 35263.8,
            "unit": "ns",
            "range": "± 3851.4069766774846"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 50)",
            "value": 38766.75,
            "unit": "ns",
            "range": "± 521.6354889511769"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 50)",
            "value": 1445.4,
            "unit": "ns",
            "range": "± 246.7585864767425"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 50)",
            "value": 10031496.7,
            "unit": "ns",
            "range": "± 395576.7067190635"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 50)",
            "value": 6810154.5,
            "unit": "ns",
            "range": "± 87971.55561032213"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 50)",
            "value": 87269.9,
            "unit": "ns",
            "range": "± 16609.39366141943"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 50)",
            "value": 33464912.3,
            "unit": "ns",
            "range": "± 1211655.46572229"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 50)",
            "value": 237727282.2,
            "unit": "ns",
            "range": "± 11343855.582006838"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 50)",
            "value": 237613240.4,
            "unit": "ns",
            "range": "± 32989298.777051754"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 100)",
            "value": 23984532999.3,
            "unit": "ns",
            "range": "± 914362828.2254132"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 100)",
            "value": 93734.8,
            "unit": "ns",
            "range": "± 19606.49473261348"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 100)",
            "value": 51885.7,
            "unit": "ns",
            "range": "± 8443.380673640151"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 100)",
            "value": 252833.7,
            "unit": "ns",
            "range": "± 240637.75923096525"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 100)",
            "value": 69683.4,
            "unit": "ns",
            "range": "± 40172.62196197804"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 100)",
            "value": 1682.1,
            "unit": "ns",
            "range": "± 685.540881348443"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 100)",
            "value": 21045344.25,
            "unit": "ns",
            "range": "± 539712.0814078404"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 100)",
            "value": 14631725.6,
            "unit": "ns",
            "range": "± 746006.5188527511"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 100)",
            "value": 141786.5,
            "unit": "ns",
            "range": "± 9454.664968504525"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 100)",
            "value": 47886646.5,
            "unit": "ns",
            "range": "± 1433391.7051045399"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 100)",
            "value": 447365101.8,
            "unit": "ns",
            "range": "± 41975970.16198234"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 100)",
            "value": 462881372.1,
            "unit": "ns",
            "range": "± 55034096.310574956"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 1)",
            "value": 9935.5,
            "unit": "ns",
            "range": "± 127.4807174961505"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 10)",
            "value": 12124.25,
            "unit": "ns",
            "range": "± 254.13562651990898"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks.CancelAllTasks(TaskCount: 50)",
            "value": 20406.75,
            "unit": "ns",
            "range": "± 1248.2287116817442"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.RegisterTask",
            "value": 5205.6,
            "unit": "ns",
            "range": "± 680.383935730408"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.StartTask",
            "value": 10973,
            "unit": "ns",
            "range": "± 354.08332352710426"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.CancelTask",
            "value": 4251.25,
            "unit": "ns",
            "range": "± 192.30596281273583"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.DeleteTask",
            "value": 187534.6,
            "unit": "ns",
            "range": "± 10870.627870550992"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks.FullLifecycle",
            "value": 230182.75,
            "unit": "ns",
            "range": "± 14638.770517020888"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 1)",
            "value": 234.08322439193725,
            "unit": "ns",
            "range": "± 1.588496034367162"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 10)",
            "value": 371.7366345405579,
            "unit": "ns",
            "range": "± 1.3070394799985003"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks.GetTasksStatus(TaskCount: 50)",
            "value": 1601.7106676101685,
            "unit": "ns",
            "range": "± 3.2451545946737044"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 10)",
            "value": 35395643.4,
            "unit": "ns",
            "range": "± 4808581.059173829"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 10)",
            "value": 15677,
            "unit": "ns",
            "range": "± 478.1568780222658"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 10)",
            "value": 18388.8,
            "unit": "ns",
            "range": "± 8366.616622028287"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 10)",
            "value": 67476.75,
            "unit": "ns",
            "range": "± 72987.7173154269"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 10)",
            "value": 27292.1,
            "unit": "ns",
            "range": "± 7600.738668050626"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 10)",
            "value": 945.75,
            "unit": "ns",
            "range": "± 99.15097915132591"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 10)",
            "value": 2002336.7,
            "unit": "ns",
            "range": "± 80062.69721412088"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 10)",
            "value": 1557156.4,
            "unit": "ns",
            "range": "± 37800.390425232385"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 10)",
            "value": 56238.25,
            "unit": "ns",
            "range": "± 6542.9086485548105"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 10)",
            "value": 23411427.1,
            "unit": "ns",
            "range": "± 543516.0976606857"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 10)",
            "value": 50685282,
            "unit": "ns",
            "range": "± 11640865.308207547"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 10)",
            "value": 40447473,
            "unit": "ns",
            "range": "± 64400.401582598846"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 50)",
            "value": 2712090680.1,
            "unit": "ns",
            "range": "± 91094685.84755877"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 50)",
            "value": 59931.4,
            "unit": "ns",
            "range": "± 11973.496682256191"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 50)",
            "value": 36436.2,
            "unit": "ns",
            "range": "± 9603.680685029047"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 50)",
            "value": 36525.6,
            "unit": "ns",
            "range": "± 3052.9337202107745"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 50)",
            "value": 40031.3,
            "unit": "ns",
            "range": "± 1842.9306009722668"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 50)",
            "value": 1404.25,
            "unit": "ns",
            "range": "± 207.59315178171622"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 50)",
            "value": 9914847.25,
            "unit": "ns",
            "range": "± 715384.4079637068"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 50)",
            "value": 6443374.3,
            "unit": "ns",
            "range": "± 77136.68377432362"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 50)",
            "value": 84223.4,
            "unit": "ns",
            "range": "± 9042.334615573569"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 50)",
            "value": 33722073.6,
            "unit": "ns",
            "range": "± 796861.4088665607"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 50)",
            "value": 242018279.6,
            "unit": "ns",
            "range": "± 18209288.487834096"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 50)",
            "value": 237358561.7,
            "unit": "ns",
            "range": "± 33108289.748571333"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_LongRunning(N: 100)",
            "value": 23930276463.75,
            "unit": "ns",
            "range": "± 997131739.128619"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_StartNWorkers_Pool(N: 100)",
            "value": 87736.5,
            "unit": "ns",
            "range": "± 5568.361518436101"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_StartNWorkers(N: 100)",
            "value": 42093.25,
            "unit": "ns",
            "range": "± 4735.609878005859"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_LongRunning(N: 100)",
            "value": 131176.7,
            "unit": "ns",
            "range": "± 83262.81941659194"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_CancelNWorkers_Pool(N: 100)",
            "value": 30620.5,
            "unit": "ns",
            "range": "± 1428.163389345444"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_CancelNWorkers(N: 100)",
            "value": 567.5,
            "unit": "ns",
            "range": "± 34.73230964582306"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_LongRunning(N: 100)",
            "value": 19694391,
            "unit": "ns",
            "range": "± 223435.14614200694"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ProcessNItems_Pool(N: 100)",
            "value": 14295277.9,
            "unit": "ns",
            "range": "± 559920.5659817292"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ProcessNItems(N: 100)",
            "value": 143715.7,
            "unit": "ns",
            "range": "± 37700.23370882467"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_LongRunning(N: 100)",
            "value": 45834092.5,
            "unit": "ns",
            "range": "± 570757.0458242281"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.NTM_ParallelBlockingWork_Pool(N: 100)",
            "value": 386403328.7,
            "unit": "ns",
            "range": "± 10764789.8274583"
          },
          {
            "name": "NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks.Dataflow_ParallelBlockingWork(N: 100)",
            "value": 462771468.8,
            "unit": "ns",
            "range": "± 55220588.99967223"
          }
        ]
      }
    ]
  }
}