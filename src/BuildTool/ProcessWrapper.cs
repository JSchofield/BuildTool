using System.Diagnostics;

namespace BuildTool
{
    public class ProcessWrapper : IProcessWrapper
    {
        private Command _info;
        private string _workingDir;
        private IOutputHandler _outputHandler;

        public ProcessWrapper(Command info, string workingDir, IOutputHandler outputHandler)
        {
            _info = info;
            _workingDir = workingDir;
            _outputHandler = outputHandler;
        }

        private void ReceiveError(object sender, DataReceivedEventArgs e)
        {
            _outputHandler.ReceiveError(e.Data);
        }

        private void ReceiveOutput(object sender, DataReceivedEventArgs e)
        {
            _outputHandler.ReceiveOutput(e.Data);
        }

        public int RunAndWaitForExit()
        {
            _outputHandler.Starting(_info);

            int exitCode;
            using (Process process = CreateProcess())
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
