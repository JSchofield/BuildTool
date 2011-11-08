using System;

namespace BuildTool
{
    public class MSBuildOutputHandler : IOutputHandler
    {
        public string CompileOutput { get; private set;}

        // State variable to keep track of when MSBuild is copying the final file
        private bool _outputFilesBeingCopied = false;

        void IOutputHandler.ReceiveOutput(string output)
        {
            // Analyse output to get the path to the binary
            if (_outputFilesBeingCopied)
            {
                var index = output.IndexOf(" -> ");
                if (index > 0)
                {
                    CompileOutput = output.Substring(index + 4);
                    _outputFilesBeingCopied = false;
                }
            }

            if (output == "CopyFilesToOutputDirectory:")
            {
                _outputFilesBeingCopied = true;
            }
        }

        void IOutputHandler.Starting(Command info)
        {
        }

        void IOutputHandler.ReceiveError(string error)
        {
        }

        void IOutputHandler.Ending(int exitCode)
        {
        }
    }
}
