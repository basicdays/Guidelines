using System;
using System.Collections;
using System.Linq;
using Guidelines.Core.Validation;

namespace Guidelines.Core
{
	/// <summary>
	/// Implements generic and non-generic Equals by comparing the GetHashCode result. Readable properties without the
	/// <see cref="NotPartOfSignatureAttribute"/> create the object's signature, including iterable collections. GetHashCode
	/// is based on the signature of the object, including iterable collections.
	/// </summary>
	/// <typeparam name="TType">The object that is using the conventional Equals methods.</typeparam>
	[ValidateObject]
	public abstract class EntityBase<TType> : IEquatable<TType>, IComparable<TType>
		where TType : EntityBase<TType>
	{
		public Guid? Id { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is TType)) {
				return false;
			}
			return GetHashCode() == obj.GetHashCode();
		}

		//review: should equals use hash instead of id, see remarks at http://msdn.microsoft.com/en-us/library/6sh2ey19.aspx
		public virtual bool Equals(TType other)
		{
			return Id == other.Id;
		}

		public virtual bool SignatureEquals(TType other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		public override int GetHashCode()
		{
			const int prime1 = 17;
			const int prime2 = 29;

			var properties = typeof (TType).GetProperties()
				.Where(property =>
					property.CanRead
					&& property.GetCustomAttributes(typeof(NotPartOfSignatureAttribute), true).Length < 1);
			var hash = prime1;

			unchecked {
				foreach (var property in properties) {
					var type = property.PropertyType;

					var testValue = property.GetValue(this, null);
					if (testValue == null) {
						continue;
					}

					if (typeof(IEnumerable).IsAssignableFrom(type)) {
						var values = (IEnumerable)testValue;
						foreach (var value in values) {
							hash = hash * prime2 + value.GetHashCode();
						}
					} else {
						hash = hash * prime2 + testValue.GetHashCode();
					}
				}
			}
			return hash;
		}

		public virtual int CompareTo(TType other)
		{
			if (other == null) return 1;
			if (!Id.HasValue && other.Id.HasValue) return -1;
			if (!Id.HasValue && !other.Id.HasValue) return 0;
			return Id.Value.CompareTo(other.Id);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", GetType().Name, Id);
		}
	}
}
