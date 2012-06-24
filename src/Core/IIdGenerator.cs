namespace Guidelines.Core
{
	public interface IIdGenerator<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId GenerateId();
	}
}