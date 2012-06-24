namespace Guidelines.Core.Commands.IntegerExtensions
{
	public interface IUpdateCommand<TDomain> : IUpdateCommand<TDomain, int?>
		where TDomain : IIdentifiable<int?>
	{ }
}