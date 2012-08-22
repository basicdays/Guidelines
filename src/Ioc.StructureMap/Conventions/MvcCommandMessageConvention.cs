using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class MvcCommandMessageConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(IQueryHandler<,>)))
			{
				MvcQueryRegistrar.BuildQueryRegistrarForHandler(type).RegisterQuery(registry);
			}

			if (type.ImplementsInterfaceTemplate(typeof(ICommandHandler<>)))
			{
				MvcCommandRegistrar.BuildCommandRegistrarForHandler(type).RegisterCommand(registry);
			}
		}
	}
}