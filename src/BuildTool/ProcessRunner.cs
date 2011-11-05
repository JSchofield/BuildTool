using System;
using System.Diagnostics;

namespace BuildTool
{
    public class ProcessRunner
    {
        private readonly string _fileName;
        private readonly string _arguments;
        private readonly DataReceivedEventHandler[] _outputReceivers;
        private int _linecount;

        public ProcessRunner(string fileName, string arguments, params DataReceivedEventHandler[] outputReceivers)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName cannot be null or empty.");
            }
            _fileName = fileName;
            _arguments = arguments;
            _outputReceivers = outputReceivers;
        }

        private void ReceiveError(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(string.Format("ERROR: {0}", e.Data));
        }

        private void ReceiveOutput(object sender, DataReceivedEventArgs e)
        {
            _linecount += 1;
            Console.WriteLine(string.Format("{0}: {1}", _linecount, e.Data));
        }

        public void Run()
        {
            _linecount = 0;
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = _fileName,
                Arguments = _arguments,
                WorkingDirectory = ".",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process p = new Process();

            p.ErrorDataReceived += new DataReceivedEventHandler(ReceiveError);
            p.OutputDataReceived += new DataReceivedEventHandler(ReceiveOutput);
            foreach (var receiver in _outputReceivers)
            {
                p.OutputDataReceived += receiver;
            }
            p.StartInfo = startInfo;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();


            p.WaitForExit();
            p.Close();
        }
    }
}
