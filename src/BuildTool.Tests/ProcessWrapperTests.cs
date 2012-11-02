using System;
using System.Collections.Generic;
using NUnit.Framework;
using BuildTool;

namespace BuildTool.Tests
{
    public abstract class ProcessWrapperSpecification
    {
        protected abstract string Instructions { get; }
        protected TestFile InstructionsFile { get; set; }
        protected IProcessOutputs Outputs { get; private set; }

        [SetUp]
        public void CreateInstructionsFile()
        {
            InstructionsFile = new TestFile(Instructions);
            When();
        }

        protected virtual void When()
        {
            Outputs = new StubOutputHandler();

            var pw = new ProcessWrapper(
                new Command() { FileName = "MockProcess.exe", Arguments = InstructionsFile.Location },
                ".",
                (IOutputHandler)Outputs);

            pw.RunAndWaitForExit();

        }

        [TearDown]
        public void DeleteInstructionsFile()
        {
            InstructionsFile.Dispose();
        }
    }

    [TestFixture]
    public class WhenProcessSendsThreeLinesToStandardOutput: ProcessWrapperSpecification
    {
        protected override string Instructions { get {
            return string.Join("\n",
                "OUT: Some output 1",
                "OUT: Some output 2",
                "OUT: Some output 3"); } }

        [Test]
        public void TheWrapperSendsOneStartingMessageWithTheFileNameAndArguments()
        {
            Assert.AreEqual(1, Outputs.Startings.Count);
            Assert.AreEqual("MockProcess.exe", Outputs.Startings[0].FileName);
            Assert.AreEqual(InstructionsFile.Location, Outputs.Startings[0].Arguments);
        }

        [Test]
        public void TheWrapperSendsThreeOutputMessages()
        {
            Assert.AreEqual(3, Outputs.Outputs.Count);
            Assert.AreEqual("Some output 1", Outputs.Outputs[0]);
            Assert.AreEqual("Some output 2", Outputs.Outputs[1]);
            Assert.AreEqual("Some output 3", Outputs.Outputs[2]);
        }

        [Test]
        public void TheWrapperSendsNoErrorMessages()
        {
            Assert.AreEqual(0, Outputs.Errors.Count);
        }
        
        [Test]
        public void TheWrapperSendsNoEndingMessages()
        {
            Assert.AreEqual(1, Outputs.Endings.Count);
            Assert.AreEqual(0, Outputs.Endings[0]);
        }
    }


}
