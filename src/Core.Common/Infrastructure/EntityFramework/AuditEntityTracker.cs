using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Core.Common.Infrastructure.EntityFramework
{
    public class AuditEntityTracker
    {
        public string Action { get; set; }
        public string EntityType { get; set; }
        public List<AuditEntityTrackerProperty> CurrentValue { get; set; }
        public List<AuditEntityTrackerProperty> OriginalValue { get; set; }

        public static AuditEntityTracker CovertModifyEntity(DbEntityEntry entry)
        {
            var auditEntityTracker = new AuditEntityTracker { Action = AuditAction.Modify.ToString(), EntityType = entry.GetType().ToString() };

            FillInPropertieValue(auditEntityTracker, entry.CurrentValues);

            FillInPropertieValue(auditEntityTracker, entry.OriginalValues);

            return auditEntityTracker;

        }

        public static AuditEntityTracker CovertRemoveEntity(DbEntityEntry entry)
        {
            var auditEntityTracker = new AuditEntityTracker { Action = AuditAction.Add.ToString(), EntityType = entry.GetType().ToString() };

            FillInPropertieValue(auditEntityTracker, entry.CurrentValues);

            return auditEntityTracker;
        }

        public static AuditEntityTracker CovertAddEntity(DbEntityEntry entry)
        {
            var auditEntityTracker = new AuditEntityTracker { Action = AuditAction.Delete.ToString(), EntityType = entry.GetType().ToString() };

            FillInPropertieValue(auditEntityTracker, entry.OriginalValues);

            return auditEntityTracker;
        }


        private static void FillInPropertieValue(AuditEntityTracker auditEntityTracker, DbPropertyValues entityValues)
        {
            foreach (var propertyName in entityValues.PropertyNames)
            {

                var auditTrackProp = new AuditEntityTrackerProperty()
                {
                    PropertyName = propertyName,
                    PropertyValue = entityValues[propertyName].ToString()
                };
                auditEntityTracker.CurrentValue.Add(auditTrackProp);

            }

        }
    }
}