using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Gtk;
//using CyclusNET.Utilities;
using System.Linq;
using System.Collections.Generic;

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

	public void OnButtonClicked_GetArchList(object sender, EventArgs e)
	{
//        var input = new CyclusNET.Input(new CyclusNET.Control(100, 1, 2016));
//        input.Simulation.Control.SimHandle = Guid.NewGuid().ToString();
//        CyclusNET.Cyclus.SimComplete += OnSimComplete;
//        CyclusNET.Cyclus.SimBegin += OnSimBegin;
//        CyclusNET.Cyclus.StandardOutput += OnStandardOutput;
//        CyclusNET.Cyclus.SimId += OnSimId;
//		CyclusNET.Cyclus.ExecuteInputFile (input);
//        CyclusNET.Cyclus.SimComplete -= OnSimComplete;
//        CyclusNET.Cyclus.SimBegin -= OnSimBegin;
//        CyclusNET.Cyclus.StandardOutput -= OnStandardOutput;
//        CyclusNET.Cyclus.SimId -= OnSimId;
        Log (textview_Debug, CyclusNET.Cyclus.GetArchetypes ());
	}

    public void OnSimBegin(object sender, CyclusNET.SimBeginArgs e)
    {
        Log(textview_Debug, String.Format("Beginning Simulation [{0}].", e.PID));
    }

    public void OnSimComplete(object sender, EventArgs e)
    {
        Log(textview_Debug, "Simulation Complete");
    }

    public void OnStandardOutput(object sender, DataReceivedEventArgs e)
    {
		Console.WriteLine (e.Data);
        Log(textview_Debug, e.Data);
    }

    public void OnSimId(object sender, DataReceivedEventArgs e)
    {
        Log(textview_Log, e.Data);
    }

}


