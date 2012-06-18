using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommandProcessorConvention : IRegistrationConvention
	{
		public void ProcessHandler(Type type, Registry registry, Type typeInterface, Type handlerInterface, Type concreteHandler)
		{
			if (type.ImplementsInterfaceTemplate(typeInterface))
			{
				Type interfaceType = type.FindFirstInterfaceThatCloses(typeInterface);
				Type[] genericArguments = interfaceType.GetGenericArguments();

				Type closeActionProcessprInterface =
					handlerInterface.MakeGenericType(genericArguments);

				Type closesActionProcessor =
					concreteHandler.MakeGenericType(genericArguments);

				registry.For(closeActionProcessprInterface).Use(closesActionProcessor);
				registry.AddType(interfaceType, type);
			}
		}

		public void Process(Type type, Registry registry)
		{
			ProcessHandler(type, registry, typeof (ICommandHandler<>), typeof (ICommandProcessor<>), typeof (CommandProcessor<>));
			ProcessHandler(type, registry, typeof (IQueryHandler<,>), typeof (IQueryProcessor<,>), typeof (QueryProcessor<,>));
		}
	}
}