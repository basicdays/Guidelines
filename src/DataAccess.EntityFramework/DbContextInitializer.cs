using Guidelines.Core.Bootstrap;

namespace Guidelines.DataAccess.EntityFramework
{
	public class DbContextInitializer : IBootstrapTask
	{
		private readonly IEntityFrameworkConfiguration _initializerBuilder;

		public DbContextInitializer(IEntityFrameworkConfiguration initializerBuilder)
		{
			_initializerBuilder = initializerBuilder;
		}

		public void Bootstrap()
		{
			_initializerBuilder.InitializeDatabase();
		}

		public int Order
		{
			get { return 0; }
		}
	}
}
