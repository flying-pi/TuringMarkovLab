using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Turing
{
	public class MarkovAlg: IAlghoritm
	{
		List<MarkovStep> program = new List<MarkovStep>();

		string currentState = "";
		int currentComand = 0;

		public MarkovAlg(string rawCode)
		{
			while (rawCode.Contains("' "))
				rawCode = rawCode.Replace("' ", "");
			while (rawCode.Contains(" '"))
				rawCode = rawCode.Replace(" '", "");
			rawCode = rawCode.Trim();
			var codeLines = rawCode.Split(new char[] { '\n' });
			foreach (var s in codeLines)
			{
				MarkovStep newStep = new MarkovStep();
				string seperaotr;
				string startitem = (s.StartsWith("'") ? "'" : "");
				string endItem = (s.EndsWith("'") ? "'" : "");
				if (s.Contains(startitem + Constants.simpleArrow + endItem))
				{
					seperaotr = startitem + Constants.simpleArrow + endItem;
					newStep.isFinish = false;
				}
				else {
					seperaotr = startitem + Constants.stopArrow + endItem;
					newStep.isFinish = true;
				}

				if (s.StartsWith(seperaotr))
				{
					newStep.fromStr = "";
					newStep.toStr = s.Replace(seperaotr, "");
				}
				else
				if (s.EndsWith(seperaotr))
				{
					newStep.fromStr = s.Replace(seperaotr, "");
					newStep.toStr = "";
				}
				else {
					var puts = s.Split(new string[] { seperaotr }, StringSplitOptions.None);
					newStep.fromStr = puts[0];
					newStep.toStr = puts[1];
				}
				newStep.fromStr = newStep.fromStr.Replace("'", "");
				newStep.toStr = newStep.toStr.Replace("'", "");
				program.Add(newStep);

			}
		}

		public string calculate(string initState)
		{
			setInitState(initState);
			startCalculation();
			while (nextStep());
			return getCurrentState();
		}

		public void setInitState(string initState)
		{
			this.currentState = initState;
		}

		public string getCurrentState()
		{
			return currentState;
		}

		public void startCalculation()
		{
			currentComand = 0;
		}

		public bool nextStep()
		{
			if (currentComand >= program.Count)
				return false;
			MarkovStep currentStep = program[currentComand];
			if (currentStep.fromStr.Length == 0)
			{
				currentState = currentStep.toStr + currentState;
				currentComand = 0;
				return !currentStep.isFinish;
			}
			if (currentState.Contains(currentStep.fromStr))
			{
				var regex = new Regex(Regex.Escape(currentStep.fromStr));
				currentState = regex.Replace(currentState,currentStep.toStr, 1);
				currentComand = 0;
				return !currentStep.isFinish;
			}
			currentComand++;
			return true;
		}
	}

	class MarkovStep
	{
		public string fromStr;
		public string toStr;
		public bool isFinish;
	}
}
