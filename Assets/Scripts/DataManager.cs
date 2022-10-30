using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public const int DATA_SIZE = 1277;
    public float[,] latitudeData = new float[1277, 1277];
    public float[,] longitudeData = new float[1277, 1277];
    public float[,] slopeData = new float[1277, 1277];
    public float[,] heightData = new float[1277, 1277];

    const float LUNAR_RADIUS = 1737.4f * 1000; // in m

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

        for (int i = 0; i < 1277; i++)
        {
            string[] currentLatitudeRow = latitudeSplitByLine[i].Split(',');
            string[] currentLongitudeRow = longitudeSplitByLine[i].Split(',');
            string[] currentSlopeRow = slopeSplitByLine[i].Split(',');
            string[] currentHeightRow = heightSplitByLine[i].Split(',');

            for (int j = 0; j < 1277; j++)
            {
                latitudeData[i, j] = float.Parse(currentLatitudeRow[j]);
                longitudeData[i, j] = float.Parse(currentLongitudeRow[j]);
                slopeData[i, j] = float.Parse(currentSlopeRow[j]);
                heightData[i, j] = float.Parse(currentHeightRow[j]);
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
}