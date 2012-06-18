namespace Guidelines.Core.Commands
{
	public interface ICreateCommandHandler<in TCommand, out TDomain>
	{
		TDomain Create(TCommand command);
	}
}