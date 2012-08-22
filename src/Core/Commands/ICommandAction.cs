namespace Guidelines.Core.Commands
{
	public interface ICommandAction<in TCommand, TDomain>
	{
		TDomain Execute(TCommand command, TDomain entity);
	}
}