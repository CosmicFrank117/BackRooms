using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 need bottom door
    //2 need top door
    //3 need left door
    //4 need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    private float waitTime = 2f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (!spawned)
        {
            switch (openingDirection)
            {
                case 1:
                    {
                        //need to spawn room with BOTTOM door
                        rand = Random.Range(0, templates.bottomRooms.Length);
                        Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation, templates.instRooms.transform);
                        break;
                    }
                case 2:
                    {
                        //need to spawn room with TOP door
                        rand = Random.Range(0, templates.topRooms.Length);
                        Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation, templates.instRooms.transform);
                        break;
                    }
                case 3:
                    {
                        //need to spawn room with LEFT door
                        rand = Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation, templates.instRooms.transform);
                        break;
                    }
                case 4:
                    {
                        //need to spawn room with RIGHT door
                        rand = Random.Range(0, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, templates.instRooms.transform);
                        break;
                    }
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity, templates.instRooms.transform);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
