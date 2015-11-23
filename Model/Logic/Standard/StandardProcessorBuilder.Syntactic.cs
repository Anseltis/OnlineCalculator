using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using static AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes.SyntacticNodeTypeHelper;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class for create processor build with standard preference
    /// </summary>
    public static partial class StandardProcessorBuilder
    {
        /// <summary>
        /// Standard target for syntactic analyzer
        /// </summary>
        public static ISyntacticNodeType SyntacticTarget { get; } = BlockOf<ExpressionBlock>();

        /// <summary>
        /// List of standard rules for syntactic parsing
        /// </summary>
        public enum SyntacticRuleType
        {
            ExprIsProductExpr,
            ExprIsProductExprAndBOpAndExpr,
            ProductExprIsUnaryExpr,
            ProductExprIsUnaryExprAndOpAndProductExpr,
            UnaryExprIsIdentifier,
            UnaryExprIsNumber,
            UnaryExprIsLBrAndExprAndRBr,
            UnaryExprIsOperatorAndUnaryExpr,
            UnaryExprIsIdentifierAndLBrAndTupleAndRBr,
            TupleIsExpr,
            TupleIsExprAndSeparatorAndTuple,
        }

        /// <summary>
        /// Standard rules for syntactic parsing
        /// </summary>
        /// Expression = ProductExpression
        /// Expression = ProductExpression Operator Expression
        /// ProductExpression = UnaryExpression
        /// ProductExpression = UnaryExpression BinaryOperator ProductExpression
        /// UnaryExpression = Identifier
        /// UnaryExpression = Number
        /// UnaryExpression = LeftBracket Expression RightBracket
        /// UnaryExpression = Operator UnaryExpression
        /// UnaryExpression = Identifier LeftBracket Tuple RightBracket
        /// Tuple = Expression
        /// Tuple = Expression Separator Tuple
        public static IEnumerable<IBlock> SyntacticRules { get; } =
            new IBlock[]
            {
                new ExpressionBlock(
                    nameof(SyntacticRuleType.ExprIsProductExpr),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<ProductExpressionBlock>()
                    }),
                new ExpressionBlock(
                    nameof(SyntacticRuleType.ExprIsProductExprAndBOpAndExpr),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<ProductExpressionBlock>(), TokenOf<OperatorToken>(), BlockOf<ExpressionBlock>()
                    }),
                new ProductExpressionBlock(
                    nameof(SyntacticRuleType.ProductExprIsUnaryExpr),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<UnaryExpressionBlock>()
                    }),
                new ProductExpressionBlock(
                    nameof(SyntacticRuleType.ProductExprIsUnaryExprAndOpAndProductExpr),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<UnaryExpressionBlock>(), TokenOf<BinaryOperatorToken>(),
                        BlockOf<ProductExpressionBlock>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsIdentifier),
                    new ISyntacticNodeType[]
                    {
                        TokenOf<IdentifierToken>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsNumber),
                    new ISyntacticNodeType[]
                    {
                        TokenOf<NumberToken>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsLBrAndExprAndRBr),
                    new ISyntacticNodeType[]
                    {
                        TokenOf<LeftBracketToken>(), BlockOf<ExpressionBlock>(), TokenOf<RightBracketToken>(),
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsOperatorAndUnaryExpr),
                    new ISyntacticNodeType[]
                    {
                        TokenOf<OperatorToken>(), BlockOf<UnaryExpressionBlock>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsIdentifierAndLBrAndTupleAndRBr),
                    new ISyntacticNodeType[]
                    {
                        TokenOf<IdentifierToken>(),
                        TokenOf<LeftBracketToken>(), BlockOf<TupleBlock>(), TokenOf<RightBracketToken>(),
                    }),
                new TupleBlock(
                    nameof(SyntacticRuleType.TupleIsExpr),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<ExpressionBlock>()
                    }),
                new TupleBlock(
                    nameof(SyntacticRuleType.TupleIsExprAndSeparatorAndTuple),
                    new ISyntacticNodeType[]
                    {
                        BlockOf<ExpressionBlock>(), TokenOf<SeparatorToken>(), BlockOf<TupleBlock>()
                    })
            };
    }
}
