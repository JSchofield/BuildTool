using System.Diagnostics;

namespace BuildTool
{
    public class ProcessWrapper : IProcessWrapper
    {
        private ProcessInfo _info;
        private IOutputHandler _outputHandler;

        public ProcessWrapper(ProcessInfo info, IOutputHandler outputHandler)
        {
            _info = info;
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

        public void RunAndWaitForExit()
        {
            Process process = CreateProcess();

            process.Start();

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            process.WaitForExit();
            process.Close();
        }

        private Process CreateProcess()
        {
            Process process =
                new Process() {
                    StartInfo = new ProcessStartInfo {
                        FileName = _info.FileName,
                        Arguments = _info.Arguments,
                        WorkingDirectory = _info.WorkingDirectory,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false } };

            process.ErrorDataReceived += new DataReceivedEventHandler(ReceiveError);
            process.OutputDataReceived += new DataReceivedEventHandler(ReceiveOutput);

            return process;
        }
    }
}
