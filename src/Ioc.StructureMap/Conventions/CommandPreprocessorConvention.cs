using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommandPreprocessorConvention : FacadeConvention
	{
		public override void Process(Type type, Registry registry)
		{
			ProcessFacade(type, registry, typeof(ICommandPreprocessor), typeof(ICommandPreprocessor<>), typeof(TypedCommandPreprocessorFacade<>));
		}
	}
}