using System;
using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Analyzer.Facade.Standard;
using AnsiSoft.Calculator.Model.Interface.Facade;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Features
{
    [Binding, Scope(Feature = "Calculator")]
    public class CalculatorSteps
    {
        private IProcessor Processor { get; set; }
        private bool HasError { get; set; }
        private double Result { get; set; }

        [Given(@"I have standard processor with standart rules")]
        public void GivenIHaveStandardProcessorWithStandartRules()
        {
            var processorBuilder = StandardProcessorBuilder.CreateProcessorBuilder();
            Processor = new Processor(processorBuilder);
        }

        [When(@"I input expression (.*)")]
        public void WhenIInput(string text)
        {
            HasError = false;
            try
            {
                Result = Processor.Calculate(text);
            }
            catch (Exception)
            {
                HasError = true;
            }

        }

        [Then(@"the result is (.*) within accuracy 1e-3")]
        public void ThenTheResultIsWithinAccuracyE(double value)
        {
            Assert.That(HasError, Is.False);
            Assert.That(Result, Is.EqualTo(value).Within(1e-3));
        }

        [Then(@"the result has errors")]
        public void ThenTheResultHasErrors()
        {
            Assert.That(HasError, Is.True);
        }

    }
}
