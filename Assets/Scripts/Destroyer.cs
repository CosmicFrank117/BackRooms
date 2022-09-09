using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private float waitTime = 10f;

    private void Start()
    {
		Destroy(gameObject, waitTime);
	}

    void OnTriggerEnter(Collider other)
	{
		if (/*other.name != "Floor" && */ other.name != "Player" && other.name != "PlayerGuide")
		{
			Destroy(other.gameObject);
		}
	}
}
