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

			For(typeof (IRepository<>)).Use(typeof (MemoryRepository<>));
			For<IUnitOfWork>().Use<TestUnitOfWork>();

			For<ILocalizationProvider>().Use<LocalizationProvider>();
			For<ICommandMessageProcessor>().Use<CommandMessageProcessor>();

			Scan(scanner =>
			{
				scanner.AssemblyContainingType<TestRegistry>();
				scanner.Convention<CommandPreprocessorConvention>();
				scanner.Convention<DefaultCrudConvention>();
				scanner.Convention<DefaultMapperConvention>();
				scanner.Convention<QuerryProcessorConvention>();
				scanner.Convention<ActionProcessorConvention>();
				scanner.WithDefaultConventions();
			});
		}
	}
}
