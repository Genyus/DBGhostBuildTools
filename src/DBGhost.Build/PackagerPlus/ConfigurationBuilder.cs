using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using System;
namespace DbGhost.Build.PackagerPlus
{
	public class ConfigurationBuilder
	{
		private readonly Parameters _parameters;
		public ConfigurationBuilder(Parameters parameters)
		{
			_parameters = parameters;
		}
		public Configuration Build()
		{
			return ConfigurationBuilder.LoadConfiguration(_parameters);
		}
		private static Configuration LoadConfiguration(Parameters parameters)
		{
			Configuration configuration = new Configuration(parameters.TemplateConfigurationPath);
			configuration.OutputDirectory = configuration.OutputDirectory.CoalesceReverse(parameters.ArtifactsDirectory);
			configuration.RootDirectory = configuration.RootDirectory.CoalesceReverse(parameters.RootDirectory);
			configuration.ConfigurationPath = configuration.ConfigurationPath.CoalesceReverse(parameters.ConfigurationPath.EnsureAbsolutePath(parameters.ArtifactsDirectory));
			configuration.InstallerVersion = configuration.InstallerVersion.CoalesceReverse(parameters.InstallerVersion);
			configuration.SourceDatabaseName = configuration.SourceDatabaseName.CoalesceReverse(parameters.SourceDatabase.Name);
			configuration.SourceDatabaseServer = configuration.SourceDatabaseServer.CoalesceReverse(parameters.SourceDatabase.Server);
			configuration.SourceDatabaseUsername = configuration.SourceDatabaseUsername.CoalesceReverse(parameters.SourceDatabase.Username);
			configuration.SourceDatabasePassword = configuration.SourceDatabasePassword.CoalesceReverse(parameters.SourceDatabase.Password);
			configuration.SourceDatabaseAuthenticationMode = configuration.SourceDatabaseAuthenticationMode.Coalesce(parameters.SourceDatabase.Authentication);
			configuration.TargetDatabaseName = configuration.TargetDatabaseName.CoalesceReverse(parameters.TargetDatabase.Name);
			configuration.TargetDatabaseServer = configuration.TargetDatabaseServer.CoalesceReverse(parameters.TargetDatabase.Server);
			configuration.TargetDatabaseUsername = configuration.TargetDatabaseUsername.CoalesceReverse(parameters.TargetDatabase.Username);
			configuration.TargetDatabasePassword = configuration.TargetDatabasePassword.CoalesceReverse(parameters.TargetDatabase.Password);
			return configuration;
		}
	}
}
