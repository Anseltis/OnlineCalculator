using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Lexical;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class for create processor build with standard preference
    /// </summary>
    public static partial class StandardProcessorBuilder
    {
        /// <summary>
        /// Standard rules for lexical parsing
        /// </summary>
        /// Identifier = /[_a-zA-Z][_a-zA-Z0-9]{0,30}/
        /// Number = /[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?/
        /// LeftBracket = /[\(]/
        /// RightBracket = /[\)]/
        /// Separator = /[,]/
        /// Operator = /[+-]/
        /// BinaryOperator = /[\*/]/
        public static IEnumerable<ILexicalRule> LexicalRules { get; } =
            new[]
            {
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[_a-zA-Z][_a-zA-Z0-9]{0,30})((?<e>([^_a-zA-Z0-9\s]|\s+\S)[\s\S]*$)|(?<r>\s*$))",
                    builder => new IdentifierToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?)((?<e>([^0-9]|\s*\S)[\s\S]*$)|(?<r>\s*$))",
                    builder => new NumberToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[\(])((?<e>[\s\S]*\S[\s\S]*$)|(?<r>\s*$))",
                    builder => new LeftBracketToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[\)])((?<e>[\s\S]*\S[\s\S]*$)|(?<r>\s*$))",
                    builder => new RightBracketToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[,])((?<e>[\s\S]*\S[\s\S]*$)|(?<r>\s*$))",
                    builder => new SeparatorToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[+-])((?<e>[\s\S]*\S[\s\S]*$)|(?<r>\s*$))",
                    builder => new OperatorToken(builder)),
                new LexicalRule(
                    @"^(?<l>\s*)(?<t>[\*/])((?<e>[\s\S]*\S[\s\S]*$)|(?<r>\s*$))",
                    builder => new BinaryOperatorToken(builder))
            };
    }
}
