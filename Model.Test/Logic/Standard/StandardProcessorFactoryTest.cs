using System;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Logic.Standard;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Test.Logic.Standard
{
    [TestFixture]
    [Category("Standard realization")]
    class StandardProcessorFactoryTest
    {
        [Test]
        public void ProcessorBuilder_SomeLibraryFactory_NotNull()
        {
            var linkedLibrary = MockRepository.GenerateStub<ILinkedLibrary>();
            var linkedLibraryFactory = MockRepository.GenerateStub<ILinkedLibraryFactory>();
            linkedLibraryFactory.Stub(lf => lf.CreateLinkedLibrary()).Return(linkedLibrary);

            var processorFactory = new StandardProcessorFactory(linkedLibraryFactory);
            Assert.That(processorFactory.ProcessorBuilder, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessorBuilder_NullLibraryFactory_ThrowException()
        {
            new StandardProcessorFactory(null);
        }
    }
}
