using System;
using System.IO;

namespace BuildTool
{
    public class ProcessRunner
    {
        private readonly IProcessFactory _processFactory;
        private readonly Command _command;
        private readonly Context _context;
        private readonly IOutputHandler[] _outputHandlers;
        private int _linecount;

        public ProcessRunner(IProcessFactory processFactory, Command command, Context context)
        {
            if (string.IsNullOrEmpty(command.FileName))
            {
                throw new ArgumentException("FileName cannot be null or empty.");
            }
            _processFactory = processFactory;
            _command = command;
            _context = context;
        }
        
        public int Run()
        {
            _linecount = 0;

            IProcessWrapper process = _processFactory.GetProcess(_command, _context);
            return process.RunAndWaitForExit();
        }      
    }
}
