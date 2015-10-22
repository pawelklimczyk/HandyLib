// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLRandomizer.cs" project="Gmtl.HandyLib" date="2015-09-20 15:57:15">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using Gmtl.HandyLib.Randomizer;

namespace Gmtl.HandyLib
{
    public class HLRandomizer
    {
        static HLRandomizer()
        {
            RandomString = new RandomString();
            RandomDouble = new RandomDouble();
        }

        public static IRandomString RandomString { get; set; }
        public static IRandomDouble RandomDouble { get; set; }
    }
}