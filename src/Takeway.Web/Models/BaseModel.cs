using System;

namespace Takeway.Web.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string Guid { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}