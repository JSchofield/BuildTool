using System.Collections.Generic;

namespace BuildTool
{
    public class ProcessFactory: IProcessFactory
    {
        public IProcessWrapper CreateProcess(Command command, string workingDirectory, IList<IOutputHandler> outputHandlers)
        {
            return new ProcessWrapper(command, workingDirectory, outputHandlers);
        }
    }
}
