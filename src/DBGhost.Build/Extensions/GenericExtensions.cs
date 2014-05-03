namespace DbGhost.Build.Extensions
{
	internal static class GenericExtensions
	{
		public static T Coalesce<T>(this T value, T newValue) where T : class
		{
			T instance = value;

			if (value == null)
			{
				instance = newValue;
			}

			return instance;
		}

		public static T CoalesceReverse<T>(this T value, T newValue) where T : class
		{
			T instance = newValue;

			if (newValue == null)
			{
				instance = value;
			}

			return instance;
		}
	}
}
