namespace Guidelines.Domain
{
	public interface IPermision<in TDomain>
	{
		bool CanWorkOn(TDomain entity);
	}
}