using System;
using NUnit.Framework;

namespace BuildTool.Tests
{
    [TestFixture]
    public class ProcessWrapperTests
    {
        /*
        Will need to write a MockProcess which be a command-line application, that will take a file as
        an argument. The file will contain instructions on how the app will behave
        EG:
         out: this is the first line to standard output
         out: this is the second line to standard output
         wait: 5000
         in: //wait for input
         err: This is the first line to standard err
         exit: 0 
         
        Then there will need to be a test helper that will write out these files and run the process
        through the process runner to set up the test.
        */
    }
}
