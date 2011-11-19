using System;
using System.Collections.Generic;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.IO;

namespace BuildTool
{
    public class MSBuildLog: Logger, ILogger
    {
        private TextWriter _streamWriter;
        public IList<BuildWarningEventArgs> Warnings { get; set; }
        public IList<BuildErrorEventArgs> Errors { get; set; }
        public IList<ProjectFinishedEventArgs> Projects { get; set; }
        public IList<BuildFinishedEventArgs> Builds { get; set; }
        
        public override void Initialize(IEventSource eventSource)
        {
            if (null == Parameters)
			{
				throw new LoggerException("Log file was not set.");
			}
			string[] parameters = Parameters.Split(';');
			
			string logFile = parameters[0];
			if (String.IsNullOrEmpty(logFile))
			{
				throw new LoggerException("Log file was not set.");
			}
			
			if (parameters.Length > 1)
			{
				throw new LoggerException("Too many parameters passed.");
			}


            // Open the file
            _streamWriter = new StreamWriter(logFile);

            //eventSource.BuildStarted += new BuildStartedEventHandler(BuildStarted);
            eventSource.BuildFinished += new BuildFinishedEventHandler(BuildFinished);
            eventSource.ErrorRaised += new BuildErrorEventHandler(ErrorRaised);
            //eventSource.MessageRaised += new BuildMessageEventHandler(MessageRaised);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(ProjectFinished);
            //eventSource.ProjectStarted += new ProjectStartedEventHandler(ProjectStarted);
            //eventSource.StatusEventRaised += new BuildStatusEventHandler(StatusEventRaised);
            //eventSource.TargetFinished += new TargetFinishedEventHandler(TargetFinished);
            //eventSource.TargetStarted += new TargetStartedEventHandler(TargetStarted);
            //eventSource.TaskFinished += new TaskFinishedEventHandler(TaskFinished);
            //eventSource.TaskStarted += new TaskStartedEventHandler(TaskStarted);
            eventSource.WarningRaised += new BuildWarningEventHandler(WarningRaised);
        }

        void WarningRaised(object sender, BuildWarningEventArgs e)
        {
            Warnings.Add(e);
        }

        void TaskStarted(object sender, TaskStartedEventArgs e)
        {
        }

        void TaskFinished(object sender, TaskFinishedEventArgs e)
        {
        }

        void TargetStarted(object sender, TargetStartedEventArgs e)
        {
        }

        void TargetFinished(object sender, TargetFinishedEventArgs e)
        {
        }

        void StatusEventRaised(object sender, BuildStatusEventArgs e)
        {
        }

        void ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
        }

        void ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            Projects.Add(e);
        }

        void MessageRaised(object sender, BuildMessageEventArgs e)
        {
        }

        void ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            Errors.Add(e);
        }

        void BuildFinished(object sender, BuildFinishedEventArgs e)
        {
        }

        void BuildStarted(object sender, BuildStartedEventArgs e)
        {
        }
    }
}
