namespace BuildTool
{
    public interface IProcessWrapper
    {
        int RunAndWaitForExit();
        void RunStandalone();
    }
}
