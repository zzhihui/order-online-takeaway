using System.Configuration;
using Core.Common.Infrastructure.EntityFramework.AuditModules;

namespace Core.Common.Infrastructure.EntityFramework
{
    public class AuditEntityFactory
    {
        public IAuditingModule CreateInstance()
        {

            var auditModule = ConfigurationManager.AppSettings["EFLogger"];

            switch (auditModule)
            {
                default:
                    return new AuditingModuleLog4Net();

            }

        }
    }
}