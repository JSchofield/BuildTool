namespace MockProcess.Instructions
{
    public class StandardErrorInstruction : IInstruction
    {
        public string Message { get; private set; }

        public StandardErrorInstruction(string message)
        {
            this.Message = message;
        }

        public void Run()
        {
            System.Console.Error.WriteLine(Message);
        }
    }
}
