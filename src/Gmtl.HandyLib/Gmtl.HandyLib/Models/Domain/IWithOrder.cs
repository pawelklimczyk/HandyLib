namespace Gmtl.HandyLib.Models.Domain
{
    /// <summary>
    /// Represents an entity with a sortable order.
    /// </summary>
    public interface IWithSortOrder
    {
        /// <summary>
        /// Gets or sets the sort order of the entity.
        /// </summary>
        int SortOrder { get; set; }
    }
}
