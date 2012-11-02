using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using BuildTool;
using System.Linq;
using BuildTool.Navigation;

namespace Example.Build
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Example.Build");
            Console.WriteLine("Args=" + string.Join(" ", args));

            var context = new Context { WorkingDirectory = @"C:\Users\Jon\BuildTool\BuildOutput", OutputHandlers = new List<IOutputHandler>() };
            context.WorkingDirectory = Path.Combine(context.WorkingDirectory, "Compile");
            Directory.CreateDirectory(context.WorkingDirectory);
            var logFile = new HtmlOutputHandler(new StreamWriter(Path.Combine(context.WorkingDirectory, "MSBuild.log")));
            var console = new LineNumberingOutputHandler(Console.Out, Console.Error);
            context.OutputHandlers.Add(logFile);
            context.OutputHandlers.Add(console);

            var solution = FindSolution.InParentDirectory("BuildTool.sln");
            var projects = solution.LoadProjects().Where(proj => !proj.Name.EndsWith("Tests"));

            var assemblies = projects.Select(p => MSBuild.Run(context, p.ProjectFile)).ToList();
            
            Console.WriteLine(string.Format("Here it is: {0}", assemblies));
           // Console.ReadLine();
        }

        static private string GetTargetProject(string projectName)
        {
            var dir = GetSolutionDirectory();
            string target = Path.Combine(dir, projectName, projectName + ".csproj");
            return target;
        }

        static private string GetSolutionDirectory()
        {
            var dir = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            while (!ContainsSolutionFile(dir))
            {
                dir = Path.GetDirectoryName(dir);
            }
            return dir;
        }

        static private bool ContainsSolutionFile(string directory)
        {
            return Directory.GetFiles(directory, "*.sln").Any();
        }
    }
}

