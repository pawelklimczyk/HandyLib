// -------------------------------------------------------------------------------------------------------------------
// <copyright file="Randomizer.cs" project="Gmtl.HandyLib" date="2015-09-20 15:49:43">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Random
{
    public class Randomizer
    {
        static Randomizer()
        {
            RandomString = new RandomString();
            RandomDouble = new RandomDouble();
        }

        public static IRandomString RandomString { get; set; }
        public static IRandomDouble RandomDouble { get; set; }
    }
}