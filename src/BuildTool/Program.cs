namespace BuildTool.Bootstrap
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper(@"C:\Users\jon\BuildTool\src\Example.Build\Example.Build.csproj");
            bootstrapper.RunBuild();
        }
    }
}
