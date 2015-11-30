using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Terms;

namespace AnsiSoft.Calculator.Model.Reflection
{
    /// <summary>
    /// Class for external source of resolving constants and functions
    /// </summary>
    /// <typeparam name="T">Target type of linked library</typeparam>
    public sealed class LinkedLibrary<T> : ILinkedLibrary
    {
        #region implement ILinkedLibrary
        public IResolvedTerm FindParamFunction(IFunctionDeclarationTerm term) =>
            FindMethodSignature(term, CheckParamMethodSignature)
                .Select(methodInfo => new ResolvedTerm(
                    children =>
                    {
                        var fixedCount = methodInfo.GetParameters().Count() - 1;
                        var head = children.Take(fixedCount);
                        var tail = children.Skip(fixedCount);
                        var tailExpression = Expression.NewArrayInit(typeof (T), tail);
                        var args = head.Concat(Enumerable.Repeat(tailExpression, 1));
                        return Expression.Call(methodInfo, args);
                    }))
                .FirstOrDefault();

        public IResolvedTerm FindFunction(IFunctionDeclarationTerm term) =>
            FindMethodSignature(term, CheckMethodSignature)
                .Select(methodInfo => new ResolvedTerm(children => Expression.Call(methodInfo, children)))
                .FirstOrDefault();

        public IResolvedTerm FindConstant(IConstantDeclarationTerm term) => 
            TypeLazy.Value.GetProperties()
            .Where(propertyInfo => propertyInfo.PropertyType == typeof (T) && propertyInfo.GetMethod.IsStatic)
            .Where(propertyInfo => propertyInfo.Name == term.Identifier)
            .Select(propertyInfo => new ResolvedTerm(children => Expression.Property(null, propertyInfo)))
            .FirstOrDefault();

        #endregion

        /// <summary>
        /// Linked static class
        /// </summary>
        public Lazy<Type> TypeLazy { get; }

        /// <summary>
        /// Check signature param method.
        /// </summary>
        /// <param name="method">Current method</param>
        /// <param name="argumentCount">Target argumant count</param>
        /// <returns>True if method of the suitable signature</returns>
        public static bool CheckParamMethodSignature(MethodInfo method, int argumentCount)
        {
            var parameters = method.GetParameters();
            if (!parameters.Any())
            {
                return false;
            }

            var init = parameters.Take(parameters.Length - 1);
            var tail = parameters.Last();
            var tailIsParam = tail
                .GetCustomAttributes(typeof(ParamArrayAttribute), false)
                .Any();

            return
                argumentCount >= parameters.Length - 1 &&
                init.All(parameterInfo => parameterInfo.ParameterType == typeof(T)) &&
                tail.ParameterType == typeof(T[]) &&
                tailIsParam;
        }

        /// <summary>
        /// Check signature method.
        /// </summary>
        /// <param name="method">Current method</param>
        /// <param name="argumentCount">Target argument count</param>
        /// <returns>True if method of the suitable signature</returns>
        public static bool CheckMethodSignature(MethodInfo method, int argumentCount)
        {
            var parameters = method.GetParameters();
            return argumentCount == parameters.Length &&
                   parameters.All(p => p.ParameterType == typeof(T));
        }

        /// <summary>
        /// Find method by signature
        /// </summary>
        /// <param name="term">Declaration term</param>
        /// <param name="signaturePredicate">Signature predicate</param>
        /// <returns>MethodInfo of found class or null</returns>
        public IEnumerable<MethodInfo> FindMethodSignature(IFunctionDeclarationTerm term, Func<MethodInfo, int, bool> signaturePredicate)
        {
            return TypeLazy.Value.GetMethods()
                .Where(methodInfo => methodInfo.Name == term.Identifier)
                .Where(methodInfo => methodInfo.IsStatic && methodInfo.ReturnType == typeof(T))
                .Where(methodInfo => signaturePredicate(methodInfo, term.ArgumentCount));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedLibrary{T}"/> class.
        /// </summary>
        /// <param name="typeLazy">Linked class in lazy type</param>

        public LinkedLibrary(Lazy<Type> typeLazy)
        {
            if (typeLazy == null)
            {
                throw new ArgumentNullException(nameof(typeLazy));
            }

            TypeLazy = typeLazy;
        }
    }
}
