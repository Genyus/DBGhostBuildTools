using DbGhost.Build.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
namespace DbGhost.Build.Reports.Text
{
	public class Converter
	{
		private const string DateFormat = "yyyy-MM-ddThh:mm:ss";
		private const string AttribEnd = "end";
		private const string AttribStart = "start";
		private const string AttribDuration = "duration";
		private readonly Boundary _boundary;
		public event EventHandler<EventArgs<Boundary>> BeginBoundary;
		public Converter(string rootName)
		{
			_boundary = new Boundary(rootName, (string n) => true, (string n) => false);
		}
		public Boundary AddBoundary(Boundary boundary)
		{
			_boundary.Boundries.Add(boundary);
			return boundary;
		}
		public XDocument Convert(string path)
		{
			XDocument xDocument = new XDocument();
			using (Reader reader = new Reader(path))
			{
				Stack<Boundary> stack = new Stack<Boundary>();
				stack.Push(_boundary);
				XElement xElement = xDocument.Add<XElement>(new XElement(_boundary.Name));
				Entry entry;
				do
				{
					entry = reader.Read();
					if (entry != null && !entry.IsEmpty)
					{
						string entryData = entry.Data;
						Boundary boundary = stack.Peek();
						if (!boundary.IsEnd(entry.Data) && boundary.Boundries.Count > 0 && boundary.Boundries.Exists((Boundary b) => b.IsStart(entryData)))
						{
							boundary = boundary.Boundries.First((Boundary b) => b.IsStart(entryData));
							stack.Push(boundary);
							xElement = xElement.Add<XElement>(new XElement(boundary.Name));
							EnsureStartTimestamp(entry, xElement);
							if (BeginBoundary != null)
							{
								BeginBoundary(this, new EventArgs<Boundary>(boundary));
							}
						}
						if (xElement != null)
						{
							if (boundary.Boundries.Count == 0)
							{
								xElement.Add(new XCData(boundary.Format(entry.Data)));
							}
							if (boundary.IsEnd(entry.Data))
							{
								EnsureEndTimestamp(entry, xElement);
								EnsureDuration(xElement);
								xElement = xElement.Parent;
								stack.Pop();
							}
						}
					}
				}
				while (entry != null);
			}
			return xDocument;
		}
		private static void EnsureStartTimestamp(Entry entry, XElement element)
		{
			if (entry.HasTimestamp)
			{
				element.Add(new XAttribute("start", entry.Timestamp.ToString("yyyy-MM-ddThh:mm:ss")));
				if (element.Parent != null && element.Parent.Attribute("start") == null)
				{
					element.Parent.Add(new XAttribute("start", entry.Timestamp.ToString("yyyy-MM-ddThh:mm:ss")));
				}
			}
		}
		private void EnsureEndTimestamp(Entry entry, XElement element)
		{
			if (entry.HasTimestamp && _boundary.Boundries.Count > 0 && element.Attribute("end") == null)
			{
				element.Add(new XAttribute("end", entry.Timestamp.ToString("yyyy-MM-ddThh:mm:ss")));
			}
			if (entry.HasTimestamp)
			{
				if (element.Parent != null && element.Parent.Attribute("end") == null)
				{
					element.Parent.Add(new XAttribute("end", entry.Timestamp.ToString("yyyy-MM-ddThh:mm:ss")));
					return;
				}
				if (element.Parent != null && element.Parent.Attributes("end").Any<XAttribute>())
				{
					element.Parent.Attributes("end").First<XAttribute>().Value = entry.Timestamp.ToString("yyyy-MM-ddThh:mm:ss");
				}
			}
		}
		private static void EnsureDuration(XElement element)
		{
			if (element.Parent == null)
			{
				return;
			}
			if (element.Parent.Attribute("start") != null && element.Parent.Attribute("end") != null)
			{
				if (element.Parent.Attribute("duration") == null)
				{
					element.Parent.Add(new XAttribute("duration", (DateTime.Parse(element.Parent.Attributes("end").First<XAttribute>().Value) - DateTime.Parse(element.Parent.Attributes("start").First<XAttribute>().Value)).ToString()));
					return;
				}
				element.Parent.Attributes("duration").First<XAttribute>().Value = (DateTime.Parse(element.Parent.Attributes("end").First<XAttribute>().Value) - DateTime.Parse(element.Parent.Attributes("start").First<XAttribute>().Value)).ToString();
			}
		}
	}
}
