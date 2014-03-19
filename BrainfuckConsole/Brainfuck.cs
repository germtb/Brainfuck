using System;
using System.Linq;
using System.Collections.Generic;

namespace Brainfuck
{
	//Example
	//var bf = new Brainfuck.BrainfuckInterpreter (
	//"> +++++ ++ [ < +++++ +++++ > - ] < ++ . >" +					//H
	//"> +++++ +++++ [ < +++++ +++++ > - ] < + . >" +				//e
	//"> +++++ +++++ [ < +++++ +++++ > - ] < +++++ +++ . >" +		//l
	//"> +++++ +++++ [ < +++++ +++++ > - ] < +++++ +++ . >" +		//l
	//"> +++++ +++++ + [ < +++++ +++++ > - ] < + .");				//o
	//bf.Run ();

	public class BrainfuckInterpreter
	{
		const char PlusOne = 	'+';
		const char MinusOne = 	'-';
		const char MoveRight = 	'>';
		const char MoveLeft = 	'<';
		const char ToASCII = 	'.';
		const char Read = 		',';
		const char LeftLoop = 	'[';
		const char RightLoop = 	']';

		readonly List<char> script = new List<char>();
		readonly List<int> cells = new List<int>();

		public BrainfuckInterpreter (string s){
			script = s.ToCharArray ().ToList ();
			for (int i = 0; i < 30000; i++)
				cells.Add (0);
		}

		public void Run(){
			int cellPointer = 0;
			int scriptPointer = 0;

			while (scriptPointer < script.Count) {

				char operation = script [scriptPointer];

				if (operation == PlusOne) {
					cells [cellPointer]++;
				} 
				else if (operation == MinusOne) {
					cells [cellPointer]--;
				} 
				else if (operation == MoveRight) {
					cellPointer++;
				} 
				else if (operation == MoveLeft) {
					cellPointer--;
				} 
				else if (operation == ToASCII) {
					if (cells [cellPointer] < 0 || cells [cellPointer] > 256)
						throw new Exception (string.Format ("Cell {0} has a value of {1} which is not convertibl to ASCII code", cellPointer, cells [cellPointer]));

					Console.WriteLine ((char)cells [cellPointer]);
				} 
				else if (operation == Read) {
					cells [cellPointer] = Console.Read ();
					Console.WriteLine ("");
				} 
				else if (operation == LeftLoop) {
					if (cells [cellPointer] == 0)
						scriptPointer = FindRightLoop (scriptPointer);
				} 
				else if (operation == RightLoop) {
					if (cells [cellPointer] != 0)
						scriptPointer = FindLeftLoop (scriptPointer);
				}

				if (cellPointer < 0 || cellPointer >= cells.Count)
					throw new Exception ("Cell pointer out of range");

				scriptPointer++;
			}
		}

		int FindRightLoop(int scriptPointer){
			int openLoopsCount = 1;

			for (int i = scriptPointer + 1; i < script.Count; i++) {

				if (script [i] == LeftLoop)
					openLoopsCount++;
				else if (script [i] == RightLoop)
					openLoopsCount--;

				if (openLoopsCount == 0)
					return i;
			}

			throw new Exception ("Loop not closed");
		}

		int FindLeftLoop(int scriptPointer){
			int openLoopsCount = 1;

			for (int i = scriptPointer - 1; i >= 0; i--) {

				if (script [i] == RightLoop)
					openLoopsCount++;
				else if (script [i] == LeftLoop)
					openLoopsCount--;

				if (openLoopsCount == 0)
					return i;
			}

			throw new Exception ("Loop not opened");
		}
	}
}

