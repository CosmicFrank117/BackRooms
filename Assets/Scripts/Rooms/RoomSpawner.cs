using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 need bottom door
    //2 need top door
    //3 need left door
    //4 need right door

    private RoomTemplates templates;
    private Collider[] colliders;
    [HideInInspector]
    public List<int> openingDirections; 
    
    
    private int numOfColliders;
    private int rand;
    private float waitTime = 1f;
    
    public bool spawned = false;
    private bool isOtherRoomSpawned = false;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }    

    private void Spawn()
    {
        if (spawned == false)
        {
            colliders = Physics.OverlapSphere(transform.position, 0.1f);

            foreach (Collider collider in colliders)
            {
                print(collider);
                if (collider.GetComponent<RoomSpawner>() == true)
                {
                    numOfColliders = colliders.Length;
                    if (collider.GetComponent<RoomSpawner>().spawned)
                    {
                        isOtherRoomSpawned = true;
                    }
                }
                else
                {
                    isOtherRoomSpawned = false;
                }
            }

            if (isOtherRoomSpawned == false)
            {

                switch (numOfColliders)
                {
                    case 1:
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

                            break;
                        }

                    case 2:
                        {
                            List<int> TLvalues = new List<int> { 1, 3 };
                            List<int> TBvalues = new List<int> { 1, 2 };
                            List<int> RTvalues = new List<int> { 1, 4 };
                            List<int> LRvalues = new List<int> { 3, 4 };
                            List<int> LBvalues = new List<int> { 2, 3 };
                            List<int> BRvalues = new List<int> { 2, 4 };

                            foreach (Collider collider in colliders)
                            {
                                if (collider.GetComponent<RoomSpawner>() == true)
                                {
                                    openingDirections.Add(collider.GetComponent<RoomSpawner>().openingDirection);
                                }
                            }

                            if (openingDirections.OrderBy(x => x).SequenceEqual(TLvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.TL, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(TBvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.TB, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(RTvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.RTL, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(LRvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.LR, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(LBvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.BL, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(BRvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.BR, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }

                            break;
                        }

                    case 3:
                        {
                            List<int> TLBvalues = new List<int> { 1, 2, 3 };
                            List<int> LBRvalues = new List<int> { 1, 3, 4 };
                            List<int> RTLvalues = new List<int> { 2, 3, 4 };
                            List<int> BRTvalues = new List<int> { 1, 2, 4 };

                            foreach (Collider collider in colliders)
                            {
                                if (collider.GetComponent<RoomSpawner>() == true)
                                {
                                    openingDirections.Add(collider.GetComponent<RoomSpawner>().openingDirection);
                                }
                            }

                            if (openingDirections.OrderBy(x => x).SequenceEqual(TLBvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.TLB, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(RTLvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.RTL, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(LBRvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.LBR, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }
                            else if (openingDirections.OrderBy(x => x).SequenceEqual(BRTvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.BRT, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }

                            break;
                        }

                    case 4:
                        {
                            List<int> TLBRvalues = new List<int> { 1, 2, 3, 4 };

                            foreach (Collider collider in colliders)
                            {
                                if (collider.GetComponent<RoomSpawner>() == true)
                                {
                                    openingDirections.Add(collider.GetComponent<RoomSpawner>().openingDirection);
                                }
                            }
                            if (openingDirections.OrderBy(x => x).SequenceEqual(TLBRvalues.OrderBy(x => x)))
                            {
                                Instantiate(templates.TLBR, transform.position, Quaternion.identity, templates.instRooms.transform);
                            }

                            break;
                        }

                }
                isOtherRoomSpawned = true;
            }
            spawned = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
