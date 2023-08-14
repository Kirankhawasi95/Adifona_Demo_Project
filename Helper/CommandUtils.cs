using System;
using System.Diagnostics;

namespace HorusUITest.Helper
{
	/// <summary>
	/// Class to help us execute programs/commands in a shell-like way.
	/// </summary>
	public static class CommandUtils
	{

		/// <summary>
		/// Executes the shell command
		/// </summary>
		/// <returns>std:out of the executed program</returns>
		/// <param name="fullCommand">full command, including absolute path and parameters</param>
		public static string ExecuteShell(string fullCommand)
		{
			char[] split = { ' ' };
			string[] parts = fullCommand.Split(split, 2);

			return ExecuteShell(parts[0], parts.Length > 1 ? parts[1] : "");
		}

		/// <summary>
		/// Executes the shell.
		/// </summary>
		/// <returns>std:out of the executed program</returns>
		/// <param name="fileName">absolute path to the desired executable</param>
		/// <param name="arguments">command line arguments for the executable.</param>
		public static string ExecuteShell(string fileName, string arguments)
		{
			var proc = new Process();
			proc.EnableRaisingEvents = false;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.ErrorDataReceived += ErrorHandler;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.FileName = fileName;
			proc.StartInfo.Arguments = arguments;
			proc.Start();
			proc.BeginErrorReadLine();
			string result = proc.StandardOutput.ReadToEnd();
			proc.WaitForExit();
			return result;
		}

		public static string ExecuteBash(string command)
		{
			ProcessStartInfo p = new ProcessStartInfo("bash");
			p.UseShellExecute = false;
			p.RedirectStandardOutput = true;
			p.RedirectStandardInput = true;
			p.CreateNoWindow = true;
			var proc = Process.Start(p);
			proc.StandardInput.WriteLine(command);
			proc.StandardInput.WriteLine("exit");
			string result = proc.StandardOutput.ReadToEnd();
			proc.WaitForExit();
			return result;
		}

		/// <summary>
		/// Handles any std:err sent by ExecuteShell method.
		/// It prints executing program info and message to console
		/// </summary>
		/// <param name="sendingProcess">Sending process.</param>
		/// <param name="errLine">Error line.</param>
		private static void ErrorHandler(object sendingProcess, DataReceivedEventArgs errLine)
		{
			if (!string.IsNullOrEmpty(errLine.Data))
			{
				var p = sendingProcess as Process;
				Console.WriteLine(new string('-', 50));
				Console.WriteLine("Error when executing program: {0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);
				Console.WriteLine("Message is:");
				Console.WriteLine(errLine.Data);
				Console.WriteLine(new string('-', 50));
			}
		}
	}
}
