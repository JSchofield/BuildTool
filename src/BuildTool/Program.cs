using System;
using System.IO;
using System.Diagnostics;

namespace BuildTool.Bootstrap
{
    public class Program
    {
        static void Main(string[] args)
        {
            string buildProjectFile = (args.Length > 0) ? args[0] : null;
            var projectFile = FindProjectFile(buildProjectFile);
            projectFile = @"C:\Users\jon\BuildTool\src\Example.Build\Example.Build.csproj";
            var executable = BuildProject(projectFile);

            string[] remainingArgs = new string[1];
            remainingArgs[0] = projectFile;
            //if (args.Length >= 1)
           // {
            //    Array.Copy(args, 1, remainingArgs, 1, args.Length - 1);
           // }
            var result = InvokeBuildExecutable(executable, remainingArgs);
            Console.ReadLine();

        }

        private static string FindProjectFile(string buildProject)
        {
            var workingDirectory = new DirectoryInfo(System.Environment.CurrentDirectory);
            string[] files = new string[0];
            while(files.Length == 0 && workingDirectory != null)
            {                 
                files = Directory.GetFiles(workingDirectory.ToString(), "*.csproj", SearchOption.TopDirectoryOnly);
                workingDirectory = Directory.GetParent(workingDirectory.ToString());
            }

            return files[0];
        }

        private static string BuildProject(string project)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe",
                Arguments = project,
                WorkingDirectory = ".",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            linecount = 0;
            Process p = new Process();

            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.StartInfo = startInfo;
            p.Start();
            p.BeginOutputReadLine();


            p.WaitForExit();
            p.Close();

            return compileOutput;

            // Read the standard output of the spawned process.

        }

        private static string compileOutput;
        private static int linecount;
        private static bool outputFilesBeingCopied = false;
        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            linecount += 1;
            if (outputFilesBeingCopied)
            {
                var index = e.Data.IndexOf(" -> ");
                if (index > 0)
                {
                    compileOutput = e.Data.Substring(e.Data.IndexOf(" -> ") + 4);
                }
                    /*
                else
                {
                    index = e.Data.IndexOf(" to ");
                    if (index > 0)
                    {
                        compileOutput = e.Data.Substring(e.Data.IndexOf(" -> ") + 4);
                    }
                }
                     * */
                outputFilesBeingCopied = false;
            }

            Console.WriteLine(string.Format("{0}: {1}", linecount, e.Data));
            if (e.Data == "CopyFilesToOutputDirectory:")
            {
                outputFilesBeingCopied = true;
            }
        }

        private static string InvokeBuildExecutable(string executable, string[] args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = executable,
                Arguments = string.Join(" ", args),
                WorkingDirectory = ".",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            linecount = 0;
            Process p = new Process();

            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.StartInfo = startInfo;
            p.Start();
            p.BeginOutputReadLine();


            p.WaitForExit();
            p.Close();

            return "done";
        }

    }
}
