using System;
using Guidelines.Core.Commands;
using Guidelines.Mapping.AutoMapper;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{ 
	public class DefaultMapperConvention : IRegistrationConvention
	{
		private static void RegisterTypes(Type type, Registry registry, Type genericCommand, Type handler, Type handlerInterface)
		{
			if (type.ImplementsInterfaceTemplate(genericCommand))
			{
				Type interfaceType = type.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closesCommandHandlerInterface = handlerInterface.MakeGenericType(type, domainEntityType);
				Type closesCommandHandler = handler.MakeGenericType(type, domainEntityType);

				registry.For(closesCommandHandlerInterface)
					.Add(closesCommandHandler);
				
				MakeMapplingLoader(domainEntityType, registry);
			}
		}

		private static void MakeMapplingLoader(Type type, Registry registry) {
			Type openMappingLoader = typeof(DefaultMappingsLoader<>);
			Type mappingLoader = openMappingLoader.MakeGenericType(type);

			registry.AddType(typeof(IDefaultMappingsLoader), mappingLoader);
		}


		private static void RegisterFactory(Type type, Registry registry, Type genericCommand, Type factoryInterface, Type factoryInstace)
		{
			if(type.ImplementsInterfaceTemplate(genericCommand)) {
				Type interfaceType = type.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closedFactoryInterface = factoryInterface.MakeGenericType(type, domainEntityType);
				Type closedFactoryType = factoryInstace.MakeGenericType(type, domainEntityType);

				registry.For(closedFactoryInterface).Use(closedFactoryType);
			}
		}

		public void Process(Type type, Registry registry)
		{
			RegisterTypes(type, registry, typeof(IUpdateCommand<>), typeof(DefaultMappingUpdater<,>), typeof(IUpdateCommandHandler<,>));
			RegisterTypes(type, registry, typeof(ICreateCommand<>), typeof(DefaultMappingCreator<,>), typeof(ICreateCommandHandler<,>));

			RegisterFactory(type, registry, typeof(IUpdateCommand<>), typeof(IUpdateHandlerFactory<,>), typeof(UpdateHandlerFactory<,>));
			RegisterFactory(type, registry, typeof(ICreateCommand<>), typeof(ICreateHandlerFactory<,>), typeof(CreateHandlerFactory<,>));
		}
	}
}