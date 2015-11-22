using System.Collections.Generic;
using AnsiSoft.Calculator.Model.Analyzer.Lexical.Tokens;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.NodeTypes;
using AnsiSoft.Calculator.Model.Interface.Nodes;

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
        public static ISyntacticNodeType SyntacticTarget { get; } = SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>();

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
                        SyntacticNodeTypeHelper.BlockOf<ProductExpressionBlock>()
                    }),
                new ExpressionBlock(
                    nameof(SyntacticRuleType.ExprIsProductExprAndBOpAndExpr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.BlockOf<ProductExpressionBlock>(), SyntacticNodeTypeHelper.TokenOf<OperatorToken>(), SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>()
                    }),
                new ProductExpressionBlock(
                    nameof(SyntacticRuleType.ProductExprIsUnaryExpr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.BlockOf<UnaryExpressionBlock>()
                    }),
                new ProductExpressionBlock(
                    nameof(SyntacticRuleType.ProductExprIsUnaryExprAndOpAndProductExpr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.BlockOf<UnaryExpressionBlock>(), SyntacticNodeTypeHelper.TokenOf<BinaryOperatorToken>(),
                        SyntacticNodeTypeHelper.BlockOf<ProductExpressionBlock>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsIdentifier),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.TokenOf<IdentifierToken>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsNumber),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.TokenOf<NumberToken>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsLBrAndExprAndRBr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.TokenOf<LeftBracketToken>(), SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>(), SyntacticNodeTypeHelper.TokenOf<RightBracketToken>(),
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsOperatorAndUnaryExpr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.TokenOf<OperatorToken>(), SyntacticNodeTypeHelper.BlockOf<UnaryExpressionBlock>()
                    }),
                new UnaryExpressionBlock(
                    nameof(SyntacticRuleType.UnaryExprIsIdentifierAndLBrAndTupleAndRBr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.TokenOf<IdentifierToken>(),
                        SyntacticNodeTypeHelper.TokenOf<LeftBracketToken>(), SyntacticNodeTypeHelper.BlockOf<TupleBlock>(), SyntacticNodeTypeHelper.TokenOf<RightBracketToken>(),
                    }),
                new TupleBlock(
                    nameof(SyntacticRuleType.TupleIsExpr),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>()
                    }),
                new TupleBlock(
                    nameof(SyntacticRuleType.TupleIsExprAndSeparatorAndTuple),
                    new ISyntacticNodeType[]
                    {
                        SyntacticNodeTypeHelper.BlockOf<ExpressionBlock>(), SyntacticNodeTypeHelper.TokenOf<SeparatorToken>(), SyntacticNodeTypeHelper.BlockOf<TupleBlock>()
                    })
            };
    }
}
