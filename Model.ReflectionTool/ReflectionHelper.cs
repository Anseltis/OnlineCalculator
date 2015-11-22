using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AnsiSoft.Calculator.Model.ReflectionTool
{
    /// <summary>
    /// Class with different utilities for reflection
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Get PropertyInfo from expression like "() => Class.Property"
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="expression">Expression for call property</param>
        /// <returns>PropertyInfo of property in expression.</returns>
        public static PropertyInfo PropertyOf<T>(Expression<Func<T>> expression)
        {
            var body = (MemberExpression)expression.Body;
            return (PropertyInfo)body.Member;
        }

        /// <summary>
        /// Get MethodInfo from expression like "() => Class.Method(...)"
        /// </summary>
        /// <typeparam name="T">Method return type</typeparam>
        /// <param name="expression">Expression for call method</param>
        /// <returns>MethodInfo of method in expression.</returns>
        public static MethodInfo MethodOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }

        /// <summary>
        /// Check than class is abstract
        /// </summary>
        /// <param name="type">Target class</param>
        /// <returns>True if class is abstract</returns>
        public static bool IsStatic(this Type type) =>
            type.IsAbstract && type.IsSealed;
    }
}
