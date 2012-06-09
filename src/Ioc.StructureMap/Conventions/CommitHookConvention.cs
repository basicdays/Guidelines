using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Guidelines.Ioc.StructureMap.Conventions
{
    public class CommitHookConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (type.ImplementsInterfaceTemplate(typeof(ICommitHook<>)))
            {
				var interfaceType = type.FindFirstInterfaceThatCloses(typeof(ICommitHook<>));
                var commandMessageTypeOne = interfaceType.GetGenericArguments()[0];

                var openQuerryProcessor = typeof(TypedCommitHookFacade<>);
                var closesQuerryProcessor =
                    openQuerryProcessor.MakeGenericType(commandMessageTypeOne);

                registry.AddType(closesQuerryProcessor);
                registry.AddType(interfaceType, type);
            }
            if (typeof(ICommitHook).IsAssignableFrom(type) && !type.IsInterface)
            {
                registry.AddType(typeof(ICommitHook), type);
            }
        }
    }
}