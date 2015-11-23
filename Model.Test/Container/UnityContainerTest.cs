using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Container
{
    [TestFixture]
    public class UnityContainerTest
    {
        private class Calculator
        {
            private IProcessor Processor { get; }
            public double Calculate(string text) => Processor.Calculate(text);
            public Calculator(IProcessorFactory processorFactory)
            {
                Processor = processorFactory.CreateProcessor();
            }
        }

        [Test]
        [TestCase("1-4+5", 2)]
        [TestCase("Sin(PI/2)", 1)]
        [TestCase("9/3/3", 1)]
        public void UnityContainer_SingletonRegistration_RightResult(string text, double value)
        {
            var container = new UnityContainer()
                .RegisterInstance<ILinkedLibraryFactory>(
                    new StaticLinkedLibraryFactory(typeof (StandardProcessorBuilder.LinkedMath)))
                .RegisterType<IProcessorFactory, StandardProcessorFactory>();

            var calculator = container.Resolve<Calculator>();
            Assert.That(calculator.Calculate(text), Is.EqualTo(value).Within(1e-7));
        }

        [Test]
        [TestCase("1-4+5", 2)]
        [TestCase("Sin(PI/2)", 1)]
        [TestCase("9/3/3", 1)]
        public void UnityContainer_FactoryRegistration_RightResult(string text, double value)
        {
            var container = new UnityContainer()
                .RegisterType<ILinkedLibraryFactory>(
                    new InjectionFactory(
                        c => new StaticLinkedLibraryFactory(typeof(StandardProcessorBuilder.LinkedMath))))
                .RegisterType<IProcessorFactory, StandardProcessorFactory>();

            var calculator = container.Resolve<Calculator>();
            Assert.That(calculator.Calculate(text), Is.EqualTo(value).Within(1e-7));
        }

    }
}
