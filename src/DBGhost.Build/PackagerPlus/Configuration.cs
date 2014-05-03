using DbGhost.Build.Common;
using DbGhost.Build.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
namespace DbGhost.Build.PackagerPlus
{
	public class Configuration : ConfigurationBase
	{
		private readonly string _path;
		public override string ConfigurationPath
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SavePath");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SavePath", value);
			}
		}
		public string ReportPath
		{
			get
			{
				string directoryName = Path.GetDirectoryName(_path);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_path);
				if (directoryName == null)
				{
					return null;
				}
				return Path.Combine(directoryName, string.Format("{0}({1})_CompAndSync.log", fileNameWithoutExtension, InstallerVersion));
			}
		}
		public string OutputDirectory
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/ProjAssemblyPath");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/ProjAssemblyPath", value);
			}
		}
		public string RootDirectory
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/ScriptFolders/RootDirectory");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/ScriptFolders/RootDirectory", value);
				SetScriptPathsRoot(value);
			}
		}
		public string InstallerVersion
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/PackagerOptionSettings/InstallerVersion");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/PackagerOptionSettings/InstallerVersion", value);
			}
		}
		public string SourceDatabaseName
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBName");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBName", value);
			}
		}
		public string SourceDatabaseServer
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBServer");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBServer", value);
			}
		}
		public string SourceDatabaseUsername
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBUserName");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBUserName", value);
			}
		}
		public string SourceDatabasePassword
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBPassword");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/DBSettings/DBPassword", value);
			}
		}
		public string SourceDatabaseAuthenticationMode
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SourceSelectedOptions/DBSettings/AuthenticationMode");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SourceSelectedOptions/DBSettings/AuthenticationMode", value);
			}
		}
		public string TargetDatabaseName
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SilentInstallDBName");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SilentInstallDBName", value);
			}
		}
		public string TargetDatabaseServer
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SilentInstallServerName");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SilentInstallServerName", value);
			}
		}
		public string TargetDatabaseUsername
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SilentInstallServerLogin");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SilentInstallServerLogin", value);
			}
		}
		public string TargetDatabasePassword
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SilentInstallServerPassword");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SilentInstallServerPassword", value);
			}
		}
		public string SilentInstallMode
		{
			get
			{
				return ConfigurationDocument.GetValue("/DBGhost/SilentInstallMode");
			}
			set
			{
				ConfigurationDocument.SetValue("/DBGhost/SilentInstallMode", value);
			}
		}
		public Configuration(string path) : base(path)
		{
			_path = path;
		}
		public Dictionary<string, string> GetSchemaScriptPaths()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			XmlNodeList xmlNodeList = ConfigurationDocument.SelectNodes("/DBGhost/SourceSelectedOptions/ScriptFolders/*/Path");
			if (xmlNodeList != null)
			{
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					string value = xmlNode.FirstChild.Value;
					if (!dictionary.ContainsKey(value) && xmlNode.ParentNode != null)
					{
						dictionary.Add(value, xmlNode.ParentNode.Name);
					}
				}
			}
			return dictionary;
		}
		private void SetScriptPathsRoot(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			XmlNodeList xmlNodeList = ConfigurationDocument.SelectNodes("/DBGhost/SourceSelectedOptions/ScriptFolders/*/Path");
			if (xmlNodeList == null)
			{
				return;
			}
			foreach (XmlNode xmlNode in xmlNodeList)
			{
				if (xmlNode.FirstChild != null && !xmlNode.FirstChild.Value.IsNullOrEmpty(true))
				{
					string text = null;
					string text2 = null;
					if (xmlNode.ParentNode != null)
					{
						string name = xmlNode.ParentNode.Name;
						switch (name)
						{
							case "BeforeBuildScript":
							case "AfterBuildScript":
							case "BeforeSyncScript":
							case "AfterSyncScript":
								text = Path.GetDirectoryName(xmlNode.FirstChild.Value);
								text2 = Path.GetFileName(xmlNode.FirstChild.Value);
								break;
							default:
								text = xmlNode.FirstChild.Value;
								break;
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						if (!string.IsNullOrEmpty(text2))
						{
							xmlNode.FirstChild.Value = Path.Combine(Path.Combine(path, text), text2);
						}
						else
						{
							string[] array = text.Split(new char[]
							{
								'\\'
							});
							text = array[array.Length - 1];
							xmlNode.FirstChild.Value = Path.Combine(path, text);
						}
					}
				}
			}
		}
	}
}
