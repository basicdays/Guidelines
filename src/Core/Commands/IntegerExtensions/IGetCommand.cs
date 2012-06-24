namespace Guidelines.Core.Commands.IntegerExtensions
{
	public interface IGetCommand<TDomain> : IGetCommand<TDomain, int?>
		where TDomain : IIdentifiable<int?>
	{ }
}