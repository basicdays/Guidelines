using System;

namespace Guidelines.Core.Bootstrap
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SkipTaskAttribute : Attribute {}
}
