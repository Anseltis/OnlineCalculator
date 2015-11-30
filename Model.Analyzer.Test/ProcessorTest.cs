using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AnsiSoft.Calculator.Model.Interface.Facade;
using AnsiSoft.Calculator.Model.Interface.Nodes;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnsiSoft.Calculator.Model.Analyzer.Test
{
    [TestFixture]
    [Category("Processor")]
    public class ProcessorTest
    {
        [Test]
        public void Constructor_Builder_SameProperty()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());

            var processor = new Processor(processorBuilder);
            Assert.That(processor.LexicalAnalyzer, Is.SameAs(processorBuilder.LexicalAnalyzer));
            Assert.That(processor.SyntacticAnalyzer, Is.SameAs(processorBuilder.SyntacticAnalyzer));
            Assert.That(processor.SyntacticTarget, Is.SameAs(processorBuilder.SyntacticTarget));
            Assert.That(processor.Translator, Is.SameAs(processorBuilder.Translator));
            Assert.That(processor.Linker, Is.SameAs(processorBuilder.Linker));
            Assert.That(processor.Compiler, Is.SameAs(processorBuilder.Compiler));
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
            var compiler = MockRepository.GenerateMock<ICompiler>();
            compiler.Expect(c => c.CreateExpression(linkedTree)).Return(expression);

            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();

            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(lexicalAnalyzer);
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(syntacticAnalyzer);
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(syntacticTarget);
            processorBuilder.Stub(pb => pb.Translator).Return(translator);
            processorBuilder.Stub(pb => pb.Linker).Return(linker);
            processorBuilder.Stub(pb => pb.Compiler).Return(compiler);

            var processor = new Processor(processorBuilder);
            var result = processor.Calculate(text);

            Assert.That(result, Is.EqualTo(1.0).Within(1e-7));

            lexicalAnalyzer.VerifyAllExpectations();
            syntacticAnalyzer.VerifyAllExpectations();
            translator.VerifyAllExpectations();
            linker.VerifyAllExpectations();
            compiler.VerifyAllExpectations();
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
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(null);
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullSyntacticAnalyzer_ThrowException()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(null);
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLSyntacticTarget_ThrowException()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(null);
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullTranslator_ThrowException()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(null);
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLinker_ThrowException()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(null);
            processorBuilder.Stub(pb => pb.Compiler).Return(MockRepository.GenerateStub<ICompiler>());
            new Processor(processorBuilder);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullCompiler_ThrowException()
        {
            var processorBuilder = MockRepository.GenerateStub<IProcessorBuilder>();
            processorBuilder.Stub(pb => pb.LexicalAnalyzer).Return(MockRepository.GenerateStub<ILexicalAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticAnalyzer).Return(MockRepository.GenerateStub<ISyntacticAnalyzer>());
            processorBuilder.Stub(pb => pb.SyntacticTarget).Return(MockRepository.GenerateStub<ISyntacticNodeType>());
            processorBuilder.Stub(pb => pb.Translator).Return(MockRepository.GenerateStub<ITranslator>());
            processorBuilder.Stub(pb => pb.Linker).Return(MockRepository.GenerateStub<ILinker>());
            processorBuilder.Stub(pb => pb.Compiler).Return(null);
            new Processor(processorBuilder);
        }

    }
}
