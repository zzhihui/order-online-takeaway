using System.Configuration;

namespace Core.Common.Infrastructure.Configuration
{
    public class DependencyAssemblyElementCollection : ConfigurationElementCollection
    {
        public DependencyAssemblyElement this[int index]
        {
            get { return BaseGet(index) as DependencyAssemblyElement; }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DependencyAssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DependencyAssemblyElement)element).AssemblyName;
        }
    }
}