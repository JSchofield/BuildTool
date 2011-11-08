using System;
using System.IO;

namespace BuildTool
{
    public class FileOutputHandler
    {
        public static IOutputHandler Create(string logFile)
        {
            var fileWriter = new StreamWriter(logFile);

            return new LineNumberingOutputHandler(fileWriter, fileWriter);
        }
    }
}
