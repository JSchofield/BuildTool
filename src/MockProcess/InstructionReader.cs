using System;
using System.Collections.Generic;
using System.IO;
namespace MockProcess
{
    /// <summary>
    /// Presents Instructions read from a TextReader as an enumeration.
    /// Actual creation of the instructions is delegated to a factory.
    /// </summary>
    public class InstructionReader : IEnumerable<IInstruction>
    {
        private readonly TextReader _instructionReader;
        private readonly ITextInstructionFactory _factory;

        public InstructionReader(TextReader instructions, ITextInstructionFactory factory)
        {
            this._instructionReader = instructions;
            this._factory = factory;
        }

        private IEnumerable<IInstruction> Instructions()
        {
            string line;
            while((line = _instructionReader.ReadLine()) != null)
            {
                yield return _factory.CreateInstruction(line);
            }
        }

        public IEnumerator<IInstruction> GetEnumerator()
        {
            return Instructions().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
