using System.Reflection;
using BenchmarkDotNet.Running;

BenchmarkSwitcher switcher = new(Assembly.GetExecutingAssembly());
switcher.Run(args);