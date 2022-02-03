using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Real_Checking.OwnClasses
{
	public class Writer : IWriter
	{
		private readonly SyncTextBox _statusConsole;
		private readonly TextBox _outputFileTextBox;
		public void ConsoleWrite(string text, SyncTextBox outSyncTextBox)
		{
			outSyncTextBox.AppendText(text);
		}
		public void ConsoleWrite(string text, Color color, SyncTextBox outSyncTextBox)
		{
			outSyncTextBox.AppendText(text, color);
		}
		public void LogFileOutput(ref List<string> parsedUsernamesList, int numMass, string loginCase = "", string parsedUsername = "")
		{
			ConsoleWrite($"{numMass}  -  " + loginCase + "\r\n", Helper.CaseColors.ContainsKey(loginCase) ? Helper.CaseColors[loginCase] : _statusConsole.ForeColor, _statusConsole);

			parsedUsernamesList.Add(parsedUsername);

			using var sw = new StreamWriter(_outputFileTextBox.Text, true, Encoding.Default);
			sw.WriteLine(parsedUsernamesList.Last());
		}

		public Writer(SyncTextBox statusConsole, TextBox outputFileTextBox) =>
			(_statusConsole, _outputFileTextBox) = (statusConsole, outputFileTextBox);
	}
}