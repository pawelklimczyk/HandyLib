// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="IRandomString.cs" project="Gmtl.HandyLib" date="2015-09-20 15:57:37">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Randomizer
{
    public interface IRandomString
    {
        string Next();
        string Next(int max);
        string NextExact(int exactStringLength);
        string Next(int min, int max);
    }
}   