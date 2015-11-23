﻿using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic.Standard
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
            var linkedLibraryFactory = new StaticLinkedLibraryFactory(typeof(StandardProcessorBuilder.LinkedMath));
            var processorFactory = new StandardProcessorFactory(linkedLibraryFactory);
            var processor = processorFactory.CreateProcessor();
            Assert.That(processor.Calculate(text), Is.EqualTo(value).Within(1e-7));
        }
    }
}