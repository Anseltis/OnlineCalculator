﻿using System;

namespace AnsiSoft.Calculator.Model.Reflection.Exceptions
{
    /// <summary>
    /// Exception class of non-static linked class
    /// This case reveals if linker initializes non-static class
    /// </summary>
    [Serializable]
    public sealed class NonStaticClassException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonStaticClassException"/> class.
        /// </summary>
        /// <param name="linkedClass">Linked class</param>
        public NonStaticClassException(Type linkedClass) :
            base($"Class {linkedClass.FullName} is not static.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonStaticClassException"/> class.
        /// </summary>
        public NonStaticClassException() : base($"There no static class")
        {
        }

    }
}
