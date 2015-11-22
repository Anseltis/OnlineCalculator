using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Analyzer.Facade;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test.Facade
{
    [TestFixture]
    public class ProcessorTest
    {
        private IProcessorBuilder CreateProcessorBuilderStub()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.LexicalAnalyzer = MockRepository.GenerateStub<ILexicalAnalyzer>();
            processorBuilder.SyntacticAnalyzer = MockRepository.GenerateStub<ISyntacticAnalyzer>();
            processorBuilder.SyntacticTarget = MockRepository.GenerateStub<ISyntacticNodeType>();
            processorBuilder.Translator = MockRepository.GenerateStub<ITranslator>();
            processorBuilder.Linker = MockRepository.GenerateStub<ILinker>();
            processorBuilder.Compilator = MockRepository.GenerateStub<ICompilator>();
            return processorBuilder;
        }
        [Test]
        public void Constructor_Builder_SameProperty()
        {
            var processorBuilder = CreateProcessorBuilderStub();

            var processor = new Processor(processorBuilder);
            Assert.That(processor.LexicalAnalyzer, Is.SameAs(processorBuilder.LexicalAnalyzer));
            Assert.That(processor.SyntacticAnalyzer, Is.SameAs(processorBuilder.SyntacticAnalyzer));
            Assert.That(processor.SyntacticTarget, Is.SameAs(processorBuilder.SyntacticTarget));
            Assert.That(processor.Translator, Is.SameAs(processorBuilder.Translator));
            Assert.That(processor.Linker, Is.SameAs(processorBuilder.Linker));
            Assert.That(processor.Compilator, Is.SameAs(processorBuilder.Compilator));
        }

        [Test]
        public void Calculate_Builder_ExpectBehavior()
        {
            const string text = "some text";
            var tokens = MockRepository.GenerateStub<IEnumerable<IToken>>();
            var lexicalAnalyzer = MockRepository.GenerateMock<ILexicalAnalyzer>();
            lexicalAnalyzer.Expect(la => la.Parse(text)).Return(tokens);

            var syntacticTarget = MockRepository.GenerateStub<ISyntacticNodeType>();
            var syntacticTree = MockRepository.GenerateStub<ISyntacticNode>();
            var syntacticAnalyzer = MockRepository.GenerateMock<ISyntacticAnalyzer>();
            syntacticAnalyzer.Expect(sa => sa.Parse(tokens, syntacticTarget)).Return(syntacticTree);

            var translatedTree = MockRepository.GenerateStub<ISyntacticNode>();
            var translator = MockRepository.GenerateMock<ITranslator>();
            translator.Expect(t => t.Translate(syntacticTree)).Return(translatedTree);
            translator.Expect(t => t.CheckResult(translatedTree));

            var linkedTree = MockRepository.GenerateStub<ISyntacticNode>();
            var linker = MockRepository.GenerateMock<ILinker>();
            linker.Expect(l => l.Resolve(translatedTree)).Return(linkedTree);
            linker.Expect(l => l.CheckResult(linkedTree));

            Expression<Func<double>> expression = () => 1.0;
            var compilator = MockRepository.GenerateMock<ICompilator>();
            compilator.Expect(c => c.CreateExpression(linkedTree)).Return(expression);

            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.LexicalAnalyzer = lexicalAnalyzer;
            processorBuilder.SyntacticAnalyzer = syntacticAnalyzer;
            processorBuilder.SyntacticTarget = syntacticTarget;
            processorBuilder.Translator = translator;
            processorBuilder.Linker = linker;
            processorBuilder.Compilator = compilator;

            var processor = new Processor(processorBuilder);
            var result = processor.Calculate(text);

            Assert.That(result, Is.EqualTo(1.0).Within(1e-7));

            lexicalAnalyzer.VerifyAllExpectations();
            syntacticAnalyzer.VerifyAllExpectations();
            translator.VerifyAllExpectations();
            linker.VerifyAllExpectations();
            compilator.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullBuilder_Throwexception()
        {
            new Processor(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLexicalAnalyzer_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.LexicalAnalyzer = null;
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullSyntacticAnalyzer_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.SyntacticAnalyzer = null;
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLSyntacticTarget_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.SyntacticTarget= null;
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullTranslator_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.Translator = null;
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLinker_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.Linker = null;
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullCompilator_ThrowException()
        {
            var processorBuilder = CreateProcessorBuilderStub();
            processorBuilder.Compilator = null;
            new Processor(processorBuilder);
        }

    }
}
