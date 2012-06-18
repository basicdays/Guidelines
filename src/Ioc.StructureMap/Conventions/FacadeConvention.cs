using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public abstract class FacadeConvention : IRegistrationConvention
	{
		public void ProcessFacade(Type type, Registry registry, Type facadeInterface, Type genericFacadeInterface, Type concreteFacade)
		{
			if (type.ImplementsInterfaceTemplate(genericFacadeInterface))
			{
				var interfaceType = type.FindFirstInterfaceThatCloses(genericFacadeInterface);
				var commandMessageTypes = interfaceType.GetGenericArguments();

				var closesQuerryProcessor =
					concreteFacade.MakeGenericType(commandMessageTypes);

				registry.AddType(closesQuerryProcessor);
				registry.AddType(interfaceType, type);
			}
			if (facadeInterface.IsAssignableFrom(type) && !type.IsInterface)
			{
				registry.AddType(facadeInterface, type);
			}
		}

		public abstract void Process(Type type, Registry registry);
	}
}