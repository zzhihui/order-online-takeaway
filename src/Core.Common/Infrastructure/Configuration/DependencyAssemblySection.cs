using System.Configuration;

namespace Core.Common.Infrastructure.Configuration
{
    public class DependencyAssemblySection : ConfigurationSection
    {
        [ConfigurationProperty("assemblies", IsRequired = false)]
        public DependencyAssemblyElementCollection Assemblies
        {
            get { return this["assemblies"] as DependencyAssemblyElementCollection; }
        }
    }
}