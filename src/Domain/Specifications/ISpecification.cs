namespace Guidelines.Domain.Specifications
{
	public interface ISpecification<TEntity>
	{
		bool IsSatisfiedBy(TEntity entity);
	}
}