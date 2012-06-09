using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class ActionProcessorConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(ICommandHandler<>)))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(ICommandHandler<>));
				var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];

				var openActionProcessprInterface = typeof(ICommandProcessor<>);
				var closeActionProcessprInterface =
					openActionProcessprInterface.MakeGenericType(commandMessageTypeOne);

				var openActionProcessor = typeof(CommandProcessor<>);
				var closesActionProcessor =
					openActionProcessor.MakeGenericType(commandMessageTypeOne);

				registry.For(closeActionProcessprInterface).Use(closesActionProcessor);
				registry.AddType(interfaceType, type);
			}
		}
	}
}