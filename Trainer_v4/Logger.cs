using System;

namespace Trainer_v4
{
	public static class Logger
	{
		private static void ConsoleLog(string str) => DevConsole.Console.Log($"Trainer Property {nameof(str)}: {str}");
		public static void Log(this string str) => ConsoleLog(str);
		public static void Log(this bool str) => ConsoleLog(str.ToString());
		public static void Log(this int str) => ConsoleLog(str.ToString());
		public static void Log(this float str) => ConsoleLog(str.ToString());
		public static void Log(this double str) => ConsoleLog(str.ToString());
		public static void Log(this object str) => ConsoleLog(str.ToString());
		public static void LogException(this Exception ex) => DevConsole.Console.Log($"Trainer Exception: {ex.Message}");
	}
}
