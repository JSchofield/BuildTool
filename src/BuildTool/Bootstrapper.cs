using System;
using System.Collections.Generic;
using System.IO;

namespace BuildTool
{
    public class Bootstrapper: IDisposable
    {
        private readonly string _buildProject;
        private readonly Context _context;
        private readonly IOutputHandler _console;
        private readonly TextWriter _fileWriter;

        public Bootstrapper(Context context, string buildProject)
        {
            _buildProject = buildProject;
            
            _context = context;
            _context.ChildWorkingDirectory("Bootstrap");            
            _context.AddHandler(new HtmlOutputHandler(Path.Combine(_context.WorkingDirectory, "Bootstrap.log")));

            _console = new LineNumberingOutputHandler(Console.Out, Console.Error);
        }

        public void RunBuild(string buildProject, string[] args)
        {
            try
            {
                _context.Starting(new Command() { FileName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase, Arguments = string.Join(" ", args)});

                var executable = BuildProject(_buildProject);
                _context.ReceiveOutput("Launching: " + executable + " " + string.Join(" ", new string[] { @"/p=" + buildProject, args[0] }));
                InvokeBuildExecutable(executable, new string[] { @"/p=" + buildProject, args[0]} );
            }
            catch(Exception e)
            {
                Console.WriteLine("BOOTSTRAP ERROR:" + e.Message);
                Console.ReadLine();
            }
        }

        private string BuildProject(string project)
        {
            _context.ReceiveOutput("<a href=\"" + Path.Combine(_context.WorkingDirectory, "MSBuild.log") + "\">Building " + project + "</a>");
            _context.OutputHandlers.Add(new HtmlOutputHandler(Path.Combine(_context.WorkingDirectory, "MSBuild.log")));
            _context.OutputHandlers.Add(_console);

            return MSBuild.Run(_context, project);
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
                    context);

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
