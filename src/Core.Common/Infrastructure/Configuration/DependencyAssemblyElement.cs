using System.Configuration;

namespace Core.Common.Infrastructure.Configuration
{
    public class DependencyAssemblyElement : ConfigurationElement
    {

        [ConfigurationProperty("assemblyname", IsRequired = true)]
        public string AssemblyName
        {
            get { return this["assemblyname"] as string; }
        }


        [ConfigurationProperty("description", IsRequired = true)]
        public string Description
        {
            get { return this["description"] as string; }
        }

    }
}