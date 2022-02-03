using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Real_Checking.OwnClasses;

namespace Real_Checking
{	public partial class Form1 : Form
	{
		/// <summary>
		/// State for finishing programm
		/// </summary>
		private bool _finish;
		/// <summary>
		/// Background Worker for async check
		/// </summary>
		private BackgroundWorker _bw;
		/// <summary>
		/// Emulating browser
		/// </summary>
		private readonly WebClient _webClient = new WebClient{Proxy = null};
		/// <summary>
		/// Writer to Log TextBox
		/// </summary>
		private Writer _writer;


		private void run_button_Click(object sender, EventArgs e)
		{
			label1.Visible = label2.Visible = !answersCheckBox.Checked;	//Labels for rule - Check first user`s answer for delta time with "now".
			_finish = false;
			loginConsole.Clear();			//Clearing Login Output Console
			statusConsole.Clear();			//Clearing Status (of login checking) Console
			runButton.Visible = false;		//Changing from start to stop - protection from cascading _bw
			stopButton.Visible = true;
			_webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");


			_writer = new Writer(statusConsole, outputFileTextBox);
			_bw = new BackgroundWorker {WorkerSupportsCancellation = true};

			_bw.DoWork += (obj, ea)=> Check_Real();		//Adding method for checking to _bw
			_bw.RunWorkerAsync();
		}

		private async void Check_Real()
		{
			using (StreamWriter nw = new StreamWriter(outputFileTextBox.Text, false, Encoding.Default))
			{
				await nw.WriteAsync("");						// Clear output txt file
			}

			var startTime = DateTime.Now;						//Start time of checking process
			var numMass = 0;									//Number of element in list of input logins
			
			var inputLogins = File.ReadAllLines(inputFileTextBox.Text).ToList();				//Input logins list
			var parsedLogins = new List<string>();														//Output logins
			
			//Starting main part of checking for each element
			foreach (string uInputLogin in inputLogins)
			{
				if(_finish)
					break;
				
				_writer.ConsoleWrite((numMass + 1) + "\t-\t".PadRight(10), loginConsole);   //Log postion number for login and pad it
				_writer.ConsoleWrite(uInputLogin + "\r\n", loginConsole);                   //Raw user login from input

				try
				{
					string url = "https://ask.fm/" + uInputLogin;						//User page url

					string htmlPage = await _webClient.DownloadStringTaskAsync(url);	//Htmp page (code) of user`s page

					//State for activity of user - default false (time checking needed)
					//If we don't check user`s post - only for availability of page. Is that user present in ask.fm database - if present - status - he is active
					var isUserActive = !answersCheckBox.Checked;

					//Is user real?
					//Checks for availible field for his username login on page
					Match usernameRegexMatch = Regex.Match(htmlPage, "(<span class=\"(.+|) dir=\"ltr\">@)(.+)(<\\/span>)", RegexOptions.IgnoreCase);                                          //Проверка на существование профиля
					if (!usernameRegexMatch.Success)                //If there is no field with username
						throw new Exception("No such user");
					
					//Is his (user) have any answers?
					//Checks for first answer on page by datetime of post
					Match uDateRegexMatch = Regex.Match(htmlPage, "(<time datetime=\")(.+)(<\\/time>)");
					if (!uDateRegexMatch.Success && !isUserActive)					//If user has no answers or exclusion with time check
						throw new Exception("No answers");
					
					
					//If we needs time checking
					//Needed rule - last answer has been published < 13 days ago.
					if (!isUserActive)
					{
						//Year of last user`s answer
						var userYear = Helper.ToInt(uDateRegexMatch.Value[16].ToString() + uDateRegexMatch.Value[17] +
						             uDateRegexMatch.Value[18] + uDateRegexMatch.Value[19]);
						//Check for year
						if (startTime.Year - userYear <= 1)
						{
							var currentFullDate = (startTime.Year * 365) + (startTime.Month * 30) + startTime.Day;					//Current full date in days format;
							var uMonth = Helper.ToInt(uDateRegexMatch.Value[21].ToString() + uDateRegexMatch.Value[22]);			//Month of last user`s answer
							//Check for month
							if (Math.Abs(startTime.Month - uMonth) <= 1)
							{
								var uFullDate = userYear * 365 + (uMonth * 30); ;													//Converting userYear + months in days;
								var uDay = Helper.ToInt(uDateRegexMatch.Value[24].ToString() + uDateRegexMatch.Value[25]);		//Day of post
								if (Math.Abs(currentFullDate - (uFullDate + uDay)) <= 12)	//If delta of current date (in days) and user` last post date (in days) < 13 
									isUserActive = true;
							}
						}

						//If delta of post (answer) and current date is >= 13
						if (!isUserActive)
							throw new Exception("Date");
					}

					//Regex for taking correct login from user`s page;
					//Why? Because database which will work with output logins is case-sensetive. User can be fool and input login in bad casing rule
					var uPositionRegex = "dir=\"ltr\">@";

					//Always true due to user`s page is avaliable
					var uPositionRegexMatch = usernameRegexMatch.Value.IndexOf(uPositionRegex, StringComparison.CurrentCultureIgnoreCase) + uPositionRegex.Length;
					
					var parsedUsername = ""; //Correct login

					//Taking correct login from MATCH result by ever char 
					//I don't know how to optimise that in .NET Framework, due to unavailable of System.Range here(((( 
					while(usernameRegexMatch.Value[uPositionRegexMatch] != '<')			
						parsedUsername += usernameRegexMatch.Value[uPositionRegexMatch++];

					numMass++; //Helping position variable, because List.IndexOf() will find first input in List. Why? Because user can input login twice. Human factor for deleteting dublicates
					
					//Add correct login to correct logins list, and output login status to console
					_writer.LogFileOutput(ref parsedLogins, numMass, "OK", parsedUsername);
				}
				catch(Exception exception)
				{
					numMass++;
					//Add empty char "" to correct logins list (skip login (empty cell for Excel)), and output login error (status) to console
					_writer.LogFileOutput(ref parsedLogins, numMass, exception.Message);
				}
			}

			_writer.ConsoleWrite((DateTime.Now - startTime).ToString(), loginConsole);			//Output how long check process was working

			stopButton.Visible = false;
			runButton.Visible = true;
			Process.Start(outputFileTextBox.Text);												//Open output txt file (too lazy to open it manually evert check)
			_webClient.Dispose();
			_bw.Dispose();

		}

		#region Events
		private void stop_button_Click(object sender, EventArgs e)
		{
			stopButton.Visible = false;
			runButton.Visible = true;
			_bw.CancelAsync();
			_finish = true;
		}

		private void input_button_Click(object sender, EventArgs e)
		{
			Helper.OpenFile(inputFileTextBox, openFile);
		}

		private void output_button_Click(object sender, EventArgs e)
		{
			Helper.OpenFile(outputFileTextBox, openFile);
		}

		private void input_file_text_TextChanged(object sender, EventArgs e)
		{
			outputFileButton.Visible = outputFileLabel.Visible = outputFileTextBox.Visible = openInputFileButton.Visible = true;
			Process.Start(inputFileTextBox.Text);
		}

		private void output_file_text_TextChanged(object sender, EventArgs e)
		{
			runButton.Visible = true;
		}

		private void out_console_TextChanged(object sender, EventArgs e)
		{
			statusConsole.Focus();
		}

		private void status_console_TextChanged(object sender, EventArgs e)
		{
			loginConsole.Focus();
		}

		private void openInputFileButton_Click(object sender, EventArgs e)
		{
			Process.Start(inputFileTextBox.Text);
		}
		#endregion

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//AllocConsole();
		}
		//[DllImport("kernel32.dll", SetLastError = true)]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//static extern bool AllocConsole();
	}
}
