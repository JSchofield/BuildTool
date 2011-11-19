using System;
using MockProcess.Instructions;

namespace MockProcess
{
    /// <summary>
    /// Parses instructions in the format CODE: {argument}.
    /// Supported instructions are:
    ///     IN:
    ///     OUT: {message}
    ///     ERR: {message}
    ///     WAIT: {milliseconds}
    ///     REM: {comment} 
    /// </summary>
    public class TextInstructionFactory : ITextInstructionFactory
    {
        public IInstruction CreateInstruction(string instruction)
        {
            string[] instructionArgs = instruction.Split(':');
            string code = instructionArgs[0];
            string args = instructionArgs[1].TrimStart(' ','\t');

            switch (code)
            {
                case "OUT": return new StandardOutInstruction(args);
                case "WAIT": return CreateWaitInstruction(args);
                case "REM": return new CommentInstruction(args);
                case "ERR": return new StandardErrorInstruction(args);
                case "IN": return new StandardInInstruction();
                default: throw new InvalidOperationException(string.Format("Unknown code '{0}.'", code));
            }
        }

        private IInstruction CreateWaitInstruction(string args)
        {
            return new WaitInstruction(int.Parse(args));
        }
    }
}