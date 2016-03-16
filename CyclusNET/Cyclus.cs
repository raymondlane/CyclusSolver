using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CyclusNET.Utilities;

namespace CyclusNET
{
	public static class Cyclus
	{

        private static string CreateInputFile(Input input)
        {
            // Create the path for the input file
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "input_files");
            // Create the input_file directory if it does not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // Combine the file name and the directory into a full path
            var file = Path.Combine(path, String.Format("{0}.json", input.Simulation.Control.SimHandle));
            // Write the Json for the simulation to the path
            File.WriteAllText(file, input.ToString());
            // Return the full path for the input file
            return file;
        }

        public static async Task<ProcessResults> RunSimulationAsync(Input input)
        {
            // Cache the sim handle of the simulation for use in identifying the simulation
            var handle = input.Simulation.Control.SimHandle;
            // Create the input file for the simulation
            Trace.WriteLine(String.Format("Creating input file for {0}.", handle));
            var file = CreateInputFile(input);
            Trace.WriteLine(String.Format("Input file {0} created.", file));
            // Cache the arguments to be passed to ProcessStartInfo (note: cyclus is a script. bash is the process, and cyclus is an argument)
            var args = String.Format("/home/lanere/miniconda2/bin/cyclus {0}", file);
            // Create the ProcessStartInfo
            var info = new ProcessStartInfo { FileName = @"/bin/bash", Arguments = args };
            // Run the simulation asynchronously
            Trace.WriteLine(String.Format("Starting simulation {0}.", handle));
            var result = await ProcessEx.RunAsync(info);
            Trace.WriteLine(String.Format("Simulation {0} completed in {1} ms.", handle, result.RunTime.Milliseconds));
            // Return the ProcessResults
            return result;
        }



	}

}

