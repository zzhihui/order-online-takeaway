using System.Data.Entity;

namespace Core.Common.Infrastructure.EntityFramework
{
    public class AuditDbContext : DbContext
    {
        public AuditEntityFactory AuditEntityFactoryInstance { get; set; }

        public override int SaveChanges()
        {
            AuditChanges();
            return base.SaveChanges();
        }

        private void AuditChanges()
        {
            //Create Audit Factory
            AuditEntityFactoryInstance = new AuditEntityFactory();

            //Instance Audit Module from factory
            var auditingModule = AuditEntityFactoryInstance.CreateInstance();

            //Get Change Entities
            var changeTrackerEntities = this.ChangeTracker.Entries();

            foreach (var entry in changeTrackerEntities)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        auditingModule.AuditEntity(AuditEntityTracker.CovertModifyEntity(entry));

                        break;
                    case EntityState.Deleted:
                        auditingModule.AuditEntity(AuditEntityTracker.CovertRemoveEntity(entry));

                        break;
                    case EntityState.Added:
                        auditingModule.AuditEntity(AuditEntityTracker.CovertAddEntity(entry));

                        break;
                    default: break;
                }
            }


        }
    }
}