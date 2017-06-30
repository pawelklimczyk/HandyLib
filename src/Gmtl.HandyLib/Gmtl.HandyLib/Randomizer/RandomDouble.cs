// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="RandomDouble.cs" project="Gmtl.HandyLib" date="2015-10-06 20:14">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib.Randomizer
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
        
        public double Next(double min, double max)
        {
            return Next(min, max, 0);
        }

        public double Next(double min, double max, int precision)
        {
            return NextDouble(min, max, precision);
        }

        public int Next(int min, int max)
        {
            return NextInt(min, max);
        }

        private double NextDouble(double min, double max, int precision)
        {
            if (min >= max) throw new ArgumentException("min must be lesser then max");

            return Math.Round(random.NextDouble(), precision) * (max - min) + min;
        }

        private int NextInt(int min, int max)
        {
            if (min >= max) throw new ArgumentException("min must be lesser then max");
            return random.Next(min, max);
        }
    }
}