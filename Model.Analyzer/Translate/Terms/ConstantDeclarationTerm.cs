﻿namespace AnsiSoft.Calculator.Model.Analyzer.Translate.Terms
{
    /// <summary>
    /// Class for constant term without real linking
    /// </summary>
    public sealed class ConstantDeclarationTerm : IDeclarationTerm
    {
        #region implement IDeclarationTerm
        public string Identifier { get; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantDeclarationTerm"/> class.
        /// </summary>
        /// <param name="identifier">Constant name</param>
        public ConstantDeclarationTerm(string identifier)
        {
            Identifier = identifier;
        }
    }

}
