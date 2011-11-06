using System.Diagnostics;
using System.Linq;
using System;

namespace BuildTool
{
    public class ProcessWrapper : IProcessWrapper
    {
        private Command _info;
        private string _workingDir;
        private IOutputHandler[] _outputHandlers;

        public ProcessWrapper(Command info, string workingDir, params IOutputHandler[] outputHandlers)
        {
            _info = info;
            _workingDir = workingDir;
            _outputHandlers = outputHandlers;
        }

        private void ReceiveError(object sender, DataReceivedEventArgs e)
        {
            foreach (var handler in _outputHandlers) { handler.ReceiveError(e.Data); }
        }

        private void ReceiveOutput(object sender, DataReceivedEventArgs e)
        {
            foreach (var handler in _outputHandlers) { handler.ReceiveOutput(e.Data); }
        }

        public int RunAndWaitForExit()
        {
            foreach (var handler in _outputHandlers) { handler.Starting(_info); }

            int exitCode;
            using (Process process = CreateProcess())
            {
                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();
                exitCode = process.ExitCode;
            }
            foreach (var handler in _outputHandlers) { handler.Ending(exitCode); }
            return exitCode;
        }

        private Process CreateProcess()
        {
            Process process =
                new Process() {
                    StartInfo = new ProcessStartInfo {
                        FileName = _info.FileName,
                        Arguments = _info.Arguments,
                        WorkingDirectory = _workingDir,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false } };

            process.ErrorDataReceived += new DataReceivedEventHandler(ReceiveError);
            process.OutputDataReceived += new DataReceivedEventHandler(ReceiveOutput);

            return process;
        }
    }
}
