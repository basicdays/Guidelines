using System;

namespace Guidelines.Core
{
	public static class ObjectExtensions
	{
		public static object GetPropertyValue(this object o, string propertyName)
		{
			object objValue = string.Empty;

			var propertyInfo = o.GetType().GetProperty(propertyName);
			if (propertyInfo != null)
			{
				objValue = propertyInfo.GetValue(o, null);
			}
			return objValue;
		}

		public static bool HasAttribute(this object obj, Type attributeType)
		{
			return !obj.GetType().GetCustomAttributes(attributeType, true).IsEmpty();
		}
	}
}
