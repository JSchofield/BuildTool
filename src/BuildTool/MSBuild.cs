using System;

namespace BuildTool
{
    public class MSBuild
    {
        private const string msBuildExeFile = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";

        public static string Run(Context context, string projectFile)
        {
            var outputHandler = new MSBuildOutputHandler();
            context.OutputHandlers.Add(outputHandler);

            var process = 
                new ProcessFactory().CreateProcess(
                    new Command {
                        FileName = msBuildExeFile,
                        Arguments = projectFile},
                    context.WorkingDirectory,
                    context);

            process.RunAndWaitForExit();
            return outputHandler.CompileOutput;
        }
    }
}
