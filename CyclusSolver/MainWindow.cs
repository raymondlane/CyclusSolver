using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Gtk;
using CyclusNET.Utilities;
using System.Linq;
using System.Collections.Generic;
using CyclusNET;
using System.Globalization;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		listener = new Listener(this);
		Trace.Listeners.Add(listener);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private void OnButtonClicked_Test(object sender, EventArgs e)
	{
		Log(textview_Log, "Test");
	}

	private void OnButtonClicked_TestDebug(object sender, EventArgs e)
	{
		Trace.WriteLine("Test");
	}

	public void Log(TextView view, string entry)
	{
		Gtk.Application.Invoke(delegate
		                       {
			var iter = view.Buffer.EndIter;
			view.Buffer.Insert(ref iter, String.Format("[ {0} ] {1}\r\n", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), entry));
			view.ScrollToIter(view.Buffer.EndIter, 0, false, 0, 0);
		});
	}	

	private Listener listener { get; set; }

	public class Listener : TraceListener
	{
		public Listener(MainWindow mainWindow)
		{
			window = mainWindow;
		}

		private MainWindow window { get; set; }

		public override void Write(string entry)
		{
			window.Log(window.textview_Debug, entry);
			this.Flush();
		}

		public override void WriteLine(string entry)
		{
			window.Log(window.textview_Debug, entry);
			this.Flush();
		}

	}

	public async void OnButtonClicked_GetArchList(object sender, EventArgs e)
	{
        var input = new CyclusNET.Input(new CyclusNET.Control(100, 1, 2016));
        input.Simulation.Control.SimHandle = Guid.NewGuid().ToString();
        var s = await Cyclus.RunSimulationAsync(input);
        foreach (var l in s.StandardOutput)
        {
            if(l.Contains("Simulation ID"))
                Log(textview_Debug, l);
        }
            
	}

}


