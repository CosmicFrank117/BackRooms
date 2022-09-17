using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGuide : MonoBehaviour
{
    public float deathDelay = 0.2f;

    private Vector3 playerPosition;
    private RoomTemplates templates;
    private NavMeshAgent navMeshAgent;

    private Vector3 startingPosition;
    private Vector3 bossLocation;
    
    private void Start()
    {
        
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (templates.spawnedBoss)
        {
            bossLocation = GameObject.FindGameObjectWithTag("Boss").transform.position;
           // print(bossLocation);

            navMeshAgent.SetDestination(bossLocation);
        }
        //print(startingPosition);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boss")
        {
            Instantiate(this, playerPosition, Quaternion.identity).name = this.name;
            Invoke("Destroy", deathDelay);

            //navMeshAgent.SetDestination(startingPosition);
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
