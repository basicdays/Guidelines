using System;
using Guidelines.Core.Commands;
using Guidelines.WebUI.ActionInvokers;
using Guidelines.WebUI.ActionResults;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class MvcQueryRegistrar 
	{
		public static MvcQueryRegistrar BuildQueryRegistrarForHandler(Type handler)
		{
			var interfaceType = handler.FindFirstInterfaceThatCloses(typeof(IQueryHandler<,>));
			var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];
			var commandMessageTypeTwo = interfaceType.GetGenericArguments()[1];

			return new MvcQueryRegistrar(commandMessageTypeOne, commandMessageTypeTwo);
		}

		public static MvcQueryRegistrar BuildQueryRegistrarForCrudCommand(Type query, Type genericCommand)
		{
			Type interfaceType = query.FindFirstInterfaceThatCloses(genericCommand);
			Type domainEntityType = interfaceType.GetGenericArguments()[0];

			return new MvcQueryRegistrar(query, domainEntityType);
		}

		public static MvcQueryRegistrar BuildQueryRegistrarForQuery(Type query, Type result)
		{
			return new MvcQueryRegistrar(query, result);
		}

		private MvcQueryRegistrar(Type query, Type result)
		{
			Query = query;
			Result = result;
		}

		public Type Query { get; private set; }
		public Type Result { get; private set; }

		public void RegisterQuery(Registry registry)
		{
			var openCommandMethodResultType = typeof(QueryResult<,>);
			var closedCommandMethodResultType = openCommandMethodResultType.MakeGenericType(Query, Result);

			var openActionMethodInvokerType = typeof(IActionMethodResultInvoker<>);
			var closedActionMethodInvokerType =
				openActionMethodInvokerType.MakeGenericType(closedCommandMethodResultType);

			var openDomainResultType = typeof(QueryResult<>);
			var closedDomainResultType = openDomainResultType.MakeGenericType(Result);

			var openCommandMethodResultInvokerType = typeof(ResultInvoker<,>);
			var closedCommandMethodResultInvokerType =
				openCommandMethodResultInvokerType.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

			registry.For(closedActionMethodInvokerType).Use(closedCommandMethodResultInvokerType);

			var openResultProcessor = typeof(IResultProcessor<,>);
			var closedResultProcessor =
				openResultProcessor.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

			var openCommandMethodResultProcessorType = typeof(QueryResultProcessor<,>);
			var closedCommandMethodResultProcessorType =
				openCommandMethodResultProcessorType.MakeGenericType(Query, Result);

			registry.For(closedResultProcessor).Use(closedCommandMethodResultProcessorType);
		}
	}
}