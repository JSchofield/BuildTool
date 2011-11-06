namespace BuildTool
{
    public interface IOutputHandler
    {
        void ReceiveOutput(string output);
        void ReceiveError(string error);
    }
}