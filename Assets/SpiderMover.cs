using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderMover : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float timeToWalk = 3f;
    float initialTimeToWalk;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(new Vector3(Random.Range(-50f, 500f), 0, Random.Range(-50, 50)));
        agent.SetDestination(player.position);
        initialTimeToWalk = timeToWalk;
    }

    private void Update()
    {
        /*timeToWalk -= Time.deltaTime;
        if (timeToWalk <= 0)
        {*/
            /*agent.isStopped = true;
            agent.ResetPath();*/
            //agent.SetDestination(new Vector3(Random.Range(-50f, -50f), 0, Random.Range(-50f, 50f)));
            agent.SetDestination(player.position);
            //timeToWalk = initialTimeToWalk;
        //}
    }

}
