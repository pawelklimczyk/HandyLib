// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="RandomDouble.cs" project="Gmtl.HandyLib" date="2015-10-06 20:14">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib.Random
{
    internal class RandomDouble : IRandomDouble
    {
        private static readonly System.Random random = new System.Random();
        private const int defaultPrecision = 5;

        public double Next()
        {
            return Next(0, random.NextDouble(), defaultPrecision);
        }

        public double Next(int precision)
        {
            return Next(0, random.Next(), precision);
        }

        public double Next(double max, int precision)
        {
            return Next(0, max, precision);
        }

        public double Next(double min, double max, int precision)
        {
            //TODO min=-8.99 max -4.11 -> issue
            double minMin = min - Math.Truncate(min);
            double maxMax = max - Math.Truncate(max);
            if (minMin >= maxMax) throw new ArgumentException("min must be lesser then max");

            return Math.Round(random.NextDouble(), precision) + random.Next((int)min, (int)max);
        }
    }
}