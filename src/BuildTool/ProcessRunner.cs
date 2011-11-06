using System;

namespace BuildTool
{
    public class ProcessRunner: IOutputHandler
    {
        private readonly ProcessInfo _processInfo;
        private readonly string _arguments;
        private readonly IOutputHandler[] _outputHandlers;
        private int _linecount;

        public ProcessRunner(ProcessInfo processInfo, params IOutputHandler[] outputHandlers)
        {
            if (string.IsNullOrEmpty(processInfo.FileName))
            {
                throw new ArgumentException("FileName cannot be null or empty.");
            }
            _processInfo = processInfo;
            _outputHandlers = outputHandlers;
        }
        
        public void Run()
        {
            _linecount = 0;

            IProcessWrapper process = new ProcessWrapper(_processInfo, this);
            process.RunAndWaitForExit();
        }

        public void ReceiveOutput(string output)
        {
            foreach (var handler in _outputHandlers)
            {
                handler.ReceiveOutput(output);
            }
            Console.WriteLine(string.Format("{0}: {1}", _linecount++, output));
        }

        public void ReceiveError(string error)
        {
            foreach (var handler in _outputHandlers)
            {
                handler.ReceiveError(error);
            }
            Console.WriteLine(string.Format("{0} ERROR: {1}", _linecount++, error));
        }
    }
}
