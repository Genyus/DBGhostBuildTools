using System.IO;
using System.Linq;
using System.Reflection;

namespace DbGhost.Build.Extensions
{
	public static class AssemblyExtensions
	{
		public static Stream FindManifestResourceStream(this Assembly assembly, string name)
		{
			string[] manifestResourceNames = assembly.GetManifestResourceNames();

			if (name != null && manifestResourceNames.Length > 0)
			{
				string text = (from n in manifestResourceNames
							   orderby n.Length descending
							   select n).FirstOrDefault(n => n.EndsWith(name));

				if (!string.IsNullOrEmpty(text))
				{
					return assembly.GetManifestResourceStream(text);
				}
			}

			return null;
		}
	}
}
