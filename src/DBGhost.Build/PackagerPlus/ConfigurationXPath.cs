using System;
namespace DbGhost.Build.PackagerPlus
{
	public static class ConfigurationXPath
	{
		public const string ApplicationName = "/DBGhost/ApplicationName";
		public const string OutputDir = "/DBGhost/ProjAssemblyPath";
		public const string ChangeSchemaScripts = "/DBGhost/SourceSelectedOptions/ScriptFolders";
		public const string ChangeRootDir = "/DBGhost/SourceSelectedOptions/ScriptFolders/RootDirectory";
		public const string ChangeSchemaScriptsPath = "/DBGhost/SourceSelectedOptions/ScriptFolders/*/Path";
		public const string SourceDbName = "/DBGhost/SourceSelectedOptions/DBSettings/DBName";
		public const string SourceDbServer = "/DBGhost/SourceSelectedOptions/DBSettings/DBServer";
		public const string SourceDbUsername = "/DBGhost/SourceSelectedOptions/DBSettings/DBUserName";
		public const string SourceDbPassword = "/DBGhost/SourceSelectedOptions/DBSettings/DBPassword";
		public const string SourceDbAuthMode = "/DBGhost/SourceSelectedOptions/DBSettings/AuthenticationMode";
		public const string TargetDbName = "/DBGhost/SilentInstallDBName";
		public const string TargetDbServer = "/DBGhost/SilentInstallServerName";
		public const string TargetDbUsername = "/DBGhost/SilentInstallServerLogin";
		public const string TargetDbPassword = "/DBGhost/SilentInstallServerPassword";
		public const string SilentInstallMode = "/DBGhost/SilentInstallMode";
		public const string InstallerVersion = "/DBGhost/PackagerOptionSettings/InstallerVersion";
		public const string ChangeSavePath = "/DBGhost/SavePath";
	}
}
