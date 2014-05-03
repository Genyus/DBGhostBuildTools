using System;
using System.IO;
namespace DbGhost.Build.Common
{
	public abstract class ParametersBase
	{
		private string _applicationPath;
		public string ApplicationPath
		{
			get
			{
				return _applicationPath;
			}
			set
			{
				_applicationPath = (value ?? _applicationPath);
			}
		}
		protected ParametersBase(string defaultAppPath32, string defaultAppPath64)
		{
			if (File.Exists(defaultAppPath32))
			{
				ApplicationPath = defaultAppPath32;
				return;
			}
			if (File.Exists(defaultAppPath64))
			{
				ApplicationPath = defaultAppPath64;
			}
		}
	}
}
