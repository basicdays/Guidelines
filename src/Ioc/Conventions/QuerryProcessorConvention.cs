using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class QuerryProcessorConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(IQueryHandler<,>)))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(IQueryHandler<,>));
				var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];
				var commandMessageTypeTwo = interfaceType.GetGenericArguments()[1];

				var openQuerryProcessprInterface = typeof(IQueryProcessor<,>);
				var closeQuerryProcessprInterface =
					openQuerryProcessprInterface.MakeGenericType(commandMessageTypeOne, commandMessageTypeTwo);

				var openQuerryProcessor = typeof(QueryProcessor<,>);
				var closesQuerryProcessor =
					openQuerryProcessor.MakeGenericType(commandMessageTypeOne, commandMessageTypeTwo);

				registry.For(closeQuerryProcessprInterface).Use(closesQuerryProcessor);
				registry.AddType(interfaceType, type);
			}
		}
	}
}