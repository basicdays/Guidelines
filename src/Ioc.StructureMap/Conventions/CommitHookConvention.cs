using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommitHookConvention : FacadeConvention
    {
        public override void Process(Type type, Registry registry)
        {
        	ProcessFacade(type, registry, typeof (ICommitHook), typeof (ICommitHook<>), typeof (TypedCommitHookFacade<>));
        }
    }
}