using System;
using NUnit.Framework;

namespace MockProcess.Tests
{
    [TestFixture]
    public class InstructionsFileProcessorTests
    {
        [Test]
        public void ProcessFile()
        {
            var processor = new InstructionsFileProcessor();
            processor.Run(@".\Instructions.txt");

        }
    }
}
