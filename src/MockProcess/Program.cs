﻿using System;
using System.IO;

namespace MockProcess
{
    class Program
    {
        static void Main(string[] args)
        {
           if (args.Length > 0)
           {
                string instructionsFile = args[0];
                var processor = new InstructionsFileProcessor();
                processor.Run(instructionsFile);
           }
        }
    }

}
