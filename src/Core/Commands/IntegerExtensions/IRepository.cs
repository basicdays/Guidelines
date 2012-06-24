namespace Guidelines.Core.Commands.IntegerExtensions
{
	public interface IRepository<TDomain> : IRepository<TDomain, int?>
		where TDomain : IIdentifiable<int?>
	{ }
}
