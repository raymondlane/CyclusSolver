using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using CyclusNET.Utilities;

namespace CyclusNET
{
	public static class Cyclus
	{

		public static string GetArchetypeList()
		{
			var info = new ProcessStartInfo();
			info.FileName = @"/home/lanere/miniconda2/bin/cyclus";
			info.Arguments = @"-a";
			info.UseShellExecute = false;
			info.RedirectStandardOutput = true;
			string result = String.Empty;
            using (Process p = Process.Start(info)) {
				Console.WriteLine (p.Id.ToString ());
				using (StreamReader reader = p.StandardOutput) {
					result = reader.ReadToEnd ();
					Console.WriteLine (result);
				}
			}
			return result;
		}

		public static string GetArchetypes()
		{
			var proc = new ProcessStartInfo();
			proc.FileName = @"/home/lanere/miniconda2/bin/cyclus";
			proc.Arguments = @"-m";
			proc.UseShellExecute = false;
			proc.RedirectStandardOutput = true;
			string result = String.Empty;
			using (Process p = Process.Start(proc)) {
				using (StreamReader reader = p.StandardOutput) {
					result = reader.ReadToEnd ();
                    Console.WriteLine (result);
				}
			}
			return result;
		}

        public static string CreateInputFile(Input input)
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "input_files");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var file = Path.Combine(path, String.Format("{0}.json", input.Simulation.Control.SimHandle));
            File.WriteAllText(file, input.ToString());
            return file;
        }

        public static string ExecuteInputFile(Input input)
        {
            var output = new StringBuilder();
            var file = CreateInputFile(input);
            var info = new ProcessStartInfo
			{ FileName = @"/home/lanere/miniconda2/bin/cyclus", 
                Arguments = file, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true
            };
            var p = new Process();
            p.EnableRaisingEvents = true;
            p.Exited += SimComplete;
//            p.Exited += (sender, e) => {
//                if(SimId != null)
//                    SimId(null, new StandardOutputArgs(output.ToString()));
//            };

            p.StartInfo = info;
            p.Start();
            p.OutputDataReceived += StandardOutput;
            p.ErrorDataReceived += Error;
			p.OutputDataReceived += SimId;
            p.OutputDataReceived += (sender, e) => {
                Console.WriteLine("Event");};
            if (SimBegin != null)
                SimBegin(null, new SimBeginArgs(p.Id));
            p.BeginOutputReadLine();
            return output.ToString();
        }

        public static event EventHandler SimComplete;

        public delegate void SimIdHandler (object sender, DataReceivedEventArgs e);

		public static event DataReceivedEventHandler SimId;

		public static event DataReceivedEventHandler Error;

        public static event SimBeginHandler SimBegin;

        public delegate void SimBeginHandler(object sender, SimBeginArgs e);

        public static event DataReceivedEventHandler StandardOutput;

//        public static async Task<ProcessResults> RunAsync(Input input)
//        {
//            var file = CreateInputFile(input);
//            var info = new ProcessStartInfo
//			{ FileName = @"/home/lanere/miniconda2/bin/cyclus", 
//                Arguments = file, UseShellExecute = false, RedirectStandardOutput = true
//            }; 
//            return await ProcessEx.RunAsync(info);
//        }



	}

    public class SimBeginArgs:EventArgs
    {
        private int _pid;

        public SimBeginArgs(int pid)
        {
            _pid = pid;
        }

        public int PID
        {
            get { return _pid;}
        }
    }

    public class StandardOutputArgs:EventArgs
    {
        private string _args;

        public StandardOutputArgs(string args)
        {
            _args = args;
        }

        public string Args
        {
            get { return _args; }
        }
    }

}

