using System;
using System.Threading;

namespace MockProcess.Instructions
{
    public class WaitInstruction : IInstruction
    {
        private int _milliseconds;

        public WaitInstruction(int milliseconds)
        {
            this._milliseconds = milliseconds;
        }

        public void Run()
        {
            Thread.Sleep(_milliseconds);
        }
    }
}
