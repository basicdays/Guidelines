using System;
using Guidelines.Core;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Guidelines.Core.Commands.GuidExtensions;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class GuidConvention : IRegistrationConvention
	{
		public void Process(Type type, Registry registry)
		{
			if (type.ImplementsInterfaceTemplate(typeof(ICreateCommand<>)))
			{
				Type interfaceType = type.FindFirstInterfaceThatCloses(typeof(ICreateCommand<>));
				Type domainEntityType = interfaceType.GetGenericArguments()[0];

				var openIdGeneratorType = typeof(IIdGenerator<,>);
				var closedIdGeneratorType =
					openIdGeneratorType.MakeGenericType(domainEntityType, typeof(Guid?));

				var openGuidGenerator = typeof(GuidIdGenerator<>);
				var closedGuidIdGenerator = openGuidGenerator.MakeGenericType(domainEntityType);

				registry.For(closedIdGeneratorType).Use(closedGuidIdGenerator);
			}
		}
	}
}