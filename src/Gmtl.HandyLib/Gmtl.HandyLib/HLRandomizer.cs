// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLRandomizer.cs" project="Gmtl.HandyLib" date="2015-09-20 15:57:15">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using Gmtl.HandyLib.Randomizer;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy random data provider
    /// </summary>
    public class HLRandomizer
    {
        static HLRandomizer()
        {
            RandomString = new RandomString();
            RandomDouble = new RandomDouble();
        }
        /// <summary>
        /// Random string provider
        /// </summary>
        /// <remarks> 
        /// <code>
        /// string randomString1 = HLRandomizer.RandomString.Next();
        /// string randomString2 = HLRandomizer.RandomString.Next(100);//max length
        /// string randomString3 = HLRandomizer.RandomString.NextExact(10);
        /// string randomString4 = HLRandomizer.RandomString.Next(10, 100);//min 100 and max 100
        /// </code>
        /// </remarks>
        public static IRandomString RandomString { get; set; }

        /// <summary>
        /// Random double provider
        /// </summary>
        /// <remarks> 
        /// <code>
        /// double randomDouble = HLRandomizer.RandomDouble.Next(1.0, 100.0);
        /// </code>
        /// </remarks>
        public static IRandomDouble RandomDouble { get; set; }
    }
}