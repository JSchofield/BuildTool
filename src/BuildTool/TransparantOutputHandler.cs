using System.IO;
namespace BuildTool
{
    public class TransparantOutputHandler : IOutputHandler
    {
        private readonly TextWriter _standardOut;
        private readonly TextWriter _standardError;

        public TransparantOutputHandler(TextWriter standardOut, TextWriter standardError)
        {
            _standardOut = standardOut;
            _standardError = standardError;
        }

        public void Starting(Command info)
        {
        }

        public void ReceiveOutput(string output)
        {
            _standardOut.WriteLine(output);
            _standardOut.Flush();
        }

        public void ReceiveError(string error)
        {
            _standardError.WriteLine(error);
            _standardError.Flush();
        }

        public void Ending(int exitCode)
        {
        }
    }
}
