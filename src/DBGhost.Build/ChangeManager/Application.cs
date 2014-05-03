using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using DbGhost.Build.Reports;
using System;
using System.Diagnostics;
using System.IO;
namespace DbGhost.Build.ChangeManager
{
	public class Application : IApplication
	{
		private readonly Parameters _parameters;
		public Application(Parameters parameters)
		{
			_parameters = parameters;
		}
		public bool Run(Action<string> logAction = null)
		{
			Configuration configuration = new ConfigurationBuilder(_parameters).Build();
			configuration.Save();
			if (File.Exists(configuration.ReportPath))
			{
				File.Delete(configuration.ReportPath);
			}
			ProcessStartInfo startInfo = new ProcessStartInfo(_parameters.ApplicationPath)
			{
				Arguments = string.Format("\"{0}\"", configuration.ConfigurationPath),
				UseShellExecute = false,
				RedirectStandardOutput = logAction != null
			};
			bool result;
			using (Process process = Process.Start(startInfo))
			{
				if (logAction != null)
				{
					using (StreamReader standardOutput = process.StandardOutput)
					{
						string obj;
						while ((obj = standardOutput.ReadLine()) != null)
						{
							logAction(obj);
						}
					}
				}
				process.WaitForExit();
				result = (process.ExitCode == 0);
			}
			if (File.Exists(configuration.ReportPath))
			{
				string text = _parameters.XmlReportFilePath.EnsureAbsolutePath(_parameters.ArtifactsDirectory);
				Report report = new Report(configuration.ReportPath, new ReportFormatter(), configuration.ConfigurationPath);
				report.Save(text);
				if (!Application.GenerateReport(configuration, text))
				{
					result = false;
				}
			}
			return result;
		}
		private static bool GenerateReport(Configuration configuration, string xmlReportPath)
		{
			Report report = new Report(configuration.ReportPath, new ReportFormatter(), configuration.ConfigurationPath);
			report.Save(xmlReportPath);
			return !report.HasErrors;
		}
	}
}
