using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using BuildTool;

namespace Example.Build
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Example.Build");

            var logFile = FileOutputHandler.Create("ExampleBuild.log");
            var console = new LineNumberingOutputHandler(Console.Out, Console.Error);

            var context = new Context { WorkingDirectory = ".", OutputHandlers = new List<IOutputHandler>() };
            context.OutputHandlers.Add(logFile);
            context.OutputHandlers.Add(console);

            var build = new MSBuild(context, GetTargetProject(args[0], "Example.Target"));
            var output = build.Run();

            Console.WriteLine(string.Format("Here it is: {0}", output));
            Console.ReadLine();
        }

        static private string GetTargetProject(string buildProject, string projectName)
        {
            var dir = Path.GetDirectoryName(Path.GetDirectoryName(buildProject));
            string target = Path.Combine(dir, projectName, projectName + ".csproj");
            return target;
        }
    }
}
