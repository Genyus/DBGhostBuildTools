using System;
namespace DbGhost.Build.Common
{
	public interface IApplication
	{
		bool Run(Action<string> logAction = null);
	}
}
