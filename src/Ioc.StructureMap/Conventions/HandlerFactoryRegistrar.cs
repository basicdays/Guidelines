using System;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class HandlerFactoryRegistrar
	{
		public HandlerFactoryRegistrar(Type classType, Registry registry)
		{
			ClassType = classType;
			Registry = registry;
		}

		public Type ClassType { get; private set; }
		public Registry Registry { get; private set; }

		public void RegisterFactory(Type genericCommand, Type factoryInterface, Type factoryInstace)
		{
			if (ClassType.ImplementsInterfaceTemplate(genericCommand))
			{
				Type interfaceType = ClassType.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closedFactoryInterface = factoryInterface.MakeGenericType(ClassType, domainEntityType);
				Type closedFactoryType = factoryInstace.MakeGenericType(ClassType, domainEntityType);

				Registry.For(closedFactoryInterface).Use(closedFactoryType);
			}
		}
	}
}