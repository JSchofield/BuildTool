using System;

namespace BuildTool
{
    public class MSBuild
    {
        private const string msBuildExeFile = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";

        private readonly Context _context;
        private readonly IProcessWrapper _process;
        private readonly MSBuildOutputHandler _outputHandler;

        public MSBuild(Context context, string projectFile)
        {
            _context = context;
            _outputHandler = new MSBuildOutputHandler();
            _context.OutputHandlers.Add(_outputHandler);
            _process = CreateMSBuildProcess(projectFile);
        }

        private IProcessWrapper CreateMSBuildProcess(string projectFile)
        {
            return 
                new ProcessFactory().CreateProcess(
                    new Command { 
                        FileName = msBuildExeFile, 
                        Arguments = projectFile },
                    _context.WorkingDirectory,
                    _context.OutputHandlers);
        }

        public string Run()
        {
            _process.RunAndWaitForExit();
            return _outputHandler.CompileOutput;
        }
    }
}
