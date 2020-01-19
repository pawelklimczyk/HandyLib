using System;

namespace Gmtl.HandyLib.Models
{
    public class DomainModelBase
    {
        protected bool SetNonEmptyStringValue(string input, ref string result)
        {
            if (!String.IsNullOrWhiteSpace(input))
            {
                result = input.Trim();
                return true;
            }

            return false;
        }
    }
}
