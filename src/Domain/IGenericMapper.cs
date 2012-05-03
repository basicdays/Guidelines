namespace Guidelines.Domain
{
    public interface IGenericMapper
    {
        TDestination Map<TSource, TDestination>(TSource input);
        TDestination Map<TSource, TDestination>(TSource input, TDestination destination);
    }
}
