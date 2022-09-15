using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{

    private KeyTracker keyTracker;

    private void Start()
    {
        keyTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyTracker>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            keyTracker.collectedKeys += 1;
            print("Key Collected! You have: " + keyTracker.collectedKeys + " of " + keyTracker.keysToCollect + " keys");
            Destroy(this.transform.parent.gameObject);
        }
    }

}
