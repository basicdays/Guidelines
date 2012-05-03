using AutoMapper;
using Guidelines.Domain;

namespace Guidelines.AutoMapper
{
    public class Mapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        private readonly IMappingEngine _mappingEngine;

        public Mapper(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        public TDestination Map(TSource input)
        {
            return _mappingEngine.Map<TSource, TDestination>(input);
        }

        public TDestination Map(TSource input, TDestination destination)
        {
            return _mappingEngine.Map(input, destination);
        }
    }
}
