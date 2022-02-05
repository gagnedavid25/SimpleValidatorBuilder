using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
BenchmarkRunner.Run(typeof(Program).Assembly);
Console.ReadLine();