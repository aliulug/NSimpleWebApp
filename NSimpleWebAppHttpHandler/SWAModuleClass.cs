using System;

namespace NSimpleWebAppHttpHandler
{
	[AttributeUsage(AttributeTargets.Class)]
	public class SWAModuleClass : Attribute
	{
		private readonly string _name;

		public SWAModuleClass(string name)
		{
			_name = name;
		}

		public string Name { get { return _name; } }
	}
}
