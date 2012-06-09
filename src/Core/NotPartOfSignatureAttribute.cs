using System;

namespace Guidelines.Core
{
	[AttributeUsage(AttributeTargets.Property)]
	public class NotPartOfSignatureAttribute : Attribute { }
}
