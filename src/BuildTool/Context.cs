namespace BuildTool
{
    public class Context
    {
        public string WorkingDirectory { get; set; }
        public string LogFile { get; set; }
        public IOutputHandler[] OutputHandlers { get; set; }
    }
}
