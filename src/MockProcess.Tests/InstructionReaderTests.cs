using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace MockProcess.Tests
{
    public class StubInstruction: IInstruction
    {
        public string Text { get; private set; }

        public StubInstruction(string instructionText)
        {
            Text = instructionText;
        }

        public void Run()
        {
 	        throw new NotImplementedException();
        }
    }

    public abstract class InstructionReaderSpecification
    {
        private TextReader _stringReader;
        protected abstract String StreamText {get;}
        
        public IEnumerable<IInstruction> Instructions {get; protected set;}

        [SetUp]
        public void When()
        {
            var mock = new Moq.Mock<ITextInstructionFactory>();

            mock.Setup(
                m => m.CreateInstruction(It.IsAny<string>()))
                .Returns((string s) => new StubInstruction(s));
            
            _stringReader = new StringReader(StreamText);
            Instructions = new InstructionReader(_stringReader, mock.Object);
        }

        protected StubInstruction[] AsArray(IEnumerable<IInstruction> instructions)
        {
            return instructions.Cast<StubInstruction>().ToArray();
        }

        [TearDown]
        public void DisposeStringReader()
        {
            _stringReader.Dispose();
        }
    }

    [TestFixture]
    public class WhenReadingThreeInstructionsFromTheStream : InstructionReaderSpecification
    {
        protected override string StreamText
        {
            get
            {
                return string.Join("\n",
                    "REM: First Instruction",
                    "REM: Second Instruction",
                    "REM: Third Instruction");
            }
        }

        [Test]
        public void TheFirstInstructionIsReturnedFirst()
        {
            Assert.AreEqual("REM: First Instruction", (Instructions.First() as StubInstruction).Text);
        }

        [Test]
        public void TheSecondInstructionIsReturnedSecond()
        {
            Assert.AreEqual("REM: Second Instruction", (Instructions.ElementAt(1) as StubInstruction).Text);
        }

        [Test]
        public void TheThirdInstructionIsReturnedThird()
        {
            Assert.AreEqual("REM: Third Instruction", (Instructions.Last() as StubInstruction).Text);
        }

        [Test]
        public void TheEnumerationEndsAfterReadingThreeInstructions()
        {
            int count = 0;
            foreach (IInstruction i in Instructions)
            {
                count++;
            }
            Assert.AreEqual(3, count);
        }
    }

    [TestFixture]
    public class WhenReadingAnEmptyStream : InstructionReaderSpecification
    {
        protected override string StreamText
        {
            get
            {
                return string.Empty;
            }
        }

        [Test]
        public void TheEnumerationEndsImmediately()
        {
            int count = 0;
            foreach (IInstruction i in Instructions)
            {
                count++;
            }
            Assert.AreEqual(0, count);
        }
    }

    [TestFixture]
    public class WhenTheInstructionReaderIsConstructed
    {
        [Test, ExpectedException(typeof(ArgumentException))]
        public void ANullFactoryArgumentResultInAnException()
        {
            new InstructionReader(new Mock<TextReader>().Object, null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ANullInstructionsTextArgumentResultInAnException()
        {
            new InstructionReader(null, new Mock<ITextInstructionFactory>().Object);
        }
    }
}
