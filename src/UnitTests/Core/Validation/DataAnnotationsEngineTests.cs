using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Guidelines.Core.Validation;
using Guidelines.Ioc.StructureMap.Adaptors;
using Guidelines.Mapping.AutoMapper;
using NUnit.Framework;
using StructureMap;

namespace Guidelines.UnitTests.Core.Validation
{
	[TestFixture]
	public class DataAnnotationsEngineTests
	{
		[SetUp]
		public void SetUp()
		{
			Engine = new DataAnnotationsEngine(new StructureMapServiceContainer(new Container()),
			                                   new Mapper<IEnumerable<ValidationResult>, IEnumerable<ValidationEngineMessage>>(
			                                   	TestContext.Mapper));
		}

		public DataAnnotationsEngine Engine { get; set; }

		[ValidateObject]
		public class StubClass
		{
			public StubClass()
			{
				MoarStub = new ValidatableClass();
				EvenMoarStub = new NonValidatableClass();
			}

			[Required]
			public string Name { get; set; }

			[Required]
			[StringLength(50, MinimumLength = 10)]
			public string Description { get; set; }

			[Required]
			public ValidatableClass MoarStub { get; set; }

			[Required]
			public NonValidatableClass EvenMoarStub { get; set; }
		}

		[ValidateObject]
		public class ValidatableClass
		{
			[Required]
			public string MoarName { get; set; }
		}

		public class NonValidatableClass
		{
			[Required]
			public string EvenMoarName { get; set; }
		}

		[Test]
		public void CanValidate()
		{
			var stub = new StubClass {Description = "test"};
			try {
				Engine.Validate(stub);
			} catch (ValidationEngineException ex) {
				Assert.That(ex.Errors.Count(), Is.EqualTo(3));
			}
		}
	}
}
