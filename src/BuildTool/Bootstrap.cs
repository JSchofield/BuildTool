using System;
using System.Collections.Generic;
using System.IO;

namespace BuildTool
{
    public class Bootstrapper: IDisposable
    {
        private readonly string _buildProject;
        private readonly IOutputHandler _logFile;
        private readonly IOutputHandler _console;
        private readonly TextWriter _fileWriter;

        public Bootstrapper(string buildProject)
        {
            _fileWriter = new StreamWriter("Bootstrap.log");
            _buildProject = buildProject;

            _logFile = new LineNumberingOutputHandler(_fileWriter, _fileWriter);
            _console = new LineNumberingOutputHandler(Console.Out, Console.Error);
        }

        public void RunBuild()
        {
            try
            {
                var executable = BuildProject(_buildProject);
                InvokeBuildExecutable(executable, new string[1] { _buildProject });
            }
            catch(Exception e)
            {
                Console.WriteLine("BOOTSTRAP ERROR:" + e.Message);
                Console.ReadLine();
            }
        }

        private string BuildProject(string project)
        {
            var context = new Context { WorkingDirectory = ".", OutputHandlers = new List<IOutputHandler>() };
            context.OutputHandlers.Add(_logFile);
            context.OutputHandlers.Add(_console);

            var boostrapBuild = new MSBuild(context, project);
            return boostrapBuild.Run();
        }

        private void InvokeBuildExecutable(string executable, string[] args)
        {
            var context = new Context { WorkingDirectory = ".", OutputHandlers = new List<IOutputHandler>() };
            context.OutputHandlers.Add(_console);

            var invokeBuildExe =
                new ProcessFactory().CreateProcess(
                    new Command { 
                        FileName = executable, 
                        Arguments = string.Join(" ", args) },
                    context.WorkingDirectory,
                    context.OutputHandlers);

            invokeBuildExe.RunStandalone();
        }

        public void Dispose()
        {
            if (_fileWriter != null)
            {
                _fileWriter.Dispose();
            }
        }
    }
}
