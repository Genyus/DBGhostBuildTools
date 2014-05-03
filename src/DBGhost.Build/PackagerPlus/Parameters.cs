using DbGhost.Build.Common;
using System;
namespace DbGhost.Build.PackagerPlus
{
	public class Parameters : ParametersBase
	{
		private const string DefaultAppPath32 = "C:\\Program Files\\DB Ghost\\PackagerPlusCmd.exe";
		private const string DefaultAppPath64 = "C:\\Program Files (x86)\\DB Ghost\\PackagerPlusCmd.exe";
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
		public string XmlReportFilePath
		{
			get;
			set;
		}
		public string InstallerVersion
		{
			get;
			set;
		}
		public Database SourceDatabase
		{
			get;
			set;
		}
		public Database TargetDatabase
		{
			get;
			set;
		}
		public Parameters() : base("C:\\Program Files\\DB Ghost\\PackagerPlusCmd.exe", "C:\\Program Files (x86)\\DB Ghost\\PackagerPlusCmd.exe")
		{
			XmlReportFilePath = "DBGhost.PackagerPlus.Report.xml";
			SourceDatabase = new Database();
			TargetDatabase = new Database();
		}
	}
}
