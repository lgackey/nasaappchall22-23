using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public const int DATA_SIZE = 1277;
    public float[,] latitudeData = new float[DATA_SIZE, DATA_SIZE];
    public float[,] longitudeData = new float[DATA_SIZE, DATA_SIZE];
    public float[,] slopeData = new float[DATA_SIZE, DATA_SIZE];
    public float[,] heightData = new float[DATA_SIZE, DATA_SIZE];
    public float[,] azimuthData = new float[DATA_SIZE, DATA_SIZE];
    public float[,] elevationData = new float[DATA_SIZE, DATA_SIZE];

    public float minHeight = float.MaxValue;
    public float maxHeight = float.MinValue;
    public float minSlope = float.MaxValue;
    public float maxSlope = float.MinValue;
    public float minAzAngle = float.MaxValue;
    public float maxAzAngle = float.MinValue;
    public float minElAngle = float.MaxValue;
    public float maxElAngle = float.MinValue;

    const float LUNAR_RADIUS = 1737.4f * 1000; // in m

    public Transform earth;

    public void ReadCSV()
    {
        TextAsset latitudeAsset = Resources.Load<TextAsset>("Data/latitude_data");
        TextAsset longitudeAsset = Resources.Load<TextAsset>("Data/longitude_data");
        TextAsset slopeAsset = Resources.Load<TextAsset>("Data/slope_data");
        TextAsset heightAsset = Resources.Load<TextAsset>("Data/height_data");

        string[] latitudeSplitByLine = latitudeAsset.text.Split('\n');
        string[] longitudeSplitByLine = longitudeAsset.text.Split('\n');
        string[] slopeSplitByLine = slopeAsset.text.Split('\n');
        string[] heightSplitByLine = heightAsset.text.Split('\n');
        
        for (int i = 0; i < DATA_SIZE; i++)
        {
            string[] currentLatitudeRow = latitudeSplitByLine[i].Split(',');
            string[] currentLongitudeRow = longitudeSplitByLine[i].Split(',');
            string[] currentSlopeRow = slopeSplitByLine[i].Split(',');
            string[] currentHeightRow = heightSplitByLine[i].Split(',');

            for (int j = 0; j < DATA_SIZE; j++)
            {
                latitudeData[i, j] = float.Parse(currentLatitudeRow[j]);
                longitudeData[i, j] = float.Parse(currentLongitudeRow[j]);
                slopeData[i, j] = float.Parse(currentSlopeRow[j]);
                heightData[i, j] = float.Parse(currentHeightRow[j]);

                Vector3 currentCartesian = GetCartesian(i, j);
                float currentAzim = getAzim(currentCartesian.x, currentCartesian.y, currentCartesian.z);
                float currentElev = getElevation(currentCartesian.x, currentCartesian.y, currentCartesian.z);

                azimuthData[i, j] = currentAzim;
                elevationData[i, j] = currentElev;

                minHeight = Mathf.Min(minHeight, currentCartesian.y);
                maxHeight = Mathf.Max(maxHeight, currentCartesian.y);
                minSlope = Mathf.Min(minSlope, slopeData[i, j]);
                maxSlope = Mathf.Max(maxSlope, slopeData[i, j]);

                minAzAngle = Mathf.Min(minAzAngle, currentAzim);
                maxAzAngle = Mathf.Max(maxAzAngle, currentAzim);
                minElAngle = Mathf.Min(minElAngle, currentElev);
                maxElAngle = Mathf.Max(maxElAngle, currentElev);
            }
        }
    }

    public Vector3 GetCartesian(int i, int j)
    {
        float radius = LUNAR_RADIUS + heightData[i, j];
        radius /= 10;
        float x = radius * Mathf.Cos(Mathf.Deg2Rad * latitudeData[i, j]) * Mathf.Cos(Mathf.Deg2Rad * longitudeData[i, j]);
        float z = radius * Mathf.Cos(Mathf.Deg2Rad * latitudeData[i, j]) * Mathf.Sin(Mathf.Deg2Rad * longitudeData[i, j]);
        float y = -(radius * Mathf.Sin(Mathf.Deg2Rad * latitudeData[i, j]) + LUNAR_RADIUS / 10);
        

        return new Vector3(x, y, z);
    }

    float getAzim(float x, float y, float z)
    {
        float latA = (float)Math.Asin(earth.position.y/1737000);
        float longA = (float)Math.Atan2(earth.position.z, earth.position.x);
        float latB = (float)Math.Asin(y/1737000);
        float longB = (float)Math.Atan2(z, x);
        float az = (float)Math.Atan2((Math.Sin(longB - longA) * Math.Cos(latB)), ((Math.Cos(latA) * Math.Sin(latB)) - (Math.Sin(latA) * Math.Cos(latB) * Math.Cos(longB - longA))));
        az = (float)(az * 180.0)/(float)Math.PI;
        return az;
    }

    float getElevation(float x, float y, float z)
    {
        float latA = (float)Math.Asin(earth.position.y/1737000);
        float longA = (float)Math.Atan2(earth.position.z, earth.position.x);
        float latB = (float)Math.Asin(y/1737000);
        float longB = (float)Math.Atan2(z, x);
        float xAB = x - earth.position.x;
        float yAB = y - earth.position.y;
        float zAB = z - earth.position.z;
        float rangeAB = (float)Math.Sqrt(xAB * xAB + yAB * yAB + zAB * zAB);
        float rz = xAB * (float)Math.Cos(latA) * (float)Math.Cos(longA) + zAB * (float)Math.Cos(latA) * (float)Math.Sin(longA) + yAB * (float)Math.Sin(latA);
        float elevationAB = (float)Math.Asin(rz/rangeAB);
        return elevationAB;
    }
}
