using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkDestroyer : MonoBehaviour
{
	private float waitTime = 10f;

	private void Start()
	{
		Destroy(gameObject, waitTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "NavMeshLink")
		{
			Destroy(other.gameObject);
		}
	}
}
