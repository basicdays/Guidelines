using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guidelines.Core;
using NUnit.Framework;

namespace Guidelines.UnitTests.Core
{
	[TestFixture]
	public class ConventionalEquatableTests
	{
		#region TestStubs
		// ReSharper disable UnusedMember.Local, UnusedAutoPropertyAccessor.Local, MemberCanBePrivate.Local, NotAccessedField.Local

		private class SectionA : EntityBase<SectionA>
		{
			public SectionA()
			{
				Stuff1 = 10;
				_stuff2 = 20;
				_stuff3 = 30;
				Stuff4 = new List<SectionB> { new SectionB(40), new SectionB(50) };
				Stuff5 = 60;
			}

			public int Stuff1 { get; set; }

			private readonly int _stuff2;
			public int Stuff2 { get { return _stuff2; } }

			private int _stuff3;
			public int Stuff3 { set { _stuff3 = value; } }

			public IEnumerable<SectionB> Stuff4 { get; set; }

			[NotPartOfSignature]
			public int Stuff5 { get; set; }

			public int? Stuff6 { get; set; }
		}

		private class SectionB : EntityBase<SectionB>
		{
			public SectionB(int value)
			{
				Things1 = value;
			}

			public int Things1 { get; set; }
		}

		// ReSharper restore UnusedMember.Local, UnusedAutoPropertyAccessor.Local, MemberCanBePrivate.Local, NotAccessedField.Local
		#endregion

		[Test]
		public void ObjectsWithSameValuesAreEqual()
		{
			Assert.That(new SectionA(), Is.EqualTo(new SectionA()));
		}

		[Test]
		public void ObjectsWithSameSignatureValuesAndDifferentNonSignatureValuesAreEqual()
		{
			var a = new SectionA();
			var b = new SectionA { Stuff5 = 500 };
			Assert.That(b, Is.EqualTo(a));
		}

		[Test]
		public void ObjectsWithDifferentWriteOnlyValuesAreEqual()
		{
			var a = new SectionA();
			var b = new SectionA { Stuff3 = 500 };
			Assert.That(b, Is.EqualTo(a));
		}

		[Test]
		public void ObjectsWithDifferentPropertyValueAreNotEqual()
		{
			var a = new SectionA();
			var b = new SectionA { Stuff1 = 500 };
			Assert.That(b, Is.Not.EqualTo(a));
		}

		[Test]
		public void ObjectsWithSameBaseValuesAndDifferentEnumeratedValuesAreNotEqual()
		{
			var a = new SectionA();
			var b = new SectionA
			{
				Stuff4 = new List<SectionB> { new SectionB(500), new SectionB(600) }
			};
			Assert.That(b, Is.Not.EqualTo(a));
		}

		[Test]
		public void NullObjectIsNotEqual()
		{
			var a = new SectionA();
			Assert.That(a, Is.Not.EqualTo(null));
		}
	}
}
