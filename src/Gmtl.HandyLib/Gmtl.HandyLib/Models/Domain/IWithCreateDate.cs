using System;

namespace Gmtl.HandyLib.Models.Domain
{
    public interface IWithCreateDate
    {
        DateTime CreatedDate { get; set; }
    }
}
