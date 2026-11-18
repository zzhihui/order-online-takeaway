using System;
using Takeaway.Data.Entities.Health;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class SyncStatusBuilder
    {
        private readonly SyncStatus _entity = new SyncStatus();

         public SyncStatusBuilder()
         {
             _entity.Created = DateTime.Now;
            
        }



        public SyncStatusBuilder WithCheckTime(DateTime? custom = null)
        {
            _entity.CheckTime = custom ?? DateTime.Now;
            return this;
        }

        public SyncStatus Build()
        {
            return _entity;
        } 
    }
}