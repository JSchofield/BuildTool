using System;
using System.IO;

namespace BuildTool
{

    public class ProcessRunner: IOutputHandler
    {
        private readonly Command _processInfo;
        private readonly Context _context;
        private readonly IOutputHandler[] _outputHandlers;
        private int _linecount;

        public ProcessRunner(Command processInfo, Context context, params IOutputHandler[] outputHandlers)
        {
            if (string.IsNullOrEmpty(processInfo.FileName))
            {
                throw new ArgumentException("FileName cannot be null or empty.");
            }
            _processInfo = processInfo;
            _context = context;
            _outputHandlers = outputHandlers;
        }
        
        public int Run()
        {
            _linecount = 0;

            IProcessWrapper process = new ProcessWrapper(_processInfo, _context.WorkingDirectory, this);
            return process.RunAndWaitForExit();
        }

        void IOutputHandler.Starting(Command info)
        {
            Console.WriteLine("STARTING PROCESS");
            Console.WriteLine(string.Format("CMD: {0} {1}", info.FileName, info.Arguments));
            Console.WriteLine(string.Format("DIR: {0}", _context.WorkingDirectory));
            Console.WriteLine(string.Format("LOG: {0}", _context.LogFile));
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var handler in _outputHandlers)
            {
                handler.Starting(info);
            }
        }

        void IOutputHandler.ReceiveOutput(string output)
        {
            Console.WriteLine(string.Format("{0}: {1}", _linecount++, output));
            foreach (var handler in _outputHandlers)
            {
                handler.ReceiveOutput(output);
            }
        }

        void IOutputHandler.ReceiveError(string error)
        {
            Console.WriteLine(string.Format("{0} ERROR: {1}", _linecount++, error));
            foreach (var handler in _outputHandlers)
            {
                handler.ReceiveError(error);
            }
        }

        void IOutputHandler.Ending(int exitCode)
        {
            Console.WriteLine(string.Format("EXITED WITH CODE {0}", exitCode));
            foreach (var handler in _outputHandlers)
            {
                handler.Ending(exitCode);
            }
        }
    }
}
