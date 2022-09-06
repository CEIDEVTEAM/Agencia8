using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    static public class DistanceExtensions
    {
        public const double EarthRadius = 6371;
        public static double GetDistance(GeoCoordinate point1, GeoCoordinate point2)
        {
            double distance = 0;
            double Lat = (point2.Latitude - point1.Latitude) * (Math.PI / 180);
            double Lon = (point2.Longitude - point1.Longitude) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitude * (Math.PI / 180)) * Math.Cos(point2.Latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = (EarthRadius * c) * 1000;
            distance = Math.Round(distance);
            return distance;
        }
    }
}
