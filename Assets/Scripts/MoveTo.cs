using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform earth;
    public TrailRenderer tr;
    public TrailRenderer tr2;
    public Transform goal;
    public Transform navCylinder;
    public NavMeshAgent agent;

    public Transform[] destinations = new Transform[10];

    public float latA;
    public float longA;
    public float latB;
    public float longB;
    private int destinationsIndex = 1;
    //public gameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        navCylinder = this.GetComponentInParent<Transform>();
        agent.destination = goal.position;
        latA = (float)Math.Asin(earth.position.y/1737000);
        longA = (float)Math.Atan2(earth.position.z, earth.position.x);
        latB = (float)Math.Asin(navCylinder.position.y/1737000);
        longB = (float)Math.Atan2(navCylinder.position.z, navCylinder.position.x);
    }

    // Update is called once per frame
    void Update()
    {

        latA = (float)Math.Asin(earth.position.y/1737000);
        longA = (float)Math.Atan2(earth.position.z, earth.position.x);
        latB = (float)Math.Asin(navCylinder.position.y/1737000);
        longB = (float)Math.Atan2(navCylinder.position.z, navCylinder.position.x);

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            navCylinder.position = new Vector3(2071, 52, 259);
            tr.Clear();
            tr2.Clear();
        }

    }
//2546
    double getAzim()
    {
        double az = (float)Math.Atan2((Math.Sin(longB - longA) * Math.Cos(latB)), ((Math.Cos(latA) * Math.Sin(latB)) - (Math.Sin(latA) * Math.Cos(latB) * Math.Cos(longB - longA))));
        az = (az * 180.0)/Math.PI;
        return az;
    }

    double getAzimHorizion()
    {
        double az = (float)Math.Atan2((Math.Sin(longB - longA) * Math.Cos(latB)), ((Math.Cos(latA) * Math.Sin(latB)) - (Math.Sin(latA) * Math.Cos(latB) * Math.Cos(longB - longA))));
        az = (az * 180.0)/Math.PI;
        return az;
    }
    
    double getElevation()
    {
        double xAB = navCylinder.position.x - earth.position.x;
        double yAB = navCylinder.position.y - earth.position.y;
        double zAB = navCylinder.position.z - earth.position.z;
        double rangeAB = Math.Sqrt((xAB * xAB) + (yAB * yAB) + (zAB * zAB));
        double rz = xAB * Math.Cos(latA) * Math.Cos(longA) + zAB * Math.Cos(latA) * Math.Sin(longA) + yAB * Math.Sin(latA);
        double elevationAB = Math.Asin(rz/rangeAB);
        return elevationAB;
    }
}
