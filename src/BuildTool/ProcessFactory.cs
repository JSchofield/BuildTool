namespace BuildTool
{
    public class ProcessFactory: IProcessFactory
    {
        public IProcessWrapper CreateProcess(Command command, Context context)
        {
            return new ProcessWrapper(command, context.WorkingDirectory, context.OutputHandlers);
        }
    }
}
