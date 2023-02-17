using System;

namespace Gmtl.HandyLib.Models.Domain
{
    public abstract class TrackableDomainEntityBase<T> : DomainEntityBase<T>, IWithUpdateDate, IWithDeleteDate
    {
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
