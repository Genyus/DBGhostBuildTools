using DbGhost.Build.Common;
using System;
namespace DbGhost.Build.ChangeManager
{
	public class Parameters : ParametersBase
	{
		public enum ProcessType
		{
			ScriptDatabase,
			ScriptDatabaseAndBuildDatabase,
			ScriptDatabaseAndBuildDatabaseAndCompare,
			ScriptDatabaseAndBuildDatabaseAndCompareAndCreateDelta,
			ScriptDatabaseAndBuildDatabaseAndCompareAndSynchronize,
			BuildDatabase,
			BuildDatabaseAndCompare,
			BuildDatabaseAndCompareAndSynchronize,
			BuildDatabaseAndCompareAndCreateDelta,
			CompareOnly,
			CompareAndSynchronize,
			CompareAndCreateDelta,
			CopyDatabase
		}
		private const string DefaultAppPath32 = "C:\\Program Files\\DB Ghost\\ChangeManagerCmd.exe";
		private const string DefaultAppPath64 = "C:\\Program Files (x86)\\DB Ghost\\ChangeManagerCmd.exe";
		public Parameters.ProcessType? ProcessMode
		{
			get;
			set;
		}
		public string TemplateConfigurationPath
		{
			get;
			set;
		}
		public string RootDirectory
		{
			get;
			set;
		}
		public string ArtifactsDirectory
		{
			get;
			set;
		}
		public string ConfigurationPath
		{
			get;
			set;
		}
		public string ReportFilePath
		{
			get;
			set;
		}
		public string XmlReportFilePath
		{
			get;
			set;
		}
		public string BuildDatabaseTemplateName
		{
			get;
			set;
		}
		public string BuildDatabaseTemplateScript
		{
			get;
			set;
		}
		public string PreserveBuildDatabase
		{
			get;
			set;
		}
		public string BuildScriptPath
		{
			get;
			set;
		}
		public string CompareDeltaScriptPath
		{
			get;
			set;
		}
		public Database ScriptSourceDatabase
		{
			get;
			set;
		}
		public Database BuildDatabase
		{
			get;
			set;
		}
		public Database CompareSourceDatabase
		{
			get;
			set;
		}
		public Database CompareTargetDatabase
		{
			get;
			set;
		}
		public Parameters() : base("C:\\Program Files\\DB Ghost\\ChangeManagerCmd.exe", "C:\\Program Files (x86)\\DB Ghost\\ChangeManagerCmd.exe")
		{
			XmlReportFilePath = "DBGhost.ChangeManager.Report.xml";
			ScriptSourceDatabase = new Database();
			BuildDatabase = new Database();
			CompareSourceDatabase = new Database();
			CompareTargetDatabase = new Database();
		}
	}
}
