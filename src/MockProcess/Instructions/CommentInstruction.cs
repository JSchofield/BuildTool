namespace MockProcess.Instructions
{
    public class CommentInstruction: IInstruction
    {
        public string Comment { get; private set; }

        public CommentInstruction(string comment)
        {
            this.Comment = comment;
        }

        public void Run()
        {
        }
    }
}
