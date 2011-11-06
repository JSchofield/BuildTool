using System;
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

            var build = new MSBuild(GetTargetProject(args[0], "Example.Target"));
            var output = build.Run();

            Console.WriteLine(string.Format("Here it is: {0}", output));

        }

        static private string GetTargetProject(string buildProject, string projectName)
        {
            var dir = Path.GetDirectoryName(Path.GetDirectoryName(buildProject));
            string target = Path.Combine(dir, projectName, projectName + ".csproj");
            return target;
        }

    }
}
