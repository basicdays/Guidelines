﻿namespace Guidelines.Core
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource input);
        TDestination Map(TSource input, TDestination destination);
    }
}
