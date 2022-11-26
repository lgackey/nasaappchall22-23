using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reader
{
    public class CSVReader : MonoBehaviour
    {

        public double[,] latitudeData = new double[1277, 1277];
        public double[,] longitudeData = new double[1277, 1277];
        public double[,] slopeData = new double[1277, 1277];
        public double[,] heightData = new double[1277, 1277];

        // Start is called before the first frame update
        public void Start()
        {
            TextAsset latitudeAsset = Resources.Load<TextAsset>("Data/FY20 ADC Regional Data File LATITUDE");
            TextAsset longitudeAsset = Resources.Load<TextAsset>("Data/FY20 ADC Regional Data File LONGITUDE");
            TextAsset slopeAsset = Resources.Load<TextAsset>("Data/FY20 ADC Regional Data File SLOPE");
            TextAsset heightAsset = Resources.Load<TextAsset>("Data/FY20 ADC Regional Data File HEIGHT");

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
                    latitudeData[i, j] = double.Parse(currentLatitudeRow[j]);
                    longitudeData[i, j] = double.Parse(currentLongitudeRow[j]);
                    slopeData[i, j] = double.Parse(currentSlopeRow[j]);
                    heightData[i, j] = double.Parse(currentHeightRow[j]);
                }
            }

            /*for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Debug.Log(latitudeData[i, j]);
                }
            }*/
        }
    }
}
