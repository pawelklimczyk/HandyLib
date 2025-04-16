using System;

namespace Gmtl.HandyLib.Models.Domain
{
    /// <summary>
    /// Represents a base class for domain entities with a generic identifier and a creation date.
    /// </summary>
    /// <typeparam name="T">The type of the identifier for the domain entity.</typeparam>
    public abstract class DomainEntityBase<T> : EntityBase<T>, IWithCreateDate
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
