using System.IO;
using System.Collections.Generic;

namespace MockProcess
{                    
    public class InstructionsFileProcessor
    {
        public void Run(string instructionsFile)
        {
            using (var fileReader = new StreamReader(instructionsFile))
            {
                var instructions = new InstructionReader(fileReader, new TextInstructionFactory());
                Run(instructions);
            }
        }

        public void Run(IEnumerable<IInstruction> instructions)
        {
            foreach (IInstruction instruction in instructions)
            {
                instruction.Run();
            }
        }
    }
}
