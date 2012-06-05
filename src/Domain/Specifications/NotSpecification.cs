namespace Guidelines.Domain.Specifications
{
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _wrapped;

        internal NotSpecification(ISpecification<TEntity> x)
        {
            _wrapped = x;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return !_wrapped.IsSatisfiedBy(candidate);
        }
    }
}