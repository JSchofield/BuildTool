using System;
using NUnit.Framework;
using BuildTool;
using BuildTool.Navigation;
using System.Reflection;
using System.IO;
using System.Linq;

namespace BuildTool.Tests.Navigation
{
    [TestFixture]
    public class ProjectTests
    {
        private string GetProjectFile(string resourceName)
        {
            var fileName = Path.Combine(Path.GetTempPath(), resourceName);
            using (var resource = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetType(), resourceName)))
            using (var file = new StreamWriter(File.OpenWrite(fileName)))
            {
                while (!resource.EndOfStream)
                {
                    file.WriteLine(resource.ReadLine());
                }
            }
            Console.WriteLine("Wrote resource {0} to {1}", resourceName, fileName);
            return fileName;
        }

        [Test]
        public void ProjectGetsGlobalProperties()
        {
            var projectFile = GetProjectFile("SampleProject.csproj");
            var project = new Project(projectFile);

            Assert.That(project.OutputType, Is.EqualTo("Library"));
            Assert.That(project.AssemblyName, Is.EqualTo("BuildTool.Tests"));
            Assert.That(project.FrameworkVersion, Is.EqualTo("v4.0"));

            Assert.That(project.Configuration, Is.EqualTo("Debug"));
            Assert.That(project.Platform, Is.EqualTo("AnyCPU"));

            var cfg = project.Configurations.First(c => c.Configuration == "Debug");
            Assert.That(cfg.OutputPath, Is.EqualTo(@"bin\Debug\"));

            var nunitRef = project.DllReferences.First(r => r.Name == "nunit.framework");
            Assert.That(nunitRef.SpecificVersion, Is.False);
            Assert.That(nunitRef.Version, Is.EqualTo("2.5.10.11092"));
            Assert.That(nunitRef.Culture, Is.EqualTo("neutral"));
            Assert.That(nunitRef.ProcessorArchitecture, Is.EqualTo("MSIL"));
            Assert.That(nunitRef.PublicKey, Is.EqualTo("96d09a1eb7f44a77"));
            Assert.That(nunitRef.Path, Is.EqualTo(@"bin\Release\BuildTool.exe"));

            var projRef = project.ProjectReferences.First(r => r.Name == "BuildTool");
            Assert.That(projRef.Guid, Is.EqualTo("B30C9BA3-126A-4BDB-8ED6-A66C9BAD8BF1"));
            Assert.That(projRef.RelativePath, Is.EqualTo(@"..\BuildTool\BuildTool.csproj"));

            var items = project.Items;

            Assert.That(items.Any(i => i.Type == ProjectItemType.Compiled && i.Path == @"Navigation\ProjectInfoTests.cs"));
        }
    }


    [TestFixture]
    public class SolutionTests
    {
        private string GetSolutionFile(string resourceName)
        {
            var fileName = Path.Combine(Path.GetTempPath(), resourceName);
            using (var resource = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetType(), resourceName)))
            using (var file = new StreamWriter(File.OpenWrite(fileName)))
            {
                while (!resource.EndOfStream)
                {
                    file.WriteLine(resource.ReadLine());
                }
            }
            Console.WriteLine("Wrote resource {0} to {1}", resourceName, fileName);
            return fileName;
        }

        [Test]
        public void SolutionCanGetProjects()
        {
            var solutionFile = GetSolutionFile("SampleSolution.sln");
            var solution = new Solution(solutionFile);

            foreach (var project in solution.GetProjectInfos())
            {
                Console.WriteLine("{0} {1}", project.Name, project.ProjectFile);
            }
        }

        [Test]
        public void SolutionCanGetProjectsWithFilter()
        {
            var solutionFile = GetSolutionFile("SampleSolution.sln");
            var solution = new Solution(solutionFile);

            foreach (var project in solution.GetProjectInfos(p => p.Name.Contains("Tests")))
            {
                Console.WriteLine("{0} {1}", project.Name, project.ProjectFile);
            }
        }
    }
}