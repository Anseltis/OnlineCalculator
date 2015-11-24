using System;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.ReflectionTool.Test
{
    [TestFixture]
    [Category("Reflection tool")]
    public class RuntimeCompileExceptionTest
    {
        [Test]
        public void Message_SomeSourceCodeAndInnerException_RightMessage()
        {
            var exception = new Exception();
            const string sourceCode = "bvcxbvnxbcvxbmjnv";
            var runtimeCompileException = new RuntimeCompileException(sourceCode, exception);
            Assert.That(runtimeCompileException.Message, Is.EqualTo("Error during runtime compilation"));
        }

        [Test]
        public void SourceCode_SomeSourceCodeAndInnerException_Same()
        {
            var exception = new Exception();
            const string sourceCode = "bvcxbvnxbcvxbmjnv";
            var runtimeCompileException = new RuntimeCompileException(sourceCode, exception);
            Assert.That(runtimeCompileException.SourceCode, Is.EqualTo(sourceCode));
        }

        [Test]
        public void InnerException_SomeSourceCodeAndInnerException_Same()
        {
            var exception = new Exception();
            const string sourceCode = "bvcxbvnxbcvxbmjnv";
            var runtimeCompileException = new RuntimeCompileException(sourceCode, exception);
            Assert.That(runtimeCompileException.InnerException, Is.SameAs(exception));
        }

    }
}
