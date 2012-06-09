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
				var interfaceType = type.FindFirstInterfaceThatCloses(genericCommand);
				var domainEntityType = interfaceType.GetGenericArguments()[0];

				Type closesCommandHandlerInterface = GetHandlerType(type, domainEntityType, registry);

				var closesCommandHandler =
					handler.MakeGenericType(type, domainEntityType);

				registry.For(closesCommandHandlerInterface).Use(closesCommandHandler);
				registry.For(genericCommand).Use(type);
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

		public void Process(Type type, Registry registry)
		{
			RegisterTypes(type, registry, typeof(IUpdateCommand<>), typeof(UpdateCommandHandler<,>), RegisterCommandProcessors);
			RegisterTypes(type, registry, typeof(IDeleteCommand<>), typeof(DeleteCommandHandler<,>), RegisterCommandProcessors);

			RegisterTypes(type, registry, typeof(ICreateCommand<>), typeof(CreateCommandHandler<,>), RegisterQueryProcessors);
			RegisterTypes(type, registry, typeof(IGetCommand<>), typeof(GetCommandHandler<,>), RegisterQueryProcessors);
		}
	}
}