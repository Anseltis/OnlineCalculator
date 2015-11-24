using System.Linq;
using NUnit.Framework;
using static AnsiSoft.Calculator.Model.ReflectionTool.CompilerHelper;

namespace AnsiSoft.Calculator.Model.ReflectionTool.Test
{
    [TestFixture]
    [Category("Reflection tool")]
    public class CompilerHelperTest
    {
        [Test]
        public void Compile_EmptyText_EmptyResult()
        {
            var types = Compile("", new string[] {});
            Assert.That(types.Any(), Is.False);
        }

        [Test]
        [ExpectedException(typeof(RuntimeCompileException))]
        public void Compile_WrongText_EmptyResult()
        {
            Compile("sa", new string[] { });
        }

        [Test]
        public void Compile_TextWithOneClass_OneClass()
        {
            var types = Compile("public class Any {}", new string[] { });
            Assert.That(types.Length, Is.EqualTo(1));
            Assert.That(types.First().Name, Is.EqualTo("Any"));
        }

        [Test]
        [ExpectedException(typeof(RuntimeCompileException))]
        public void Compile_TextWithoutDependency_OneClass()
        {
            Compile("using System.Linq;",new string[] { });
        }
        [Test]
        public void Compile_TextWithDependency_RightResult()
        {
            var types = Compile("using System.Linq;", new[] { "System.Core.dll" });
            Assert.That(types.Any(), Is.False);
        }

        [Test]
        [ExpectedException(typeof(RuntimeCompileException))]
        public void Compile_WrongDependency_RightResult()
        {
            Compile("", new[] { "System.Core5.dll" });
        }

    }
}
