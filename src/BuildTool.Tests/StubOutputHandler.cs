using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildTool.Tests
{
    public interface IProcessOutputs
    {
        IList<Command> Startings { get; }
        IList<string> Outputs { get; }
        IList<string> Errors { get; }
        IList<int> Endings { get; }
    }

    public class StubOutputHandler : IOutputHandler, IProcessOutputs
    {
        public IList<Command> Startings { get; private set; }
        public IList<string> Outputs { get; private set; }
        public IList<string> Errors { get; private set; }
        public IList<int> Endings { get; private set; }

        public StubOutputHandler()
        {
            Startings = new List<Command>();
            Outputs = new List<string>();
            Errors = new List<string>();
            Endings = new List<int>();
        }

        public void Starting(Command info)
        {
            Startings.Add(info);
            Console.WriteLine(string.Format("STARTING: FileName={0}; Arguments={1}", info.FileName, info.Arguments));
        }

        public void ReceiveOutput(string output)
        {
            Outputs.Add(output);
            Console.WriteLine("OUT: " + output);
        }

        public void ReceiveError(string error)
        {
            Errors.Add(error);
            Console.WriteLine("ERR: " + error);
        }

        public void Ending(int exitCode)
        {
            Endings.Add(exitCode);
            Console.WriteLine("EXIT: " + exitCode);
        }
    }

}
