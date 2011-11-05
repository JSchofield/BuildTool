using System;
using System.IO;
using System.Diagnostics;

namespace Example.Build
{
    public class Program
    {
        static void Main(string[] args)
        {
            string project = args[0];
            string dir = Path.GetDirectoryName(project);
            dir = Path.GetDirectoryName(dir);
            string target = Path.Combine(dir, "Example.Target", "Example.Target.csproj");
            Console.WriteLine(target);
            var output = BuildProject(target);
            Console.WriteLine(string.Format("Here it is: {0}", output));
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
                outputFilesBeingCopied = false;
            }

            Console.WriteLine(string.Format("X{0}: {1}", linecount, e.Data));
            if (e.Data == "CopyFilesToOutputDirectory:")
            {
                outputFilesBeingCopied = true;
            }
        }
    }
}
