using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Blocks;
using AnsiSoft.Calculator.Model.Analyzer.Syntactic.Nodes;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Operators;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Rewriter;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using AnsiSoft.Calculator.Model.Interface.Transit;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class for create processor build with standard preference
    /// </summary>
    public static partial class StandardProcessorBuilder
    {
        /// <summary>
        /// Standard rules for trnslation
        /// </summary>
        /// 1. Remove brackets in function expression
        /// 2. Remove brackets in unary expression
        /// 3. Pull up product expression
        /// 4. Pull up unary expression
        /// 5. Create tuple chain
        /// 6. Create operator chain (for binary operator case)
        /// 7. Create binary operator chain
        /// 8. Translate into term nodes
        /// 9. Carry out lazy calculations
        public static IEnumerable<ISyntaxRewriter> TranslateRules { get; } =
            new ISyntaxRewriter[]
            {
                new SyntaxRewriter(
                    node => node.IsBlockOf(nameof(SyntacticRuleType.TupleIsExprAndSeparatorAndTuple)),
                    (node, children) =>
                    {
                        var blockNode = (BlockSyntacticNode) node;
                        return new BlockSyntacticNode(
                            blockNode.Block,
                            children.Take(1).Concat(children.Last().Nodes));
                    }),
                new SyntaxRewriter(
                    node => node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsLBrAndExprAndRBr)),
                    (node, children) => children.Skip(1).First()),
                new SyntaxRewriter(
                    node => node.IsBlockOf(nameof(SyntacticRuleType.ExprIsProductExpr)),
                    (node, children) => children.First()),
                new SyntaxRewriter(
                    node => node.IsBlockOf(nameof(SyntacticRuleType.ProductExprIsUnaryExpr)),
                    (node, children) => children.First()),
                new SyntaxRewriter(
                    node => node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsIdentifierAndLBrAndTupleAndRBr)),
                    (node, children) =>
                    {
                        var blockNode = (BlockSyntacticNode) node;
                        return new BlockSyntacticNode(
                            blockNode.Block,
                            children.Take(1).Concat(children.Skip(2).Take(1)));
                    }),
                new SyntaxRewriter(
                    node => node.IsBlockOf<ProductExpressionBlock>() &&
                            node.Nodes.Last().IsBlockOf<ProductExpressionBlock>(),
                    (node, children) => node.Rewrite(children.Take(2).Concat(children.Last().Nodes))),
                new SyntaxRewriter(
                    node => node.IsBlockOf<ExpressionBlock>() &&
                            node.Nodes.Last().IsBlockOf<ExpressionBlock>(),
                    (node, children) => node.Rewrite(children.Take(2).Concat(children.Last().Nodes))),
                new SyntaxRewriter(node => node is BlockSyntacticNode,
                    (node, children) =>
                    {
                        if (node.IsBlockOf<UnaryExpressionBlock>())
                        {
                            var tokenNode = (TokenSyntacticNode) children.First();
                            if (node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsIdentifier)))
                            {
                                var term = new ConstantDeclarationTerm(tokenNode.Token.Lexeme);
                                return new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());
                            }
                            if (node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsNumber)))
                            {
                                var term = new NumberTerm(
                                    double.Parse(tokenNode.Token.Lexeme, CultureInfo.InvariantCulture));
                                return new TermSyntacticNode(term, Enumerable.Empty<ISyntacticNode>());

                            }
                            if (node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsIdentifierAndLBrAndTupleAndRBr)))
                            {
                                var term = new FunctionDeclarationTerm(tokenNode.Token.Lexeme);
                                return new TermSyntacticNode(term, children.Last().Nodes);
                            }
                            if (node.IsBlockOf(nameof(SyntacticRuleType.UnaryExprIsOperatorAndUnaryExpr)))
                            {
                                var operators = new Dictionary<string, IUnaryOperator>
                                {
                                    ["+"] = new UnaryPlusOperator(),
                                    ["-"] = new UnaryMinusOperator()
                                };
                                var term = new UnaryOperatorTerm(operators[tokenNode.Token.Lexeme]);
                                return new TermSyntacticNode(term, children.Skip(1));
                            }
                        }
                        if (node.IsBlockOf<ProductExpressionBlock>() || node.IsBlockOf<ExpressionBlock>())
                        {
                            return children.ToLeftAssociationTree(
                                new Dictionary<string, IBinaryOperator>
                                {
                                    ["+"] = new PlusOperator(),
                                    ["-"] = new MinusOperator(),
                                    ["*"] = new MultiplicationOperator(),
                                    ["/"] = new DivisionOperator()
                                });
                        }
                        return node.Rewrite(children);
                    }),
                new SyntaxRewriter(
                    node => true,
                    (node, children) => node.Rewrite(children.ToList()))
            };

    }
}
