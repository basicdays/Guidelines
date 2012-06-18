namespace Guidelines.Core.Commands
{
	public interface IUpdateCommandHandler<in TCommand, TDomain>
	{
		TDomain Update(TCommand command, TDomain workOn);
	}
}