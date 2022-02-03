using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Real_Checking.OwnClasses
{
	public static class Helper
	{
		public static Dictionary<string, Color> CaseColors = new Dictionary<string, Color>
		{
			{"OK", Color.Green},
			{"Date", Color.DarkOrange},
			{"NO", Color.Red},
			{"No such user", Color.Red},
			{"Incorrect input login!", Color.Red},
			{"No answers", Color.Red}
		};

		/// <summary>
		/// Convering string to integer
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int ToInt(string str)
		{
			return Convert.ToInt32(str);
		}

		/// <summary>
		/// Open needed file (too Lazy to open it manually)
		/// </summary>
		/// <param name="textBox"> TextBox with needed filepath</param>
		/// <param name="openFileDialog">OpenFileDialog instance</param>
		public static void OpenFile(TextBox textBox, OpenFileDialog openFileDialog)
		{
			openFileDialog.ShowDialog();
			textBox.Text = openFileDialog.FileName;
		}
	}

	public static class SyncTextBoxExtensions
	{
		public static void AppendText(this RichTextBox box, string text, Color color)
		{
			box.SelectionStart = box.TextLength;
			box.SelectionLength = 0;
			box.SelectionColor = color;
			box.AppendText(text);
			box.SelectionColor = box.ForeColor;
		}
	}

}
