using AutoMapper;
using Guidelines.Core;
using Guidelines.Core.Commands;
using Guidelines.Core.Validation;
using Guidelines.Ioc.StructureMap.Conventions;
using Guidelines.WebUI;
using log4net;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.Commands
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			For<ILog>().Use(context => LogManager.GetLogger(context.ParentType ?? typeof(TestRegistry)));
			For<IValidationEngine>().Use<DataAnnotationsEngine>();
			For<IIdPolicy>().Use<CombGuidIdPolicy>();

			For(typeof (IRepository<,>)).Use(typeof (MemoryRepository<,>));
			For<IUnitOfWork>().Use<TestUnitOfWork>();

			For<ILocalizationProvider>().Use<LocalizationProvider>();
			For<ICommandMessageProcessor>().Use<CommandMessageProcessor>();

			Scan(scanner =>
			{
				scanner.AssemblyContainingType<TestRegistry>();
				scanner.Convention<DefaultMapperConvention>();
				scanner.Convention<CommandPreprocessorConvention>();
				scanner.Convention<CommitHookConvention>();
				scanner.Convention<DefaultCrudConvention>();
				scanner.Convention<GuidIdGeneratorConvention>();
				scanner.Convention<CommandProcessorConvention>();
				scanner.AddAllTypesOf<Profile>();
				scanner.ConnectImplementationsToTypesClosing(typeof (ICommandPermision<,>));
				scanner.ConnectImplementationsToTypesClosing(typeof (IPermision<>));
				scanner.ConnectImplementationsToTypesClosing(typeof (IUpdateCommandHandler<,>));
				scanner.ConnectImplementationsToTypesClosing(typeof (ICreateCommandHandler<,>));
				scanner.WithDefaultConventions();
			});
		}
	}
}
