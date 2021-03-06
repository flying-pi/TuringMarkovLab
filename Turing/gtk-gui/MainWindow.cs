
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView programTexton;

	private global::Gtk.Entry initState;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView textview2;

	private global::Gtk.Button turingBtn;

	private global::Gtk.Button markovBtn;

	private global::Gtk.Button saveProgramText;

	private global::Gtk.Button loadProgramTextBtn;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed();
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.WidthRequest = 790;
		this.GtkScrolledWindow.HeightRequest = 250;
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.programTexton = new global::Gtk.TextView();
		this.programTexton.Buffer.Text = "'*0'->'0*'\n'*1'->'1*'\n'*2'->'2*'\n'*3'->'3*'\n'*4'->'4*'\n'*5'->'5*'\n'*6'->'6*'\n'*7'->'7*'\n'*8'->'8*'\n'*9'->'9*'\n'0*'|->'0 - Число є парним'\n'1*'|->'1 - Число не є парним'\n'2*'|->'2 - Число є парним'\n'3*'|->'3 - Число не є парним'\n'4*'|->'4 - Число є парним'\n'5*'|->'5 - Число не є парним'\n'6*'|->'6 - Число є парним'\n'7*'|->'7 - Число не є парним'\n'8*'|->'8 - Число є парним'\n'9*'|->'9 - Число не є парним'\n->'*'";
		this.programTexton.CanFocus = true;
		this.programTexton.Name = "programTexton";
		this.GtkScrolledWindow.Add(this.programTexton);
		this.fixed1.Add(this.GtkScrolledWindow);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow]));
		w2.X = 5;
		w2.Y = 5;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.initState = new global::Gtk.Entry();
		this.initState.WidthRequest = 790;
		this.initState.CanFocus = true;
		this.initState.Name = "initState";
		this.initState.Text = global::Mono.Unix.Catalog.GetString("1234");
		this.initState.IsEditable = true;
		this.initState.InvisibleChar = '●';
		this.fixed1.Add(this.initState);
		global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.initState]));
		w3.X = 5;
		w3.Y = 260;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow1.WidthRequest = 790;
		this.GtkScrolledWindow1.HeightRequest = 290;
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview2 = new global::Gtk.TextView();
		this.textview2.Buffer.Text = "Программа розрахована на емуляцію машини Т'юрінга та на ємуляцію роботи нормальних алгоритмів Маркова. \n\nПеревірок на те чи відповіда программа вибраній категорії нема,  треба бути обережним) \n\nДля запису елементів алфовіту чи фраз слід використувати одинарні кавички(') \n\nДля запису переходів слід використовувати послідовність \"стрілочка\" -> та кінцева стрілочка для алгоримів Маркова |-> \n\nДля запису команд машини тюринга слід дотримуватись наступних правил:\n\t- команди можна відділяти за допомогою знаку табуляції або переносу на нову строку.  Послідовність не має значення \n\t- стан описується літерою q та його порядковим номером \n\t- для позначення переміщень використовують літери R та L для переміщення вправо та вліво відповідно \n\t- символ \"собачка\" зарезервований для позначення попрожньої комірки \n\t- стан q0 зарезервований для закінчення програми";
		this.textview2.CanFocus = true;
		this.textview2.Name = "textview2";
		this.textview2.Editable = false;
		this.GtkScrolledWindow1.Add(this.textview2);
		this.fixed1.Add(this.GtkScrolledWindow1);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow1]));
		w5.X = 5;
		w5.Y = 320;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.turingBtn = new global::Gtk.Button();
		this.turingBtn.CanFocus = true;
		this.turingBtn.Name = "turingBtn";
		this.turingBtn.UseUnderline = true;
		this.turingBtn.Label = global::Mono.Unix.Catalog.GetString("Розглядати як алгоритм тьюринга");
		this.fixed1.Add(this.turingBtn);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.turingBtn]));
		w6.X = 5;
		w6.Y = 285;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.markovBtn = new global::Gtk.Button();
		this.markovBtn.CanFocus = true;
		this.markovBtn.Name = "markovBtn";
		this.markovBtn.UseUnderline = true;
		this.markovBtn.Label = global::Mono.Unix.Catalog.GetString("Розглядати як алгоритм маркова");
		this.fixed1.Add(this.markovBtn);
		global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.markovBtn]));
		w7.X = 225;
		w7.Y = 285;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.saveProgramText = new global::Gtk.Button();
		this.saveProgramText.CanFocus = true;
		this.saveProgramText.Name = "saveProgramText";
		this.saveProgramText.UseUnderline = true;
		this.saveProgramText.Label = global::Mono.Unix.Catalog.GetString("Зберегти текст програми");
		this.fixed1.Add(this.saveProgramText);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.saveProgramText]));
		w8.X = 440;
		w8.Y = 285;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.loadProgramTextBtn = new global::Gtk.Button();
		this.loadProgramTextBtn.CanFocus = true;
		this.loadProgramTextBtn.Name = "loadProgramTextBtn";
		this.loadProgramTextBtn.UseUnderline = true;
		this.loadProgramTextBtn.Label = global::Mono.Unix.Catalog.GetString("Завантажити текст програми");
		this.fixed1.Add(this.loadProgramTextBtn);
		global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.loadProgramTextBtn]));
		w9.X = 610;
		w9.Y = 285;
		this.Add(this.fixed1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 805;
		this.DefaultHeight = 620;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
	}
}
