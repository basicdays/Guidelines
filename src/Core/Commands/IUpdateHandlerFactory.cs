namespace Guidelines.Core.Commands
{
	public interface IUpdateHandlerFactory<in TCommand, TDomain> {
		IUpdateCommandHandler<TCommand, TDomain> BuildUpdater();
	}
}