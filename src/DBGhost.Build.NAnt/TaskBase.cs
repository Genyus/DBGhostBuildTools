using NAnt.Core;
using System;
using System.Text.RegularExpressions;
namespace DbGhost.Build.NAnt
{
	public abstract class TaskBase : Task
	{
		private readonly Regex _pattern = new Regex("[a-zA-Z]+", RegexOptions.Compiled);
		protected void LogEntry(string line)
		{
			int num = line.IndexOf("...", StringComparison.OrdinalIgnoreCase);
			int num2 = num + 3;
			if (num > 0 && _pattern.IsMatch(line, num2))
			{
				Log(Level.Info, line.Substring(num2));
			}
		}
		public virtual void Run()
		{
			ExecuteTask();
		}
	}
}
