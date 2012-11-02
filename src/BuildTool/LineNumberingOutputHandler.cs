using System;
using System.IO;

namespace BuildTool
{
    public class LineNumberingOutputHandler: IOutputHandler
    {
        private readonly TextWriter _standardOut;
        private readonly TextWriter _standardError;

        private int _linecount = 0;

        public LineNumberingOutputHandler(TextWriter standardOut, TextWriter standardError)
        {
            _standardOut = standardOut;
            _standardError = standardError;
        }

        public void Starting(Command info)
        {
            _standardOut.WriteLine(string.Format("STARTING PROCESS: {0} {1}", info.FileName, info.Arguments));
            _standardOut.WriteLine("-------------------------------------------------------------------------------------");
            _standardOut.Flush();
        }

        public void ReceiveOutput(string output)
        {
            _linecount++;
            _standardOut.WriteLine(string.Format("{0}: {1}", _linecount.ToString("0000"), output));
            _standardOut.Flush();
        }

        public void ReceiveError(string error)
        {
            _linecount++;
            _standardError.WriteLine(string.Format("{0} ERROR: {1}", _linecount.ToString("0000"), error));
            _standardError.Flush();
        }

        public void Ending(int exitCode)
        {
            _standardOut.WriteLine(string.Format("EXITED WITH CODE {0}", exitCode));
            _standardOut.Flush();
        }
    }
}
