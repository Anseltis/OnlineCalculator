using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Exceptions;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Lexical
{
    [TestFixture]
    public class LexicalAnalyzerTest
    {
        [Test, Description("Exception if rules are null")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Countructor_NullRules_ThrowException()
        {
            new LexicalAnalyzer(null);
        }

        [Test, Description("Set rule property in constructor")]
        public void Constructor_Rules_SameCount()
        {
            var lexicalRule = MockRepository.GenerateStub<ILexicalRule>();
            lexicalRule.Stub(x => x.Pattern).Return("pattern");
            var rules = new List<ILexicalRule> {lexicalRule, lexicalRule};

            var analyzer = new LexicalAnalyzer(rules);
            Assert.That(analyzer.Rules.Count(), Is.EqualTo(rules.Count));
        }

        [Test, Description("Wrong rules")]
        [ExpectedException(typeof(WrongLexicalRuleException))]
        public void Parse_WrongRules_Throw()
        {
            const string pattern = @"^(?<e>[\s\S]*)$";
            const string text = "  a1vd bu ";

            var token = MockRepository.GenerateStub<IToken>();
            var lexicalRule = MockRepository.GenerateStub<ILexicalRule>();

            lexicalRule.Stub(r => r.Pattern).Return(pattern);
            lexicalRule.Stub(r => r.TokenFactory).Return(builder => token);

            var analyzer = new LexicalAnalyzer(new[] {lexicalRule});
            analyzer.Parse(text);
        }
    }
}
