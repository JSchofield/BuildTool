using System.Collections.Generic;

namespace BuildTool
{
    public interface IProcessFactory
    {
        IProcessWrapper CreateProcess(Command command, string workingDirectory, IList<IOutputHandler> outputHandlers);
    }
}
