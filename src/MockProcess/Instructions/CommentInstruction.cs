namespace MockProcess.Instructions
{
    public class CommentInstruction: IInstruction
    {
        private string _comment;

        public CommentInstruction(string comment)
        {
            this._comment = comment;
        }

        public void Run()
        {
        }
    }
}
