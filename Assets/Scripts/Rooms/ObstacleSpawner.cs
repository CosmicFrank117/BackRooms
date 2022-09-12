using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private RoomTemplates templates;

    private Vector3 positionInRoom;
    private float roomBounds;

    private int maxObstacles;
    private int numberOfObstacles;
    private int randObstacle;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomBounds = templates.roomBounds;
        maxObstacles = templates.maxObstacles;
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        numberOfObstacles = Random.Range(0, maxObstacles);

        for (int i = 0; i < numberOfObstacles; i++)
        {
            randObstacle = Random.Range(0, templates.obstacles.Length);
            positionInRoom = transform.position + new Vector3(Random.Range(-roomBounds, roomBounds), 0, Random.Range(-roomBounds, roomBounds));
            Instantiate(templates.obstacles[randObstacle], positionInRoom, templates.obstacles[randObstacle].transform.rotation, templates.instObs.transform);
        }
    }
}
