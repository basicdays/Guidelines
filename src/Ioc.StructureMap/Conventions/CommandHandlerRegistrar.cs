using System;
using Guidelines.Core.Commands;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Conventions
{
	public class CommandHandlerRegistrar : HandlerRegistrar
	{
		public CommandHandlerRegistrar(Type classType, Registry registry) : base(classType, registry) {}

		protected override Type RegisterProcessorAndGetHandlerType(Type domainEntityType)
		{
			var openActionProcessprInterface = typeof(ICommandProcessor<>);
			var closeActionProcessprInterface =
				openActionProcessprInterface.MakeGenericType(ClassType);

			var openActionProcessor = typeof(CommandProcessor<>);
			var closesActionProcessor =
				openActionProcessor.MakeGenericType(ClassType);

			Registry.For(closeActionProcessprInterface).Use(closesActionProcessor);

			var openCommandHandlerInterface = typeof(ICommandHandler<>);
			return openCommandHandlerInterface.MakeGenericType(ClassType);
		}
	}
}