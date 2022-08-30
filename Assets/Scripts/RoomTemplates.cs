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
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [HideInInspector]
    public List<GameObject> rooms;

    //obstacles info

    public GameObject[] obstacles;
    public float roomBounds = 5f;
    public int maxObstacles = 3;

    private void Start()
    {
        instRooms = GameObject.FindGameObjectWithTag("RoomHolder");
        instObs = GameObject.FindGameObjectWithTag("ObstacleHolder");
    }
}
