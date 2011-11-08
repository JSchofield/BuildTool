using System.Collections.Generic;

namespace BuildTool
{
    public struct Context
    {
        public string WorkingDirectory { get; set; }
        public List<IOutputHandler> OutputHandlers;

        static public Context Default()
        {
            return new Context {
                WorkingDirectory = ".",
                OutputHandlers = new List<IOutputHandler>() };
        }
    }
}
