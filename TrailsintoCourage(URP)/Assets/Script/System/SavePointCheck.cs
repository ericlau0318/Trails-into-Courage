using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointCheck : MonoBehaviour
{
    private bool istouched;

    private void OnTriggerEnter(Collider other)
    {
        if (istouched == false)
        {
            if (other.CompareTag("Player"))
            {
                LavaController.lastSavePoint = gameObject.transform;
                istouched = true;
            }
        }
    }
}
