using DbGhost.Build.Extensions;
using DbGhost.Build.Reports;
using DbGhost.Build.Reports.Text;
using System;
using System.Collections.Generic;
namespace DbGhost.Build.PackagerPlus
{
	public class ReportFormatter : IReportFormatter
	{
		private readonly Converter _converter;
		private bool _hasErrors;
		public ReportFormatter()
		{
			_converter = CreateConverter();
		}
		public FormatterResult Load(string path)
		{
			return new FormatterResult(_converter.Convert(path), _hasErrors);
		}
		private Converter CreateConverter()
		{
			Converter converter = new Converter("report");
			converter.BeginBoundary += delegate(object o, EventArgs<Boundary> e)
			{
				if (e.Value.Name == "error")
				{
					_hasErrors = true;
				}
			};
			Boundary item = new Boundary("error", (string e) => e == "<ERROR>", (string e) => e == "</ERROR>", new List<Boundary>
			{
				new Boundary("message")
			});
			Boundary item2 = new Boundary("warning", (string e) => e == "<WARNING>", (string e) => e == "</WARNING>", new List<Boundary>
			{
				new Boundary("message")
			});
			Converter arg_181_0 = converter;
			string arg_17C_0 = "scripter";
			Predicate<string> arg_17C_1 = (string e) => e.StartsWith("DB Ghost Data and Schema Scripter");
			Predicate<string> arg_17C_2 = (string e) => e.StartsWith("DB Ghost Data and Schema Scripter") && e.Contains("complete");
			List<Boundary> list = new List<Boundary>();
			list.Add(new Boundary("script", (string e) => e.StartsWith("File Scripted"), (string f) => f.Replace("File Scripted", string.Empty).Trim()));
			list.Add(item);
			list.Add(item2);
			arg_181_0.AddBoundary(new Boundary(arg_17C_0, arg_17C_1, arg_17C_2, list));
			Converter arg_230_0 = converter;
			string arg_22B_0 = "builder";
			Predicate<string> arg_22B_1 = (string e) => e.StartsWith("DB Ghost Database Builder");
			Predicate<string> arg_22B_2 = (string e) => e.StartsWith("DB Ghost Database Builder") && e.Contains("complete");
			List<Boundary> list2 = new List<Boundary>();
			list2.Add(new Boundary("script", (string e) => e.StartsWith("Executing file"), (string f) => f.Remove(0, f.IndexOf("-") + 1).Trim()));
			list2.Add(item);
			list2.Add(item2);
			arg_230_0.AddBoundary(new Boundary(arg_22B_0, arg_22B_1, arg_22B_2, list2));
			Converter arg_2C2_0 = converter;
			string arg_2BD_0 = "compare";
			Predicate<string> arg_2BD_1 = (string e) => e.StartsWithAny(StringComparison.OrdinalIgnoreCase, new string[]
			{
				"DB Ghost PackagerPlus",
				"DB Ghost Change Manager",
				"Innovartis.DBGhost.PackagerPlusCmd",
				"Running (DB Ghost Change Manager)"
			});
			Predicate<string> arg_2BD_2 = (string e) => (e.StartsWith("DB Ghost Change Manager") || e.StartsWith("Innovartis.DBGhost.PackagerPlusCmd")) && e.Contains("complete");
			List<Boundary> list3 = new List<Boundary>();
			list3.Add(new Boundary("object", (string e) => e.StartsWithAny(StringComparison.OrdinalIgnoreCase, new string[]
			{
				"Created",
				"Altered",
				"Inserted",
				"Deleted",
				"Added",
				"Updated",
				"Modified",
				"Dropped",
				"Renamed",
				"Delta file"
			})));
			list3.Add(item);
			list3.Add(item2);
			arg_2C2_0.AddBoundary(new Boundary(arg_2BD_0, arg_2BD_1, arg_2BD_2, list3));
			return converter;
		}
	}
}
