using System;
using AnsiSoft.Calculator.Model.Reflection.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Reflection.Test
{
    [TestFixture]
    [Category("Reflection")]
    public class StaticLinkedLibraryFactoryTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullType_ThrowException()
        {
            new StaticLinkedLibraryFactory(null);
        }

        [Test]
        [ExpectedException(typeof(NonStaticClassException))]
        public void Constructor_NonStaticType_ThrowException()
        {
            new StaticLinkedLibraryFactory(GetType());
        }

        [Test]
        public void TypeLazy_StaticType_LazyOfStaticType()
        {
            var factory = new StaticLinkedLibraryFactory(typeof(Math));
            Assert.That(factory.TypeLazy.Value, Is.EqualTo(typeof(Math)));
        }

        [Test]
        public void TypeLazy_StaticType_LLinkedLibraryNotNull()
        {
            var factory = new StaticLinkedLibraryFactory(typeof(Math));
            var linkedLibrary = factory.CreateLinkedLibrary();
            Assert.That(linkedLibrary, Is.Not.Null);

        }

    }
}
