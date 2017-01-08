using System;
namespace Turing
{
	public partial class TuringDisplay : Gtk.Window
	{
		private TuringAlg turingAlg = null;
		public TuringDisplay() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}

		public void setCurrentTuringAlg(TuringAlg alg)
		{
			turingAlg = alg;
		}
	}
}
