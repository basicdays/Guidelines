using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class DefaultCrudConvention : IRegistrationConvention
	{
		private static void RegisterFactories(Type type, Registry registry)
		{
			var factoryRegistrar = new HandlerFactoryRegistrar(type, registry);
			factoryRegistrar.RegisterFactory(typeof(IUpdateCommand<,>), typeof(IUpdateHandlerFactory<,>), typeof(UpdateHandlerFactory<,>));
			factoryRegistrar.RegisterFactory(typeof(ICreateCommand<,>), typeof(ICreateHandlerFactory<,>), typeof(CreateHandlerFactory<,>));
		}

		private static void RegisterQueries(Type type, Registry registry)
		{
			var queryHandlerRegistrar = new QueryHandlerRegistrar(type, registry);
			queryHandlerRegistrar.RegisterTypes(typeof(ICreateCommand<,>), typeof(CreateCommandHandler<,,>));
			queryHandlerRegistrar.RegisterTypes(typeof(IGetCommand<,>), typeof(GetCommandHandler<,,>));
		}

		private static void RegisterCommands(Type type, Registry registry)
		{
			var commandHandlerRegistrarm = new CommandHandlerRegistrar(type, registry);
			commandHandlerRegistrarm.RegisterTypes(typeof(IUpdateCommand<,>), typeof(UpdateCommandHandler<,,>));
			commandHandlerRegistrarm.RegisterTypes(typeof(IDeleteCommand<,>), typeof(DeleteCommandHandler<,,>));
		}

		public void Process(Type type, Registry registry)
		{
			RegisterCommands(type, registry);
			RegisterQueries(type, registry);
			RegisterFactories(type, registry);
		}
	}
}