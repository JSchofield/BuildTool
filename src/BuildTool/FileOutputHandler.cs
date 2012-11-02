using System;
using System.IO;

namespace BuildTool
{
    public class FileOutputHandler : IOutputHandler
    {
        private readonly IOutputHandler handler;

        public FileOutputHandler(string logFile)
        {
            var fileWriter = new StreamWriter(logFile);

            handler = new HtmlOutputHandler(fileWriter);
        }

        public void Starting(Command info)
        {
            handler.Starting(info);
        }

        public void ReceiveOutput(string output)
        {
            handler.ReceiveOutput(output);
        }

        public void ReceiveError(string error)
        {
            handler.ReceiveError(error);
        }

        public void Ending(int exitCode)
        {
            handler.Ending(exitCode);
        }
    }
}
