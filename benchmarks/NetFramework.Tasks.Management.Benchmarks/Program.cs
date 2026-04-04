using BenchmarkDotNet.Running;

// Run a specific class:   dotnet run -c Release -f net8.0 -- --filter *Lifecycle*
// Run everything:         dotnet run -c Release -f net8.0 -- --filter *
// Interactive menu:       dotnet run -c Release -f net8.0
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
