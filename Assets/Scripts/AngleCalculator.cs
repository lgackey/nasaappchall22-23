using System;
using UnityEngine;
using TMPro;

public class AngleCalculator : MonoBehaviour
{
    public Transform earth;
    public Transform player;
    TextMeshProUGUI txt;

    double latA;
    double longA;
    double latB;
    double longB;

    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMeshProUGUI>();
    }

    string azimText;
    string elevText;

    // Update is called once per frame
    void Update()
    {
        if(("" + this.getAzim()).Length < 9) {
            azimText = "" + this.getAzim();
        }
        else {
            azimText = ("" + this.getAzim()).Substring(0, 9);
        }

        if(("" + this.getElevation()).Length < 9) {
            elevText = "" + this.getElevation();
        }
        else {
            elevText = ("" + this.getElevation()).Substring(0, 9);
        }
        
        txt.SetText("Azimuth Angle: " + azimText + " Degrees" + "\n" + "Elevation Angle: " + elevText + " Degrees");
    }
    //https://stackoverflow.com/questions/1185408/converting-from-longitude-latitude-to-cartesian-coordinates  
    //https://gis.stackexchange.com/questions/108547/how-to-calculate-distance-azimuth-and-dip-from-two-xyz-coordinates

    float getAzim()
    {
        float az = (float)Math.Atan2(((float)earth.position.x - player.position.x), ((float)earth.position.z - player.position.z));
        az = (float)((az * 180)/Math.PI);
        az += 90;
        if(az < 0) {
            az += 360;
        }
        return az;
    }

    double getElevation()
    {
        double xAB = player.position.x - earth.position.x;
        double yAB = player.position.y - earth.position.y;
        double zAB = player.position.z - earth.position.z;
        double rangeAB = Math.Sqrt(xAB * xAB + yAB * yAB + zAB * zAB);
        double rz = xAB * Math.Cos(latA) * Math.Cos(longA) + zAB * Math.Cos(latA) * Math.Sin(longA) + yAB * Math.Sin(latA);
        double elevationAB = Math.Asin(rz/rangeAB);
        elevationAB = (double)((elevationAB*180)/Math.PI);
        return elevationAB;
    }
}