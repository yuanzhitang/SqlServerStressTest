using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SQLStressTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			var taskList = new List<Task>();
			taskList.Add(Task.Run(() => ExecuteBatchSQL(1)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(2)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(3)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(4)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(5)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(6)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(7)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(8)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(9)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(10)));

			taskList.Add(Task.Run(() => ExecuteBatchSQL(11)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(12)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(13)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(14)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(15)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(16)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(17)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(18)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(19)));
			taskList.Add(Task.Run(() => ExecuteBatchSQL(20)));

			Task.WaitAll(taskList.ToArray());

			sw.Stop();

			var millSeconds = sw.ElapsedMilliseconds;
			Console.WriteLine($"Elapsed {millSeconds} millSeconds");
			Console.ReadKey();
		}

		private static void ExecuteBatchSQL(int no)
		{
			Console.WriteLine($"Start...{no}");

			long count = no * 100000;
			var connStr = "Data Source=localhost;Initial Catalog=SQLTarget;Integrated Security=True;Connect Timeout=30;";

			while (count > (no - 1) * 100000)
			{
				using (var conn = new SqlConnection(connStr))
				{
					conn.Open();

					for (int i = 0; i < 200; i++)
					{
						var cmd = new SqlCommand($"INSERT INTO Person values('{count}','mm','13899999999',85,'Shanghai','xxx.yyy@qq.com')")
						{
							Connection = conn
						};
						cmd.ExecuteNonQuery();

						count--;
					}
				}
			}
		}
	}
}
