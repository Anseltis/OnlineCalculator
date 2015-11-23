﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnsiSoft.Calculator.Model.Analyzer.Translate.Exceptions;
using NUnit.Framework;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Translate.Exceptions
{
    [TestFixture]
    public class CannotResolveIdentifierExceptionTest
    {
        [Test]
        public void Identifier_SomeIdentifier_Same()
        {
            const string identifier = "myFunc";
            var exception = new CannotResolveIdentifierException(identifier);
            Assert.That(exception.Identifier, Is.EqualTo(identifier));
        }

        [Test]
        public void Message_SomeIdentifier_CannotResolve()
        {
            var exception = new CannotResolveIdentifierException("myFunc");
            Assert.That(exception.Message, Is.EqualTo("Does not resolve identifier 'myFunc'"));
        }
    }
}
