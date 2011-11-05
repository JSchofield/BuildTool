using System;
using System.Diagnostics;

namespace BuildTool
{
    public class MSBuild
    {
        private const string msBuildExeFile = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";
        private readonly string _projectFile;
        private readonly ProcessRunner _process;

        private string _compileOutput;

        // State variable to keep track of when MSBuild is copying the final file
        private bool _outputFilesBeingCopied = false;

        public MSBuild(string projectFile)
        {
            _projectFile = projectFile;
            _process = CreateMSBuildProcess(projectFile);
        }

        private ProcessRunner CreateMSBuildProcess(string projectFile)
        {
            return new ProcessRunner(msBuildExeFile, projectFile, DataReceiver);
        }

        public string Run()
        {
            _process.Run();
            return _compileOutput;
        }

        private void DataReceiver(object sender, DataReceivedEventArgs e)
        {
            // Analyse output to get the path to the binary
            if (_outputFilesBeingCopied)
            {
                var index = e.Data.IndexOf(" -> ");
                if (index > 0)
                {
                    _compileOutput = e.Data.Substring(index + 4);
                }
                _outputFilesBeingCopied = false;
            }

            if (e.Data == "CopyFilesToOutputDirectory:")
            {
                _outputFilesBeingCopied = true;
            }
        }
    }
}
