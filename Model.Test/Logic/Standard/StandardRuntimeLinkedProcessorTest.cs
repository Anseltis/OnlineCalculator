using AnsiSoft.Calculator.Model.Logic.Standard;
using AnsiSoft.Calculator.Model.Reflection;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Test.Logic.Standard
{
    [TestFixture]
    public class StandardRuntimeLinkedProcessorTest
    {
        private string LinkedText { get; } = @"

            using System;
            using System.Linq;

            public static class LinkedMath
            {
                public static double Sin(double alpha)
                { 
				    return Math.Sin(alpha);
				}

				public static double Min(double first, params double[] args)
				{
					return args.Concat(Enumerable.Repeat(first, 1)).Min();
				}

	            public static double PI {get; private set; }
		
            	static LinkedMath()
	            {
		            PI = Math.PI;
	            }
            }        
        ";


        [Test]
        [TestCase("1-4+5",2)]
        [TestCase("Sin(PI/2)", 1)]
        [TestCase("9/3/3", 1)]
        [TestCase("Min(9/3,4)", 3)]
        public void Calculate_Expression_TargetValue(string text, double value)
        {
            var linkedLibraryFactory = new RuntimeLinkedLibraryFactory(LinkedText);
            var processorFactory = new StandardProcessorFactory(linkedLibraryFactory);
            var processor = processorFactory.CreateProcessor();
            Assert.That(processor.Calculate(text), Is.EqualTo(value).Within(1e-7));
        }
    }


}
