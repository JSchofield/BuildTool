using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;

namespace BuildTool.Navigation
{
    public class Project
    {
        public IEnumerable<ProjectConfiguration> Configurations { get; private set; }
        public IEnumerable<DllReference> DllReferences { get; private set; }
        public IEnumerable<ProjectReference> ProjectReferences { get; private set; }
        public IEnumerable<ProjectItem> Items { get; private set; }

        public string Name { get; private set; }
        public string OutputType { get; private set; }
        public string AssemblyName { get; private set; }
        public string FrameworkVersion { get; private set; }
        public string Configuration { get; private set; }
        public string Platform { get; private set; }
        public string ProjectFile { get; private set; }

        private XmlNamespaceManager _nsmgr;

        public Project(string projectFile)
        {
            ProjectFile = projectFile;
            Name = Path.GetFileNameWithoutExtension(projectFile);


            XmlDocument projectXml = new XmlDocument();
            projectXml.Load(projectFile);
            _nsmgr = new XmlNamespaceManager(projectXml.NameTable);
            _nsmgr.AddNamespace("msd", "http://schemas.microsoft.com/developer/msbuild/2003");

            var globalProperties = projectXml.SelectSingleNode("/msd:Project/msd:PropertyGroup[not(@*)]", _nsmgr);

            if (globalProperties == null)
            {
                throw new InvalidOperationException("Could not find global property group in the project file");
            }

            OutputType = GetChildElementValueSafely(globalProperties, "OutputType");
            AssemblyName = GetChildElementValueSafely(globalProperties, "AssemblyName");
            FrameworkVersion = GetChildElementValueSafely(globalProperties, "TargetFrameworkVersion");
            Configuration = GetChildElementValueSafely(globalProperties, "Configuration");
            Platform = GetChildElementValueSafely(globalProperties, "Platform");


            var configurations = new List<ProjectConfiguration>();
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:PropertyGroup[@Condition]", _nsmgr))
            {
                configurations.Add(GetConfiguration(node));
            }
            Configurations = configurations;

            var dllReferences = new List<DllReference>();
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:ItemGroup/msd:Reference", _nsmgr))
            {
                dllReferences.Add(GetDllReference(node));
            }
            DllReferences = dllReferences;

            var projectReferences = new List<ProjectReference>();
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:ItemGroup/msd:ProjectReference", _nsmgr))
            {
                projectReferences.Add(GetProjectReference(node));
            }
            ProjectReferences = projectReferences;

            var items = new List<ProjectItem>();
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:ItemGroup/msd:Compile", _nsmgr))
            {
                items.Add(new ProjectItem { Type = ProjectItemType.Compiled, Path = node.Attributes["Include"].Value });
            }
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:ItemGroup/msd:Content", _nsmgr))
            {
                items.Add(new ProjectItem { Type = ProjectItemType.Content, Path = node.Attributes["Include"].Value });
            }
            foreach (XmlNode node in projectXml.SelectNodes("/msd:Project/msd:ItemGroup/msd:EmbeddedResource", _nsmgr))
            {
                items.Add(new ProjectItem { Type = ProjectItemType.EmbeddedResource, Path = node.Attributes["Include"].Value });
            }


            Items = items;
        }

        private ProjectReference GetProjectReference(XmlNode parent)
        {
            return new ProjectReference
            {
                RelativePath = parent.Attributes["Include"].Value,
                Name = GetChildElementValueSafely(parent, "Name"),
                Guid = GetChildElementValueSafely(parent, "Project").Trim('{', '}')
            };
        }

        private DllReference GetDllReference(XmlNode parent)
        {
            var itemArray = parent.Attributes["Include"].Value.Split(',');

            var includeAttributes = itemArray.Skip(1).Select(item => item.Split('=')).ToDictionary(item => item[0].Trim(' '), item => item[1].Trim(' '));

            var reference = new DllReference { Name = itemArray[0].Trim(' '), CopyLocal = false };

            reference.Version = GetValue(includeAttributes, "Version", null);
            reference.Culture = GetValue(includeAttributes, "Culture", null);
            reference.PublicKey = GetValue(includeAttributes, "PublicKeyToken", null);
            reference.ProcessorArchitecture = GetValue(includeAttributes, "processorArchitecture", null);

            reference.CopyLocal = bool.Parse(GetChildElementValueSafely(parent, "Private", "False"));
            reference.SpecificVersion = bool.Parse(GetChildElementValueSafely(parent, "SpecificVersion", "False"));
            reference.Path = GetChildElementValueSafely(parent, "HintPath", null);

            return reference;
        }


        private string GetValue(IDictionary<string, string> dictionary, string key, string defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return defaultValue;
        }

        private ProjectConfiguration GetConfiguration(XmlNode parent)
        {
            var configPlatform = parent.SelectSingleNode("@Condition").Value;
            configPlatform = configPlatform.Substring(configPlatform.LastIndexOf('=') + 1).Trim(' ', '\'');
            var cf = configPlatform.Split('|');

            return new ProjectConfiguration
            {
                Configuration = cf[0],
                Platform = cf[1],
                OutputPath = GetChildElementValueSafely(parent, "OutputPath")
            };
        }

        private string GetChildElementValueSafely(XmlNode parent, string childName, string defaultValue = null)
        {
            var node = parent.SelectSingleNode("msd:" + childName, _nsmgr);
            if (node != null)
            {
                return node.InnerText;
            }
            return defaultValue;
        }
    }

}
