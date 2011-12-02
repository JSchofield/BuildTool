using System;
using NUnit.Framework;
using MockProcess.Instructions;

namespace MockProcess.Tests
{
    [TestFixture]
    public class WhenCreatingAnOutInstruction
    {
        private IInstruction _instruction;

        [SetUp]
        public void CreateInstruction()
        {
            _instruction = new TextInstructionFactory().CreateInstruction("OUT: standard output");
        }

        [Test]
        public void AStandardOutInstructionIsCreated()
        {
            Assert.IsInstanceOf<StandardOutInstruction>(_instruction);
        }

        [Test]
        public void TheMessageIsTakenFromTheInput()
        {
            Assert.AreEqual("standard output", (_instruction as StandardOutInstruction).Message);
        }
    }

    [TestFixture]
    public class WhenCreatingAnErrInstruction
    {
        private IInstruction _instruction;

        [SetUp]
        public void CreateInstruction()
        {
            _instruction = new TextInstructionFactory().CreateInstruction("ERR: standard error");
        }

        [Test]
        public void AStandardOutInstructionIsCreated()
        {
            Assert.IsInstanceOf<StandardErrorInstruction>(_instruction);
        }

        [Test]
        public void TheMessageIsTakenFromTheInput()
        {
            Assert.AreEqual("standard error", (_instruction as StandardErrorInstruction).Message);
        }
    }

    [TestFixture]
    public class WhenCreatingARemInstruction
    {
        private IInstruction _instruction;

        [SetUp]
        public void CreateInstruction()
        {
            _instruction = new TextInstructionFactory().CreateInstruction("REM: comment");
        }

        [Test]
        public void AStandardOutInstructionIsCreated()
        {
            Assert.IsInstanceOf<CommentInstruction>(_instruction);
        }

        [Test]
        public void TheMessageIsTakenFromTheInput()
        {
            Assert.AreEqual("comment", (_instruction as CommentInstruction).Comment);
        }
    }

    [TestFixture]
    public class WhenCreatingAWaitInstruction
    {
        private IInstruction _instruction;

        [SetUp]
        public void CreateInstruction()
        {
            _instruction = new TextInstructionFactory().CreateInstruction("WAIT: 5000");
        }

        [Test]
        public void AStandardOutInstructionIsCreated()
        {
            Assert.IsInstanceOf<WaitInstruction>(_instruction);
        }

        [Test]
        public void TheMessageIsTakenFromTheInput()
        {
            Assert.AreEqual(5000, (_instruction as WaitInstruction).Milliseconds);
        }
    }

    [TestFixture]
    public class WhenCreatingAInInstruction
    {
        private IInstruction _instruction;

        [SetUp]
        public void CreateInstruction()
        {
            _instruction = new TextInstructionFactory().CreateInstruction("IN: ignored");
        }

        [Test]
        public void AStandardOutInstructionIsCreated()
        {
            Assert.IsInstanceOf<StandardInInstruction>(_instruction);
        }
    }
}
