﻿using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Lexical.Tokens
{
    [TestFixture]
    [Category("Lexical analyzer")]
    public class LeftBracketTokenTest
    {
        [Test, Description("Check error under constructor")]
        public void Constructor_Do_DoesNotThrow()
        {
            var builder = new TokenBuilder();
            Assert.DoesNotThrow(() => new LeftBracketToken(builder));
        }

    }
}
