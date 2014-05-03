using DbGhost.Build.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
namespace DbGhost.Build.ChangeManager
{
	public class Configuration
	{
		private readonly XmlDocument _configuration;
		public string ProcessTypeString
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/ChangeManagerProcessType");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/ChangeManagerProcessType", value);
			}
		}
		public Parameters.ProcessType ProcessType
		{
			get
			{
				return (Parameters.ProcessType)Enum.Parse(typeof(Parameters.ProcessType), ProcessTypeString);
			}
			set
			{
				ProcessTypeString = value.ToString();
			}
		}
		public string RootDirectory
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SchemaScripts/RootDirectory");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SchemaScripts/RootDirectory", value);
				_configuration.SetValue("/DBGhost/Scripter/OutputFolder", value);
				SetScriptPathsRoot(value);
			}
		}
		public string ConfigurationPath
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SavePath");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SavePath", value);
			}
		}
		public string BuildFile
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/BuildSQLFileName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/BuildSQLFileName", value);
			}
		}
		public string DeltaFile
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/DeltaScriptsFileName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/DeltaScriptsFileName", value);
			}
		}
		public string ReportPath
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/ReportFileName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/ReportFileName", value);
				_configuration.SetValue("/DBGhost/Scripter/ReportFilename", value);
			}
		}
		public XmlDocument Source
		{
			get
			{
				return _configuration;
			}
		}
		public string BuildDatabaseName
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/BuildDBName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/BuildDBName", value);
			}
		}
		public string PreserveBuildDatabase
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/CompareOptions/KeepNewDatabase");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/CompareOptions/KeepNewDatabase", value);
			}
		}
		public string BuildTemplateDatabaseScript
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SchemaScripts/DropCreateDatabaseScript");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SchemaScripts/DropCreateDatabaseScript", value);
			}
		}
		public string BuildTemplateDatabaseName
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TemplateDB/DBName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TemplateDB/DBName", value);
			}
		}
		public string BuildTemplateDatabaseServer
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TemplateDB/DBServer");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TemplateDB/DBServer", value);
			}
		}
		public string BuildTemplateDatabaseUsername
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TemplateDB/DBUserName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TemplateDB/DBUserName", value);
			}
		}
		public string BuildTemplateDatabasePassword
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TemplateDB/DBPassword");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TemplateDB/DBPassword", value);
			}
		}
		public string BuildTemplateDatabaseAuthenticationMode
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TemplateDB/AuthenticationMode");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TemplateDB/AuthenticationMode", value);
			}
		}
		public string CompareSourceDatabaseName
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SourceDB/DBName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SourceDB/DBName", value);
			}
		}
		public string CompareSourceDatabaseServer
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SourceDB/DBServer");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SourceDB/DBServer", value);
			}
		}
		public string CompareSourceDatabaseUsername
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SourceDB/DBUserName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SourceDB/DBUserName", value);
			}
		}
		public string CompareSourceDatabasePassword
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SourceDB/DBPassword");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SourceDB/DBPassword", value);
			}
		}
		public string CompareSourceDatabaseAuthenticationMode
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/SourceDB/AuthenticationMode");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/SourceDB/AuthenticationMode", value);
			}
		}
		public string CompareTargetDatabaseName
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TargetDB/DBName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TargetDB/DBName", value);
			}
		}
		public string CompareTargetDatabaseServer
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TargetDB/DBServer");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TargetDB/DBServer", value);
			}
		}
		public string CompareTargetDatabaseUsername
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TargetDB/DBUserName");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TargetDB/DBUserName", value);
			}
		}
		public string CompareTargetDatabasePassword
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TargetDB/DBPassword");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TargetDB/DBPassword", value);
			}
		}
		public string CompareTargetDatabaseAuthenticationMode
		{
			get
			{
				return _configuration.GetValue("/DBGhost/ChangeManager/TargetDB/AuthenticationMode");
			}
			set
			{
				_configuration.SetValue("/DBGhost/ChangeManager/TargetDB/AuthenticationMode", value);
			}
		}
		public string ScriptDatabaseName
		{
			get
			{
				return _configuration.GetValue("/DBGhost/Scripter/DatabaseToScript/Database");
			}
			set
			{
				_configuration.SetValue("/DBGhost/Scripter/DatabaseToScript/Database", value);
			}
		}
		public string ScriptDatabaseServer
		{
			get
			{
				return _configuration.GetValue("/DBGhost/Scripter/DatabaseToScript/Server");
			}
			set
			{
				_configuration.SetValue("/DBGhost/Scripter/DatabaseToScript/Server", value);
			}
		}
		public string ScriptDatabaseUsername
		{
			get
			{
				return _configuration.GetValue("/DBGhost/Scripter/DatabaseToScript/Username");
			}
			set
			{
				_configuration.SetValue("/DBGhost/Scripter/DatabaseToScript/Username", value);
			}
		}
		public string ScriptDatabasePassword
		{
			get
			{
				return _configuration.GetValue("/DBGhost/Scripter/DatabaseToScript/Password");
			}
			set
			{
				_configuration.SetValue("/DBGhost/Scripter/DatabaseToScript/Password", value);
			}
		}
		public Configuration(string path)
		{
			_configuration = new XmlDocument();
			_configuration.Load(path);
		}
		public Dictionary<string, string> GetSchemaScriptPaths()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			XmlNodeList xmlNodeList = _configuration.SelectNodes("/DBGhost/ChangeManager/SchemaScripts/*/Path");
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
		public void Save()
		{
			_configuration.Save(ConfigurationPath);
		}
		private void SetScriptPathsRoot(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			XmlNodeList xmlNodeList = _configuration.SelectNodes("/DBGhost/ChangeManager/SchemaScripts/*/Path");
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
