using System;
using System.Linq;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Reflection.Test
{
    [TestFixture]
    public class LinkedLibraryTest
    {
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
            var propertyInfo = linkedClass.FindProperty("PI");
            Assert.That(propertyInfo, Is.Not.Null);
            Assert.That(propertyInfo.GetValue(null), Is.EqualTo(Math.PI).Within(1e-7));
        }

        [Test]
        public void FindFindProperty_AbsenttProperty_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var propertyInfo = linkedClass.FindProperty("Pi");
            Assert.That(propertyInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistMethod_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindMethod("Sin", 1);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {Math.PI/2};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(1).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_AbsentMethod_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindMethod("sin", 1);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistMethodAndWrongArgumentCount_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindMethod("Sin", 2);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_ExistParamMethod_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindParamMethod("Max", 1);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {1.0, new double[] {}};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(1).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_ExistParamMethodMoreArgumentCount_Method()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindParamMethod("Max", 2);
            Assert.That(methodInfo, Is.Not.Null);
            var args = new object[] {1.0, new[] {2.0}};
            Assert.That(methodInfo.Invoke(null, args), Is.EqualTo(2).Within(1e-7));
        }

        [Test]
        public void FindFindMthod_ExistParamMethodLessArgumentCount_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindParamMethod("Max", 0);
            Assert.That(methodInfo, Is.Null);
        }

        [Test]
        public void FindFindMthod_AbsentParamMethod_Null()
        {
            var linkedClass = new LinkedLibrary(new Lazy<Type>(() => typeof(LinkedMath)));
            var methodInfo = linkedClass.FindParamMethod("max", 1);
            Assert.That(methodInfo, Is.Null);
        }

    }
}
