using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public abstract class HandlerRegistrar
	{
		protected HandlerRegistrar(Type classType, Registry registry)
		{
			ClassType = classType;
			Registry = registry;
		}

		public Type ClassType { get; private set; }
		public Registry Registry { get; private set; }

		protected abstract Type RegisterProcessorAndGetHandlerType(Type domainEntityType);

		public void RegisterTypes(Type genericCommand, Type handler)
		{
			if (ClassType.ImplementsInterfaceTemplate(genericCommand))
			{
				Type interfaceType = ClassType.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];
				Type idType = interfaceType.GetGenericArguments()[1];

				Type closesCommandHandlerInterface = RegisterProcessorAndGetHandlerType(domainEntityType);

				Type closesCommandHandler =
					handler.MakeGenericType(ClassType, domainEntityType, idType);

				Registry.For(closesCommandHandlerInterface).Use(closesCommandHandler);
				Registry.For(genericCommand).Use(ClassType);

				if (typeof(IRegisterMappings).IsAssignableFrom(closesCommandHandler))
				{
					Registry.For(typeof(IRegisterMappings)).Add(closesCommandHandler);
				}
			}
		}
	}
}