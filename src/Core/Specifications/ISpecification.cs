namespace Guidelines.Core.Specifications
{
	public interface ISpecification<TEntity>
	{
		bool IsSatisfiedBy(TEntity entity);
	}
}