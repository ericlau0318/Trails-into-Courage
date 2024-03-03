using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellattack : MonoBehaviour
{
    public float spellspeed=10f;
    public float destroyDelay = 2f;

    private void Start()
    {
       Destroy(transform.parent.gameObject, destroyDelay);
    }
    private void Update()
    {
        transform.position += transform.forward * spellspeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
