using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class CommandActionTests : BasicCreateCommandTests
	{
		protected static bool ActionRan { get; set; }

		public class PostCreateAction : ICommandAction<Create, TestEntity>
		{
			public TestEntity Execute(Create command, TestEntity entity)
			{
				ActionRan = true;

				return entity;
			}
		}

		public override void SetupTestContext()
		{
			ActionRan = false;

			base.SetupTestContext();
		}

		[Test]
		public void PostCreateActionWasExecuted()
		{
			Assert.That(ActionRan, Is.EqualTo(true));
		}
	}
}