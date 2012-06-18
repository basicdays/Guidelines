using System;
using System.Collections.Generic;
using System.Linq;
using Guidelines.Core.Commands;
using Guidelines.Mapping.AutoMapper;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Interceptors;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class DefaultMappingUpdaterHack<TCommand, TEntity> : IUpdateCommandHandler<TCommand, TEntity>
	{
		private readonly IEnumerable<IUpdateCommandHandler<TCommand, TEntity>> _mappers;

		public DefaultMappingUpdaterHack(IEnumerable<IUpdateCommandHandler<TCommand, TEntity>> mappers)
		{
			_mappers = mappers;
		}

		public TEntity Update(TCommand command, TEntity workOn)
		{
			if(_mappers.Count() > 1) {
				return _mappers
					.Where(mapper => !(mapper is DefaultMappingUpdater<TCommand, TEntity>))
					.FirstOrDefault().Update(command, workOn);
			} else {
				return _mappers.FirstOrDefault().Update(command, workOn);
			}
		}
	}

	public class blarg : ContextEnrichmentHandler<>

	public class Interceptor : TypeInterceptor
	{
		public object Process(object target, IContext context)
		{
			context.GetAllInstances()
		}

		public bool MatchesType(Type type)
		{
			return typeof (IUpdateCommandHandler<,>).IsAssignableFrom(type);
		}
	}

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
					
				MakeMapplingLoader(domainEntityType, registry);
			}
		}

		private static void MakeMapplingLoader(Type type, Registry registry) {
			Type openMappingLoader = typeof(DefaultMappingsLoader<>);
			Type mappingLoader = openMappingLoader.MakeGenericType(type);

			registry.AddType(typeof(IDefaultMappingsLoader), mappingLoader);
		}

		public void Process(Type type, Registry registry)
		{
			registry.RegisterInterceptor();
			RegisterTypes(type, registry, typeof(IUpdateCommand<>), typeof(DefaultMappingUpdater<,>), typeof(IUpdateCommandHandler<,>));
			RegisterTypes(type, registry, typeof(ICreateCommand<>), typeof(DefaultMappingCreator<,>), typeof(ICreateCommandHandler<,>));
		}
	}
}