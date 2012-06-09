using System;
using Guidelines.AutoMapper;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.IntegrationTests.IoC
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

				registry.For(closesCommandHandlerInterface).Use(closesCommandHandler);
			}
		}

		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(EntityBase<>)))
			{
				var openMappingLoader = typeof(DefaultMappingsLoader<>);
				var mappingLoader =
					openMappingLoader.MakeGenericType(type);

				registry.AddType(typeof(IDefaultMappingsLoader), mappingLoader);
			}

			RegisterTypes(type, registry, typeof(IUpdateCommand<>), typeof(DefaultMappingUpdater<,>), typeof(IUpdateCommandHandler<,>));
			RegisterTypes(type, registry, typeof(ICreateCommand<>), typeof(DefaultMappingCreator<,>), typeof(ICreateCommandHandler<,>));
		}
	}
}