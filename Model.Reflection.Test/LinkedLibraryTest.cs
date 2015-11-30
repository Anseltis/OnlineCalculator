using System;
using System.Linq;
using AnsiSoft.Calculator.Model.Interface.Terms;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Reflection.Test
{
    [TestFixture]
    [Category("Reflection")]
    public class LinkedLibraryTest
    {
        /*
        /// <summary>
        /// Class for resolve external variable and function
        /// </summary>
        private static class LinkedMath
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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullType_Throwexception()
        {
            new LinkedLibrary(null);
        }

        [Test]
        public void Type_StaticClass_Same()
        {
            var type = typeof (LinkedMath);
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => type));
            Assert.That(linkedClass.TypeLazy.Value, Is.SameAs(type));
        }

        [Test]
        public void FindFindProperty_ExistProperty_Property()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IConstantDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("PI");
            var propertyInfo = linkedClass.FindConstant(term);
            Assert.That(propertyInfo, Is.Not.Null);
            Assert.That(propertyInfo.GetValue(null), Is.EqualTo(Math.PI).Within(1e-7));
        }

        [Test]
        public void FindFindProperty_AbsenttProperty_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IConstantDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Pu");
            var propertyInfo = linkedClass.FindConstant(term);
            Assert.That(propertyInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistMethod_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Sin");
            term.Stub(t => t.ArgumentCount).Return(1);
            var methodInfo = linkedClass.FindFunction(term);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {Math.PI/2};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(1).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_AbsentMethod_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("sin");
            term.Stub(t => t.ArgumentCount).Return(1);
            var methodInfo = linkedClass.FindFunction(term);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistMethodAndWrongArgumentCount_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Sin");
            term.Stub(t => t.ArgumentCount).Return(2);
            var methodInfo = linkedClass.FindFunction(term);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistParamMethod_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Max");
            term.Stub(t => t.ArgumentCount).Return(1);
            var methodInfo = linkedClass.FindParamFunction(term);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {1.0, new double[] {}};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(1).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_ExistParamMethodMoreArgumentCount_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Max");
            term.Stub(t => t.ArgumentCount).Return(2);
            var methodInfo = linkedClass.FindParamFunction(term);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {1.0, new[] {2.0}};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(2).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_ExistParamMethodLessArgumentCount_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("Max");
            term.Stub(t => t.ArgumentCount).Return(0);
            var methodInfo = linkedClass.FindParamFunction(term);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_AbsentParamMethod_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var term = MockRepository.GenerateStub<IFunctionDeclarationTerm>();
            term.Stub(t => t.Identifier).Return("max");
            term.Stub(t => t.ArgumentCount).Return(1);
            var methodInfo = linkedClass.FindParamFunction(term);
            Assert.That(methodInfo, Is.Null);
        }
        */
    }
}
