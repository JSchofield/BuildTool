using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BuildTool.Navigation
{
    public class Solution
    {
        private IEnumerable<ProjectInfo> _projects;

        public Solution(string solutionFile)
        {
            var solutionDir = Path.GetDirectoryName(solutionFile);

            var projects = new List<ProjectInfo>();
            using (var file = File.OpenText(solutionFile))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("Project"))
                    {
                        projects.Add(new ProjectInfo(solutionDir, line));
                    }
                }
            }
            _projects = projects;
        }

        public IEnumerable<ProjectInfo> GetProjectInfos()
        {
            return GetProjectInfos(p => true);
        }

        public IEnumerable<ProjectInfo> GetProjectInfos(Func<ProjectInfo, bool> predicate)
        {
            return _projects.Where(predicate);
        }

        public IEnumerable<Project> LoadProjects()
        {
            return _projects.Select(info => new Project(info.ProjectFile));
        }

    }
}
