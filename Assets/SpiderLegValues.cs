using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderLegValues : MonoBehaviour
{
    NavMeshAgent agent;

    public float stepHeight = 2f;
    public float legSpeed = 10f;
    public float turnSpeed = 10f;
    float lerp;

    Vector3 curDir; // compass indicating direction
    float vertSpeed = 0f; // vertical speed (see note)
    Vector3 curNormal = Vector3.up; // smoothed terrain normal

    Ray ray;

    int groundLayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    /*private void Update()
    {

        ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, groundLayer))
        {
            if (lerp >= 1)
            {
                lerp = 0;
                curNormal = Vector3.Lerp(curNormal, hit.normal, 4 * Time.deltaTime);

                Quaternion grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);
                transform.rotation = grndTilt;

            }

            if(lerp < 1)
            {
                curDir = Vector3.Lerp(curDir, Vector3.Normalize(transform.position + agent.destination), lerp);
                lerp += Time.deltaTime * turnSpeed;
                    
                Quaternion dirTilt = Quaternion.FromToRotation(Vector3.forward, curDir);
                transform.rotation = dirTilt;

                
            }
            
            

             
            
            
        }
    }*/
}
