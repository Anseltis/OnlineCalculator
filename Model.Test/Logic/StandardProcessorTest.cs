using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Logic;
using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic
{
    [TestFixture]
    public class StandardProcessorTest
    {
        [Test]
        [TestCase("1-4+5",2)]
        [TestCase("Sin(PI/2)", 1)]
        [TestCase("9/3/3", 1)]
        public void Calculate_Expression_TargetValue(string text, double value)
        {
            var linkedLibrary = new LinkedLibrary(typeof(StandardProcessorBuilder.LinkedMath));
            var processorBuilder = StandardProcessorBuilder.CreateProcessorBuilder(linkedLibrary);
            var processor = new Processor(processorBuilder);
            Assert.That(processor.Calculate(text), Is.EqualTo(value).Within(1e-7));
        }
    }
}
