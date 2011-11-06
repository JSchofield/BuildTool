using System;
using System.Diagnostics;

namespace BuildTool
{
    public class ProcessWrapper : IProcessWrapper
    {
        private Command _command;
        private string _workingDir;
        private IOutputHandler[] _outputHandlers;

        public ProcessWrapper(Command command, string workingDir, params IOutputHandler[] outputHandlers)
        {
            _command = command;
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

        public void Run()
        {
            foreach (var handler in _outputHandlers) { handler.Starting(_command); }

            Process process = CreateProcess();
            process.Start();
            Console.WriteLine("started");
        }

        public int RunAndWaitForExit()
        {
            foreach (var handler in _outputHandlers) { handler.Starting(_command); }

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
                        FileName = _command.FileName,
                        Arguments = _command.Arguments,
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
