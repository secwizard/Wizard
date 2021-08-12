using System;
namespace AccountsReceivable.API.Entities.BaseEntities
{
    public abstract class IAuditableEntity
    {
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
