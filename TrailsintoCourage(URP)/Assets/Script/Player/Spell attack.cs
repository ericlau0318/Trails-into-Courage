using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellattack : MonoBehaviour
{
    public float spellspeed=10f;
    public float rotationSpeed = 180f;
    public float destroyDelay = 2f;
    public GameObject Explosion;
    private void Start()
    {
        Destroy(transform.parent.gameObject, destroyDelay);
    }
    private void Update()
    {
        transform.position +=  Time.deltaTime * transform.forward;
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
            Explosion.SetActive(true);
            Destroy(gameObject , 0.5f);
        }
    }
}
