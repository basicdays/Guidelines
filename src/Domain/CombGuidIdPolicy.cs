using System;

namespace Guidelines.Domain
{
	/// <summary>
	/// Represents an Id generator for Guids using the COMB algorithm.
	/// </summary>
	/// <remarks>
	/// Taken from MongoDB CSharp Driver.
	/// Replace last 6 bytes of a new Guid with 2 bytes from days and 4 bytes from milliseconds.
	/// see: The Cost of GUIDs as Primary Keys by Jimmy Nilson
	/// at: http://www.informit.com/articles/article.aspx?p=25862&seqNum=7
	/// </remarks>
	public class CombGuidIdPolicy : IIdPolicy
	{
		/// <summary>
		/// Generates a new ID.
		/// </summary>
		/// <returns>An ID.</returns>
		public Guid GetId()
		{
			var baseDate = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var now = DateTime.UtcNow;
			var days = (ushort)(now - baseDate).TotalDays;
			var milliseconds = (int)now.TimeOfDay.TotalMilliseconds;

			var bytes = Guid.NewGuid().ToByteArray();
			Array.Copy(BitConverter.GetBytes(days), 0, bytes, 10, 2);
			Array.Copy(BitConverter.GetBytes(milliseconds), 0, bytes, 12, 4);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(bytes, 10, 2);
				Array.Reverse(bytes, 12, 4);
			}
			return new Guid(bytes);
		}

		public TEntity SetId<TEntity>(TEntity entity)
			where TEntity : EntityBase<TEntity>
		{
			entity.Id = GetId();
			return entity;
		}
	}
}
