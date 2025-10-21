using System;

namespace Gmtl.HandyLib
{
    public static class HLGeo
    {
        /// <summary>
        /// Calculates distance between two geo coordinates in kilometers
        /// </summary>
        public static double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth's radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = (Math.Sin(dLat / 2) * Math.Sin(dLat / 2)) +
                       (Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        public static double ToRadians(double angle) => angle * Math.PI / 180;

    }
}
