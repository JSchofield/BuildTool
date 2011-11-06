namespace BuildTool
{
    public class MSBuild: IOutputHandler
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
            return new ProcessRunner(new Command { FileName = msBuildExeFile, Arguments = projectFile },new Context{ WorkingDirectory = ".", LogFile="Log.txt"}, this);
        }

        public string Run()
        {
            _process.Run();
            return _compileOutput;
        }

        void IOutputHandler.Starting(Command info)
        {
        }

        void IOutputHandler.ReceiveOutput(string output)
        {
            // Analyse output to get the path to the binary
            if (_outputFilesBeingCopied)
            {
                var index = output.IndexOf(" -> ");
                if (index > 0)
                {
                    _compileOutput = output.Substring(index + 4);
                }
                _outputFilesBeingCopied = false;
            }

            if (output == "CopyFilesToOutputDirectory:")
            {
                _outputFilesBeingCopied = true;
            }
        }

        void IOutputHandler.ReceiveError(string error)
        {
        }

        void IOutputHandler.Ending(int exitCode)
        {
        }
    }
}
