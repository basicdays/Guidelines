using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class QueryHandlerRegistrar : HandlerRegistrar
	{
		public QueryHandlerRegistrar(Type classType, Registry registry)
			: base(classType, registry) { }

		protected override Type RegisterProcessorAndGetHandlerType(Type domainEntityType)
		{
			var openQuerryProcessprInterface = typeof(IQueryProcessor<,>);
			var closeQuerryProcessprInterface =
				openQuerryProcessprInterface.MakeGenericType(ClassType, domainEntityType);

			var openQuerryProcessor = typeof(QueryProcessor<,>);
			var closesQuerryProcessor =
				openQuerryProcessor.MakeGenericType(ClassType, domainEntityType);

			Registry.For(closeQuerryProcessprInterface).Use(closesQuerryProcessor);

			var openCommandHandlerInterface = typeof(IQueryHandler<,>);
			return openCommandHandlerInterface.MakeGenericType(ClassType, domainEntityType);
		}
	}
}