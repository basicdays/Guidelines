using System;
using Guidelines.Core.Commands;
using Guidelines.WebUI.ActionInvokers;
using Guidelines.WebUI.ActionResults;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;
using CommandResult = Guidelines.Core.Commands.CommandResult;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommandMessageConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(IQueryHandler<,>)))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(IQueryHandler<,>));
				var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];
				var commandMessageTypeTwo = interfaceType.GetGenericArguments()[1];

				var openCommandMethodResultType = typeof(QueryResult<,>);
				var closedCommandMethodResultType = openCommandMethodResultType.MakeGenericType(commandMessageTypeOne, commandMessageTypeTwo);

				var openActionMethodInvokerType = typeof(IActionMethodResultInvoker<>);
				var closedActionMethodInvokerType =
					openActionMethodInvokerType.MakeGenericType(closedCommandMethodResultType);

				var openDomainResultType = typeof(QueryResult<>);
				var closedDomainResultType = openDomainResultType.MakeGenericType(commandMessageTypeTwo);

				var openCommandMethodResultInvokerType = typeof(ResultInvoker<,>);
				var closedCommandMethodResultInvokerType =
					openCommandMethodResultInvokerType.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

				registry.For(closedActionMethodInvokerType).Use(closedCommandMethodResultInvokerType);

				var openResultProcessor = typeof(IResultProcessor<,>);
				var closedResultProcessor =
					openResultProcessor.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

				var openCommandMethodResultProcessorType = typeof(QueryResultProcessor<,>);
				var closedCommandMethodResultProcessorType =
					openCommandMethodResultProcessorType.MakeGenericType(commandMessageTypeOne, commandMessageTypeTwo);

				registry.For(closedResultProcessor).Use(closedCommandMethodResultProcessorType);
			}

			if (type.ImplementsInterfaceTemplate(typeof(ICommandHandler<>)))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(ICommandHandler<>));
				var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];

				var openCommandMethodResultType = typeof(CommandResult<>);
				var closedCommandMethodResultType = openCommandMethodResultType.MakeGenericType(commandMessageTypeOne);

				var openActionMethodInvokerType = typeof(IActionMethodResultInvoker<>);
				var closedActionMethodInvokerType =
					openActionMethodInvokerType.MakeGenericType(closedCommandMethodResultType);

				var closedDomainResultType = typeof(CommandResult);

				var openCommandMethodResultInvokerType = typeof(ResultInvoker<,>);
				var closedCommandMethodResultInvokerType =
					openCommandMethodResultInvokerType.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

				registry.For(closedActionMethodInvokerType).Use(closedCommandMethodResultInvokerType);

				var openResultProcessor = typeof(IResultProcessor<,>);
				var closedResultProcessor =
					openResultProcessor.MakeGenericType(closedCommandMethodResultType, closedDomainResultType);

				var openCommandMethodResultProcessorType = typeof(CommandResultProcessor<>);
				var closedCommandMethodResultProcessorType =
					openCommandMethodResultProcessorType.MakeGenericType(commandMessageTypeOne);

				registry.For(closedResultProcessor).Use(closedCommandMethodResultProcessorType);
			}
		}
	}
}