using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class MvcCrudCommandConvention : IRegistrationConvention
	{
		private static void RegisterQueryForDefaultCrudCommands(Type type, Type genericCommand, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(genericCommand))
			{
				MvcQueryRegistrar.BuildQueryRegistrarForCrudCommand(type, genericCommand).RegisterQuery(registry);
			}
		}

		private static void RegisterCommandForDefaultCrudCommands(Type type, Type genericCommand, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(genericCommand))
			{
				MvcCommandRegistrar.BuildCommandRegistrarForCommand(type).RegisterCommand(registry);
			}
		}

		public void Process(Type type, Registry registry)
		{
			RegisterQueryForDefaultCrudCommands(type, typeof(ICreateCommand<,>), registry);
			RegisterQueryForDefaultCrudCommands(type, typeof(IGetCommand<,>), registry);

			RegisterCommandForDefaultCrudCommands(type, typeof(IUpdateCommand<,>), registry);
			RegisterCommandForDefaultCrudCommands(type, typeof(IDeleteCommand<,>), registry);
		}
	}
}