using AnsiSoft.Calculator.Model.Analyzer.Translate.Resolvers;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Terms;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Resolvers
{
    [TestFixture]
    class ResolverTypeTest
    {
        [Test]
        public void Resolve_AppropriateTermType_True()
        {
            var resolverType = new ResolverType<FunctionDeclarationTerm>();
            var term = new FunctionDeclarationTerm("variable");
            Assert.That(resolverType.Resolve(term), Is.True);
        }

        [Test]
        public void Resolve_InappropriateTermType_True()
        {
            var resolverType = new ResolverType<FunctionDeclarationTerm>();
            var term = new ConstantDeclarationTerm("variable");
            Assert.That(resolverType.Resolve(term), Is.False);
        }

    }
}
