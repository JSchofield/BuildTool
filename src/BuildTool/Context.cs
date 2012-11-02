using System.Collections.Generic;
using System.IO;

namespace BuildTool
{
    public class Context: IOutputHandler
    {
        public string WorkingDirectory { get; set; }
        public List<IOutputHandler> OutputHandlers;

        public Context()
        {
            OutputHandlers = new List<IOutputHandler>();
        }

        static public Context Default()
        {
            return new Context {
                WorkingDirectory = ".",
                OutputHandlers = new List<IOutputHandler>() };
        }

        public void AddHandler(IOutputHandler handler)
        {
            OutputHandlers.Add(handler);
        }

        public void ChildWorkingDirectory(string name)
        {
            this.WorkingDirectory = Path.Combine(this.WorkingDirectory, name);
            Directory.CreateDirectory(this.WorkingDirectory);
        }

        public void Starting(Command info)
        {
            foreach (var handler in OutputHandlers) { handler.Starting(info); }
        }

        public void ReceiveOutput(string output)
        {
            foreach (var handler in OutputHandlers) { handler.ReceiveOutput(output); }
        }

        public void ReceiveError(string error)
        {
            foreach (var handler in OutputHandlers) { handler.ReceiveError(error); }
        }

        public void Ending(int exitCode)
        {
            foreach (var handler in OutputHandlers) { handler.Ending(exitCode); }
        }
    }
}
