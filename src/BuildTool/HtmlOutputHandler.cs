using System;
using System.IO;

namespace BuildTool
{
    public class HtmlOutputHandler : IOutputHandler
    {
        private readonly TextWriter _writer;

        public HtmlOutputHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public HtmlOutputHandler(string path)
        {
            _writer = new StreamWriter(path);
        }

        public void Starting(Command info)
        {
            _writer.WriteLine("<html>");
            _writer.WriteLine("<head>");
            _writer.WriteLine("<style>");
            var ass = System.Reflection.Assembly.GetExecutingAssembly();
            using(var stream = ass.GetManifestResourceStream("BuildTool.HtmlOutputHandler.css"))
            {
                int b;
                stream.ReadByte();
                stream.ReadByte();
                stream.ReadByte();
                while((b = stream.ReadByte()) >= 0)
                {
                    _writer.Write(((char)b));
                }
            }            
            _writer.WriteLine("\n</style>");
            _writer.WriteLine("</head>");
            _writer.WriteLine("<body>");
            _writer.WriteLine("<div class='header'>");
            _writer.WriteLine("<div class='start'>");
            _writer.WriteLine(string.Format("<div class='fileName'>{0}</div><div class='args'>{1}</div>", info.FileName, info.Arguments));
            _writer.WriteLine("</div>");
            _writer.WriteLine("</div>");
            _writer.WriteLine("<table class='output' cellspacing='0'>");
            _writer.Flush();
        }

        public void ReceiveOutput(string output)
        {
            _writer.WriteLine("<tr class='out'><td class='time'>{0}</td><td class='msg'>{1}</td></tr>", DateTime.Now.ToString("HH:mm:ss.fff"), output);
            _writer.Flush();
        }

        public void ReceiveError(string error)
        {
            _writer.WriteLine("<tr class='err'><td class='time'>{0}</td><td class='msg'>{1}</td></tr>", DateTime.Now, error);
            _writer.Flush();
        }

        public void Ending(int exitCode)
        {
            _writer.WriteLine("</table>");
            _writer.WriteLine(string.Format("<div class='footer'>Exit Code {0}</div>", exitCode));
            _writer.WriteLine("</body>");
            _writer.WriteLine("</html>");
            _writer.Flush();


        }
    }
}
