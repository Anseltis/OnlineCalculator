using System;
using System.Linq;

namespace Web
{
    /// <summary>
    /// Configuration file for static/runtime compile
    /// </summary>
    public static class Configuration
    {
        public static double Sin(double alpha)
        {
            return Math.Sin(alpha);
        }
        public static double Cos(double alpha)
        {
            return Math.Cos(alpha);
        }
        public static double Min(double first, params double[] args)
        {
            return Enumerable.Repeat(first, 1).Concat(args).Min();
        }
        public static double Max(double first, params double[] args)
        {
            return Enumerable.Repeat(first, 1).Concat(args).Max();
        }
        public static double Sum(double first, params double[] args)
        {
            return Enumerable.Repeat(first, 1).Concat(args).Sum();
        }
        public static double Exp(double x)
        {
            return Math.Exp(x);
        }
        public static double Pow(double x, double y)
        {
            return Math.Pow(x, y);
        }
        public static double PI { get; private set; }
        public static double E { get; private set; }

        /// <summary>
        /// Initialize static property (constant)
        /// </summary>
        static Configuration()
        {
            PI = Math.PI;
            E = Math.E;
        }
    }
}
