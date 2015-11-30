using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Resolvers
{
    [TestFixture]
    [Category("Translator linker compiler")]
    public class ConstantResolverTest
    {
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        private static class LinkedMath
        {
            public static double PI { get; }

            static LinkedMath()
            {
                PI = Math.PI;
            }
        }

        /*
        [Test]
        public void Resolve_ExistConstant_ResolvedTerm()
        {
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();

            var term = new ConstantDeclarationTerm("PI");

            linkedClass.Stub(lc => lc.FindConstant(term)).Return(PropertyOf(() => LinkedMath.PI));

            var resolver = new ConstantResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Not.Null);
            Assert.That(resolvedTerm, Is.TypeOf<ConstantTerm>());

            var constantTerm = (ConstantTerm) resolvedTerm;
            Assert.That(constantTerm.PropertyInfo.GetValue(null), Is.EqualTo(Math.PI).Within(1e-5));
        }
        */
        [Test]
        public void Resolve_NullConstant_Null()
        {
            var children = Enumerable.Empty<ISyntacticNode>();
            var linkedClass = MockRepository.GenerateStub<ILinkedLibrary>();
            var term = new ConstantDeclarationTerm("Pi");
            linkedClass.Stub(lc => lc.FindConstant(term)).Return(null);
            

            var resolver = new ConstantResolver();
            var resolvedTerm = resolver.Resolve(term, children, linkedClass);

            Assert.That(resolvedTerm, Is.Null);
        }
    }
}
