using System;
using NUnit.Framework;
using Example.Target;

namespace Example.Tests
{
    [TestFixture]
    public class AdderTests
    {
        [Test]
        public void OnePlusOneEqualsTwo()
        {
            var adder = new Adder();
            var result = adder.Add(1, 1);
            Assert.AreEqual(2, result);
        }
    }
}
