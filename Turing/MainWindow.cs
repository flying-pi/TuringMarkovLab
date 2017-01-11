using System;
using Gtk;
using Turing;

public partial class MainWindow : Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		this.loadProgramTextBtn.Clicked += this.onLoadBtnClick;
		this.saveProgramText.Clicked += this.osSaveProgramm;

		this.markovBtn.Clicked += this.onAlgoritmBtnClick;
		this.turingBtn.Clicked += this.onAlgoritmBtnClick;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	private void onAlgoritmBtnClick(object obj, EventArgs args)
	{
		string sourceCode = programTexton.Buffer.Text;
		IAlghoritm alg;
		if (obj == turingBtn) alg = new TuringAlg(sourceCode);
		else alg = new MarkovAlg(sourceCode);
		string initStateStr = initState.Text;
		initState.Text = alg.calculate(initStateStr);
		TuringDisplay display = new TuringDisplay();
		alg.setInitState(initStateStr);
		display.setCurrentTuringAlg((Turing.TuringAlg)alg);
		display.Show();
	}

	private void onMarkovClick(object obj, EventArgs args)
	{
		TuringAlg alg = new TuringAlg(programTexton.Buffer.Text);
		initState.Text = alg.calculate(initState.Text);
	}

	private void onLoadBtnClick(object obj, EventArgs args)
	{
		Gtk.FileChooserDialog filechooser =
		new Gtk.FileChooserDialog("Виберіть файл для завнтаження",
			this,
			FileChooserAction.Open,
			"Відміна", ResponseType.Cancel,
			"Відкрити", ResponseType.Accept);

		if (filechooser.Run() == (int)ResponseType.Accept)
		{
			System.IO.StreamReader file = new System.IO.StreamReader(filechooser.Filename);
			programTexton.Buffer.Text = file.ReadToEnd();
		}
		filechooser.Destroy();
	}

	private void osSaveProgramm(object obj, EventArgs args)
	{
		Gtk.FileChooserDialog filechooser =
		new Gtk.FileChooserDialog("Виберіть файл для збереження",
			this,
								  FileChooserAction.Save,
								  "Відміна", ResponseType.Cancel,
								  "Відкрити", ResponseType.Accept);

		if (filechooser.Run() == (int)ResponseType.Accept)
			System.IO.File.WriteAllText(filechooser.Filename, programTexton.Buffer.Text);
		filechooser.Destroy();
	}
}
