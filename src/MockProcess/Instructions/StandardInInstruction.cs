using System;

namespace MockProcess.Instructions
{
    public class StandardInInstruction : IInstruction
    {
        public void Run()
        {
            Console.ReadLine();
        }
    }
}
