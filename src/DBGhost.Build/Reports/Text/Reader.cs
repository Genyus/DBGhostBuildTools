using System;
using System.IO;
namespace DbGhost.Build.Reports.Text
{
	internal class Reader : IDisposable
	{
		private readonly TextReader _reader;
		public Reader(string path)
		{
			_reader = new StreamReader(path);
		}
		public Entry Read()
		{
			string text = _reader.ReadLine();
			if (text == null)
			{
				return null;
			}
			text = text.Trim();
			string[] array = text.Split(new string[]
			{
				"..."
			}, 2, StringSplitOptions.None);
			switch (array.Length)
			{
			case 1:
				return new Entry(text);
			case 2:
				return new Entry(array[0].Trim(), array[1].Trim());
			default:
				return new Entry(text);
			}
		}
		public void Dispose()
		{
			if (_reader != null)
			{
				_reader.Dispose();
			}
		}
	}
}
