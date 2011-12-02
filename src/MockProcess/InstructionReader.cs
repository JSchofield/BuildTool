using System;
using System.Collections.Generic;
using System.IO;

namespace MockProcess
{
    /// <summary>
    /// Presents IInstructions read from a TextReader as an enumeration.
    /// Actual creation of the instructions is delegated to a factory.
    /// </summary>
    public class InstructionReader : IEnumerable<IInstruction>
    {
        private readonly TextReader _instructionReader;
        private readonly ITextInstructionFactory _factory;

        public InstructionReader(TextReader instructionsText, ITextInstructionFactory factory)
        {
            if (instructionsText == null)
            {
                throw new ArgumentException("Argument cannot be null.", "instructionsText");
            }

            if (factory == null)
            {
                throw new ArgumentException("Argument cannot be null.", "factory");
            }

            this._instructionReader = instructionsText;
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
