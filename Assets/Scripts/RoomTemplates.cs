using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [HideInInspector]
    public GameObject instObs;
    [HideInInspector]
    public GameObject instRooms;

    public GameObject closedRoom;
    public GameObject TL, TR, BL, BR, TB, LR;
    public GameObject BRT, LBR, RTL, TLB;
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //[HideInInspector]
    public List<GameObject> rooms;

    //obstacles info
    public GameObject[] obstacles;
    public float roomBounds = 5f;
    public int maxObstacles = 3;

    //boss info
    public float waitTime;
    [HideInInspector]
    public bool spawnedBoss;
    public GameObject boss;

    private void Start()
    {
        instRooms = GameObject.FindGameObjectWithTag("RoomHolder");
        instObs = GameObject.FindGameObjectWithTag("ObstacleHolder");
    }

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {

            for (int j = 0; j < rooms.Count; j++)
            {
                if (j == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[j].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }      
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
