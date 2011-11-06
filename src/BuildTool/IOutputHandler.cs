namespace BuildTool
{
    public interface IOutputHandler
    {
        void Starting(Command info);
        void ReceiveOutput(string output);
        void ReceiveError(string error);
        void Ending(int exitCode);
    }
}