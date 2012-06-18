using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class DefaultCrudConvention : IRegistrationConvention
	{
		private static void RegisterTypes(Type type, Registry registry, Type genericCommand, Type handler, Func<Type, Type, Registry, Type> GetHandlerType)
		{
			if (type.ImplementsInterfaceTemplate(genericCommand))
			{
				Type interfaceType = type.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closesCommandHandlerInterface = GetHandlerType(type, domainEntityType, registry);

				Type closesCommandHandler =
					handler.MakeGenericType(type, domainEntityType);

				registry.For(closesCommandHandlerInterface).Use(closesCommandHandler);
				registry.For(genericCommand).Use(type);

				if (typeof(IRegisterMappings).IsAssignableFrom(closesCommandHandler))
				{
					registry.For(typeof(IRegisterMappings)).Add(closesCommandHandler);
				}
			}
		}

		private static Type RegisterCommandProcessors(Type commandType, Type returnType, Registry registry) {
			var openActionProcessprInterface = typeof(ICommandProcessor<>);
			var closeActionProcessprInterface =
				openActionProcessprInterface.MakeGenericType(commandType);

			var openActionProcessor = typeof(CommandProcessor<>);
			var closesActionProcessor =
				openActionProcessor.MakeGenericType(commandType);

			registry.For(closeActionProcessprInterface).Use(closesActionProcessor);
			
			var openCommandHandlerInterface = typeof(ICommandHandler<>);
			return openCommandHandlerInterface.MakeGenericType(commandType);
		}

		private static Type RegisterQueryProcessors(Type commandType, Type returnType, Registry registry) {
			var openQuerryProcessprInterface = typeof(IQueryProcessor<,>);
			var closeQuerryProcessprInterface =
				openQuerryProcessprInterface.MakeGenericType(commandType, returnType);

			var openQuerryProcessor = typeof(QueryProcessor<,>);
			var closesQuerryProcessor =
				openQuerryProcessor.MakeGenericType(commandType, returnType);

			registry.For(closeQuerryProcessprInterface).Use(closesQuerryProcessor);

			var openCommandHandlerInterface = typeof(IQueryHandler<,>);
			return openCommandHandlerInterface.MakeGenericType(commandType, returnType);
		}

		private static void RegisterFactory(Type type, Registry registry, Type genericCommand, Type factoryInterface, Type factoryInstace)
		{
			if (type.ImplementsInterfaceTemplate(genericCommand))
			{
				Type interfaceType = type.FindFirstInterfaceThatCloses(genericCommand);
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closedFactoryInterface = factoryInterface.MakeGenericType(type, domainEntityType);
				Type closedFactoryType = factoryInstace.MakeGenericType(type, domainEntityType);

				registry.For(closedFactoryInterface).Use(closedFactoryType);
			}
		}

		public void Process(Type type, Registry registry)
		{
			RegisterTypes(type, registry, typeof(IUpdateCommand<>), typeof(UpdateCommandHandler<,>), RegisterCommandProcessors);
			RegisterTypes(type, registry, typeof(IDeleteCommand<>), typeof(DeleteCommandHandler<,>), RegisterCommandProcessors);

			RegisterTypes(type, registry, typeof(ICreateCommand<>), typeof(CreateCommandHandler<,>), RegisterQueryProcessors);
			RegisterTypes(type, registry, typeof(IGetCommand<>), typeof(GetCommandHandler<,>), RegisterQueryProcessors);

			RegisterFactory(type, registry, typeof(IUpdateCommand<>), typeof(IUpdateHandlerFactory<,>), typeof(UpdateHandlerFactory<,>));
			RegisterFactory(type, registry, typeof(ICreateCommand<>), typeof(ICreateHandlerFactory<,>), typeof(CreateHandlerFactory<,>));
		}
	}
}