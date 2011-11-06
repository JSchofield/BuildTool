namespace BuildTool
{
    public interface IProcessWrapper
    {
        void Run();
        int RunAndWaitForExit();
    }
}
