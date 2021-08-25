using System;
namespace AccountsReceivable.API.Entities.BaseEntities
{
    public abstract class IAuditableEntity
    {
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
