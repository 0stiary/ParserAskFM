using System.Collections.Generic;
using System.Drawing;

namespace Real_Checking.OwnClasses
{
	public interface IWriter
	{
		/// <summary>
		/// Write to needed console needed text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="outSyncTextBox"> Login / Status console</param>
		public void ConsoleWrite(string text, SyncTextBox outSyncTextBox);
		/// <summary>
		/// Write to needed console needed text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="color">Color for text</param>
		/// <param name="outSyncTextBox"> Login / Status console</param>
		public void ConsoleWrite(string text, Color color, SyncTextBox outSyncTextBox);

		/// <summary>
		/// Add correct login to output list of logins (for debug!)
		/// Write status of login to log console
		/// Add last login in list of correct output logins to file (output) 
		/// </summary>
		/// <param name="parsedUsernamesList">Correct logins list</param>
		/// <param name="numMass">Position referenced to login console</param>
		/// <param name="loginCase">Status of login</param>
		/// <param name="parsedUsername">Correct login</param>
		public void LogFileOutput(ref List<string> parsedUsernamesList, int numMass, string loginCase = "", string parsedUsername = "");
	}
}