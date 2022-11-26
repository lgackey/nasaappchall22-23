using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class UtilFunctions : MonoBehaviour
    {
        private static float LUNAR_RADIUS = 1737.4F;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Returns aximuth from reference A to target B.
        public float azimuthAB(float latA, float longA, float latB, float longB)
        {
            return Mathf.Atan2(Mathf.Sin(longB - longA) * Mathf.Cos(latB),
                ((Mathf.Cos(latA) * Mathf.Sin(latB)) - (Mathf.Sin(latA) * Mathf.Cos(latB) * Mathf.Cos(longB - longA))));
        }

        /**
         * Following 3 methods convert from spherical to cartesian coordinates
         */
        public float getX(float lat, float lon, float height)
        {
            float radius = LUNAR_RADIUS + height;
            return radius * Mathf.Cos(lat) * Mathf.Cos(lon);
        }

        public float getY(float lat, float lon, float height)
        {
            float radius = LUNAR_RADIUS + height;
            return radius * Mathf.Cos(lat) * Mathf.Sin(lon);
        }

        public float getZ(float lat, float height)
        {
            return (LUNAR_RADIUS + height) * Mathf.Sin(lat);
        }

        // Returns difference in coordinates of two points
        public float[] coordinateDifference(float[] b, float[] a)
        {
            float[] ans = new float[3];
            for (int i = 0; i < 3; i++)
            {
                ans[i] = b[i] - a[i];
            }
            return ans;
        }

        // Returns distance between points given distances in x, y, z directions
        public float rangeAB(float[] dist)
        {
            return Mathf.Sqrt(dist[0] * dist[0] + dist[1] * dist[1] + dist[2] * dist[2]);
        }

        public float rz(float[] dist, float latA, float lonA)
        {
            return dist[0] * Mathf.Cos(latA) * Mathf.Cos(lonA) +
                dist[1] * Mathf.Cos(latA) * Mathf.Sin(lonA) +
                dist[2] * Mathf.Sin(latA);
        }

        public float elevation(float[] dist, float latA, float lonA)
        {
            return Mathf.Asin(rz(dist, latA, lonA) / rangeAB(dist));
        }
    }
}
