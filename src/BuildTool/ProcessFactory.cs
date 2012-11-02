using System.Collections.Generic;

namespace BuildTool
{
    public class ProcessFactory: IProcessFactory
    {
        public IProcessWrapper CreateProcess(Command command, string workingDirectory, IOutputHandler outputHandler)
        {
            return new ProcessWrapper(command, workingDirectory, outputHandler);
        }
    }
}
