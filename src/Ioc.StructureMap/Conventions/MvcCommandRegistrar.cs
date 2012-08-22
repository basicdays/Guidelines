using System;
using Guidelines.Core.Commands;
using Guidelines.WebUI.ActionInvokers;
using Guidelines.WebUI.ActionResults;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;
using CommandResult = Guidelines.Core.Commands.CommandResult;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class MvcCommandRegistrar
	{
		public static MvcCommandRegistrar BuildCommandRegistrarForHandler(Type handler)
		{
			var interfaceType = handler.FindFirstInterfaceThatCloses(typeof(ICommandHandler<>));
			var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];

			return new MvcCommandRegistrar(commandMessageTypeOne);
		}

		public static MvcCommandRegistrar BuildCommandRegistrarForCommand(Type command)
		{
			return new MvcCommandRegistrar(command);
		}

		private  MvcCommandRegistrar(Type commandType)
		{
			CommandType = commandType;
		}

		public Type CommandType { get; private set; }

		public void RegisterCommand(Registry registry)
		{
			var openCommandMethodResultType = typeof(CommandResult<>);
			var closedCommandMethodResultType = openCommandMethodResultType.MakeGenericType(CommandType);

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
				openCommandMethodResultProcessorType.MakeGenericType(CommandType);

			registry.For(closedResultProcessor).Use(closedCommandMethodResultProcessorType);
		}
	}
}