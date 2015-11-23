using System;
using System.Linq;

namespace Web
{
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
            return args.Min();
        }
        public static double Max(double first, params double[] args)
        {
            return Enumerable.Repeat(first, 1).Concat(args).Max();
        }
        public static double Sum(double first, params double[] args)
        {
            return Enumerable.Repeat(first, 1).Concat(args).Sum();
        }
        public static double Pi { get; private set; }
        static Configuration()
        {
            Pi = Math.PI;
        }
    }
}
