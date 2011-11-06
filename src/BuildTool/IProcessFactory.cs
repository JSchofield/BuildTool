namespace BuildTool
{
    public interface IProcessFactory
    {
        IProcessWrapper CreateProcess(Command command, Context context);
    }
}
