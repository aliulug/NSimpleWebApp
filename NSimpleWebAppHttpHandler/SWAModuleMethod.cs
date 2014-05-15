using System;

namespace NSimpleWebAppHttpHandler
{
	[AttributeUsage(AttributeTargets.Method)]
	public class SWAModuleMethod : Attribute
	{
		private readonly string _name;

		public SWAModuleMethod(string name)
		{
			_name = name;
		}

		public string Name { get { return _name; } }
	}
}
