namespace Guidelines.Core.Commands.IntegerExtensions
{
	public interface ICreateCommand<TDomain> : ICreateCommand<TDomain, int?>
		where TDomain : IIdentifiable<int?>
	{ }
}