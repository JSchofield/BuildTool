using System;
using System.Threading;

namespace MockProcess.Instructions
{
    public class WaitInstruction : IInstruction
    {
        public int Milliseconds { get; private set; }

        public WaitInstruction(int milliseconds)
        {
            this.Milliseconds = milliseconds;
        }

        public void Run()
        {
            Thread.Sleep(Milliseconds);
        }
    }
}
