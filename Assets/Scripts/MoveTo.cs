using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal;
    public Transform navCylinder;
    // Start is called before the first frame update
    void Start()
    {
        navCylinder = this.GetComponentInParent<Transform>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            navCylinder.position = new Vector3(2071, 96, 259);
        }
    }
}
