using System;
using System.Collections.Generic;
namespace DbGhost.Build.Reports.Text
{
	public class Boundary
	{
		private readonly string _name;
		private readonly Predicate<string> _startMarker;
		private readonly Predicate<string> _endMarker;
		private readonly List<Boundary> _boundries;
		private readonly Func<string, string> _formatter;
		public string Name
		{
			get
			{
				return _name;
			}
		}
		public List<Boundary> Boundries
		{
			get
			{
				return _boundries;
			}
		}
		public Boundary(string name) : this(name, (string e) => true, (string e) => true, new List<Boundary>(), null)
		{
		}
		public Boundary(string name, Func<string, string> formatter) : this(name, (string e) => true, (string e) => true, new List<Boundary>(), formatter)
		{
		}
		public Boundary(string name, Predicate<string> startMarker) : this(name, startMarker, (string e) => true, new List<Boundary>(), null)
		{
		}
		public Boundary(string name, Predicate<string> startMarker, Func<string, string> formatter) : this(name, startMarker, (string e) => true, new List<Boundary>(), formatter)
		{
		}
		public Boundary(string name, Predicate<string> startMarker, Predicate<string> endMarker) : this(name, startMarker, endMarker, new List<Boundary>(), null)
		{
		}
		public Boundary(string name, Predicate<string> startMarker, Predicate<string> endMarker, Func<string, string> formatter) : this(name, startMarker, endMarker, new List<Boundary>(), formatter)
		{
		}
		public Boundary(string name, Predicate<string> startMarker, Predicate<string> endMarker, List<Boundary> boundries) : this(name, startMarker, endMarker, boundries, null)
		{
		}
		public Boundary(string name, Predicate<string> startMarker, Predicate<string> endMarker, List<Boundary> boundries, Func<string, string> formatter)
		{
			_name = name;
			_startMarker = startMarker;
			_endMarker = endMarker;
			_boundries = boundries;
			_formatter = formatter;
		}
		public bool IsStart(string value)
		{
			return _startMarker(value);
		}
		public bool IsEnd(string value)
		{
			return _endMarker(value);
		}
		public string Format(string value)
		{
			if (_formatter == null)
			{
				return value;
			}
			return _formatter(value);
		}
	}
}
