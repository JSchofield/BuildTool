namespace BuildTool
{
    public class HtmlOutputHandler : IOutputHandler
    {
        private bool hasStarted = false;
        private bool hasEnded = false;

        public void Starting(Command info)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveOutput(string output)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveError(string error)
        {
            throw new System.NotImplementedException();
        }

        public void Ending(int exitCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
