using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    public Transform body;
    public SpiderLegValues values;
    public LegMover otherLeg;

    Ray ray;

    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;

    private float stepHeight;
    private float speed;    
    private float stepDistance;
    private float footSpacingX, footSpacingZ;
    private float lerp = 1;
    private int groundLayer;

    void Start()
    {
        stepHeight = values.stepHeight * transform.lossyScale.x;
        stepDistance = 2 * transform.lossyScale.x;
        footSpacingX = transform.localPosition.x * transform.lossyScale.x;
        footSpacingZ = transform.localPosition.y * transform.lossyScale.y; //spider is rotated
        groundLayer = LayerMask.GetMask("Ground");
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
    }

    void Update()
    {
        speed = 1 / transform.lossyScale.x * values.legSpeed;
        transform.position = currentPosition;
        transform.up = currentNormal;

        ray = new Ray(body.position + (body.right * footSpacingX) + (body.up * footSpacingZ), Vector3.down);
        

        if (Physics.Raycast(ray, out RaycastHit hit, 10, groundLayer))
        {
            if (Vector3.Distance(newPosition, hit.point) > stepDistance && !otherLeg.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;
                newPosition = hit.point /*+ (body.up * stepLength * direction) + footOffset*/;
                newNormal = hit.normal;
            }
        }
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);

            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }
}
