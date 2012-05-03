using System;

namespace Guidelines.Domain
{
	[AttributeUsage(AttributeTargets.Property)]
	public class NotPartOfSignatureAttribute : Attribute { }
}
