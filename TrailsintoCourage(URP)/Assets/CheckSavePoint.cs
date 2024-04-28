using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSavePoint : MonoBehaviour
{
    private bool istouched=false;
    public Transform savePoint;
    private LavaController lavaController;

    private void Start()
    {
        lavaController = GameObject.Find("Lava").GetComponent<LavaController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (istouched == false)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Checked 1");

                lavaController.lastSavePoint = savePoint;
                istouched = true;
                Debug.Log(savePoint);
            }
        }
    }
}
