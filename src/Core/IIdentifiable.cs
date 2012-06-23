namespace Guidelines.Core
{
	public interface IIdentifiable<TId>
	{
		TId Id { get; set; }
	}
}