namespace Guidelines.Domain.Specifications
{
    internal class OrSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _spec1;
        private readonly ISpecification<TEntity> _spec2;

        internal OrSpecification(ISpecification<TEntity> s1, ISpecification<TEntity> s2)
        {
            _spec1 = s1;
            _spec2 = s2;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return _spec1.IsSatisfiedBy(candidate) || _spec2.IsSatisfiedBy(candidate);
        }
    }
}