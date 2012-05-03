using System;

namespace Guidelines.Domain.Validation
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class ValidateObjectAttribute : Attribute
	{
		
	}
}
