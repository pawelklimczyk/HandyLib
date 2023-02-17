using System;

namespace Gmtl.HandyLib.Models.Domain
{
    public abstract class DomainEntityBase<T> : EntityBase<T>, IWithCreateDate
    {
        public DateTime CreatedDate { get; set; }
    }
}
