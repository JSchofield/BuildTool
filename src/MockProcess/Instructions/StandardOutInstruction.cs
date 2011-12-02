namespace MockProcess.Instructions
{
    public class StandardOutInstruction : IInstruction
    {
        public string Message { get; private set; }

        public StandardOutInstruction(string message)
        {
            this.Message = message;
        }

        public void Run()
        {
            System.Console.WriteLine(Message);
        }
    }
}
