namespace Guidelines.Core.Commands
{
	public interface IDeleteCommandHandler<TDomain>
	{
		TDomain Update(TDomain workOn);
	}
}