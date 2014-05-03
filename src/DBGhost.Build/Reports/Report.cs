using System;
using System.Xml.Linq;
namespace DbGhost.Build.Reports
{
	public class Report
	{
		private XDocument _report;
		private bool _hasErrors;
		public bool HasErrors
		{
			get
			{
				return _hasErrors;
			}
		}
		public Report(string reportPath, IReportFormatter formatter, string configurationPath)
		{
			Load(reportPath, formatter, configurationPath);
		}
		public void Save(string path)
		{
			_report.Save(path);
		}
		private void Load(string reportPath, IReportFormatter formatter, string configurationPath)
		{
			FormatterResult formatterResult = formatter.Load(reportPath);
			XDocument xDocument = XDocument.Load(configurationPath);
			_report = new XDocument(new object[]
			{
				new XElement("dbGhost", new object[]
				{
					new XAttribute("errors", formatterResult.HasErrors),
					formatterResult.Report.Elements(),
					xDocument.Elements()
				})
			});
			_hasErrors = formatterResult.HasErrors;
		}
	}
}
