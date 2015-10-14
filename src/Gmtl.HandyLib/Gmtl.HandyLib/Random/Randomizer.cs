// -------------------------------------------------------------------------------------------------------------------
// <copyright file="Randomizer.cs" project="Gmtl.HandyLib" date="2015-09-20 15:49:43">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

namespace Gmtl.HandyLib.Random
{
    public class Randomizer : IRandomizer
    {
        private static Randomizer instance;

        public static Randomizer Instance
        {
            get { return instance ?? (instance = InitDefault()); }
            set { instance = value; }
        }

        private static Randomizer InitDefault()
        {
            Randomizer randomizer = new Randomizer
            {
                RandomString = new RandomString(),
                RandomDouble = new RandomDouble()
            };

            return randomizer;
        }

        public IRandomString RandomString { get; set; }
        public IRandomDouble RandomDouble { get; set; }
    }
}