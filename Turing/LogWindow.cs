using System;
using System.Collections.Generic;

namespace Turing
{
	public partial class LogWindow : Gtk.Window
	{
		public LogWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		public void setLogs(List<string> logMessages)
		{
			string log = "";
			foreach (var s in logMessages)
				log += s + "\n";
			logWindow.Buffer.Text = log;
		}
	}
}
