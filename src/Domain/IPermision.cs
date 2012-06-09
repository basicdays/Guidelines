namespace Guidelines.Core
{
	public interface IPermision<in TDomain>
	{
		bool CanWorkOn(TDomain entity);
	}
}