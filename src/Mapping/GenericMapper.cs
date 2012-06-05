using AutoMapper;
using Guidelines.Domain;

namespace Guidelines.AutoMapper
{
    public class GenericMapper : IGenericMapper
    {
        private readonly IMappingEngine _mappingEngine;

        public GenericMapper(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        public TDestination Map<TSource, TDestination>(TSource input)
        {
            return _mappingEngine.Map<TSource, TDestination>(input);
        }

        public TDestination Map<TSource, TDestination>(TSource input, TDestination destination)
        {
            return _mappingEngine.Map(input, destination);
        }
    }
}
