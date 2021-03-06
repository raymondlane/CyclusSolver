using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CyclusNET.Utilities
{
    public static partial class ProcessEx
    {
        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo)
        {
            return RunAsync(processStartInfo, CancellationToken.None);
        }

        public static Task<ProcessResults> RunAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken)
        {
            // force some settings in the start info so we can capture the output
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            var startTime = DateTime.Now;

            var tcs = new TaskCompletionSource<ProcessResults>();

            var standardOutput = new List<string>();
            var standardError = new List<string>();

            var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
					Console.WriteLine(args.Data);
                    standardOutput.Add(args.Data);
                }
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    standardError.Add(args.Data);
                }
            };

            process.Exited += (sender, args) => tcs.TrySetResult(new ProcessResults(process, standardOutput.ToArray(), standardError.ToArray(), startTime, DateTime.Now));

            cancellationToken.Register(() =>
                                       {
                tcs.TrySetCanceled();
                process.Kill();
            });

            cancellationToken.ThrowIfCancellationRequested();



            if (!process.Start())
            {
                tcs.TrySetException(new InvalidOperationException("Failed to start process"));
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            var result = tcs.Task;

            return result;
        }
    }
}