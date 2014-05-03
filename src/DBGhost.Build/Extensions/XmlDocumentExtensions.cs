using System;
using System.Xml;
namespace DbGhost.Build.Extensions
{
	public static class XmlDocumentExtensions
	{
		public static T GetValue<T>(this XmlDocument document, string xpath)
		{
			string value = document.GetValue(xpath);
			if (value == null)
			{
				return default(T);
			}
			return (T)(Convert.ChangeType(value, typeof(T)));
		}
		public static string GetValue(this XmlDocument document, string xpath)
		{
			XmlNode xmlNode = document.SelectSingleNode(xpath);
			if (xmlNode != null && xmlNode.FirstChild != null)
			{
				return xmlNode.FirstChild.Value;
			}
			return null;
		}
		public static void SetValue<T>(this XmlDocument document, string xpath, T value)
		{
			document.SetValue(xpath, value.ToString());
		}
		public static void SetValue(this XmlDocument document, string xpath, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				XmlNode xmlNode = document.SelectSingleNode(xpath);
				if (xmlNode != null)
				{
					if (xmlNode.FirstChild != null)
					{
						xmlNode.FirstChild.Value = value;
						return;
					}
					xmlNode.AppendChild(document.CreateTextNode(value));
				}
			}
		}
	}
}
