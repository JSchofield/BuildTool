using System;
using System.IO;
using NUnit.Framework;

namespace BuildTool.Tests
{
    [TestFixture]
    public class StandardOutTests
    {
        [Test]
        public void CanWriteToConsoleUsingStandardOut()
        {
            TextWriter writer = Console.Out;

            writer.WriteLine("Hello World");
        }
    }
}
