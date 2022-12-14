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

    // Update is called once per frame
    void Update()
    {
        latA = Math.Asin(earth.position.y/1737000);
        longA = Math.Atan2(earth.position.z, earth.position.x);
        latB = Math.Asin(player.position.y/1737000);
        longB = Math.Atan2(player.position.z, player.position.x);

        txt.SetText("Azimuth Angle " + ("" + this.getAzim()).Substring(0, 7) + "\nElevation Angle " + ("" + this.getElevation()).Substring(0, 6));
    }
    //https://sciencing.com/slope-circle-8216787.html   

    double getAzim()
    {
        double az = (float)Math.Atan2((Math.Sin(longB - longA) * Math.Cos(latB)), ((Math.Cos(latA) * Math.Sin(latB)) - (Math.Sin(latA) * Math.Cos(latB) * Math.Cos(longB - longA))));
        az = (az * 180.0)/Math.PI;
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
        return elevationAB;
    }
}