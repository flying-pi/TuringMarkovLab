
using System;
namespace Turing
{
	public interface IAlghoritm
	{
		string calculate(string initState);
		void setInitState(string initState);
		string getCurrentState();
		void startCalculation();
		bool nextStep();
	}
}
