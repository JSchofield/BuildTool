namespace MockProcess.Instructions
{
    public class StandardErrorInstruction : IInstruction
    {
        private string _message;

        public StandardErrorInstruction(string message)
        {
            this._message = message;
        }

        public void Run()
        {
            System.Console.Error.WriteLine(_message);
        }
    }
}
