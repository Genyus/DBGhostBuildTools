using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using DbGhost.Build.PackagerPlus;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace DbGhost.Build.NAnt.PackagerPlus
{
	[TaskName("dbghost-packagerplus")]
	public class Task : TaskBase
	{
		private readonly Parameters _parameters = new Parameters();
		[TaskAttribute("installerVersion", Required = false)]
		public string ProcessType
		{
			get
			{
				return _parameters.InstallerVersion;
			}
			set
			{
				_parameters.InstallerVersion = value;
			}
		}
		[TaskAttribute("applicationPath", Required = false)]
		public string ApplicationPath
		{
			get
			{
				return _parameters.ApplicationPath;
			}
			set
			{
				_parameters.ApplicationPath = value;
			}
		}
		[TaskAttribute("templateConfigurationPath", Required = true)]
		public string TemplateConfigurationPath
		{
			get
			{
				return _parameters.TemplateConfigurationPath;
			}
			set
			{
				_parameters.TemplateConfigurationPath = value;
			}
		}
		[TaskAttribute("rootDirectory", Required = false)]
		public string RootFolder
		{
			get
			{
				return _parameters.RootDirectory;
			}
			set
			{
				_parameters.RootDirectory = value;
			}
		}
		[TaskAttribute("artifactsDirectory", Required = true)]
		public string ArtifactsFolder
		{
			get
			{
				return _parameters.ArtifactsDirectory;
			}
			set
			{
				_parameters.ArtifactsDirectory = value;
			}
		}
		[TaskAttribute("configurationPath", Required = false)]
		public string ConfigurationPath
		{
			get
			{
				return _parameters.ConfigurationPath;
			}
			set
			{
				_parameters.ConfigurationPath = value;
			}
		}
		[TaskAttribute("xmlReportFile", Required = false)]
		public string XmlReportFilePath
		{
			get
			{
				return _parameters.XmlReportFilePath;
			}
			set
			{
				_parameters.XmlReportFilePath = value;
			}
		}
		[TaskAttribute("resultProperty")]
		public string ResultProperty
		{
			get;
			set;
		}
		[TaskAttribute("sourceDatabaseName", Required = false)]
		public string SourceDatabaseName
		{
			get
			{
				return _parameters.SourceDatabase.Name;
			}
			set
			{
				_parameters.SourceDatabase.Name = value;
			}
		}
		[TaskAttribute("sourceDatabaseServer", Required = false)]
		public string SourceDatabaseServer
		{
			get
			{
				return _parameters.SourceDatabase.Server;
			}
			set
			{
				_parameters.SourceDatabase.Server = value;
			}
		}
		[TaskAttribute("sourceDatabaseUsername", Required = false)]
		public string SourceDatabaseUsername
		{
			get
			{
				return _parameters.SourceDatabase.Username;
			}
			set
			{
				_parameters.SourceDatabase.Username = value;
			}
		}
		[TaskAttribute("sourceDatabasePassword", Required = false)]
		public string SourceDatabasePassword
		{
			get
			{
				return _parameters.SourceDatabase.Password;
			}
			set
			{
				_parameters.SourceDatabase.Password = value;
			}
		}
		[TaskAttribute("sourceDatabaseAuthenticationMode", Required = false)]
		public string SourceDatabaseAuthenticationMode
		{
			get
			{
				return _parameters.SourceDatabase.Authentication.ToString();
			}
			set
			{
				_parameters.SourceDatabase.Authentication = value.ParseEnum<Database.AuthenticationMode>();
			}
		}
		[TaskAttribute("targetDatabaseName", Required = false)]
		public string TargetDatabaseName
		{
			get
			{
				return _parameters.TargetDatabase.Name;
			}
			set
			{
				_parameters.TargetDatabase.Name = value;
			}
		}
		[TaskAttribute("targetDatabaseServer", Required = false)]
		public string TargetDatabaseServer
		{
			get
			{
				return _parameters.TargetDatabase.Server;
			}
			set
			{
				_parameters.TargetDatabase.Server = value;
			}
		}
		[TaskAttribute("targetDatabaseUsername", Required = false)]
		public string TargetDatabaseUsername
		{
			get
			{
				return _parameters.TargetDatabase.Username;
			}
			set
			{
				_parameters.TargetDatabase.Username = value;
			}
		}
		[TaskAttribute("targetDatabasePassword", Required = false)]
		public string TargetDatabasePassword
		{
			get
			{
				return _parameters.TargetDatabase.Password;
			}
			set
			{
				_parameters.TargetDatabase.Password = value;
			}
		}
		protected override void ExecuteTask()
		{
			Application application = new Application(_parameters);
			if (!application.Run(LogEntry))
			{
				if (ResultProperty != null)
				{
					Properties[ResultProperty] = "1";
				}
				throw new BuildException("DBGhost Change Manager encountered an error. View the log for more information.");
			}
			if (ResultProperty != null)
			{
				Properties[ResultProperty] = "0";
			}
		}
	}
}
