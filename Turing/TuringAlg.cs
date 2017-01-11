using System;
using System.Collections.Generic;

namespace Turing
{
	public delegate void TuringMoveEventHandler(object sender, TuringMoveEventArgs e);

	public class TuringMoveEventArgs : EventArgs
	{
		public TuringMoveEventArgs(List<string> _elements, int _currentPosition, int _currentComand, int _cureentRow)
		{
			elements = _elements;
			currentPosition = _currentPosition;
			currentComand = _currentComand;
			cureentRow = _cureentRow;
		}
		public List<string> elements;
		public int currentPosition;
		public int currentComand;
		public int cureentRow;
	}

	public class TuringAlg : IAlghoritm
	{
		public event TuringMoveEventHandler MoveEvent;

		string[] alphabit;
		string[] comands;
		ComandDescription[,] programm;
		List<string> type = new List<string>();
		int currentPosition = 0;
		int currentMachineState = 0;
		int zeroPosition = 0;

		FormalTuringProgramm formalProgram;

		public TuringAlg(string rawCode)
		{
			while (rawCode.Contains("  "))
				rawCode = rawCode.Replace("  ", " ");


			while (rawCode.Contains(" ->"))
				rawCode = rawCode.Replace(" ->", "->");

			while (rawCode.Contains("-> "))
				rawCode = rawCode.Replace("-> ", "->");

			List<string> comandsList = parseComandList(rawCode);
			parseComandsAndAlphabits(comandsList);
			parseProgram(comandsList);
			generateFormalProgram();

		}

		public string calculate(string initState)
		{
			startCalculation();
			setInitState(initState);
			while (nextStep()) ;
			return getCurrentState();
		}

		public void setInitState(string initState)
		{
			type.Clear();
			foreach (char c in initState)
				type.Add(c.ToString());
			if (type.Count == 0 || type[type.Count - 1] != Constants.emptyTuringSymbol)
				type.Add(Constants.emptyTuringSymbol);
			currentPosition = 0;

		}

		public string getCurrentState()
		{
			string result = "";
			for (int i = 0; i < type.Count; i++)
				result += (type[i] == null ? Constants.emptyTuringSymbol : type[i].ToString());
			return result;
		}

		public void startCalculation()
		{
			currentPosition = 0;
			zeroPosition = 0;
			currentMachineState = 1;
			if (MoveEvent != null) {
				MoveEvent(this, new TuringMoveEventArgs(type, zeroPosition + currentPosition, currentMachineState, alphabitIndex(type[zeroPosition + currentPosition])));
			}
		}

