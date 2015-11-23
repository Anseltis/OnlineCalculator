using System;
using AnsiSoft.Calculator.Model.Reflection.Exceptions;
using AnsiSoft.Calculator.Model.ReflectionTool;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Reflection.Test
{
    [TestFixture]
    public class RuntimeLinkedLibraryFactoryTest
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_NullSomeCode_ThrowException()
        {
            new RuntimeLinkedLibraryFactory(null, new string[] {});
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_NullAssemblies_ThrowException()
        {
            new RuntimeLinkedLibraryFactory("some code", null);
        }

        [Test]
        public void SourceCodeAndAssemblies_SomeCodeAndAssemblies_Same()
        {
            const string sourceCode = "some source code";
            var assemblies = new[] {"one", "two"};
            var factory = new RuntimeLinkedLibraryFactory(sourceCode, assemblies);
            Assert.That(factory.SourceCode, Is.EqualTo(sourceCode));
            Assert.That(factory.Assemblies, Is.SameAs(assemblies));
        }

        [Test]
        public void SourceCodeAndAssemblies_SomeCodeAndStandardAssemblies_Same()
        {
            const string sourceCode = "some source code";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            Assert.That(factory.SourceCode, Is.EqualTo(sourceCode));
            Assert.That(factory.Assemblies, Is.SameAs(RuntimeLinkedLibraryFactory.StandardAssemblies));
        }

        [Test]
        public void LazyType_SomeCodeAndtandardAssemblies_NotNull()
        {
            const string sourceCode = "some source code";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            Assert.That(factory.TypeLazy, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof (RuntimeCompileException))]
        public void LazyType_WrongCode_ThrowExceptionOnEvalLazy()
        {
            const string sourceCode = "wrong code";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            var type = factory.TypeLazy.Value;
        }

        [Test]
        [ExpectedException(typeof (NonStaticClassException))]
        public void LazyType_NoClass_ThrowExceptionOnEvalLazy()
        {
            const string sourceCode = "";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            var type = factory.TypeLazy.Value;
        }


        [Test]
        [ExpectedException(typeof (NonStaticClassException))]
        public void LazyType_NonStaticClass_ThrowExceptionOnEvalLazy()
        {
            const string sourceCode = "public class Any {}";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            var type = factory.TypeLazy.Value;
        }

        [Test]
        public void LazyType_StaticClass_DoesNotThrow()
        {
            const string sourceCode = "public static class Any {}";
            var factory = new RuntimeLinkedLibraryFactory(sourceCode);
            Assert.DoesNotThrow(() => { var type = factory.TypeLazy.Value; });
        }

    }
}
