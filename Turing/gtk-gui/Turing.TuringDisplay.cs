
// This file has been generated by the GUI designer. Do not modify.
namespace Turing
{
	public partial class TuringDisplay
	{
		private global::Gtk.Fixed fixed2;

		private global::Gtk.ScrolledWindow scrolledwindow1;

		private global::Gtk.DrawingArea turing_code;

		private global::Gtk.DrawingArea turingAria;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Turing.TuringDisplay
			this.Name = "Turing.TuringDisplay";
			this.Title = global::Mono.Unix.Catalog.GetString("TuringDisplay");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Turing.TuringDisplay.Gtk.Container+ContainerChild
			this.fixed2 = new global::Gtk.Fixed();
			this.fixed2.Name = "fixed2";
			this.fixed2.HasWindow = false;
			// Container child fixed2.Gtk.Fixed+FixedChild
			this.scrolledwindow1 = new global::Gtk.ScrolledWindow();
			this.scrolledwindow1.WidthRequest = 690;
			this.scrolledwindow1.HeightRequest = 350;
			this.scrolledwindow1.CanFocus = true;
			this.scrolledwindow1.Name = "scrolledwindow1";
			this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow1.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.turing_code = new global::Gtk.DrawingArea();
			this.turing_code.Name = "turing_code";
			w1.Add(this.turing_code);
			this.scrolledwindow1.Add(w1);
			this.fixed2.Add(this.scrolledwindow1);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed2[this.scrolledwindow1]));
			w4.X = 5;
			w4.Y = 5;
			// Container child fixed2.Gtk.Fixed+FixedChild
			this.turingAria = new global::Gtk.DrawingArea();
			this.turingAria.WidthRequest = 690;
			this.turingAria.HeightRequest = 75;
			this.turingAria.Name = "turingAria";
			this.fixed2.Add(this.turingAria);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed2[this.turingAria]));
			w5.X = 5;
			w5.Y = 425;
			this.Add(this.fixed2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 705;
			this.DefaultHeight = 525;
			this.Show();
		}
	}
}
