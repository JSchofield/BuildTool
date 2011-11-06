namespace BuildTool
{
    public interface IProcessFactory
    {
        IProcessWrapper GetProcess(Command command, Context context);
    }
}
