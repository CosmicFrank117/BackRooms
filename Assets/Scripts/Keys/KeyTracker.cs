using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyTracker : MonoBehaviour
{
    public GameObject keyItemPrefab;
    public GameObject keyHolder;
    public GameObject playerGuide;
    public TextMeshProUGUI keysToCollectText;
    private RoomTemplates templates;
    
    public int keysToCollect;
    public int collectedKeys = 0;
    public bool isGuideSpawned = false;

    private int roomNumber;
    private bool spawnedKeys = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        keysToCollectText.enabled = false;
    }

    private void Update()
    {
        if (templates.spawnedBoss)
        {
            keysToCollect = templates.rooms.Count / 5;
            keysToCollectText.enabled = true;
            keysToCollectText.text = collectedKeys + " of " + keysToCollect + " keys collected";


            if (!spawnedKeys)
            {
                for (int i = 1; i <= keysToCollect; i++)
                {
                    roomNumber = (templates.rooms.Count - 2) / keysToCollect * i;
                    if (roomNumber == 0)
                    {
                        roomNumber = 1;
                    }
                    Instantiate(keyItemPrefab, templates.rooms[roomNumber].transform.position, Quaternion.identity, keyHolder.transform);
                }
                spawnedKeys = true;
                
            }

            if (collectedKeys == keysToCollect)
            {
                if (!isGuideSpawned)
                {
                    SpawnPlayerGuide();
                    isGuideSpawned = true;
                }
            }

        }
       
        
    }

    private void SpawnPlayerGuide()
    {
        Instantiate(playerGuide, transform.position, Quaternion.identity);
    }
}
