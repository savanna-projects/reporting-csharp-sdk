using System;

namespace Reportium.Model.Util
{
    public static class EnvironmentVariableUtils
    {
		public static string GetEnvironmentVariable(params string[] names)
		{
			return Array.Find(names, i => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(i)));
		}
    }
}
