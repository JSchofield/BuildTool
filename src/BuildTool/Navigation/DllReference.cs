namespace BuildTool.Navigation
{
    public class DllReference
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Culture { get; set; }
        public string PublicKey { get; set; }
        public bool CopyLocal { get; set; }
        public string ProcessorArchitecture { get; set; }
        public string Path { get; set; }
        public bool SpecificVersion { get; set; }
    }

}
