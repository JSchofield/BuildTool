using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace BuildTool.Navigation
{
    public static class FindSolution
    {
        static public Solution InParentDirectory(string solutionFileName)
        {
            var dir = new DirectoryInfo(".");
            do
            {
                dir = dir.Parent;
                var files = dir.GetFiles(solutionFileName);
                if (files.Any())
                {
                    return new Solution(files.First().FullName);
                }
            } while (dir != dir.Root);

            return new Solution(solutionFileName);
        }
    }
}
