using System;
using NUnit.Framework;
using BuildTool;
using BuildTool.Navigation;

namespace BuildTool.Tests
{
    [TestFixture]
    public class ProjectInfoTests
    {
        [Test]
        public void ConstructionForSolutionDefinition()
        {
            string projectRef = @"Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""BuildTool.Tests"", ""..\BuildTool.Tests\BuildTool.Tests.csproj"", ""{6390DF2C-9635-49FC-8E2E-6E96A9B00737}""";

            var projInfo = new ProjectInfo(@"C:\src\sln", projectRef);            
            
            Assert.That(projInfo.Guid, Is.EqualTo("6390DF2C-9635-49FC-8E2E-6E96A9B00737"));
            Assert.That(projInfo.TypeGuid, Is.EqualTo("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"));
            Assert.That(projInfo.Name, Is.EqualTo("BuildTool.Tests"));
            Assert.That(projInfo.ProjectFile, Is.EqualTo(@"C:\BuildTool.Tests\BuildTool.Tests.csproj"));
        }
    }
}
