using System;
using System.Collections.Generic;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Resolvers;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Logic.Standard
{
    /// <summary>
    /// Class for create processor build with standard preference
    /// </summary>
    public static partial class StandardProcessorBuilder
    {
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        public static class LinkedMath
        {
            public static double Sin(double alpha) => Math.Sin(alpha);
            public static double Cos(double alpha) => Math.Cos(alpha);

            public static double Min(double first, params double[] args) =>
                Enumerable.Repeat(first, 1).Concat(args).Min();

            public static double Max(double first, params double[] args) =>
                Enumerable.Repeat(first, 1).Concat(args).Max();

            public static double PI { get; }

            static LinkedMath()
            {
                PI = Math.PI;
            }
        }

        /// <summary>
        /// Standard resolving rules
        /// </summary>
        public static IDictionary<IResolverType, IEnumerable<IResolver>> LinkerRules { get; } =
            new Dictionary<IResolverType, IEnumerable<IResolver>>
            {
                [new ResolverType<ConstantDeclarationTerm>()] = new IResolver[]
                {
                    new ConstantResolver()
                },
                [new ResolverType<FunctionDeclarationTerm>()] = new IResolver[]
                {
                    new FunctionResolver(),
                    new ParamFunctionResolver()
                }
            };

    }
}
