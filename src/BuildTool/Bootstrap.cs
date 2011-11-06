using System;

namespace BuildTool
{
    public class Bootstrapper
    {
        private readonly string _buildProject;

        public Bootstrapper(string buildProject)
        {
            _buildProject = buildProject;
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
                Console.WriteLine("ERROR:" + e.Message);
            }
            Console.ReadLine();
        }

        private string BuildProject(string project)
        {
            var boostrapBuild = new MSBuild(project);
            return boostrapBuild.Run();
        }

        private void InvokeBuildExecutable(string executable, string[] args)
        {
            Console.WriteLine(string.Format("{0} {1}", executable, args[0]));
            var invokeBuildExe = new ProcessRunner(new ProcessInfo { FileName = executable, Arguments = string.Join(" ", args), WorkingDirectory = "." });
            invokeBuildExe.Run();
        }
    }
}
