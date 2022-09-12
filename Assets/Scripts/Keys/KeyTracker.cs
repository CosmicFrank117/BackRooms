using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTracker : MonoBehaviour
{
    public GameObject keyItemPrefab;
    private RoomTemplates templates;
    
    public int keysToCollect;
    public int collectedKeys = 0;

    private int roomNumber;
    private bool spawnedKeys = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Update()
    {
        if (templates.spawnedBoss)
        {
            keysToCollect = templates.rooms.Count / 5;
            print("Keys to collect: " + keysToCollect);


            if (!spawnedKeys)
            {
                for (int i = 1; i <= keysToCollect; i++)
                {
                    roomNumber = (templates.rooms.Count - 2) / keysToCollect * i;
                    if (roomNumber == 0)
                    {
                        roomNumber = 1;
                    }
                    Instantiate(keyItemPrefab, templates.rooms[roomNumber].transform.position, Quaternion.identity, this.transform);
                }
                spawnedKeys = true;
            }
        }
    }
}
