namespace MockProcess.Instructions
{
    public class StandardOutInstruction : IInstruction
    {
        private string _message;

        public StandardOutInstruction(string message)
        {
            this._message = message;
        }

        public void Run()
        {
            System.Console.WriteLine(_message);
        }
    }
}
