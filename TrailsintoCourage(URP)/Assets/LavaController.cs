using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public  Transform lastSavePoint;
    public PlayerState playerState;
    public float lavadamage;
    void Start()
    {
        lastSavePoint.position = new Vector3(87f,16f,174f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag==("Player"))
        {
            Debug.Log("Player Lava");
            playerState.TakeDamage(lavadamage);
            other.gameObject.transform.position = lastSavePoint.position;
        }
    }
}
