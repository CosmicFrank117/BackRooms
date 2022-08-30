using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public float waitTime = 2f;

    private void Start()
    {
		Destroy(gameObject, waitTime);
	}

    void OnTriggerEnter(Collider other)
	{
		if (other.name != "Plane")
		{
			Destroy(other.gameObject);
		}
	}
}
