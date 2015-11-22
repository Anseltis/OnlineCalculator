using System;
using AnsiSoft.Calculator.Model.Analyzer.Facade.Standard;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic;
using AnsiSoft.Calculator.Model.Interface.Facade;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Features
{
    [Binding, Scope(Feature = "SyntacticValidation")]
    public class SyntacticValidationSteps
    {
        private ILexicalAnalyzer LexicalAnalyzer { get; set; }
        private ISyntacticAnalyzer SyntacticAnalyzer { get; set; }
        private bool HasError { get; set; }

        [Given(@"I have standard lexical and syntactic analyzers")]
        public void GivenIHaveStandardLexicalAnSyntacticAnalyzers()
        {
            LexicalAnalyzer = new LexicalAnalyzer(StandardProcessorBuilder.LexicalRules);
            SyntacticAnalyzer = new SyntacticAnalyzer(StandardProcessorBuilder.SyntacticRules);

        }

        [When(@"I input expression (.*)")]
        public void WhenIInputExpressionFunc(string text)
        {
            HasError = false;
            try
            {
                var tokens = LexicalAnalyzer.Parse(text);
                SyntacticAnalyzer.Parse(tokens, StandardProcessorBuilder.SyntacticTarget);
            }
            catch (Exception)
            {
                HasError = true;
            }

        }

        [Then(@"the result hasn't errors")]
        public void ThenTheResultHasnTErrors()
        {
            Assert.That(HasError, Is.False);
        }

        [Then(@"the result has errors")]
        public void ThenTheResultHasErrors()
        {
            Assert.That(HasError, Is.True);
        }

    }
}
