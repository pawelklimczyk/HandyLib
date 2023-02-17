using System;

namespace Gmtl.HandyLib.Models.Domain
{
    public interface IWithDeleteDate
    {
        DateTime? DeletedDate { get; set; }
    }
}
