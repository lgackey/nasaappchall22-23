using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class UtilFunctions : MonoBehaviour
    {
        private static double LUNAR_RADIUS = 1737.4;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Returns aximuth from reference A to target B.
        public double azimuthAB(double latA, double longA, double latB, double longB)
        {
            return Math.Atan2(Math.Sin(longB - longA) * Math.Cos(latB),
                ((Math.Cos(latA) * Math.Sin(latB)) - (Math.Sin(latA) * Math.Cos(latB) * Math.Cos(longB - longA))));
        }

        /**
         * Following 3 methods convert from spherical to cartesian coordinates
         */
        public double getX(double lat, double lon, double height)
        {
            double radius = LUNAR_RADIUS + height;
            return radius * Math.Cos(lat) * Math.Cos(lon);
        }

        public double getY(double lat, double lon, double height)
        {
            double radius = LUNAR_RADIUS + height;
            return radius * Math.Cos(lat) * Math.Sin(lon);
        }

        public double getZ(double lat, double height)
        {
            return (LUNAR_RADIUS + height) * Math.Sin(lat);
        }

        // Returns difference in coordinates of two points
        public double[] coordinateDifference(double[] b, double[] a)
        {
            double[] ans = new double[3];
            for (int i = 0; i < 3; i++)
            {
                ans[i] = b[i] - a[i];
            }
            return ans;
        }

        // Returns distance between points given distances in x, y, z directions
        public double rangeAB(double[] dist)
        {
            return Math.Sqrt(dist[0] * dist[0] + dist[1] * dist[1] + dist[2] * dist[2]);
        }

        public double rz(double[] dist, double latA, double lonA)
        {
            return dist[0] * Math.Cos(latA) * Math.Cos(lonA) +
                dist[1] * Math.Cos(latA) * Math.Sin(lonA) +
                dist[2] * Math.Sin(latA);
        }

        public double elevation(double[] dist, double latA, double lonA)
        {
            return Math.Asin(rz(dist, latA, lonA) / rangeAB(dist));
        }
    }
}
