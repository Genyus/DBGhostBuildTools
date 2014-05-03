using System;
using System.IO;
using System.Linq;
namespace DbGhost.Build.Extensions
{
	public static class StringExtensions
	{
		public static bool StartsWithAny(this string value, StringComparison comparisonType, params string[] values)
		{
			return value != null && values.Any((string comparand) => value.StartsWith(comparand, comparisonType));
		}
		public static bool IsNullOrEmpty(this string value, bool ignoreWhitespace)
		{
			return value == null || value.IsEmpty(ignoreWhitespace);
		}
		public static bool IsEmpty(this string value, bool ignoreWhitespace)
		{
			return value != null && ((!ignoreWhitespace && value.Length == 0) || (ignoreWhitespace && value.Trim().Length == 0));
		}
		public static string EnsureAbsolutePath(this string value, string path)
		{
			if (value == null)
			{
				return null;
			}
			if (!Path.IsPathRooted(value))
			{
				return Path.GetFullPath(Path.Combine(path, value));
			}
			return value;
		}
		public static string ToRelativePath(this string root, string path)
		{
			if (!path.StartsWith(root, StringComparison.OrdinalIgnoreCase))
			{
				return path;
			}
			return path.Substring(root.Length);
		}
		public static string GetParentDirectoryName(this string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName == null)
			{
				return null;
			}
			string[] array = directoryName.Split(new char[]
			{
				Path.DirectorySeparatorChar
			});
			return array[array.GetUpperBound(0)];
		}
		public static T ParseEnum<T>(this string value)
		{
			return (T)((object)Enum.Parse(typeof(T), value));
		}
		public static string Coalesce<T>(this string value, T? newValue) where T : struct
		{
			if (!newValue.HasValue)
			{
				return value;
			}
			T value2 = newValue.Value;
			return value2.ToString();
		}
	}
}
