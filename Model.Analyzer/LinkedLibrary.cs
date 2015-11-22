﻿using System;
using System.Linq;
using System.Reflection;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using AnsiSoft.Calculator.Model.Interface;
using AnsiSoft.Calculator.Model.Interface.Facade;

namespace AnsiSoft.Calculator.Model.Analyzer
{
    /// <summary>
    /// Class for external source of resolving constants and functions
    /// </summary>
    public sealed class LinkedLibrary : ILinkedLibrary
    {
        #region implement ILinkedLibrary
        public MethodInfo FindParamMethod(string name, int argumentCount) => 
            FindMethodSignature(name, argumentCount, CheckParamMethodSignature);

        public MethodInfo FindMethod(string name, int argumentCount) => 
            FindMethodSignature(name, argumentCount, CheckMethodSignature);

        public PropertyInfo FindProperty(string name)
        {
            return Type.GetProperties()
                .Where(p => p.PropertyType == typeof(double) && p.GetMethod.IsStatic)
                .FirstOrDefault(p => p.Name == name);
        }
        #endregion

        /// <summary>
        /// Linked static class
        /// </summary>
        public Type Type { get; }

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
                init.All(p => p.ParameterType == typeof(double)) &&
                tail.ParameterType == typeof(double[]) &&
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
                   parameters.All(p => p.ParameterType == typeof(double));
        }

        /// <summary>
        /// Find method by signature
        /// </summary>
        /// <param name="name">Method name</param>
        /// <param name="argumentCount">Target argument count</param>
        /// <param name="signaturePredicate">Signature predicate</param>
        /// <returns>MethodInfo of found class or null</returns>
        public MethodInfo FindMethodSignature(string name, int argumentCount, Func<MethodInfo, int, bool> signaturePredicate)
        {
            return Type.GetMethods()
                .Where(method => method.Name == name)
                .Where(method => method.IsStatic && method.ReturnType == typeof(double))
                .FirstOrDefault(m => signaturePredicate(m, argumentCount));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedLibrary"/> class.
        /// </summary>
        /// <param name="type">Linked class</param>
        public LinkedLibrary(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var isStatic = type.IsAbstract && type.IsSealed;
            if (!isStatic)
            {
                throw new NonStaticClassException(type);
            }

            Type = type;
        }
    }
}
