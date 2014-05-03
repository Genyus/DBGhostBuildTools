using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using DbGhost.Build.Reports;
using System;
using System.Diagnostics;
using System.IO;

namespace DbGhost.Build.PackagerPlus
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
				string xmlReportPath = _parameters.XmlReportFilePath.EnsureAbsolutePath(_parameters.ArtifactsDirectory);
				if (!Application.GenerateReport(configuration, xmlReportPath))
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
