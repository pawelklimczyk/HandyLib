// -------------------------------------------------------------------------------------------------------------------
// <copyright file="IRandomText.cs" project="Gmtl.HandyLib" date="2015-09-20 15:57:37">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Random
{
    public interface IRandomString
    {
        string Next();
        string Next(int max);
        string Next(int min, int max);
    }
}