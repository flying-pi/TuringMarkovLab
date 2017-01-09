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
		FormalTuringProgramm programm = null;

		int selectComand = -1;
		int selectRow = -1;

		List<string> log = new List<string>();

		public TuringDisplay() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			nextStepBtn.Clicked += this.onNextStep;
			showLog.Clicked += this.onLogBtnClick;
			runToEnd.Clicked += this.onRunToEndClick;
			turingAria.DoubleBuffered =true;
			turingAria.ExposeEvent += this.HandleDrawingAreaExposeEvent;
			turingCode.ExposeEvent += this.HandleDrawingAreaExposeEvent;
		}

		private void HandleDrawingAreaExposeEvent(object o, ExposeEventArgs args)
		{
			if (o == turingAria)
				redrawType(lastItems, lastCurrentPosition);
			else
				drawProgramm();
			args.RetVal = false;
		}


		public void setCurrentTuringAlg(TuringAlg alg)
		{
			isEnd = false;
			log.Clear();

			turingAlg = alg;
			this.programm = alg.getFormalProgramm();
			turingAlg.MoveEvent += this.onTuringMove;
			turingAlg.startCalculation();
		}
		private void onTuringMove(object sender, TuringMoveEventArgs e)
		{
			this.selectComand = e.currentComand;
			this.selectRow = e.cureentRow;
			redrawType(e.elements, e.currentPosition);
			drawProgramm();
			log.Add("Comand\t::\t"+programm.comandTable[selectComand, selectRow]);
			log.Add("\t"+turingAlg.getCurrentState());
			log.Add("");
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
					gc.RgbFgColor = new Gdk.Color(100, 100, 255);
				turingAria.GdkWindow.DrawRectangle(gc, true, i * heigth, 0, heigth, heigth);

				gc = new Gdk.GC((Drawable)turingAria.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(0, 255, 0);
				Pango.Layout l = new Pango.Layout(turingAria.PangoContext);
				l.SetText(items[i]);
				l.Alignment = Pango.Alignment.Center;

				int w, h;
				l.GetPixelSize(out w, out h);
				turingAria.GdkWindow.DrawLayout(gc, i * heigth+(heigth-h)/2,(heigth - w) / 2 , l);
			}
			turingAria.GdkWindow.EndPaint();
		}

		private void drawProgramm()
		{
			if (programm == null)
				return;
			turingCode.GdkWindow.Clear();
			int w = 30 + programm.comandCount * 100;
			int h = 30 + programm.alphabit.Length*30;
			int textW, textH;
			turingCode.SetSizeRequest(w, h);
			w = turingCode.SizeRequest().Width;
			h = turingCode.SizeRequest().Height;
			Gdk.GC gc;

			gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
			gc.RgbFgColor = new Gdk.Color(200, 200, 200);
			turingCode.GdkWindow.DrawRectangle(gc, true, 0, 0, w, h);

			gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
			gc.RgbFgColor = new Gdk.Color(206, 112, 35);
			turingCode.GdkWindow.DrawRectangle(gc, true, 0, 30, 30, h-30);

			if (selectRow > -1 && selectComand > -1) {
				gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(186, 136, 239);
				turingCode.GdkWindow.DrawRectangle(gc, true, 0, 30 + selectRow * 30, w, 30);
				turingCode.GdkWindow.DrawRectangle(gc, true, 30 + selectComand * 100, 0, 100, h);
			}

			gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
			gc.RgbFgColor = new Gdk.Color(206, 112, 35);
			turingCode.GdkWindow.DrawRectangle(gc, true, 0, 30, 30, h - 30);
			for (int i = 0; i < programm.alphabit.Length; i++) {
				gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(3, 86, 89);
				turingCode.GdkWindow.DrawLine(gc, 0, 30 + i * 30, w, 30 + i * 30);


				gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(0, 255, 0);
				Pango.Layout l = new Pango.Layout(turingCode.PangoContext);
				l.SetText(programm.alphabit[i]);
				l.Alignment = Pango.Alignment.Center;

				l.GetPixelSize(out textW, out textH);
				turingCode.GdkWindow.DrawLayout(gc, (30 - textW) / 2, 30 + i * 30 + (30 - textH) / 2, l);

			}

			gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
			gc.RgbFgColor = new Gdk.Color(206, 112, 35);
			turingCode.GdkWindow.DrawRectangle(gc, true, 30, 0, w-30, 30);
			for (int i = 0; i < programm.comandCount; i++) {
				gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(3, 86, 89);
				turingCode.GdkWindow.DrawLine(gc, 30+i*100, 0, 30 + i * 100, h);


				gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
				gc.RgbFgColor = new Gdk.Color(0, 255, 0);
				Pango.Layout l = new Pango.Layout(turingCode.PangoContext);
				l.SetText("q" + i.ToString());
				l.Alignment = Pango.Alignment.Center;

				l.GetPixelSize(out textW, out textH);
				turingCode.GdkWindow.DrawLayout(gc,30+ 100*i+(100 - textW) / 2,  (30 - textH) / 2, l);

			}

			for (int i = 0; i < programm.comandCount; i++) {
				for (int j = 0; j < programm.alphabit.Length; j++) {
					int startX = 30 + 100 * i;
					int startY =30 + 30 * j;
					if (i == selectComand && j == selectRow) {
						gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
						gc.RgbFgColor = new Gdk.Color(108, 17, 160);
						turingCode.GdkWindow.DrawRectangle(gc, true, startX, startY,100, 30);
					}
					gc = new Gdk.GC((Drawable)turingCode.GdkWindow);
					gc.RgbFgColor = new Gdk.Color(42, 24, 61);
					Pango.Layout l = new Pango.Layout(turingCode.PangoContext);
					l.SetText(programm.comandTable[i,j]);
					l.Alignment = Pango.Alignment.Center;
					l.Context.FontDescription.Style = Pango.Style.Oblique;

					l.GetPixelSize(out textW, out textH);
					turingCode.GdkWindow.DrawLayout(gc, startX + (100 - textW) / 2, startY+(30 - textH) / 2, l);
				}
			}


		}

		private void onRunToEndClick(object sender, EventArgs e)
		{
			nextStepBtn.Hide();
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Interval = 1000;
			aTimer.Elapsed += this.onNextStep;
			aTimer.AutoReset = true;
			aTimer.Enabled = true;
		}

		private void onNextStep(object sender, EventArgs e)
		{
			if (isEnd)
				return;
			isEnd = !turingAlg.nextStep();
		}

		private void onLogBtnClick(object sender, EventArgs e)
		{
			LogWindow logW = new LogWindow();
			logW.setLogs(log);
			logW.Show();
		}
	}
}
