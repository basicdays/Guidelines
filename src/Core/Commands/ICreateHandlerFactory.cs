namespace Guidelines.Core.Commands
{
	public interface ICreateHandlerFactory<in TCommand, out TDomain>
	{
		ICreateCommandHandler<TCommand, TDomain> BuildCreator();
	}
}