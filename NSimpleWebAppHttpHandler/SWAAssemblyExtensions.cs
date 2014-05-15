using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NSimpleWebAppHttpHandler
{
	public static class SWAAssemblyExtensions
	{
		public static IEnumerable<Type> YuklenebilirTipleriAl(this Assembly assembly)
		{
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)//type load hatası alırsan yüklenebilen typeları dön
			{
				return e.Types.Where(t => t != null);
			}
			catch (Exception)//herhangi bir exception alırsan boş type listesi dön
			{
				return new List<Type>();
			}
		}
	}
}
