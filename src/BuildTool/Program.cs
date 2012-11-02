namespace BuildTool.Bootstrap
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper =
                new Bootstrapper(
                    new Context() { WorkingDirectory = @"C:\Users\Jon\BuildTool\BuildOutput" },
                    @"C:\Users\jon\BuildTool\src\Example.Build\Example.Build.csproj");

            bootstrapper.RunBuild(@"C:\Users\jon\BuildTool\src\Example.Build\Example.Build.csproj", args);
        }
    }
}
