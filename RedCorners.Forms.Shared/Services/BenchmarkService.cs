using RedCorners.Forms.Services;

using RedCorners.Components;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(BenchmarkService))]
namespace RedCorners.Forms.Services {
    public class BenchmarkService
    {
        private bool isEnabled = false;
        private readonly ConcurrentDictionary<Benchmark, string> messages = new ConcurrentDictionary<Benchmark, string>();
        private readonly ConcurrentBag<(DateTime Date, TimeSpan Duration, string Message)> finishedTasks = new ConcurrentBag<(DateTime Date, TimeSpan Duration, string Message)>();
        private volatile bool isPrintTimerActive = false;

        public event EventHandler<Exception> OnErrorTracked;
        public bool IsDebug;

        public void SetEnabled(bool flag)
        {
            isEnabled = flag;
        }

        public IDisposable StartBenchmark([CallerMemberName] string message = null)
        {
            if (!isEnabled)
                return null;

            var benchmark = new Benchmark();
            messages[benchmark] = message;
            benchmark.Stopped += Benchmark_Stopped;
            Console.WriteLine($"[Benchmark Start | {messages.Count} active] {message}");
            StartPrintTimer();
            return benchmark;
        }

        private void Benchmark_Stopped(object sender, TimeSpan e)
        {
            var b = sender as Benchmark;
            b.Stopped -= Benchmark_Stopped;
            StopBenchmark(b);
        }

        public void StopBenchmark(IDisposable handle, string message = null)
        {
            if (handle == null)
                return;

            var benchmark = handle as Benchmark;
            if (messages.TryRemove(benchmark, out var startMessage))
            {
                finishedTasks.Add((DateTime.Now, benchmark.Elapsed, $"{startMessage} {message}"));
                Console.WriteLine($"[Benchmark End | {messages.Count} active] Elapsed: {benchmark.StopToString()} {startMessage} {message}");
                PrintStatus();
            }
        }

        private void StartPrintTimer()
        {
            Task.Run(async () =>
            {
                if (messages.IsEmpty || isPrintTimerActive)
                    return;

                isPrintTimerActive = true;
                while (isPrintTimerActive && messages.Count > 0)
                {
                    await Task.Delay(2000);
                    PrintStatus();
                }
                isPrintTimerActive = false;
            });
        }

        public void PrintStatus()
        {
            if (!isEnabled)
                return;

            Console.WriteLine(" ");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("ACTIVE BENCHMARKS");
            foreach (var message in messages)
                Console.WriteLine($"{message.Key}\t{message.Value}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("RECENT BENCHMARKS");
            foreach (var message in finishedTasks.OrderByDescending(x => x.Date).Take(5))
                Console.WriteLine(message);
            Console.WriteLine("------------------------------------");
            Console.WriteLine("FINISHED BENCHMARKS");
            var summaries = finishedTasks.GroupBy(x => x.Message).Select(x => new
            {
                Duration = TimeSpan.FromSeconds(x.Sum(y => y.Duration.TotalSeconds)),
                Message = x.Key,
                Count = x.Count()
            }).OrderByDescending(x => x.Duration);
            foreach (var item in summaries)
                Console.WriteLine($"{item.Duration}\tx{item.Count,-5}\t\t{item.Duration.TotalSeconds / item.Count:N3} s/t\t\t{item.Message}");
            Console.WriteLine(" ");
        }

        public void Throw(Exception ex, [CallerMemberName] string caller = null)
        {
            Report(ex, caller);
            throw ex;
        }

        public void ThrowIfDebug(Exception ex, [CallerMemberName] string caller = null)
        {
            Report(ex, caller);
            if (IsDebug)
            throw ex;
        }

        public void Report(Exception ex, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"[Exception Thrown by {caller}] {ex}");
            OnErrorTracked?.Invoke(caller, ex);
        }

        public static void ThrowDefault(Exception ex, [CallerMemberName] string caller = null)
        {
            DependencyService.Get<BenchmarkService>().Throw(ex, caller);
        }
    }
}