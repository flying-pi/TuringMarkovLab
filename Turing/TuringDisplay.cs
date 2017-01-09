using System;
using System.Collections.Generic;
using Gdk;
using Gtk;

namespace Turing
{
	public partial class TuringDisplay : Gtk.Window
	{
		private TuringAlg turingAlg = null;
		bool isEnd = true;
		List<string> lastItems = new List<string>();
		int lastCurrentPosition = 0;

		public TuringDisplay() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			nextStepBtn.Clicked += this.onNextStepClick;
			turingAria.DoubleBuffered =true;
			turingAruaContainer.ScrollEvent += this.onTuringDataScroll;
			turingAria.ExposeEvent += this.HandleDrawingAreaExposeEvent;
		}

		private void HandleDrawingAreaExposeEvent(object o, ExposeEventArgs args)
		{
			redrawType(lastItems, lastCurrentPosition);
			args.RetVal = false;
		}

		public void setCurrentTuringAlg(TuringAlg alg)
		{
			turingAlg = alg;
			turingAlg.startCalculation();
			isEnd = false;
			turingAlg.MoveEvent += this.onTuringMove;
		}
		private void onTuringMove(object sender, TuringMoveEventArgs e)
		{
			redrawType(e.elements, e.currentPosition);
		}

		private void redrawType(List<string> items, int currentPosition)
		{
			lastItems = items;
			lastCurrentPosition = currentPosition;
			int heigth = turingAruaContainer.SizeRequest().Height;
			int width = heigth * items.Count;
			turingAria.SetSizeRequest(width, heigth);

			turingAria.GdkWindow.BeginPaintRegion(Region.Rectangle(new Rectangle(0, 0, width, heigth)));
			for (int i = 0; i < items.Count; i++) {
				
				Gdk.GC gc = new Gdk.GC((Drawable)turingAria.GdkWindow);
				if (i == currentPosition)
					gc.RgbFgColor = new Gdk.Color(108, 17, 160);
				else
					gc.RgbFgColor = new Gdk.Color(180, 180, 180);
				turingAria.GdkWindow.DrawRectangle(gc, true, i * heigth, 0, heigth, heigth);

				gc = new Gdk.GC((Drawable)turingAria.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(0, 255, 0);
				Pango.Layout l = new Pango.Layout(turingAria.PangoContext);
				l.SetText(items[i]);
				//l.SetMarkup("<span size=\"12\">" + items[i] + "</span>");
				//l.SetMarkup("<h1>" + items[i] + "</h1>");
				l.Alignment = Pango.Alignment.Center;

				int w, h;
				l.GetPixelSize(out w, out h);
				turingAria.GdkWindow.DrawLayout(gc, i * heigth+(heigth-h)/2,(heigth - w) / 2 , l);
			}
			turingAria.GdkWindow.EndPaint();
		}

		private void onTuringDataScroll(object sender, EventArgs e)
		{
			//turingAria.Draw(new Rectangle(0, 0, turingAria.SizeRequest().Width, turingAria.SizeRequest().Height));
			//turingAria.GdkWindow.InvalidateRect(new Rectangle(0, 0, turingAria.SizeRequest().Width, turingAria.SizeRequest().Height),true);
			turingAria.QueueDraw();
		}

		private void onNextStepClick(object sender, EventArgs e)
		{
			if (isEnd)
				return;
			isEnd = !turingAlg.nextStep();
			//for (int i = 0; i < 10; i++) {
			//	Gdk.GC gc = new Gdk.GC((Drawable)turingAria.GdkWindow);
			//	gc.RgbFgColor = new Gdk.Color(255, 50, 50);
			//	gc.RgbBgColor = new Gdk.Color(0, 0, 0);
			//	gc.SetLineAttributes(3, LineStyle.OnOffDash,
			//						  CapStyle.Projecting, JoinStyle.Round);
			//	Gdk.Point[] pts = new Gdk.Point[8];
			//	pts[0] = new Gdk.Point(10+i*5, 0);
			//	pts[1] = new Gdk.Point(15+ i * 5, 20);
			//	pts[2] = new Gdk.Point(20+ i * 5, 30);
			//	pts[3] = new Gdk.Point(25+ i * 5, 20);
			//	pts[4] = new Gdk.Point(30+ i * 5, 30);
			//	pts[5] = new Gdk.Point(40+ i * 5, 40);
			//	pts[6] = new Gdk.Point(55+ i * 5, 35);
			//	pts[7] = new Gdk.Point(75+ i * 5, 15);
			//	turingAria.GdkWindow.Clear();
			//	turingAria.GdkWindow.DrawLines(gc, pts);
			//}
			//Pango.Layout l = new Pango.Layout(turingAria.PangoContext);
			//l.SetText("dkdkd");
		}
	}
}
