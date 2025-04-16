using System;

namespace Gmtl.HandyLib.Models.Domain
{
    public interface IWithUpdateDate
    {
        DateTime UpdateDate { get; set; }
    }
}
