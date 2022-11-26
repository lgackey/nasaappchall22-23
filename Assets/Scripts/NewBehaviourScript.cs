using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    // copied code should re-engineer
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform movePositionTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
