using System.IO;

namespace BuildTool.Navigation
{
    public class ProjectInfo
    {
        public ProjectInfo(string baseDir, string projectRef)
        {
            string[] fields = projectRef.Split('=', ',');

            char[] trimChars = new char[4] { ' ', '"', '{', '}' };

            TypeGuid = fields[0].Substring(10, 36);
            Name = fields[1].Trim(trimChars);
            ProjectFile = Path.GetFullPath(Path.Combine(baseDir, fields[2].Trim(trimChars)));
            Guid = fields[3].Trim(trimChars);
        }

        public string Guid { get; private set; }
        public string TypeGuid { get; private set; }
        public string Name { get; private set; }
        public string ProjectFile { get; private set; }
    }
}
