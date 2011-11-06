namespace BuildTool
{
    public class ProcessFactory: IProcessFactory
    {
        public IProcessWrapper GetProcess(Command command, Context context)
        {
            return new ProcessWrapper(command, context.WorkingDirectory, context.OutputHandlers);
        }
    }
}
