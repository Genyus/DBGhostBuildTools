using System;
namespace DbGhost.Build.Common
{
	public class Database
	{
		public enum AuthenticationMode
		{
			SqlServer,
			Windows
		}
		public string Name
		{
			get;
			set;
		}
		public string Server
		{
			get;
			set;
		}
		public string Username
		{
			get;
			set;
		}
		public string Password
		{
			get;
			set;
		}
		public Database.AuthenticationMode? Authentication
		{
			get;
			set;
		}
	}
}
