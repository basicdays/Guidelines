using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommandPreprocessorConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(ICommandPreprocessor<>)))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(ICommandPreprocessor<>));
				var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];

				var openQuerryProcessor = typeof(TypedCommandPreprocessorFacade<>);
				var closesQuerryProcessor =
					openQuerryProcessor.MakeGenericType(commandMessageTypeOne);

				registry.AddType(closesQuerryProcessor);
				registry.AddType(interfaceType, type);
			}
			if (typeof(ICommandPreprocessor).IsAssignableFrom(type) && !type.IsInterface) {
				registry.AddType(typeof(ICommandPreprocessor), type);
			}
		}
	}
}