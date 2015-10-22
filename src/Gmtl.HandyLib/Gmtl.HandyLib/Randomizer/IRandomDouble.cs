// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="IRandomDouble.cs" project="Gmtl.HandyLib" date="2015-10-06 20:13:55">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Randomizer
{
    public interface IRandomDouble
    {
        double Next();
        double Next(int precision);
        double Next(double max, int precision);
        double Next(double min, double max, int precision);
    }
}