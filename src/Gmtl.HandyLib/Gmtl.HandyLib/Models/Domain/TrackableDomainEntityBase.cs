using System;

namespace Gmtl.HandyLib.Models.Domain
{
    /// <summary>
    /// Represents a base class for domain entities that are trackable.
    /// Provides properties for tracking update and deletion dates.
    /// </summary>
    /// <typeparam name="T">The type of the entity's identifier.</typeparam>
    public abstract class TrackableDomainEntityBase<T> : DomainEntityBase<T>, IWithUpdateDate, IWithDeleteDate
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated. First update is set to CreateDate.
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was deleted, if applicable.
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }
}
