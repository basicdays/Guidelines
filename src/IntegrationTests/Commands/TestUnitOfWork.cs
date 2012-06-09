using Guidelines.Core;

namespace Guidelines.IntegrationTests.Commands
{
	public class TestUnitOfWork : IUnitOfWork
	{
		public static bool Commited { get; private set; }
		public static bool RolledBack { get; private set; }

		public void Dispose() { }

		public void Begin()
		{
			Commited = false;
			RolledBack = false;
		}

		public void Commit()
		{
			Commited = true;
		}

		public void RollBack()
		{
			RolledBack = true;
		}
	}
}