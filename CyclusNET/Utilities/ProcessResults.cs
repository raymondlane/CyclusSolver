using System;
using System.Diagnostics;

namespace CyclusNET.Utilities
{
    public class ProcessResults
    {
        readonly Process _process;
        readonly string[] _standardOutput;
        readonly string[] _standardError;
        readonly int _exitCode;
        readonly TimeSpan _runTime;

        public ProcessResults(Process process, string[] standardOutput, string[] standardError)
        {
            _process = process;
            _exitCode = process.ExitCode;
            _runTime = process.ExitTime - process.StartTime;
            _standardOutput = standardOutput;
            _standardError = standardError;
        }

        public ProcessResults(Process process, string[] standardOutput, string[] standardError, DateTime start, DateTime exit)
        {
            _process = process;
            _exitCode = process.ExitCode;
            _runTime = exit - start;
            _standardOutput = standardOutput;
            _standardError = standardError;
        }

        public Process Process
        {
            get { return _process; }
        }

        public int ExitCode 
        {
            get { return _exitCode; }
        }

        public TimeSpan RunTime
        {
            get { return _runTime; }
        }

        public string[] StandardOutput
        {
            get { return _standardOutput; }
        }

        public string[] StandardError
        {
            get { return _standardError; }
        }
    }
}