		public bool nextStep()
		{
			ComandDescription currentStep = programm[currentMachineState,
													 alphabitIndex(type[zeroPosition + currentPosition])];
			if (currentStep == null) {
				//todo  add error sender
				return false;
			}
			if (currentStep.isEnd) {
				//todo  add error sender
				return false;
			}
			type.RemoveAt(zeroPosition + currentPosition);
			type.Insert(zeroPosition + currentPosition, alphabit[currentStep.newSymbol]);
			switch (currentStep.move) {
				case moveType.none:
					break;
				case moveType.left:
					currentPosition--;
					break;
				case moveType.right:
					currentPosition++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			while ((zeroPosition + currentPosition) < 0) {
				type.Insert(0, Constants.emptyTuringSymbol);
				zeroPosition++;
			}
			while ((zeroPosition + currentPosition) >= type.Count)
				type.Add(Constants.emptyTuringSymbol);
			currentMachineState = currentStep.newState;
			if (MoveEvent != null) {
				MoveEvent(this, new TuringMoveEventArgs(type, zeroPosition + currentPosition, currentMachineState, alphabitIndex(type[zeroPosition + currentPosition])));
			}

			return true;
		}

		public FormalTuringProgramm getFormalProgramm()
		{
			return formalProgram;
		}

		private void parseProgram(List<string> comandsList)
		{
			for (int i = 0; i < comands.Length; i++)
				for (int j = 0; j < alphabit.Length; j++) {
					if (i == 0) {
						ComandDescription newComand = new ComandDescription();
						newComand.newState = 0;
						newComand.newSymbol = 0;
						newComand.move = moveType.none;
						newComand.isEnd = true;
						programm[i, j] = newComand;
					} else {
						programm[i, j] = null;
					}
				}
			foreach (var comand in comandsList) {
				string[] comandsPut = comand.Split(new string[] { Constants.simpleArrow }, StringSplitOptions.None);
				string[] fromData = comandsPut[0].Split(new char[] { ' ' });
				string[] toData = comandsPut[1].Split(new char[] { ' ' });
				ComandDescription newComand = new ComandDescription();
				newComand.newState = comandIndex(toData[0]);
				newComand.newSymbol = alphabitIndex(toData[1]);
				newComand.isEnd = false;
				newComand.move = toData.Length == 2 ? moveType.none : ((toData[2]).Equals("R") ? moveType.right : moveType.left);
				programm[comandIndex(fromData[0]), alphabitIndex(fromData[1])] = newComand;
			}
		}

		private void generateFormalProgram()
		{
			formalProgram = new FormalTuringProgramm();
			formalProgram.alphabit = alphabit;
			formalProgram.comandCount = comands.Length;
			formalProgram.comandTable = new string[comands.Length, alphabit.Length];
			formalProgram.comandsName = comands;
			for (int i = 0; i < comands.Length; i++) {
				for (int j = 0; j < alphabit.Length; j++) {
					ComandDescription programInstruction = programm[i, j];
					string item;
					if (programInstruction == null) {
						item = "none";
					} else {
						item = comands[i] + " '" + alphabit[j] + "'->";
						if (i > 0) {
							item += comands[programInstruction.newState] + " '" + alphabit[programInstruction.newSymbol] + "' ";
							if (programInstruction.move != moveType.none)
								item += programInstruction.move == moveType.right ? "R" : "L";
						} else {
							item += "exit";
						}
					}
					formalProgram.comandTable[i, j] = item;
				}
			}
		}

		private List<string> parseComandList(string rawCode)
		{
			List<string> comandsList = new List<string>();
			var splitResult = rawCode.Split(new char[] { '\n' });
			foreach (var line in splitResult) {
				var lineSplit = line.Split(new char[] { '\t' });
				foreach (var s in lineSplit) {
					if (s.Length > 2)
						comandsList.Add(s);
				}
			}
			return comandsList;
		}

		private void parseComandsAndAlphabits(List<string> comandsList)
		{
			HashSet<string> alphabitSet = new HashSet<string>();
			HashSet<string> comandNameSet = new HashSet<string>();

			alphabitSet.Add(Constants.emptyTuringSymbol);
			comandNameSet.Add("q0");
			string[] comandPuts;
			string[] elements;
			foreach (var s in comandsList) {
				comandPuts = s.Split(new string[] { Constants.simpleArrow }, StringSplitOptions.None);
				for (int i = 0; i < 2; i++) {
					elements = comandPuts[i].Split(new char[] { ' ' });
					comandNameSet.Add(elements[0]);
					alphabitSet.Add(elements[1].Replace("'", ""));
				}
			}
			programm = new ComandDescription[comandNameSet.Count, alphabitSet.Count];
			alphabit = new string[alphabitSet.Count];
			alphabitSet.CopyTo(alphabit);
			comands = new string[comandNameSet.Count];
			comandNameSet.CopyTo(comands);
		}

		private int comandIndex(string comand)
		{
			return getFirstPositionInArray(comands, comand);
		}

		private int alphabitIndex(String symbol)
		{
			if (symbol.StartsWith("'") && symbol.EndsWith("'") && symbol.Length > 1)
				symbol = symbol.Substring(1, symbol.Length - 2);
			return getFirstPositionInArray(alphabit, symbol);
		}

		private int getFirstPositionInArray(object[] array, object item)
		{
			for (int i = 0; i < array.Length; i++)
				if (array[i].Equals(item))
					return i;
			return -1;
		}

	}

	public class FormalTuringProgramm
	{
		public int comandCount;
		public string[] alphabit;
		public string[] comandsName;
		public string[,] comandTable;
	}

	public enum moveType
	{
		none, left, right
	}

	public class ComandDescription
	{
		public int newState;
		public int newSymbol;
		public moveType move;
		public bool isEnd = false;
	}

}
