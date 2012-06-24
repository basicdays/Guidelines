namespace Guidelines.Core.Commands.IntegerExtensions
{
	public interface IDeleteCommand<TDomain> : IDeleteCommand<TDomain, int?>
		where TDomain : IIdentifiable<int?>
	{ }
}