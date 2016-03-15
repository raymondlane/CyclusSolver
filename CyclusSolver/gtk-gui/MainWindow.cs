
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
	private global::Gtk.Notebook notebook1;
	private global::Gtk.HPaned hpaned1;
	private global::Gtk.VButtonBox vbuttonbox1;
	private global::Gtk.Button button_;
	private global::Gtk.Button button_TestLog;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TextView textview_Log;
	private global::Gtk.Label label_Test;
	private global::Gtk.HPaned hpaned2;
	private global::Gtk.VButtonBox vbuttonbox2;
	private global::Gtk.Button button_ArchList;
	private global::Gtk.Button button_DebugTest;
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	private global::Gtk.TextView textview_Debug;
	private global::Gtk.Label label_Debug;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.BorderWidth = ((uint)(3));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.notebook1 = new global::Gtk.Notebook ();
		this.notebook1.CanFocus = true;
		this.notebook1.Name = "notebook1";
		this.notebook1.CurrentPage = 1;
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.hpaned1 = new global::Gtk.HPaned ();
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 85;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.vbuttonbox1 = new global::Gtk.VButtonBox ();
		this.vbuttonbox1.Name = "vbuttonbox1";
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button_ = new global::Gtk.Button ();
		this.button_.CanFocus = true;
		this.button_.Name = "button_";
		this.button_.UseUnderline = true;
		this.button_.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
		this.vbuttonbox1.Add (this.button_);
		global::Gtk.ButtonBox.ButtonBoxChild w1 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1 [this.button_]));
		w1.Expand = false;
		w1.Fill = false;
		// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
		this.button_TestLog = new global::Gtk.Button ();
		this.button_TestLog.CanFocus = true;
		this.button_TestLog.Name = "button_TestLog";
		this.button_TestLog.UseUnderline = true;
		this.button_TestLog.Label = global::Mono.Unix.Catalog.GetString ("Test Log");
		this.vbuttonbox1.Add (this.button_TestLog);
		global::Gtk.ButtonBox.ButtonBoxChild w2 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1 [this.button_TestLog]));
		w2.Position = 1;
		w2.Expand = false;
		w2.Fill = false;
		this.hpaned1.Add (this.vbuttonbox1);
		global::Gtk.Paned.PanedChild w3 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.vbuttonbox1]));
		w3.Resize = false;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textview_Log = new global::Gtk.TextView ();
		this.textview_Log.CanFocus = true;
		this.textview_Log.Name = "textview_Log";
		this.textview_Log.Editable = false;
		this.textview_Log.WrapMode = ((global::Gtk.WrapMode)(2));
		this.GtkScrolledWindow.Add (this.textview_Log);
		this.hpaned1.Add (this.GtkScrolledWindow);
		this.notebook1.Add (this.hpaned1);
		// Notebook tab
		this.label_Test = new global::Gtk.Label ();
		this.label_Test.Name = "label_Test";
		this.label_Test.LabelProp = global::Mono.Unix.Catalog.GetString ("Test");
		this.notebook1.SetTabLabel (this.hpaned1, this.label_Test);
		this.label_Test.ShowAll ();
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.hpaned2 = new global::Gtk.HPaned ();
		this.hpaned2.CanFocus = true;
		this.hpaned2.Name = "hpaned2";
		this.hpaned2.Position = 97;
		// Container child hpaned2.Gtk.Paned+PanedChild
		this.vbuttonbox2 = new global::Gtk.VButtonBox ();
		this.vbuttonbox2.Name = "vbuttonbox2";
		// Container child vbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
		this.button_ArchList = new global::Gtk.Button ();
		this.button_ArchList.CanFocus = true;
		this.button_ArchList.Name = "button_ArchList";
		this.button_ArchList.UseUnderline = true;
		this.button_ArchList.Label = global::Mono.Unix.Catalog.GetString ("Arch List");
		this.vbuttonbox2.Add (this.button_ArchList);
		global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox2 [this.button_ArchList]));
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
		this.button_DebugTest = new global::Gtk.Button ();
		this.button_DebugTest.CanFocus = true;
		this.button_DebugTest.Name = "button_DebugTest";
		this.button_DebugTest.UseUnderline = true;
		this.button_DebugTest.Label = global::Mono.Unix.Catalog.GetString ("Debug Test");
		this.vbuttonbox2.Add (this.button_DebugTest);
		global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox2 [this.button_DebugTest]));
		w8.Position = 1;
		w8.Expand = false;
		w8.Fill = false;
		this.hpaned2.Add (this.vbuttonbox2);
		global::Gtk.Paned.PanedChild w9 = ((global::Gtk.Paned.PanedChild)(this.hpaned2 [this.vbuttonbox2]));
		w9.Resize = false;
		// Container child hpaned2.Gtk.Paned+PanedChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview_Debug = new global::Gtk.TextView ();
		this.textview_Debug.CanFocus = true;
		this.textview_Debug.Name = "textview_Debug";
		this.textview_Debug.Editable = false;
		this.textview_Debug.WrapMode = ((global::Gtk.WrapMode)(2));
		this.GtkScrolledWindow1.Add (this.textview_Debug);
		this.hpaned2.Add (this.GtkScrolledWindow1);
		this.notebook1.Add (this.hpaned2);
		global::Gtk.Notebook.NotebookChild w12 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.hpaned2]));
		w12.Position = 1;
		// Notebook tab
		this.label_Debug = new global::Gtk.Label ();
		this.label_Debug.Name = "label_Debug";
		this.label_Debug.LabelProp = global::Mono.Unix.Catalog.GetString ("Debug");
		this.notebook1.SetTabLabel (this.hpaned2, this.label_Debug);
		this.label_Debug.ShowAll ();
		this.Add (this.notebook1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 902;
		this.DefaultHeight = 595;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.button_TestLog.Clicked += new global::System.EventHandler (this.OnButtonClicked_Test);
		this.button_ArchList.Clicked += new global::System.EventHandler (this.OnButtonClicked_GetArchList);
		this.button_DebugTest.Clicked += new global::System.EventHandler (this.OnButtonClicked_TestDebug);
	}
}
