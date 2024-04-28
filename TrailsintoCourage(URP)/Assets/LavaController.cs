using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public  Transform lastSavePoint= null;
    public PlayerState playerState;
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag==("Player"))
        {
            Debug.Log("Player Lava");
            playerState.TakeDamage(50);
            other.gameObject.transform.position = lastSavePoint.position;
        }
    }
}
