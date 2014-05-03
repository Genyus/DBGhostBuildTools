using System;
using System.Xml;
namespace DbGhost.Build.Common
{
	public abstract class ConfigurationBase
	{
		protected readonly XmlDocument ConfigurationDocument;
		public abstract string ConfigurationPath
		{
			get;
			set;
		}
		public XmlDocument Source
		{
			get
			{
				return ConfigurationDocument;
			}
		}
		protected ConfigurationBase(string path)
		{
			ConfigurationDocument = new XmlDocument();
			ConfigurationDocument.Load(path);
		}
		public void Save()
		{
			ConfigurationDocument.Save(ConfigurationPath);
		}
	}
}
