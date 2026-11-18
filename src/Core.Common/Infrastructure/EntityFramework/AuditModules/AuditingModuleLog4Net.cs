using System;
using Core.Common.Utility;
using log4net;

namespace Core.Common.Infrastructure.EntityFramework.AuditModules
{
    public interface IAuditingModule
    {
        void AuditEntity(object auditItem);
    }

    public class AuditingModuleLog4Net : IAuditingModule
    {
        private static readonly ILog Logger = LogManager.GetLogger("entityAudit");
        public void AuditEntity(object auditItem)
        {
            var ent = (AuditEntityTracker)auditItem;
            var xml = new SerializeHelper().SerializeObject(ent);
            Logger.InfoFormat("Audit: {0} => {1}; {2}", ent.EntityType, xml, Environment.NewLine);

        }
    }
}