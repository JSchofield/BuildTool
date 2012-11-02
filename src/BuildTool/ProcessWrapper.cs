using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BuildTool
{
    public class ProcessWrapper : IProcessWrapper
    {
        private Command _command;
        private string _workingDir;
        private IOutputHandler _outputHandler;

        public ProcessWrapper(Command command, string workingDir, IOutputHandler outputHandlers)
        {
            _command = command;
            _workingDir = workingDir;
            _outputHandler = outputHandlers;
        }

        private void ReceiveError(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) { return; }
            _outputHandler.ReceiveError(e.Data); 
 
        }

        private void ReceiveOutput(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) { return; }
            _outputHandler.ReceiveOutput(e.Data); 
        }

        public void RunStandalone()
        {
            using (Process process = CreateProcess(true))
            {
                process.Start();
            }
        }

        public int RunAndWaitForExit()
        {
            _outputHandler.Starting(_command);

            int exitCode;
            using (Process process = CreateProcess(false))
            {
                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();
                exitCode = process.ExitCode;
            }
            _outputHandler.Ending(exitCode);
            return exitCode;
        }

        private Process CreateProcess(bool shell)
        {
            Process process =
                new Process() {
                    StartInfo = new ProcessStartInfo {
                        FileName = _command.FileName,
                        Arguments = _command.Arguments,
                        WorkingDirectory = _workingDir,
                        RedirectStandardError = !shell,
                        RedirectStandardOutput = !shell,
                        UseShellExecute = shell } };

            if (!shell)
            {
                process.ErrorDataReceived += new DataReceivedEventHandler(ReceiveError);
                process.OutputDataReceived += new DataReceivedEventHandler(ReceiveOutput);
            }

            return process;
        }
    }
}
