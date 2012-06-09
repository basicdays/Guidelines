using System;
using Guidelines.Core.Properties;

namespace Guidelines.Core
{
	public abstract class ValueObjectBase<TValue> : IEquatable<TValue>, IComparable<TValue>
		where TValue : IEquatable<TValue>, IComparable<TValue>
	{
		public TValue Value { get; private set; }

		protected ValueObjectBase() { }

		protected ValueObjectBase(TValue value)
		{
			if (ReferenceEquals(value, null)) {
				throw new InvalidOperationException(Resources.Error_ValueCannotBeNull);
			}
			Value = value;
		}

		public static bool operator ==(ValueObjectBase<TValue> valueObject1, ValueObjectBase<TValue> valueObject2)
		{
			if (ReferenceEquals(valueObject1, null) || ReferenceEquals(valueObject2, null)) {
				return false;
			}

			return valueObject1.Equals(valueObject2);
		}

		public static bool operator !=(ValueObjectBase<TValue> valueObject1, ValueObjectBase<TValue> valueObject2)
		{
			return !(valueObject1 == valueObject2);
		}

		public override bool Equals(object obj)
		{
			return !ReferenceEquals(obj, null) && Value.Equals(obj);
		}

		public virtual bool Equals(TValue other)
		{
			return !ReferenceEquals(other, null) && Value.Equals(other);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public virtual int CompareTo(TValue other)
		{
			return Value.CompareTo(other);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", GetType().Name, Value);
		}
	}
}