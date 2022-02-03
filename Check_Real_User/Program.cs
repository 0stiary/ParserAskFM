using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Check_Real_User
{
	class Program
	{
		static void Main(string[] args)
		{

			string path = @"";
			Console.WriteLine("Input your file path !");
			Console.Write("--> ");
			path = Console.ReadLine();

			Console.WriteLine("\n\n\n");

			bool finish = false;
			do
			{

				using(StreamWriter nw = new StreamWriter(path + @"\output_logins.txt", false, System.Text.Encoding.Default))
				{
					nw.Write("");
				}

				DateTime starttime = DateTime.Now;					//Начало таймера времени работы программы
				WebClient client = new WebClient();					//Создание Веб-Клиента

				string url = "https://ask.fm/";						//ссылка на сайт	
				int num_mass = 0;									//номер елемента в массиве

				#region Путь с загрузкой
				string input_path = path + @"\input_logins.txt";		//путь к входному текстовому файл с логинами

				string[] logins_input = File.ReadAllLines(input_path);              //массив логинов - входной
				List<string> logins_output = new List<string>();                    //массив логинов - выходной

				
				foreach(string login_string in logins_input)
				{
					int i = 0;
					client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");

					try
					{
						Stream data = client.OpenRead(url + logins_input[num_mass]);                //Открытие страницы в Веб-Клиент (поток)
																									//StreamReader reader = new StreamReader(data, Encoding.UTF8);				//запуск потока чтения для страницы
						string html = client.DownloadString(url + logins_input[num_mass]);                  //код станицы текущего пользователя
						string regex_pattern_userName = "(<span class=\"userName(.+|)\" dir=\"ltr\">@)(.+)(<\\/span>)";         //шаблон для проверки логина
						Match regex_match_userName = Regex.Match(html, regex_pattern_userName, RegexOptions.IgnoreCase);                                          //Проверка на существование профиля

						//Group regex_user_name_group = regex_match.Groups[i];
						string regex_pattern_userLastDate = "(<time datetime=\")(.+)(<\\/time>)";
						Match regex_match_userLastDate = Regex.Match(html, regex_pattern_userLastDate);


						Console.Write("{0}  -  {1}", num_mass + 1, logins_input[num_mass].PadRight(48));                                        //Вывести логин который проверяется

						if(regex_match_userLastDate.Success)
						{
							bool status_activity = false;

							string user_Year = "";
							user_Year += regex_match_userLastDate.Value[16].ToString() + regex_match_userLastDate.Value[17].ToString() + regex_match_userLastDate.Value[18].ToString() + regex_match_userLastDate.Value[19].ToString();

							if(DateTime.Now.Year - Convert.ToInt32(user_Year) <= 1)
							{
								int date_cur_summ = (DateTime.Now.Year * 365) + (DateTime.Now.Month * 30) + DateTime.Now.Day;
								int date_user_summ = Convert.ToInt32(user_Year) * 365;

								string user_Month = "";
								user_Month += regex_match_userLastDate.Value[21].ToString() + regex_match_userLastDate.Value[22].ToString();

								if(Math.Abs(DateTime.Now.Month - Convert.ToInt32(user_Month)) <= 1)
								{
									date_user_summ += (Convert.ToInt32(user_Month) * 30);

									string user_Day = "";
									user_Day += regex_match_userLastDate.Value[24].ToString() + regex_match_userLastDate.Value[25].ToString();

									if(Math.Abs(date_cur_summ - (date_user_summ + Convert.ToInt32(user_Day))) <= 12)
									{
										status_activity = true;
									}
								}
							}



							if(status_activity)
							{
								int username_position = regex_match_userName.Value.IndexOf("dir=\"ltr\">@", StringComparison.CurrentCultureIgnoreCase) + 11;

								string userName_output = "";
								while(regex_match_userName.Value[username_position] != '<')
								{
									userName_output += regex_match_userName.Value[username_position++];
								}

								Console.Write("-\t");
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("OK");
								Console.ResetColor();
								logins_output.Add(userName_output);
								using(StreamWriter sw = new StreamWriter(path + @"\output_logins.txt", true, System.Text.Encoding.Default))
								{
									sw.WriteLine(logins_output[logins_output.Count-1]);
								}
							}
							else
							{
								Console.Write("-\t");
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("NO");
								Console.ResetColor();
								logins_output.Add("");
								using(StreamWriter sw = new StreamWriter(path + @"\output_logins.txt", true, System.Text.Encoding.Default))
								{
									sw.WriteLine(logins_output[logins_output.Count - 1]);
								}
						}
						}
						else
						{

							Console.Write("-\t");
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("NO");
							Console.ResetColor();
							logins_output.Add("");
							using(StreamWriter sw = new StreamWriter(path + @"\output_logins.txt", true, System.Text.Encoding.Default))
							{
								sw.WriteLine(logins_output[logins_output.Count - 1]);
							}
					}



						num_mass++;                                                     //переход к следующему логину (номер елемента в массиве)


						data.Close();                                                   //закрыть Веб-Клиент (поток)
																						//reader.Close();													//закрыть поток чтения страницы
					}
					catch(Exception e)
					{
						Console.Write("{0}  -  ".PadRight(56), num_mass + 1);
						Console.Write("-\t" + e.Message + "\n");
						num_mass++;
						logins_output.Add("");
						using(StreamWriter sw = new StreamWriter(path + @"\output_logins.txt", true, System.Text.Encoding.Default))
						{
							sw.WriteLine(logins_output[logins_output.Count - 1]);
						}
						continue;
					}
				}
				#endregion

			
			

				//Console.WriteLine(i);
				DateTime endtime = DateTime.Now;										//конец таймера времени работы программы
				Console.WriteLine(endtime - starttime);									//вывести как долго работала программа

				/*foreach(string login in logins_output)
				{
					Console.WriteLine(login);

					using(StreamWriter sw = new StreamWriter(path + @"\output_logins.txt", true, System.Text.Encoding.Default))
					{
						
					}
					Console.WriteLine("Запись выполнена");
				}*/


				client.Dispose();
				finish = true;

				if(finish)
				{
					Console.WriteLine("\n\n\n\n\n");
					Console.WriteLine("\t\t\t\t Do you want to start checking again?");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("\n\t\t\t\t     yes");
					Console.ResetColor();
					Console.Write("   /   ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("any other word/key\n\n");

					Console.ForegroundColor = ConsoleColor.Blue;
					Console.Write("--> ");
					Console.ResetColor();
					if(Console.ReadLine() == "yes")
					{
						finish = false;
						Console.Clear();
					}
				}
			} while(!finish);
		}
	}
}
