using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/*
namespace BuildTool
{
    public interface IContext
    {
        string WorkingDirectory { get; }
        int IndentLevel { get; }
        IContext CreateSiblingConext();
        IContext CreateChildContext();
    }

    public class DefaultContext: IContext
    {
        public string WorkingDirectory { get; private set;}
        public int IndentLevel { get; private set; }
        public IContext CreateSiblingConext();
        public IContext CreateChildContext();

        public static IContext EmptyContext()
        {
            return new DefaultContext() {
                WorkingDirectory = Environment.CurrentDirectory, 
                IndentLevel = 0 };
        }
    }

    public class BaseTask
    {
        private readonly IContext context;

        public BaseTask()
        {
            context = new EmptyContext();
        }

        public BaseTask(IContext context)
        {
            this.context = context;
        }
    }

    public class ShellTask: BaseTask
    {
        public IList<FileInfo> FilesOut { get; }
        public string StdOut { get; }
        public string StdErr { get; }
    }



}
*/