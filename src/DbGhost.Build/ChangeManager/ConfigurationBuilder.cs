using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using System;
using System.IO;
using System.Reflection;
namespace DbGhost.Build.ChangeManager
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
			ConfigurationBuilder.EnsureCreateDatabaseScriptParameters(_parameters);
			return ConfigurationBuilder.LoadConfiguration(_parameters);
		}
		private static void EnsureCreateDatabaseScriptParameters(Parameters parameters)
		{
			if (!string.IsNullOrEmpty(parameters.BuildDatabaseTemplateScript) || !string.IsNullOrEmpty(parameters.BuildDatabaseTemplateName) || (parameters.ProcessMode != Parameters.ProcessType.BuildDatabase && parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompare && parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompareAndCreateDelta && parameters.ProcessMode != Parameters.ProcessType.BuildDatabaseAndCompareAndSynchronize))
			{
				return;
			}
			string text = parameters.BuildDatabase.Name = (parameters.BuildDatabase.Name ?? Guid.NewGuid().ToString());
			string text2 = text.EnsureAbsolutePath(parameters.ArtifactsDirectory);
			parameters.BuildDatabaseTemplateScript = text2;
			parameters.CompareSourceDatabase.Name = text;
			parameters.BuildDatabaseTemplateName = parameters.CompareTargetDatabase.Name;
			File.WriteAllText(text2, string.Format(new StreamReader(Assembly.GetExecutingAssembly().FindManifestResourceStream("DropAndCreateDatabase.sql")).ReadToEnd(), text));
		}
		private static Configuration LoadConfiguration(Parameters parameters)
		{
			Configuration configuration = new Configuration(parameters.TemplateConfigurationPath);
			configuration.ProcessTypeString = configuration.ProcessTypeString.Coalesce(parameters.ProcessMode);
			configuration.RootDirectory = configuration.RootDirectory.CoalesceReverse(parameters.RootDirectory);
			configuration.ConfigurationPath = configuration.ConfigurationPath.CoalesceReverse(parameters.ConfigurationPath.EnsureAbsolutePath(parameters.ArtifactsDirectory));
			configuration.BuildFile = configuration.BuildFile.CoalesceReverse(parameters.BuildScriptPath.EnsureAbsolutePath(parameters.ArtifactsDirectory));
			configuration.DeltaFile = configuration.DeltaFile.CoalesceReverse(parameters.CompareDeltaScriptPath.EnsureAbsolutePath(parameters.ArtifactsDirectory));
			configuration.ReportPath = configuration.ReportPath.CoalesceReverse(parameters.ReportFilePath.EnsureAbsolutePath(parameters.ArtifactsDirectory));
			configuration.BuildTemplateDatabaseName = configuration.BuildTemplateDatabaseName.CoalesceReverse(parameters.BuildDatabaseTemplateName);
			configuration.BuildTemplateDatabaseScript = configuration.BuildTemplateDatabaseScript.CoalesceReverse(parameters.BuildDatabaseTemplateScript);
			configuration.BuildDatabaseName = configuration.BuildDatabaseName.CoalesceReverse(parameters.BuildDatabase.Name);
			configuration.BuildTemplateDatabaseServer = configuration.BuildTemplateDatabaseServer.CoalesceReverse(parameters.BuildDatabase.Server);
			configuration.BuildTemplateDatabaseUsername = configuration.BuildTemplateDatabaseUsername.CoalesceReverse(parameters.BuildDatabase.Username);
			configuration.BuildTemplateDatabasePassword = configuration.BuildTemplateDatabasePassword.CoalesceReverse(parameters.BuildDatabase.Password);
			configuration.BuildTemplateDatabaseAuthenticationMode = configuration.BuildTemplateDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);
			configuration.PreserveBuildDatabase = ((parameters.ProcessMode == Parameters.ProcessType.BuildDatabase) ? "true" : configuration.PreserveBuildDatabase.CoalesceReverse(parameters.PreserveBuildDatabase));
			configuration.CompareSourceDatabaseName = configuration.CompareSourceDatabaseName.CoalesceReverse(parameters.BuildDatabase.Name);
			configuration.CompareSourceDatabaseServer = configuration.CompareSourceDatabaseServer.CoalesceReverse(parameters.BuildDatabase.Server);
			configuration.CompareSourceDatabaseUsername = configuration.CompareSourceDatabaseUsername.CoalesceReverse(parameters.BuildDatabase.Username);
			configuration.CompareSourceDatabasePassword = configuration.CompareSourceDatabasePassword.CoalesceReverse(parameters.BuildDatabase.Password);
			configuration.CompareSourceDatabaseAuthenticationMode = configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.BuildDatabase.Authentication);
			configuration.CompareSourceDatabaseName = configuration.CompareSourceDatabaseName.CoalesceReverse(parameters.CompareSourceDatabase.Name);
			configuration.CompareSourceDatabaseServer = configuration.CompareSourceDatabaseServer.CoalesceReverse(parameters.CompareSourceDatabase.Server);
			configuration.CompareSourceDatabaseUsername = configuration.CompareSourceDatabaseUsername.CoalesceReverse(parameters.CompareSourceDatabase.Username);
			configuration.CompareSourceDatabasePassword = configuration.CompareSourceDatabasePassword.CoalesceReverse(parameters.CompareSourceDatabase.Password);
			configuration.CompareSourceDatabaseAuthenticationMode = configuration.CompareSourceDatabaseAuthenticationMode.Coalesce(parameters.CompareSourceDatabase.Authentication);
			configuration.CompareTargetDatabaseName = configuration.CompareTargetDatabaseName.CoalesceReverse(parameters.CompareTargetDatabase.Name);
			configuration.CompareTargetDatabaseServer = configuration.CompareTargetDatabaseServer.CoalesceReverse(parameters.CompareTargetDatabase.Server);
			configuration.CompareTargetDatabaseUsername = configuration.CompareTargetDatabaseUsername.CoalesceReverse(parameters.CompareTargetDatabase.Username);
			configuration.CompareTargetDatabasePassword = configuration.CompareTargetDatabasePassword.CoalesceReverse(parameters.CompareTargetDatabase.Password);
			configuration.CompareTargetDatabaseAuthenticationMode = configuration.CompareTargetDatabaseAuthenticationMode.Coalesce(parameters.CompareTargetDatabase.Authentication);
			configuration.ScriptDatabaseName = configuration.ScriptDatabaseName.CoalesceReverse(parameters.ScriptSourceDatabase.Name);
			configuration.ScriptDatabaseServer = configuration.ScriptDatabaseServer.CoalesceReverse(parameters.ScriptSourceDatabase.Server);
			configuration.ScriptDatabaseUsername = configuration.ScriptDatabaseUsername.CoalesceReverse(parameters.ScriptSourceDatabase.Username);
			configuration.ScriptDatabasePassword = configuration.ScriptDatabasePassword.CoalesceReverse(parameters.ScriptSourceDatabase.Password);
			return configuration;
		}
	}
}
