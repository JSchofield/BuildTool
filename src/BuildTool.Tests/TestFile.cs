using System;
using System.IO;

namespace BuildTool.Tests
{
    public class TestFile : IDisposable
    {
        public string Location { get; private set; }

        public TestFile(string contents): this()
        {
            File.WriteAllText(Location, contents);
        }

        public TestFile()
        {
            Location = Path.GetTempFileName();
        }

        public string Contents
        {
            get { return File.ReadAllText(Location); }
        }

        public void Dispose()
        {
            if (File.Exists(Location)) { File.Delete(Location); }
            GC.SuppressFinalize(this);
        }
    }
}
