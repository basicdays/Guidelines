using Guidelines.DataAccess.Mongo;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using Guidelines.Domain.Validation;
using Guidelines.Ioc.Conventions;
using Guidelines.WebUI;
using log4net;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.IoC
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			For<ILog>().Use(context => LogManager.GetLogger(context.ParentType ?? typeof(TestRegistry)));
			For<IValidationEngine>().Use<DataAnnotationsEngine>();
			For<IIdPolicy>().Use<CombGuidIdPolicy>();

			For<IMongoCredentialProvider>().Singleton().Use<MongoCredentialProvider>();
			For<IMongoConfigProvider>().Singleton().Use<MongoConfigProvider>();
			For(typeof(IMongoCollectionProvider<>)).Use(typeof(ObjectNameCollectionProvider<>));
			For(typeof (IRepository<>)).Use(typeof (MongoRepository<>));

			For<ILocalizationProvider>().Use<LocalizationProvider>();

			Scan(scanner =>
			{
				scanner.AssemblyContainingType<IMongoConfigProvider>();
				scanner.AssemblyContainingType<ILocalizedDateConverter>();
				scanner.WithDefaultConventions();
			});

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
