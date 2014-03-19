using System;
using Brainfuck;

namespace BrainfuckConsole
{
	class MainClass
	{
		public static void Main (string[] args){
			Console.WriteLine ("Welcome to brainfuck console");
			Loop ();
		}

		public static void Loop(){
			Console.WriteLine ("Please write a script to run or type quit to exit the application");
			var s = Console.ReadLine ();

			if (s == "quit" || s == "Quit" || s == "QUIT")
				return;

			var bf = new BrainfuckInterpreter (s);

			try{
				bf.Run ();
			}
			catch (Exception ex){
				Console.WriteLine (ex.ToString ());
			}

			Loop ();
		}
	}
}
