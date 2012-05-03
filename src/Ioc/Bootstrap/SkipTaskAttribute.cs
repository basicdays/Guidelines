using System;

namespace Guidelines.Ioc.Bootstrap
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SkipTaskAttribute : Attribute {}
}
